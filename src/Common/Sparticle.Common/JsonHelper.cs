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
        public static string ToJson(object t)
        {
            try
            {
                var ser = new JavaScriptSerializer();
                ser.MaxJsonLength = Int32.MaxValue;
                return ser.Serialize(t);
            }
            catch (Exception ex)
            {

            }

            return "";
        }

        public static TData FromJson<TData>(string json)
        {
            try
            {
                var ser = new JavaScriptSerializer();
                ser.MaxJsonLength = Int32.MaxValue;
                return ser.Deserialize<TData>(json);
            }
            catch (Exception ex)
            {

            }

            return Activator.CreateInstance<TData>();
        }
    }
}
