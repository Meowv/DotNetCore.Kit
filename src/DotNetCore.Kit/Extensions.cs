using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        /// IfFalse
        /// </summary>
        /// <param name="this"></param>
        /// <param name="trueAction"></param>
        /// <param name="falseAction"></param>
        public static void IfFalse(this bool @this, Action trueAction, Action falseAction = null)
        {
            if (!@this)
            {
                trueAction();
            }
            else
            {
                falseAction?.Invoke();
            }
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
        /// IfNotNull<TItem>
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
        /// IfNotNull<TResult, TItem>
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
        /// IfNull<TItem>
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TItem IfNull<TItem>(this TItem item, Func<TItem, TItem> action) where TItem : class
        {
            return item ?? action(item);
        }

        /// <summary>
        /// IfNullOrEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string IfNullOrEmpty(this string value, string defaultValue = "")
        {
            return value.IsNotNullOrEmpty() ? value : defaultValue;
        }

        /// <summary>
        /// IfNullSetEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IfNullSetEmpty(this string value)
        {
            return value ?? string.Empty;
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
        /// InRange
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        /// <summary>
        /// InvokeSafely
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender)
        {
            eventHandler.InvokeSafely(sender, EventArgs.Empty);
        }

        /// <summary>
        /// InvokeSafely
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e)
        {
            eventHandler?.Invoke(sender, e);
        }

        /// <summary>
        /// InvokeSafely<TEventArgs>
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeSafely<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            eventHandler?.Invoke(sender, e);
        }

        /// <summary>
        /// IsAfternoon
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsAfternoon(this DateTime @this)
        {
            return @this.TimeOfDay >= new DateTime(1995, 3, 7, 12, 0, 0).TimeOfDay;
        }

        /// <summary>
        /// IsAlpha
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsAlpha(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z]");
        }

        /// <summary>
        /// IsAlphaNumeric
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z0-9]");
        }

        /// <summary>
        /// IsAsync
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task) || (method.ReturnType.GetTypeInfo().IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }

        /// <summary>
        /// IsBool
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBool(this Type type)
        {
            return type == typeof(bool);
        }

        /// <summary>
        /// IsEmpty
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string @this)
        {
            return @this == "";
        }

        /// <summary>
        /// IsEnum
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type type)
        {
            return type.IsEnum;
        }

        /// <summary>
        /// IsGenericType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGenericType(this Type type)
        {
            return type.IsGenericType;
        }

        /// <summary>
        /// IsIn<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// IsMatch
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// IsMatch
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsMatch(this string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        /// <summary>
        /// IsMorning
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsMorning(this DateTime @this)
        {
            return @this.TimeOfDay < new DateTime(1995, 3, 7, 12, 0, 0).TimeOfDay;
        }

        /// <summary>
        /// IsNotEmpty
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string @this)
        {
            return @this != "";
        }

        /// <summary>
        /// IsNotNull
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// IsNotNull
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNull(this string @this)
        {
            return @this != null;
        }

        /// <summary>
        /// IsNotNullOrDefault<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNullOrDefault<T>(this T obj) where T : class
        {
            return obj != null && EqualityComparer<T>.Default.Equals(obj, null);
        }

        /// <summary>
        /// IsNotNullOrEmpty
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }

        /// <summary>
        /// IsNotNullOrWhiteSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// IsNow
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNow(this DateTime @this)
        {
            return @this == DateTime.Now;
        }

        /// <summary>
        /// IsNull
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNull(this string @this)
        {
            return @this == null;
        }

        /// <summary>
        /// IsNull
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// IsNullableType
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type @this)
        {
            return @this.IsGenericType && @this.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
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
        /// IsNullOrWhiteSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// IsNumeric
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^0-9]");
        }

        /// <summary>
        /// IsUrl
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static bool IsUrl(this string strUrl)
        {
            return Regex.IsMatch(strUrl, "^(http|https)\\://([a-zA-Z0-9\\.\\-]+(\\:[a-zA-Z0-9\\.&%\\$\\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\\-]+\\.)*[a-zA-Z0-9\\-]+\\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\\:[0-9]+)*(/($|[a-zA-Z0-9\\.\\,\\?\\'\\\\\\+&%\\$#\\=~_\\-]+))*$");
        }

        /// <summary>
        /// IsValidBase64String
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidBase64String(this string str)
        {
            return Regex.IsMatch(str, "[A-Za-z0-9\\+\\/\\=]");
        }

        /// <summary>
        /// IsValidEmail
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string obj)
        {
            return Regex.IsMatch(obj, "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }

        /// <summary>
        /// IsValidIP
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsValidIP(this string obj)
        {
            return Regex.IsMatch(obj, "^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
        }

        /// <summary>
        /// IsValidMobile
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsValidMobile(string mobile)
        {
            if (mobile.IsNullOrEmpty())
            {
                return false;
            }
            mobile = mobile.Trim();
            return Regex.IsMatch(mobile, "^(1[3|4|5|6|7|8|9])\\d{9}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// IsValidSafeSqlString
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidSafeSqlString(this string str)
        {
            return !Regex.IsMatch(str, "[-|;|,|\\/|\\(|\\)|\\[|\\]|\\}|\\{|%|@|\\*|!|\\']");
        }

        /// <summary>
        /// IsValueType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueType(this Type type)
        {
            return type.IsValueType;
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Join(this string separator, string[] value)
        {
            return string.Join(separator, value);
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Join(this string separator, string[] value, int startIndex, int count)
        {
            return string.Join(separator, value, startIndex, count);
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join(this string separator, object[] values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Join<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join<T>(this string separator, IEnumerable<T> values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join(this string separator, IEnumerable<string> values)
        {
            return string.Join(separator, values);
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

        /// <summary>
        /// Locking<T, TResult>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult Locking<T, TResult>(this T source, Func<T, TResult> func) where T : class
        {
            lock (source)
            {
                return func(source);
            }
        }

        /// <summary>
        /// Locking<TResult>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult Locking<TResult>(this object source, Func<TResult> func)
        {
            lock (source)
            {
                return func();
            }
        }

        /// <summary>
        /// Locking<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void Locking<T>(this T source, Action<T> action) where T : class
        {
            lock (source)
            {
                action(source);
            }
        }

        /// <summary>
        /// Locking
        /// </summary>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void Locking(this object source, Action action)
        {
            lock (source)
            {
                action();
            }
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
    }
}