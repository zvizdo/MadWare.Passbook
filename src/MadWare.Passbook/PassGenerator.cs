using MadWare.Passbook.PassSerializer;
using MadWare.Passbook.PassSigner;
using MadWare.Passbook.PassStyle;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using MadWare.Passbook.SpecialFields;
using System.Security.Cryptography;
using MadWare.Passbook.Enums;
using Newtonsoft.Json;
using System.IO.Compression;
using MadWare.Passbook.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MadWare.Passbook
{
    public class PassGenerator
    {
        protected PassGeneratorOptions passOptions;

        protected IPassSerializer passSerializer;

        protected IPassSigner passSigner;


        public PassGenerator(PassGeneratorOptions opt, IPassSerializer passSerializer, IPassSigner passSigner)
        {
            this.passOptions = opt;
            this.passSerializer = passSerializer;
            this.passSigner = passSigner;
        }

        public PassGenerator(PassGeneratorOptions opt) : this(opt, new JsonPassSerializer(), new NetFrameworkPassSigner())
        {
        }

        public void ValidatePass<T>(Pass<T> p) where T : BasePassStyle
        {

            var vr = new List<ValidationResult>();
            var vc = new ValidationContext(p);
            if (!Validator.TryValidateObject(p, vc, vr, true))
            {
                throw new Exception(string.Format("{0} not valid! \n{1}",
                                                   this.GetType().Name,
                                                   string.Join(",", vr.Select(r => r.ErrorMessage))));
            }
        }
        public byte[] Generate<T>(Pass<T> p) where T : BasePassStyle
        {
            if (p == null)
                throw new ArgumentNullException("p", "You must pass an instance of Pass<T>");

            this.passSigner.ValidateCertificate(this.passOptions.PassCert, p);
            this.ValidatePass(p);

            return CreatePackage(p);
        }

        private byte[] CreatePackage<T>(Pass<T> p) where T : BasePassStyle
        {
            byte[] passFile = passSerializer.Serialize(p);
            Dictionary<string, byte[]> localizationFile = GenerateLocalizationFiles(p);
            byte[] manifestFile = GenerateManifestFile(p, localizationFile, passFile);
            byte[] signatureFile = passSigner.Sign(manifestFile, this.passOptions.PassCert, this.passOptions.AppleCert);
            return ZipPackage(p, localizationFile, passFile, manifestFile, signatureFile);
        }

        private Dictionary<string, byte[]> GenerateLocalizationFiles<T>(Pass<T> p) where T : BasePassStyle
        {
            Dictionary<string, byte[]> localizationFiles = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);

            if (p.Localizations == null) return localizationFiles;

            foreach (KeyValuePair<string, Localization> locale in p.Localizations)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter sr = new StreamWriter(ms, Encoding.UTF8))
                    {
                        sr.WriteLine("\"{0}\" = \"{1}\";\n", locale.Key, locale.Value);
                        sr.Flush();
                        localizationFiles.Add(locale.Key, ms.ToArray());
                    }
                }
            }

            return localizationFiles;
        }

        private byte[] GenerateManifestFile<T>(Pass<T> p, Dictionary<string, byte[]> localizationFiles, byte[] passFile) where T : BasePassStyle
        {
            byte[] manifestFile;
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                    {
                        jsonWriter.Formatting = Formatting.Indented;
                        jsonWriter.WriteStartObject();

                        string hash = null;

                        hash = GetHashForBytes(passFile);
                        jsonWriter.WritePropertyName(@"pass.json");
                        jsonWriter.WriteValue(hash);

                        if (p.Images != null)
                        {
                            foreach (KeyValuePair<PassbookImageType, byte[]> image in p.Images)
                            {
                                hash = GetHashForBytes(image.Value);
                                jsonWriter.WritePropertyName(image.Key.ToFilename());
                                jsonWriter.WriteValue(hash);
                            }
                        }

                        foreach (KeyValuePair<string, byte[]> localization in localizationFiles)
                        {
                            hash = GetHashForBytes(localization.Value);
                            jsonWriter.WritePropertyName(string.Format("{0}.lproj/pass.strings", localization.Key.ToLower()));
                            jsonWriter.WriteValue(hash);
                        }
                    }

                    manifestFile = ms.ToArray();
                }

            }
            return manifestFile;
        }

        private string GetHashForBytes(byte[] bytes)
        {
            using (SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider())
            {
                return System.BitConverter.ToString(hasher.ComputeHash(bytes)).Replace("-", string.Empty).ToLower();
            }
        }

        private byte[] ZipPackage<T>(Pass<T> p, Dictionary<string, byte[]> localizationFiles, byte[] passFile, byte[] manifestFile, byte[] signatureFile) where T : BasePassStyle
        {
            using (MemoryStream zipToOpen = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update, true))
                {
                    if (p.Images != null)
                    {
                        foreach (KeyValuePair<PassbookImageType, byte[]> image in p.Images)
                        {
                            ZipArchiveEntry imageEntry = archive.CreateEntry(image.Key.ToFilename()); // File name

                            using (BinaryWriter writer = new BinaryWriter(imageEntry.Open()))
                            {
                                writer.Write(image.Value);
                                writer.Flush();
                            }
                        }
                    }

                    foreach (KeyValuePair<string, byte[]> localization in localizationFiles)
                    {
                        ZipArchiveEntry localizationEntry = archive.CreateEntry(string.Format("{0}.lproj/pass.strings", localization.Key.ToLower()));

                        using (BinaryWriter writer = new BinaryWriter(localizationEntry.Open()))
                        {
                            writer.Write(localization.Value);
                            writer.Flush();
                        }
                    }

                    ZipArchiveEntry PassJSONEntry = archive.CreateEntry(@"pass.json");
                    using (BinaryWriter writer = new BinaryWriter(PassJSONEntry.Open()))
                    {
                        writer.Write(passFile);
                        writer.Flush();
                    }

                    ZipArchiveEntry ManifestJSONEntry = archive.CreateEntry(@"manifest.json");
                    using (BinaryWriter writer = new BinaryWriter(ManifestJSONEntry.Open()))
                    {
                        writer.Write(manifestFile);
                        writer.Flush();
                    }

                    ZipArchiveEntry SignatureEntry = archive.CreateEntry(@"signature");
                    using (BinaryWriter writer = new BinaryWriter(SignatureEntry.Open()))
                    {
                        writer.Write(signatureFile);
                        writer.Flush();
                    }
                }
                byte[] pkPassFile = null;
                pkPassFile = zipToOpen.ToArray();
                zipToOpen.Flush();

                return pkPassFile;
            }
        }
    }
}




