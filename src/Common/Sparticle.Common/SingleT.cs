using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public class SingleT<T>
        where T : class, new()
    {
        private class Nested
        {
            static Nested() { }
            internal static readonly T Instance = new T();
        }

        public static T Instance
        {
            get
            {
                return Nested.Instance;
            }
        }
    }
}
