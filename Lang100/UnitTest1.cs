using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Lang100
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test00()
        {
            var str = "stressed";
            var result = string.Join("", str.Reverse());
        }

        [TestMethod]
        public void Test01()
        {
            var str = "パタトクカシーー";
            var result = "" + str[0] + str[2] + str[4] + str[6];
        }

        [TestMethod]
        public void Test02()
        {
            var str1 = "パトカー";
            var str2 = "タクシー";
            var result = "";
            for (var i = 0; i < str1.Length; i++)
            {
                result += str1[i];
                result += str2[i];
            }
        }

        [TestMethod]
        public void Test03()
        {
            var str = "Now I need a drink, alcoholic of course, after the heavy lectures involving quantum mechanics.";
            var result = str.Split(' ').Select(s => s.Length);
        }

        [TestMethod]
        public void Test04()
        {
            var str = "Hi He Lied Because Boron Could Not Oxidize Fluorine. New Nations Might Also Sign Peace Security Clause. Arthur King Can.";
            var firstPicks = new[] { 1, 5, 6, 7, 8, 9, 15, 16, 19 }.Select(i => i - 1);
            var words = str.Split(' ');
            var result = new Dictionary<string, int>();
            for(var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                result.Add(firstPicks.Contains(i) ? word.Substring(0, 1) : word.Substring(0, 2), word.Length);
            }
        }

        [TestMethod]
        public void Test05()
        {
            var str = "I am an NLPer";
            var result = CreateNGram(2, str);
        }
        public IEnumerable<string> CreateNGram(int n, string sentence)
        {
            var list = new List<string>();
            for(var i = 0; i < sentence.Length; i++)
            {
                var length = Math.Min(n, sentence.Length - i);
                list.Add(sentence.Substring(i, length));
            }
            return list;
        }


        [TestMethod]
        public void Test06()
        {
            var strX = "paraparaparadise";
            var strY = "paragraph";
            var x = CreateNGram(2, strX);
            var y = CreateNGram(2, strY);

            var union = x.Union(y);
            var intersection = x.Intersect(y);
            var difference = x.Except(y);

            var resultX = x.Contains("se");
            var resultY = y.Contains("se");
        }

        [TestMethod]
        public void Test07()
        {
            var result = CreateString("12", "気温", "22.4");
        }
        public string CreateString(string x, string y, string z)
        {
            return string.Format("{0}時の{1}は{2}", x, y, z);
        }

        [TestMethod]
        public void Test08()
        {
            var str = "abcABCあいうxyzXYZ";
            var cipher = Cipher(str);
            var decipher = Cipher(cipher);
        }
        public string Cipher(string str)
        {
            var result = "";
            foreach(var c in str)
            {
                if (c >= 'a' && c <= 'z')
                    result += (char)(219 - c);
                else
                    result += c;
            }
            return result;
        }
        public string Decipher(string str)
        {
            return Cipher(str);
        }

        [TestMethod]
        public void Test09()
        {
            var str = "I couldn't believe that I could actually understand what I was reading : the phenomenal power of the human mind .";
            var rand = new Random();
            var result = string.Join(" ", str.Split(' ').Select(s =>
            {
                if (s.Length < 4)
                    return s;

                var ary = s.ToArray();
                for (var i = ary.Length - 2; i > 1; i--)
                {
                    var j = rand.Next(1, i + 1);
                    var tmp = ary[i];
                    ary[i] = ary[j];
                    ary[j] = tmp;
                }
                return string.Join("", ary);
            }));

        }
    }
}
