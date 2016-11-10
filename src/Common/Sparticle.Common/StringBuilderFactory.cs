using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public class StringBuilderFactory
    {
        private StringBuilderFactory()
        {

        }

        public static readonly StringBuilderFactory Instance = new StringBuilderFactory();

        /// <summary>
        /// 线程隔离
        /// </summary>
        [ThreadStatic]
        private static StringBuilder _cachedStringBuilder;

        /// <summary>
        /// 允许多次调用
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStringBuilder()
        {
            var result = _cachedStringBuilder;
            if (result == null)
            {
                return new StringBuilder();
            }

            _cachedStringBuilder.Clear();

            _cachedStringBuilder = null;

            return result;
        }

        /// <summary>
        /// 只回收最后的一个
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        public String GetStringAndReleaseBuilder(StringBuilder sb)
        {
            _cachedStringBuilder = sb;

            return sb.ToString();
        }
    }
}
