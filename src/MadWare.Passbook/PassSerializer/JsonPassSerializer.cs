using System;
using System.Collections.Generic;
using System.Text;
using MadWare.Passbook.PassStyle;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MadWare.Passbook.PassSerializer
{
    public class JsonPassSerializer : IPassSerializer
    {
        public byte[] Serialize<T>(Pass<T> p) where T : BasePassStyle
        {
            string passJson = JsonConvert.SerializeObject(p,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
               );

            return Encoding.UTF8.GetBytes(passJson);
        }
    }
}
