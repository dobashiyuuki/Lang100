using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;

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
            var result = new List<int>();
            
            var reg = new Regex("\\[\\[カテゴリ:(.*?)\\]\\]", RegexOptions.Compiled);
            for (var i = 0; i < _targetLines.Length; i++ )
            {
                var line = _targetLines[i];
                var matches = reg.Matches(line);
                if (matches.Count > 0) result.Add(i);
             }
        }

        [TestMethod]
        public void Test22()
        {
            var result = new Dictionary<int, string>();

            var reg = new Regex("\\[\\[カテゴリ:(.*?)\\]\\]", RegexOptions.Compiled);
            for (var i = 0; i < _targetLines.Length; i++)
            {
                var line = _targetLines[i];
                var matches = reg.Matches(line);

                if (matches.Count == 0) continue;

                var categories = "";
                foreach(Match m in matches)
                {
                    categories += (m.Groups[1].Value + " ");
                }
                result.Add(i, categories);
            }
        }

        [TestMethod]
        public void Test23()
        {
            var result = new Dictionary<int, string>();

            var reg = new Regex("(={2,}) (.*?) (={2,})", RegexOptions.Compiled);
            for (var i = 0; i < _targetLines.Length; i++)
            {
                var line = _targetLines[i];
                var matches = reg.Matches(line);

                if (matches.Count == 0) continue;

                var categories = "";
                foreach (Match m in matches)
                {
                    categories += string.Format("{0}:{1} ", m.Groups[1].Value.Length, m.Groups[2].Value);
                }
                result.Add(i, categories);
            }
        }

        [TestMethod]
        public void Test24()
        {
            var result = new Dictionary<int, string>();

            var reg = new Regex("\\[\\[ファイル:(.*?)\\|", RegexOptions.Compiled);
            for (var i = 0; i < _targetLines.Length; i++)
            {
                var line = _targetLines[i];
                var matches = reg.Matches(line);

                if (matches.Count == 0) continue;

                var categories = "";
                foreach (Match m in matches)
                {
                    categories += (m.Groups[1].Value + " ");
                }
                result.Add(i, categories);
            }
        }

        [TestMethod]
        public void Test25()
        {
            var result = InnerTest25();
        }

        private static Dictionary<int, Dictionary<string, string>> InnerTest25()
        {
            var result = new Dictionary<int, Dictionary<string, string>>();

            for (var i = 0; i < _targetLines.Length; i++)
            {
                var line = _targetLines[i];
                var m = Regex.Match(line, "\\{\\{基礎情報[^\\|]*(.*?)\\}\\}\\\\n[^\\|]");
                if (!m.Success)
                {
                    Debug.WriteLine("[{0}] not found.", i);
                    continue;
                }

                var infoStr = m.Groups[1].Value;
                var infoArray = infoStr.Split(new[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                var infoDic = new Dictionary<string, string>();
                foreach (var info in infoArray)
                {
                    var m2 = Regex.Match(info, "\\|(.*) =(.*)$");
                    infoDic[m2.Groups[1].Value] = m2.Groups[2].Value;
                }
                result[i] = infoDic;
            }

            return result;
        }

        [TestMethod]
        public void Test26()
        {
            var orgDic = InnerTest25();
            var resultDic = new Dictionary<int, Dictionary<string, string>>();

            foreach (var lineKvp in orgDic)
            {
                var lineNo = lineKvp.Key;
                var infoDic = lineKvp.Value;
                
                var resultInfoDic = new Dictionary<string, string>();
                resultDic[lineNo] = resultInfoDic;

                foreach(var infoKvp in infoDic)
                {
                    var key = infoKvp.Key;
                    var val = infoKvp.Value;

                    var replaced = Regex.Replace(val, "'''''([^']+?)'''''|'''([^']+?)'''|''([^']+?)''", "$1");
                    resultInfoDic[key] = replaced;
                }
            }
        }

        [TestMethod]
        public void Test27()
        {
            var orgDic = InnerTest25();
            var resultDic = new Dictionary<int, Dictionary<string, string>>();

            foreach (var lineKvp in orgDic)
            {
                var lineNo = lineKvp.Key;
                var infoDic = lineKvp.Value;

                var resultInfoDic = new Dictionary<string, string>();
                resultDic[lineNo] = resultInfoDic;

                foreach (var infoKvp in infoDic)
                {
                    var key = infoKvp.Key;
                    var val = infoKvp.Value;

                    var replaced = Regex.Replace(val, "'''''([^']+?)'''''|'''([^']+?)'''|''([^']+?)''", "$1");
                    replaced = Regex.Replace(replaced, "\\[\\[(?:.*?)\\|?(.+?)\\]\\]", "$1");
                    resultInfoDic[key] = replaced;
                }
            }
        }
    }
}
