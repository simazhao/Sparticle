using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sparticle.Common
{
    public static class JsonHelper
    {
        public static string ToJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {

            }

            return "";
        }

        public static TData FromJson<TData>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<TData>(json);
            }
            catch (Exception)
            {

            }

            return Activator.CreateInstance<TData>();
        }
    }
}
