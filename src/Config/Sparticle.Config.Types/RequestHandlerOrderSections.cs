using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Sparticle.Config.Types
{
    /// <summary>
    /// RequestHandlerOrder Config Section
    /// </summary>
    public class RequestHandlerOrderSection : ConfigurationSection
    {
        [ConfigurationProperty("Handlers", IsRequired = true)]
        public RequestConfigHanlders RequestConfigHanlders
        {
            get { return (RequestConfigHanlders)base["Handlers"]; }
            set { base["Handlers"] = value; }
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }

    /// <summary>
    /// RequestHandlerOrder Config Section
    /// </summary>
    public class RequestConfigHanlders : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RequestHandlerOrder();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RequestHandlerOrder)element).Handler;

        }

        public override bool IsReadOnly()
        {
            return false;
        }

        public IEnumerable<string> AllKeys { get { return BaseGetAllKeys().Cast<string>(); } }

        public new RequestHandlerOrder this[string name]
        {
            get
            {
                if (this.BaseGetAllKeys().Contains(name))
                {
                    return (RequestHandlerOrder)BaseGet(name);
                }
                return new RequestHandlerOrder();

            }
        }

        public void Add(ConfigurationElement element)
        {
            this.BaseAdd(element);
        }

        public void Add(IEnumerable<RequestHandlerOrder> elements)
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
    /// RequestHandler Order 
    /// </summary>
    public class RequestHandlerOrder : ConfigurationElement
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
