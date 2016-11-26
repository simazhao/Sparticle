using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    public class SerializeTimer
    {
        private Timer _timer;
        private TimeSpan dueTime;
        private TimeSpan period;

        public SerializeTimer(TimeSpan dueTime, TimeSpan period)
        {
            _timer = new Timer(SerializeOnTime, ServiceAddressPool.Instance, dueTime, period);
        }

        private void SerializeOnTime(object state)
        {
            var pool = state as ServiceAddressPool;

            if (pool == null)
                return;

            _timer.Change(int.MaxValue, int.MaxValue);

            var poolModel = pool.ToModel();

            var poolStoreFile = ConfigurationManager.AppSettings["poolStore"];
            
            try
            {
                using (var writer = new StreamWriter(poolStoreFile, false))
                {
                    var fileContent = JsonConvert.SerializeObject(poolModel);

                    writer.Write(fileContent);
                }
            }
            catch(Exception ex)
            {
                // todo: add some error handle
            }

            _timer.Change(period, period);
        }
    }
}