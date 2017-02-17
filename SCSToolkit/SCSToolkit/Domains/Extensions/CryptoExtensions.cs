using System;
using System.Text;
using Microsoft.Ajax.Utilities;

namespace SCSToolkit.Domains.Extensions
{
    public static class CryptoExtensions
    {
        public static string ToEncodedBase64UrlSafe(this string plainText)
        {
            if (plainText.IsNullOrWhiteSpace()) return "";
            var ptBytes = Encoding.UTF8.GetBytes(plainText);
            var encText = Convert.ToBase64String(ptBytes);
            encText = encText.Replace("=", "").Replace("+", "-").Replace("/", "_");
            return encText;
        }

        public static string ToPlainText(this string base64UrlSafeText)
        {
            if (base64UrlSafeText.IsNullOrWhiteSpace()) return "";
            var base64Text = base64UrlSafeText.Replace("-", "+").Replace("_", "/");
            var paddingCount = base64Text.Length%4;
            switch (paddingCount)
            {
                case 3:
                    base64Text += "=";
                    break;
                case 2:
                    base64Text += "==";
                    break;
                case 1:
                    base64Text += "===";
                    break;
            }
            var ptBytes = Convert.FromBase64String(base64Text);
            var plainText = Encoding.UTF8.GetString(ptBytes);
            return plainText;
        }

        public static byte[] CaesarShift(this byte[] bytes, int numberOfShift)
        {
            if (bytes.Length == 0) return bytes;
            var shift = numberOfShift%byte.MaxValue;
            for (var x = 0; x < bytes.Length; x++)
            {
                bytes[x] = (byte) ((bytes[x] + shift)%byte.MaxValue);
            }
            return bytes;
        }
    }
}