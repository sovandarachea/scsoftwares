using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSToolkit.Domains.Extensions;

namespace SCS.Test
{
    public class CryptoExtensionsTester
    {
        [TestCase("hello world")]
        public void ToEncodedBase64UrlSafe(string plainText)
        {
            Console.WriteLine(plainText.ToEncodedBase64UrlSafe());
        }
        [TestCase("aGVsbG8gd29ybGQ")]
        public void ToPlainText(string base64UrlSafeText)
        {
            Console.WriteLine(base64UrlSafeText.ToPlainText());
        }
        [TestCase("ABCDEF", 1)]
        [TestCase("ABCDEF", 13)]
        public void CaesarShift(string texts, int shift)
        {
            var bytes = Encoding.UTF8.GetBytes(texts).CaesarShift(shift);
            var newText = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(newText);
        }
    }
}
