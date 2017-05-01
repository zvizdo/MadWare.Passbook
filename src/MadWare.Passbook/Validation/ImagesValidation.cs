using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadWare.Passbook.Validation
{
    public class ImagesValidation : ValidationAttribute
    {
        public ImagesValidation()
        {
        }

        public ImagesValidation(string errorMessage) : base(errorMessage)
        {
        }

        public ImagesValidation(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Dictionary {0} must contains {1}, {2}", name, Enums.PassbookImageType.IconRetina, Enums.PassbookImageType.Icon);

        }

        public override bool IsValid(object value)
        {
            if (!(value is Dictionary<Enums.PassbookImageType, byte[]>))
                throw new NotSupportedException("ImagesValidation is only supported on dictionary type.");

            Dictionary<Enums.PassbookImageType, byte[]> content = value as Dictionary<Enums.PassbookImageType, byte[]>;

            byte[] bytes = null;
            var isIcon = content.TryGetValue(Enums.PassbookImageType.Icon, out bytes);
            var isIconRetina = content.TryGetValue(Enums.PassbookImageType.IconRetina, out bytes);

            return isIcon && isIconRetina;
        }
    }
}
