using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using oAuthConnection;

namespace Testinvi
{
    /// <summary>
    /// Token test goal is to test Token object by himself
    /// It does not test the TokenBuilder.cs functions
    /// </summary>
    [TestClass]
    public class TokenTest
    {
        #region Private Attributes
        private TestContext testContextInstance; 
        #endregion

        #region Public Attributes
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        #region Constructor
        public TokenTest()
        {
        } 
        #endregion

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #region Test Methods
        [TestMethod]
        public void Constructor1()
        {
            Token token = new Token();

            Assert.AreEqual(token.AccessToken, "");
            Assert.AreEqual(token.AccessTokenSecret, "");
            Assert.AreEqual(token.ConsumerKey, "");
            Assert.AreEqual(token.ConsumerSecret, "");
        } 
        #endregion
    }
}
