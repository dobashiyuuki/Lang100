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
            var hightempLines = _hightempText.Split('\n');
            File.WriteAllText("col1.txt", hightempLines[0]);
            File.WriteAllText("col2.txt", hightempLines[1]);
        }
        
    }
}
