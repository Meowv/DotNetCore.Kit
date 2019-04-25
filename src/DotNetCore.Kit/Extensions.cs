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
    }
}