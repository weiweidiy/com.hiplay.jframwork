using System;
using System.Text.RegularExpressions;

namespace JFramework
{
    public static class StringExtensions
    {
        /// <summary>
        /// 支持百分号自动乘100并格式化小数位的字符串扩展
        /// 用法: "A:{0:F1}% B:{1:0}% C:{2}".FormatPercent(0.9123, 0.876, "test")
        /// </summary>
        public static string FormatPercent(this string format, params object[] args)
        {
            var regex = new Regex(@"\{(\d+)(:[^}]*)?\}(%?)");
            int lastIndex = 0;
            var result = "";
            foreach (Match match in regex.Matches(format))
            {
                result += format.Substring(lastIndex, match.Index - lastIndex);

                int argIndex = int.Parse(match.Groups[1].Value);
                string customFormat = match.Groups[2].Success ? match.Groups[2].Value : "";
                bool hasPercent = match.Groups[3].Value == "%";

                object value = args[argIndex];
                if (hasPercent && value is IConvertible)
                {
                    value = Convert.ToDouble(value) * 100;
                }

                string formatted = customFormat != ""
                    ? string.Format("{0" + customFormat + "}", value is IConvertible ? Convert.ToDouble(value) : value)
                    : value.ToString();

                result += formatted;
                if (hasPercent) result += "%";

                lastIndex = match.Index + match.Length;
            }
            result += format.Substring(lastIndex);
            return result;
        }
    }
}