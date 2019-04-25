using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace DotNetCore.Kit
{
    /// <summary>
    /// Extensions
    /// </summary>
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

        /// <summary>
        /// ContainsAll
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAll(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ContainsAll
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparisonType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAll(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ContainsAny
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ContainsAny
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparisonType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Copy(this string str)
        {
            return string.Copy(str);
        }

        /// <summary>
        /// Cut
        /// </summary>
        /// <param name="this"></param>
        /// <param name="maxLength"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string Cut(this string @this, int maxLength, string suffix = "...")
        {
            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }
            int length = maxLength - suffix.Length;
            return @this.Substring(0, length) + suffix;
        }

        /// <summary>
        /// CutWithCN
        /// </summary>
        /// <param name="p_SrcString"></param>
        /// <param name="p_StartIndex"></param>
        /// <param name="p_Length"></param>
        /// <param name="p_TailString"></param>
        /// <returns></returns>
        public static string CutWithCN(this string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string result = p_SrcString;
            if (p_Length >= 0)
            {
                byte[] bytes = Encoding.Default.GetBytes(p_SrcString);
                if (bytes.Length > p_StartIndex)
                {
                    int num = bytes.Length;
                    if (bytes.Length > p_StartIndex + p_Length)
                    {
                        num = p_Length + p_StartIndex;
                    }
                    else
                    {
                        p_Length = bytes.Length - p_StartIndex;
                        p_TailString = "";
                    }
                    int num2 = p_Length;
                    int[] array = new int[p_Length];
                    int num3 = 0;
                    for (int i = p_StartIndex; i < num; i++)
                    {
                        if (bytes[i] > 127)
                        {
                            num3++;
                            if (num3 == 3)
                            {
                                num3 = 1;
                            }
                        }
                        else
                        {
                            num3 = 0;
                        }
                        array[i] = num3;
                    }
                    if (bytes[num - 1] > 127 && array[p_Length - 1] == 1)
                    {
                        num2 = p_Length + 1;
                    }
                    byte[] array2 = new byte[num2];
                    Array.Copy(bytes, p_StartIndex, array2, 0, num2);
                    result = Encoding.Default.GetString(array2);
                    result += p_TailString;
                }
            }
            return result;
        }

        /// <summary>
        /// CutWithCN
        /// </summary>
        /// <param name="p_SrcString"></param>
        /// <param name="p_Length"></param>
        /// <param name="p_TailString"></param>
        /// <returns></returns>
        public static string CutWithCN(this string p_SrcString, int p_Length, string p_TailString)
        {
            return p_SrcString.CutWithCN(0, p_Length, p_TailString);
        }

        /// <summary>
        /// CutWithPostfix
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string CutWithPostfix(this string str, int maxLength)
        {
            return str.CutWithPostfix(maxLength, "...");
        }

        /// <summary>
        /// CutWithPostfix
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string CutWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }
            if (str == string.Empty || maxLength == 0)
            {
                return string.Empty;
            }
            if (str.Length <= maxLength)
            {
                return str;
            }
            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }
            return str.Left(maxLength - postfix.Length) + postfix;
        }

        /// <summary>
        /// DecodeBase64
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string DecodeBase64(this string @this)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(@this));
        }

        /// <summary>
        /// DecodeUTF8Base64
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string DecodeUTF8Base64(this string @this)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(@this));
        }

        /// <summary>
        /// Elapsed
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static TimeSpan Elapsed(this DateTime startTime, DateTime endTime)
        {
            return startTime - endTime;
        }

        /// <summary>
        /// Elapsed
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }

        /// <summary>
        /// EncodeBase64
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string EncodeBase64(this string @this)
        {
            return Convert.ToBase64String(Activator.CreateInstance<ASCIIEncoding>().GetBytes(@this));
        }

        /// <summary>
        /// EncodeUTF8Base64
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string EncodeUTF8Base64(this string @this)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(@this));
        }

        /// <summary>
        /// EndOfDay
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(1.0).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }

        /// <summary>
        /// EnsureEndsWith
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string EnsureEndsWith(this string str, char c)
        {
            return str.EnsureEndsWith(c, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// EnsureEndsWith
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string EnsureEndsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (str.EndsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }
            return str + c.ToString();
        }

        /// <summary>
        /// EnsureEndsWith
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (str.EndsWith(c.ToString(CultureInfo.InvariantCulture), comparisonType))
            {
                return str;
            }
            return str + c.ToString();
        }

        /// <summary>
        /// EnsureMaximumLength
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string EnsureMaximumLength(this string str, int maxLength, string postfix = null)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }
            if (str.Length > maxLength)
            {
                string text = str.Substring(0, maxLength);
                if (!string.IsNullOrEmpty(postfix))
                {
                    text += postfix;
                }
                return text;
            }
            return str;
        }

        /// <summary>
        /// EnsureNotNull
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EnsureNotNull(this string str)
        {
            if (str == null)
            {
                return string.Empty;
            }
            return str;
        }

        /// <summary>
        /// EnsureNumericOnly
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EnsureNumericOnly(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// EnsureStartsWith
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string EnsureStartsWith(this string str, char c)
        {
            return str.EnsureStartsWith(c, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// EnsureStartsWith
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (str.StartsWith(c.ToString(CultureInfo.InvariantCulture), comparisonType))
            {
                return str;
            }
            return c.ToString() + str;
        }

        /// <summary>
        /// EnsureStartsWith
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string EnsureStartsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (str.StartsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }
            return c.ToString() + str;
        }

        /// <summary>
        /// ExistInEnum<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ExistInEnum<T>(this int value)
        {
            Type typeFromHandle = typeof(T);
            bool result = false;
            string[] names = Enum.GetNames(typeFromHandle);
            foreach (string value2 in names)
            {
                if (Enum.Format(typeFromHandle, Enum.Parse(typeFromHandle, value2), "d").ToInt() == value)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// For
        /// </summary>
        /// <param name="n"></param>
        /// <param name="action"></param>
        public static void For(this int n, Action<int> action)
        {
            for (int i = 0; i < n; i++)
            {
                action(i);
            }
        }

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public static string Format(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string format, object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string Format(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <returns></returns>
        public static string Format(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        /// <summary>
        /// FormatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, params object[] values)
        {
            return string.Format(@this, values);
        }

        /// <summary>
        /// FormatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, object arg0, object arg1, object arg2)
        {
            return string.Format(@this, arg0, arg1, arg2);
        }

        /// <summary>
        /// FormatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, object arg0, object arg1)
        {
            return string.Format(@this, arg0, arg1);
        }

        /// <summary>
        /// FormatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="arg0"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, object arg0)
        {
            return string.Format(@this, arg0);
        }

        /// <summary>
        /// GetAssembly
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }

        /// <summary>
        /// GetAttribute<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this MethodBase methodInfo) where T : class
        {
            return methodInfo.GetCustomAttribute(typeof(T)) as T;
        }

        /// <summary>
        /// GetDirectoryPathOrNull
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetDirectoryPathOrNull(this Assembly assembly)
        {
            string location = assembly.Location;
            return location == null ? null : new FileInfo(location).Directory?.FullName;
        }

        /// <summary>
        /// GetFileName
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetFileName(this string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] array = url.Split(new char[1]
            {
            '/'
            });
            return array[array.Length - 1].Split(new char[1]
            {
            '?'
            })[0];
        }

        /// <summary>
        /// GetIndexAfterNextDoubleQuote
        /// </summary>
        /// <param name="this"></param>
        /// <param name="allowEscape"></param>
        /// <returns></returns>
        public static int GetIndexAfterNextDoubleQuote(this StringBuilder @this, bool allowEscape)
        {
            return @this.GetIndexAfterNextDoubleQuote(0, allowEscape);
        }

        /// <summary>
        /// GetIndexAfterNextDoubleQuote
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int GetIndexAfterNextDoubleQuote(this StringBuilder @this)
        {
            return @this.GetIndexAfterNextDoubleQuote(0, allowEscape: false);
        }

        /// <summary>
        /// GetIndexAfterNextDoubleQuote
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int GetIndexAfterNextDoubleQuote(this StringBuilder @this, int startIndex)
        {
            return @this.GetIndexAfterNextDoubleQuote(startIndex, allowEscape: false);
        }

        /// <summary>
        /// GetIndexAfterNextDoubleQuote
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startIndex"></param>
        /// <param name="allowEscape"></param>
        /// <returns></returns>
        public static int GetIndexAfterNextDoubleQuote(this StringBuilder @this, int startIndex, bool allowEscape)
        {
            while (startIndex < @this.Length)
            {
                char c = @this[startIndex];
                startIndex++;
                char c2;
                if (allowEscape && c == '\\' && startIndex < @this.Length && ((c2 = @this[startIndex]) == '\\' || c2 == '"'))
                {
                    startIndex++;
                }
                else if (c == '"')
                {
                    return startIndex;
                }
            }
            return startIndex;
        }

        /// <summary>
        /// GetIndexAfterNextSingleQuote
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int GetIndexAfterNextSingleQuote(this StringBuilder @this)
        {
            return @this.GetIndexAfterNextSingleQuote(0, allowEscape: false);
        }

        /// <summary>
        /// GetIndexAfterNextSingleQuote
        /// </summary>
        /// <param name="this"></param>
        /// <param name="allowEscape"></param>
        /// <returns></returns>
        public static int GetIndexAfterNextSingleQuote(this StringBuilder @this, bool allowEscape)
        {
            return @this.GetIndexAfterNextSingleQuote(0, allowEscape);
        }

        /// <summary>
        /// GetIndexAfterNextSingleQuote
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int GetIndexAfterNextSingleQuote(this StringBuilder @this, int startIndex)
        {
            return @this.GetIndexAfterNextSingleQuote(startIndex, allowEscape: false);
        }

        /// <summary>
        /// GetIndexAfterNextSingleQuote
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startIndex"></param>
        /// <param name="allowEscape"></param>
        /// <returns></returns>
        public static int GetIndexAfterNextSingleQuote(this StringBuilder @this, int startIndex, bool allowEscape)
        {
            while (startIndex < @this.Length)
            {
                char c = @this[startIndex];
                startIndex++;
                char c2;
                if (allowEscape && c == '\\' && startIndex < @this.Length && ((c2 = @this[startIndex]) == '\\' || c2 == '\''))
                {
                    startIndex++;
                }
                else if (c == '\'')
                {
                    return startIndex;
                }
            }
            return startIndex;
        }

        /// <summary>
        /// GetProperty
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetProperty(name);
        }

        /// <summary>
        /// GetSingleAttributeOfTypeOrBaseTypesOrNull
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="type"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TAttribute GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(this Type type, bool inherit = true) where TAttribute : Attribute
        {
            TAttribute singleAttributeOrNull = type.GetTypeInfo().GetSingleAttributeOrNull<TAttribute>();
            if (singleAttributeOrNull != null)
            {
                return singleAttributeOrNull;
            }
            if (type.GetTypeInfo().BaseType == null)
            {
                return null;
            }
            return type.GetTypeInfo().BaseType.GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(inherit);
        }

        /// <summary>
        /// GetSingleAttributeOrNull
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TAttribute GetSingleAttributeOrNull<TAttribute>(this MemberInfo memberInfo, bool inherit = true) where TAttribute : Attribute
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }
            object[] array = memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).ToArray();
            if (array.Length != 0)
            {
                return (TAttribute)array[0];
            }
            return null;
        }

        /// <summary>
        /// GetSpacesString
        /// </summary>
        /// <param name="spacesCount"></param>
        /// <returns></returns>
        public static string GetSpacesString(this int spacesCount)
        {
            StringBuilder sb = new StringBuilder();
            spacesCount.For(delegate
            {
                sb.Append(" &nbsp;&nbsp;");
            });
            return sb.ToString();
        }

        /// <summary>
        /// GetSubString
        /// </summary>
        /// <param name="p_SrcString"></param>
        /// <param name="p_StartIndex"></param>
        /// <param name="p_Length"></param>
        /// <param name="p_TailString"></param>
        /// <returns></returns>
        public static string GetSubString(this string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            return p_SrcString.CutWithCN(p_StartIndex, p_Length, p_TailString);
        }

        /// <summary>
        /// HtmlDecode
        /// </summary>
        /// <param name="s"></param>
        /// <param name="output"></param>
        public static void HtmlDecode(this string s, TextWriter output)
        {
            HttpUtility.HtmlDecode(s, output);
        }

        /// <summary>
        /// HtmlDecode
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string s)
        {
            return HttpUtility.HtmlDecode(s);
        }

        /// <summary>
        /// HtmlEncode
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        /// <summary>
        /// HtmlEncode
        /// </summary>
        /// <param name="s"></param>
        /// <param name="output"></param>
        public static void HtmlEncode(this string s, TextWriter output)
        {
            HttpUtility.HtmlEncode(s, output);
        }

        /// <summary>
        /// ToInt
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int ToInt(this object expression, int defValue = 0)
        {
            return expression.IfNotNull((object x) => x.ToString().ToInt(), defValue);
        }

        /// <summary>
        /// IfNotNull
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        public static void IfNotNull(this object obj, Action action)
        {
            if (obj != null)
            {
                action();
            }
        }

        /// <summary>
        /// IfNotNull
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="action"></param>
        public static void IfNotNull<TItem>(this TItem item, Action<TItem> action) where TItem : class
        {
            if (item != null)
            {
                action(item);
            }
        }

        /// <summary>
        /// IfNotNull
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="action"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TResult IfNotNull<TResult, TItem>(this TItem item, Func<TItem, TResult> action, TResult defaultValue = default) where TItem : class
        {
            return (item != null) ? action(item) : defaultValue;
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
        /// IsNullOrEmpty
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        /// <summary>
        /// Left
        /// </summary>
        /// <param name="this"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string @this, int length)
        {
            return @this.LeftSafe(length);
        }

        /// <summary>
        /// LeftSafe
        /// </summary>
        /// <param name="this"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string LeftSafe(this string @this, int length)
        {
            return @this.Substring(0, Math.Min(length, @this.Length));
        }
    }
}