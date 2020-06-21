using Shared.ClassExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Shared.ClassExtensions
{
    public static class StringExtensions
    {
        public static string ReplaceInvalidCharSpaces(this string value)
        {
            if (value is null)
                return value;

            value = Regex.Replace(value, @"[^0-9a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ]+", "");

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            value = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            value = value.Trim().Replace(" ", "").ToLower();

            return value;
        }

        public static string ReplaceInvalidChar(this string value)
        {
            if (value is null)
                return value;

            value = value
                .Replace("’", "'")
                .Replace("–", "-")
                .Replace("—", "-");

            value = Regex.Replace(value, @"[^ 0-9a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ'\-/|,.;:]+", "");

            return value;
        }

        private static double CalculatePercentageSimilarityOfTwoStrings(string source, string target, bool ignoreCase)
        {
            if (source == null || target == null)
                return 0.0;

            if (!ignoreCase)
            {
                source = source.ToLower();
                target = target.ToLower();
            }


            if (source.Length == 0 || target.Length == 0) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return 1.0 - stepsToSame / (double)Math.Max(source.Length, target.Length);
        }

        private static int ComputeLevenshteinDistance(string source, string target)
        {
            if (source == null || target == null) return 0;
            if (source.Length == 0 || target.Length == 0) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = target[j - 1] == source[i - 1] ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        public static string GetContentType(this string value)
        {
            return GetMimeTypes()[value];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {"text/plain", ".txt"},
                {"application/pdf",".pdf"},
                {"application/vnd.ms-word",".docx"},
                {"application/vnd.ms-excel",".xls"},
                {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",".xlsx"},
                {"image/png",".png"},
                {"image/jpeg",".jpg"},
                {"image/gif",".gif"},
                {"text/csv",".csv"}
            };
        }

        public static string GetContentFormatType(this string value)
        {
            return GetExtensionFormat()[value];
        }

        private static Dictionary<string, string> GetExtensionFormat()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg","image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public static T DynamicConvert<T>(this string value)
        {
            try
            {
                if (typeof(T) == typeof(bool))
                    return (T)value.ParseToBool();
                else if (typeof(T) == typeof(DateTime))
                    return (T)value.ParseToDateTime();
                else if (typeof(T) == typeof(decimal))
                    return (T)ParseToDecimal(value);

                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    // Cast ConvertFromString(string text) : object to (T)
                    return (T)converter.ConvertFromString(value);
                }

                return default;
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }

        private static object ParseToDecimal(string value)
        {
            return Convert.ToDecimal(value, new CultureInfo("pt-BR"));
        }

        private static object ParseToBool(this string value)
        {
            value = value.ToLower().Trim();

            if (value == "y" || value == "s" ||
                value == "yes" || value == "sim" ||
                value == "true" || value == "1")
                return true;

            return false;
        }

        private static object ParseToDateTime(this string value)
        {
            //string[] formats = { "dd/MM/yyyy", "dd/MM/yyyy HH:mm", "dd/MM/yyyy HH:mm:ss:ffffff" };
            string[] formats = { "M/d/yyyy", "d/M/yyyy", "dd/MM/yyyy", "dd-MM-yyyy","MM/dd/yyyy", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yy/MM/dd", "yy/M/dd", "yy/MM/d", "yy/M/d","d/M/yyyy HH:mm:ss tt",
                                "M/d/yyyy HH:mm:ss tt", "M/d/yyyy HH:mm:ss tt", "M/d/yyyy h:mm:ss tt", "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm","dd/MM/yyyy HH:mm:ss:ffffff",
                                "dd/M/yyyy HH:mm:ss:ffffff","dd/M/yyyy HH:mm:ss:ffffff tt", "yyyy/MM/dd tt", "yyyy/MM/dd", "MM/dd/yyyy tt","MM/dd/yyyy", "M/dd/yyyy HH:mm:ss tt", "dd/MM/yyyy HH:mm:ss"};

            return DateTime.ParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
        public static string FormatCnpj(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                value = value.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).Replace("_", string.Empty);

            if (value.Length != 14)
                value = value.ZerosEsquerda(14);

            return value;
        }

        private static string ZerosEsquerda(this string strString, int intTamanho)
        {
            string strResult = "";

            for (int intCont = 1; intCont <= intTamanho - strString.Length; intCont++)
            {
                strResult += "0";
            }

            return strResult + strString;
        }
    }
}
