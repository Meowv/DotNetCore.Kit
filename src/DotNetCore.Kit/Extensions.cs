using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.Kit
{
    public static class Extensions
    {
        /// <summary>
        /// AppendIf<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (T val in values)
            {
                if (predicate(val))
                {
                    @this.Append(val);
                }
            }
            return @this;
        }

        /// <summary>
        /// AppendLineFormat
        /// </summary>
        /// <param name="this"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
        {
            @this.AppendLine(string.Format(format, args));
            return @this;
        }

        /// <summary>
        /// AppendLineFormat
        /// </summary>
        /// <param name="this"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, params object[] args)
        {
            @this.AppendLine(string.Format(format, args));
            return @this;
        }

        /// <summary>
        /// AppendLineIf<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                T arg = values[i];
                if (predicate(arg))
                {
                    @this.AppendLine(arg.ToString());
                }
            }
            return @this;
        }

        /// <summary>
        /// As<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As<T>(this object obj) where T : class
        {
            return (T)obj;
        }

        /// <summary>
        /// Between
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool Between(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        /// <summary>
        /// Br2Nl
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Br2Nl(this string @this)
        {
            return @this.Replace("<br />", "\r\n").Replace("<br>", "\r\n");
        }

        /// <summary>
        /// CombineApiUrl
        /// </summary>
        /// <param name="host"></param>
        /// <param name="url"></param>
        /// <param name="isSsl"></param>
        /// <returns></returns>
        public static string CombineApiUrl(this string host, string url, bool isSsl = false)
        {
            string returnUrl = "";
            if (host.EndsWith("/"))
            {
                returnUrl = host + url.TrimStart('/');
            }
            else
            {
                returnUrl = host + "/" + url.TrimStart('/');
            }
            isSsl.IfTrue(delegate
            {
                if (!returnUrl.Contains("https://"))
                {
                    returnUrl = returnUrl + "https://" + returnUrl;
                }
            }, delegate
            {
                if (!returnUrl.Contains("http://"))
                {
                    returnUrl = returnUrl + "http://" + returnUrl;
                }
            });
            return returnUrl;
        }

        /// <summary>
        /// IfTrue
        /// </summary>
        /// <param name="this"></param>
        /// <param name="trueAction"></param>
        /// <param name="falseAction"></param>
        public static void IfTrue(this bool @this, Action trueAction, Action falseAction = null)
        {
            if (@this)
            {
                trueAction();
            }
            else
            {
                falseAction?.Invoke();
            }
        }

        /// <summary>
        /// ConcatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ConcatWith(this string @this, params string[] values)
        {
            return @this + values;
        }

        /// <summary>
        /// ConcatWith
        /// </summary>
        /// <param name="str0"></param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="str3"></param>
        /// <param name="str4"></param>
        /// <returns></returns>
        public static string ConcatWith(this string str0, string str1, string str2, string str3, string str4)
        {
            return str0 + str1 + str2 + str3 + str4;
        }

        /// <summary>
        /// ConcatWith
        /// </summary>
        /// <param name="str0"></param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="str3"></param>
        /// <returns></returns>
        public static string ConcatWith(this string str0, string str1, string str2, string str3)
        {
            return str0 + str1 + str2 + str3;
        }

        /// <summary>
        /// ConcatWith
        /// </summary>
        /// <param name="str0"></param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static string ConcatWith(this string str0, string str1, string str2)
        {
            return str0 + str1 + str2;
        }

        /// <summary>
        /// ConcatWith
        /// </summary>
        /// <param name="str0"></param>
        /// <param name="str1"></param>
        /// <returns></returns>
        public static string ConcatWith(this string str0, string str1)
        {
            return str0 + str1;
        }
    }
}