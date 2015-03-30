using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lang100
{
    [TestClass]
    public class UnitTest3
    {
        public static string _jsonText;
        public static string[] _jsonLines;
        public static string[] _targetLines;
        public static string _targetText;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _jsonText = File.ReadAllText("jawiki-country.json");
            _jsonLines = _jsonText.Split('\n');
            _targetLines = _jsonLines.Where(l => l.Contains("イギリス")).ToArray();
            _targetText = string.Join("\n", _targetLines);
        }

        [TestMethod]
        public void Test20()
        {
            var result = _targetLines;
        }

        [TestMethod]
        public void Test21()
        {
            //var reg = new Regex("\\[\\[カテゴリ:(.*?)\\]\\]", RegexOptions.Compiled);
            var reg = new Regex("カテゴリ", RegexOptions.Compiled);
            for (var i = 0; i < _targetLines.Length; i++ )
            {
                var line = _targetLines[i];
                var matches = reg.Matches(line);
                
             }
        }
    }
}
