using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OctoPerf.PetStore.Automation.Framework.Utilities
{
    public class TestHelper
    {
        private static Random random = new Random();
        private const string alphaChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string numericChars = "0123456789";
        private const string alphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [ThreadStatic]
        public static string[] CurrentTags;

        public static string GetRandomAlphaNumericString(int length)
        {
            return new string(Enumerable.Repeat(alphanumericChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomAlphaNumericString()
        {
            return new string(Enumerable.Repeat(alphanumericChars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomString(int length)
        {
            return new string(Enumerable.Repeat(alphaChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomString()
        {
            return new string(Enumerable.Repeat(alphaChars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomNumericString(int length)
        {
            return new string(Enumerable.Repeat(numericChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomNumericString()
        {
            return new string(Enumerable.Repeat(numericChars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
