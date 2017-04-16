using MadWare.Passbook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.Extensions
{
    internal static class PassbookImageExtensions
    {
        public static string ToFilename(this PassbookImageType passbookImage)
        {
            switch (passbookImage)
            {
                case PassbookImageType.Icon:
                    return "icon.png";
                case PassbookImageType.IconRetina:
                    return "icon@2x.png";
                case PassbookImageType.Logo:
                    return "logo.png";
                case PassbookImageType.LogoRetina:
                    return "logo@2x.png";
                case PassbookImageType.Background:
                    return "background.png";
                case PassbookImageType.BackgroundRetina:
                    return "background@2x.png";
                case PassbookImageType.Strip:
                    return "strip.png";
                case PassbookImageType.StripRetina:
                    return "strip@2x.png";
                case PassbookImageType.Thumbnail:
                    return "thumbnail.png";
                case PassbookImageType.ThumbnailRetina:
                    return "thumbnail@2x.png";
                case PassbookImageType.Footer:
                    return "footer.png";
                case PassbookImageType.FooterRetina:
                    return "footer@2x.png";
                default:
                    throw new NotImplementedException("Unknown PassbookImageType type.");
            }
        }
    }
}
