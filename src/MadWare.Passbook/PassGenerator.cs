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

namespace MadWare.Passbook
{
    public class PassGenerator
    {
        protected PassGeneratorOptions passOptions;

        protected IPassSerializer passSerializer;

        protected IPassSigner passSigner;

        private Dictionary<string, byte[]> localizationFiles = null;
        private byte[] manifestFile = null;
        private byte[] passFile = null;
        private byte[] pkPassFile = null;
        private byte[] signatureFile = null;

        public PassGenerator(PassGeneratorOptions opt, IPassSerializer passSerializer, IPassSigner passSigner)
        {
            this.passOptions = opt;
            this.passSerializer = passSerializer;
            this.passSigner = passSigner;
        }

        public PassGenerator(PassGeneratorOptions opt) : this(opt, new JsonPassSerializer(), new NetFrameworkPassSigner())
        {
        }

        public byte[] Generate<T>(Pass<T> p) where T : BasePassStyle
        {
            if (p == null)
                throw new ArgumentNullException("p", "You must pass an instance of Pass<T>");

            this.passSigner.ValidateCertificate(this.passOptions.PassCert, p);

            CreatePackage(p);
            ZipPackage(p);

            return pkPassFile;
        }

        private void CreatePackage<T>(Pass<T> p) where T : BasePassStyle
        {
            passFile = passSerializer.Serialize(p);
            GenerateLocalizationFiles(p);
            GenerateManifestFile(p);
        }

        private void GenerateLocalizationFiles<T>(Pass<T> p) where T : BasePassStyle
        {
            localizationFiles = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);

            if (p.Localizations == null) return;

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
        }

        private void GenerateManifestFile<T>(Pass<T> p) where T : BasePassStyle
        {

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
                                jsonWriter.WritePropertyName(image.Key.ToString());
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
                this.signatureFile = passSigner.Sign(manifestFile, this.passOptions.PassCert, this.passOptions.AppleCert);

            }
        }

        private string GetHashForBytes(byte[] bytes)
        {
            using (SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider())
            {
                return System.BitConverter.ToString(hasher.ComputeHash(bytes)).Replace("-", string.Empty).ToLower();
            }
        }

        private void ZipPackage<T>(Pass<T> p) where T : BasePassStyle
        {
            {
                using (MemoryStream zipToOpen = new MemoryStream())
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update, true))
                    {
                        if (p.Images != null)
                        {
                            foreach (KeyValuePair<PassbookImageType, byte[]> image in p.Images)
                            {
                                ZipArchiveEntry imageEntry = archive.CreateEntry(image.Key.ToString()); // File name

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

                    pkPassFile = zipToOpen.ToArray();
                    zipToOpen.Flush();
                }
            }
        }
    }
}




