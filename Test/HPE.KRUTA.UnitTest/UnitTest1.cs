using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPE.Kruta.Domain;

namespace HPE.KRUTA.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DocumentManager doc = new DocumentManager();
            var result = doc.GetDocumentPath(3);
            Assert.IsNull(result);
        }
    }
}
