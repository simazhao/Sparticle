using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressPoolLoader
    {
        public void Load()
        {
            var poolfile = ConfigurationManager.AppSettings["poolStore"];

            try
            {
                using (var reader = new StreamReader(poolfile))
                {
                    var fileContent = reader.ReadToEnd();

                    var poolModel = JsonConvert.DeserializeObject<ServiceAddressPoolModel>(fileContent);

                    ServiceAddressPool.Instance.FromModel(poolModel);
                }
            }
            catch (Exception ex)
            {
                // todo: add some error handle
            }
        }
    }
}