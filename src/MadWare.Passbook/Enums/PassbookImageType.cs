using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.Enums
{
    public enum PassbookImageType
    {
        /// <summary>
        /// Background image, 180x220 points
        /// </summary>
        Background,
        /// <summary>
        /// Retina background image, 180x220 points
        /// </summary>
        BackgroundRetina,
        /// <summary>
        /// Icon as per https://developer.apple.com/library/ios/documentation/UserExperience/Conceptual/MobileHIG/Alerts.html#//apple_ref/doc/uid/TP40006556-CH14
        /// </summary>
        Icon,
        /// <summary>
        /// Retina icon as per https://developer.apple.com/library/ios/documentation/UserExperience/Conceptual/MobileHIG/Alerts.html#//apple_ref/doc/uid/TP40006556-CH14
        /// </summary>
        IconRetina,
        /// <summary>
        /// Logo, 160x50 points
        /// </summary>
        Logo,
        /// <summary>
        /// Retina logo, 160x50 points
        /// </summary>
        LogoRetina,
        /// <summary>
        /// Strip
        /// <list type="bullet">
        ///		<item>
        ///			<description>On iPhone 6 and 6 Plus The allotted space is 375x98 points for event tickets, 375x144 points for gift cards and coupons, and 375x123 in all other cases.</description>
        ///		</item>
        ///		<item>
        ///			<description>On prior hardware The allotted space is 320x84 points for event tickets, 320x110 points for other pass styles with a square barcode on devices with 3.5 inch screens, and 320x123 in all other cases.</description>
        ///		</item>
        ///		<item>
        ///			<description>On iOS 6 The strip image is only 312 points wide; the height varies as described above. A shine effect is automatically applied to the strip image; to suppress it use the suppressStripShine key.</description>
        ///		</item>
        /// </list>
        /// </summary>
        Strip,
        /// <summary>
        /// Retina strip
        /// <list type="bullet">
        ///		<item>
        ///			<description>On iPhone 6 and 6 Plus The allotted space is 375x98 points for event tickets, 375x144 points for gift cards and coupons, and 375x123 in all other cases.</description>
        ///		</item>
        ///		<item>
        ///			<description>On prior hardware The allotted space is 320x84 points for event tickets, 320x110 points for other pass styles with a square barcode on devices with 3.5 inch screens, and 320x123 in all other cases.</description>
        ///		</item>
        ///		<item>
        ///			<description>On iOS 6 The strip image is only 312 points wide; the height varies as described above. A shine effect is automatically applied to the strip image; to suppress it use the suppressStripShine key.</description>
        ///		</item>
        /// </list>
        /// </summary>
        StripRetina,
        /// <summary>
        /// Thumbnail, 90x90 points
        /// </summary>
        Thumbnail,
        /// <summary>
        /// Retina thumbnail, 90x90 points
        /// </summary>
        ThumbnailRetina,
        /// <summary>
        /// Footer, 286x15 points
        /// </summary>
        Footer,
        /// <summary>
        /// Retina footer, 286x15 points
        /// </summary>
        FooterRetina
    }
}
