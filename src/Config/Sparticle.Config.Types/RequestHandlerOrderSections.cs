using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FundTrade.API.Service.Common
{
    public class RequestHandlerOrderSections : ConfigurationSection
    {
        [ConfigurationProperty("Sections", IsRequired = true)]
        public RequestConfigSourceCollection RequestHandlerOrderSection
        {
            get { return (RequestConfigSourceCollection)base["Sections"]; }
            set { base["Sections"] = value; }
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }

    public class RequestConfigSourceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RequestHandlerOrderSection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RequestHandlerOrderSection)element).Handler;

        }

        public override bool IsReadOnly()
        {
            return false;
        }

        public IEnumerable<string> AllKeys { get { return BaseGetAllKeys().Cast<string>(); } }

        public new RequestHandlerOrderSection this[string name]
        {
            get
            {
                if (this.BaseGetAllKeys().Contains(name))
                {
                    return (RequestHandlerOrderSection)BaseGet(name);
                }
                return new RequestHandlerOrderSection();

            }
        }

        public void Add(ConfigurationElement element)
        {
            this.BaseAdd(element);
        }

        public void Add(IEnumerable<RequestHandlerOrderSection> elements)
        {
            if (elements == null || !elements.Any())
            {
                return;
            }

            foreach (var element in elements)
            {
                this.Add(element);
            }
        }
    }


    /// <summary>
    /// Section 信息
    /// </summary>
    public class RequestHandlerOrderSection : ConfigurationElement
    {
        [ConfigurationProperty("Handler", IsRequired = true)]
        public string Handler
        {
            get { return (string)base["Handler"]; }
            set { base["Handler"] = value; }
        }
        [ConfigurationProperty("Order", IsRequired = true)]
        public int Order
        {
            get { return (int)base["Order"]; }
            set { base["Order"] = value; }
        }
    }

}
