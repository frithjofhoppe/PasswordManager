using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace PasswordManagerTest
{
    /// <summary>
    /// Summary description for SerachPassword_InvalidIdentifier_NotFound
    /// </summary>
    [CodedUITest]
    public class SerachPassword_InvalidIdentifier_NotFound
    {
        public SerachPassword_InvalidIdentifier_NotFound()
        {
        }

        [TestMethod]
        public void CodedUITestMethod1()
        {
            this.UIMap.OpenApplication();
            this.UIMap.CreateTestCategory();
            this.UIMap.CreatePassword();
            this.UIMap.AssertPasswordCreated();
            this.UIMap.GoBackToList();
            this.UIMap.AssertRowCount();
            this.UIMap.AssertCreatedPasswordIdentifier();
            this.UIMap.SearchAfterNotExistingIdentifier();
            this.UIMap.AssertIdentifierNotFound();
            this.UIMap.DeleteCategory();
            this.UIMap.CloseApplication();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
