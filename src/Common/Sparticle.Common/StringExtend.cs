using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Sparticle.Common
{
    public static class StringExtend
    {
        public static string DetailMessage(this Exception ex)
        {
            return StringHelper.GetExpMessage(ex);
        }

        public static string Md5(this string str)
        {
            return StringHelper.MakeMd5(str);
        }

        public static string GetUrlParamertes(this string names, params object[] args)
        {
            return StringHelper.GetUrlParamertes(names, args);
        }
    }

    public static class StringHelper
    { 
        public static string GetExpMessage(Exception ex)
        {
            var error = StringBuilderFactory.Instance.GetStringBuilder();

            error.AppendLine("");
            error.AppendLine("#####Exception classes#####   ");
            error.AppendLine(GetExceptionTypeStack(ex));
            error.AppendLine("#####Exception messages#####");
            error.AppendLine(GetExceptionMessageStack(ex));
            error.AppendLine("#####Exception Stack Traces#####");
            error.AppendLine(GetExceptionCallStack(ex));

            return StringBuilderFactory.Instance.GetStringAndReleaseBuilder(error);
        }

        public static string MakeMd5(string str)
        {
            using (var md5 = MD5.Create())
            {
                return GetMd5Hash(md5, str);
            }
        }

        public static string GetUrlParamertes(string names, params object[] args)
        {
            var nameArray = names.Split(',');
            System.Diagnostics.Debug.Assert(nameArray.Count() == args.Count());

            var dict = new Dictionary<string, object>();

            var i = 0;
            var sb = new StringBuilder();
            foreach (var arg in args)
            {
                sb.AppendFormat("{0}={1}", nameArray[i].Trim(), arg);

                if (i < args.Length - 1)
                {
                    sb.Append('&');
                }

                ++i;
            }

            return sb.ToString();
        }

        private static string GetExceptionTypeStack(Exception e)
        {
            if (e.InnerException != null)
            {
                var message = StringBuilderFactory.Instance.GetStringBuilder();
                message.AppendLine(GetExceptionTypeStack(e.InnerException));
                message.AppendLine("   " + e.GetType().ToString());
                return StringBuilderFactory.Instance.GetStringAndReleaseBuilder(message);
            }

            return "   " + e.GetType().ToString();
        }

        private static string GetExceptionMessageStack(Exception e)
        {
            if (e.InnerException != null)
            {
                var message = StringBuilderFactory.Instance.GetStringBuilder();
                message.AppendLine(GetExceptionMessageStack(e.InnerException));
                message.AppendLine("   " + e.Message);
                return StringBuilderFactory.Instance.GetStringAndReleaseBuilder(message);
            }

            return "   " + e.Message;
        }

        private static string GetExceptionCallStack(Exception e)
        {
            if (e.InnerException != null)
            {
                var message = StringBuilderFactory.Instance.GetStringBuilder();
                message.AppendLine(GetExceptionCallStack(e.InnerException));
                message.AppendLine("--- Next Call Stack:");
                message.AppendLine(e.StackTrace);
                return StringBuilderFactory.Instance.GetStringAndReleaseBuilder(message);
            }

            return e.StackTrace;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = StringBuilderFactory.Instance.GetStringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return StringBuilderFactory.Instance.GetStringAndReleaseBuilder(sBuilder);
        }
    }
}
