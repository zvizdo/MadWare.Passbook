using System;
using System.Collections.Generic;
using System.Text;
using MadWare.Passbook.PassStyle;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace MadWare.Passbook.PassSerializer
{
    public class JsonPassSerializer : CamelCasePropertyNamesContractResolver, IPassSerializer
    {
        public byte[] Serialize<T>(Pass<T> p) where T : BasePassStyle
        {
            string passJson = JsonConvert.SerializeObject(p,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = this,
                    Formatting = Formatting.Indented
                }
               );

            return Encoding.UTF8.GetBytes(passJson);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyName.Equals("passStyle", StringComparison.OrdinalIgnoreCase))
            {
                property.PropertyName = this.PassStyleClassNameToJsonPropName(property.PropertyType.Name);
            }

            return property;
        }

        private string PassStyleClassNameToJsonPropName(string passStyleClassName)
        {
            switch(passStyleClassName)
            {
                case "GenericPassStyle":
                    return "generic";

                case "BoardingPassStyle":
                    return "boardingPass";

                case "CouponPassStyle":
                    return "coupon";

                case "EventTicketPassStyle":
                    return "eventTicket";

                case "StoreCardPassStyle":
                    return "storeCard";

                default:
                    throw new ArgumentException("Not any known passStyleClassName!");
            }
        }
    }
}
