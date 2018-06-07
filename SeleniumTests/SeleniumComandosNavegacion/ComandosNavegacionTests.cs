using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace SeleniumTests.SeleniumComandosNavegacion
{
    class ComandosNavegacionTests
    {
        IWebDriver _myDriver;
        [SetUp]
        void InitializeTests()
        {
            _myDriver = new FirefoxDriver();
        }

        [Test]
        void GoToUrlCommandTest()
        {
            _myDriver.Url = "http://www.facebook.com";
            _myDriver.Navigate().GoToUrl("https://accounts.google.com");
        }

        [TearDown]
        void TearDownTests()
        {

        }
    }
}
