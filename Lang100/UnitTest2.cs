using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Lang100
{
    [TestClass]
    public class UnitTest2
    {
        public static string _hightempText;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _hightempText = File.ReadAllText("hightemp.txt");
        }

        [TestMethod]
        public void Test10()
        {
            // wc hightemp.txt
            var result = _hightempText.Split('\n').Length;
        }

        [TestMethod]
        public void Test11()
        {
            // cat hightemp.txt | sed -e 's/\t/ /g'
            // cat hightemp.txt | tr '\t' ' '
            var result = _hightempText.Replace('\t', ' ');
        }

        [TestMethod]
        public void Test12()
        {
            // cat hightemp.txt | cut -d$'\n' -f1,2
            var hightempLines = _hightempText.Split('\n');
            File.WriteAllText("col1.txt", hightempLines[0]);
            File.WriteAllText("col2.txt", hightempLines[1]);
        }

        [TestMethod]
        public void Test13()
        {
            // paste コマンドがない・・・
            var result = File.ReadAllText("col1.txt") + "\n" + File.ReadAllText("col2.txt");
        }

        [TestMethod]
        public void Test14()
        {
            // head -n 10 hightemp.txt
            var n = 10;
            var hightempLines = _hightempText.Split('\n');
            var result = string.Join("\n", hightempLines.Take(n));
        }

        [TestMethod]
        public void Test15()
        {
            // tail -n 10 hightemp.txt
            var n = 10;
            var hightempLines = _hightempText.Split('\n');
            var result = string.Join("\n",
                hightempLines.Skip(Math.Max(0, hightempLines.Length - n)).Take(n));
        }

        [TestMethod]
        public void Test16()
        {
            // split は、行数指定で分割。分割数指定ではない
            var n = 5;

            var hightempLines = _hightempText.Split('\n');
            var rowCount = hightempLines.Length / n;
            var result = new string[n];
            for (var i = 0; i < n - 1; i++)
                result[i] = string.Join("\n", hightempLines.Skip(i * rowCount).Take(rowCount));
            result[n - 1] = string.Join("\n", hightempLines.Skip((n - 1) * rowCount));
        }

        [TestMethod]
        public void Test17()
        {
            // cat hightemp.txt | cut -c 1-3 | sort | uniq
            var hightempLines = _hightempText.Split('\n');
            var result = hightempLines.Select(s => s[0]).Distinct().OrderBy(s => s);
        }

        [TestMethod]
        public void Test18()
        {
            // cat hightemp.txt | sort -t $'\t' -k 3
            var hightempLines = _hightempText.Split('\n');
            var result = hightempLines.OrderBy(s => s.Split('\t')[2]);
        }

        [TestMethod]
        public void Test19()
        {
            // shell わかんにゃい・・
            var hightempLines = _hightempText.Split('\n');
            var prefCountDic = hightempLines.Select(s => s.Split('\t')[0])
                .GroupBy(p => p).ToDictionary(g => g.Key, g => g.Count());
            var result = hightempLines
                .OrderByDescending(s => prefCountDic[s.Split('\t')[0]])
                .ThenBy(s => s.Split('\t')[0]);

        }
    }
}
