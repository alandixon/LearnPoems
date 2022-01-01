using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_LearnPoems
{
    [TestClass]
    public class Test_Helpers
    {
        [TestInitialize]
        public void testInit()
        {

        }

        [DataRow("a", 1, "a")]
        [DataRow("ba", 1, "b")]
        [DataTestMethod]
        public void Test_RestrictStringLength(string inStr, int len, string expectedOutStr)
        {
            string foundOutStr = LearnPoems.Helpers.StringHelper.RestrictStringLength(inStr, len);
            Assert.AreEqual(expectedOutStr, foundOutStr);
        }
    }
}
