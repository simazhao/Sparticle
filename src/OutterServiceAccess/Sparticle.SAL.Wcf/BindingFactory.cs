using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Sparticle.SAL.Wcf
{
    internal class BindingFactory
    {
        public static object Lock = new object();

        private static ConcurrentDictionary<string, Binding> _cache = new ConcurrentDictionary<string, Binding>();

        public static Binding GetBinding(string bindingName)
        {
            if (!_cache.ContainsKey(bindingName))
            {
                CreateAllBinding();
            }

            if (!_cache.ContainsKey(bindingName))
            {
                return null;
            }

            return _cache[bindingName];
        }

        private static void CreateAllBinding()
        {
            _cache.GetOrAdd("basicHttpBinding", s =>
            {
                return CreateBasicHttpBinding();
            });

            _cache.GetOrAdd("wsHttpBinding", s =>
            {
                return CreateWsHttpBinding();
            });

            _cache.GetOrAdd("netTcpBinding", s =>
            {
                return CreateNetTcpBinding();
            });
        }

        public static BasicHttpBinding BasicHttpBinding
        {
            get { return GetBinding("basicHttpBinding") as BasicHttpBinding; }
        }

        public static WSHttpBinding WSHttpBinding
        {
            get { return GetBinding("wsHttpBinding") as WSHttpBinding; }
        }

        public static NetTcpBinding NetTcpBinding
        {
            get { return GetBinding("netTcpBinding") as NetTcpBinding; }
        }

        public static BasicHttpBinding CreateBasicHttpBinding()
        {
            var binding = new BasicHttpBinding
            {
                MaxBufferPoolSize = Int32.MaxValue,
                MaxBufferSize = Int32.MaxValue,
                MaxReceivedMessageSize = Int32.MaxValue,
                OpenTimeout = new TimeSpan(0, 20, 0),
                ReceiveTimeout = new TimeSpan(0, 20, 0),
                SendTimeout = new TimeSpan(0, 20, 0),
                CloseTimeout = new TimeSpan(0, 20, 0),
            };

            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = 16384;

            return binding;
        }

        public static WSHttpBinding CreateWsHttpBinding()
        {
            var binding = new WSHttpBinding
            {
                MaxBufferPoolSize = Int32.MaxValue,
                MaxReceivedMessageSize = Int32.MaxValue,
                OpenTimeout = new TimeSpan(0, 20, 0),
                ReceiveTimeout = new TimeSpan(0, 20, 0),
                SendTimeout = new TimeSpan(0, 20, 0),
                CloseTimeout = new TimeSpan(0, 20, 0),
            };

            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = 16384;

            binding.ReliableSession.Ordered = true;
            binding.ReliableSession.InactivityTimeout = new TimeSpan(0, 10, 0);
            binding.ReliableSession.Enabled = false;

            binding.Security.Mode = SecurityMode.None;

            return binding;
        }

        public static NetTcpBinding CreateNetTcpBinding()
        {
            var binding = new NetTcpBinding(SecurityMode.None)
            {
                MaxBufferPoolSize = Int32.MaxValue,
                MaxBufferSize = Int32.MaxValue,
                MaxReceivedMessageSize = Int32.MaxValue,
                OpenTimeout = new TimeSpan(0, 30, 0),
                ReceiveTimeout = new TimeSpan(1, 0, 0),
                SendTimeout = new TimeSpan(1, 0, 0),
                CloseTimeout = new TimeSpan(0, 30, 0),
            };

            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = 0x4000;

            return binding;
        }
    }
}