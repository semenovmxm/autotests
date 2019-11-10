using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace auto_web_tests
{
    [TestFixture]
    public class MailruTestCase
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://mail.ru/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void LoginTest()
        {
            string inboxPageUrl = "https://e.mail.ru/messages/inbox";           
            string login = "testtztz";
            string password = "NtcnNtcn1";

            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("mailbox:login")).Click();
            driver.FindElement(By.Id("mailbox:login")).Clear();
            driver.FindElement(By.Id("mailbox:login")).SendKeys(login);
            driver.FindElement(By.XPath("//label[@id='mailbox:submit']/input")).Click();
            driver.FindElement(By.Id("mailbox:password")).Click();
            driver.FindElement(By.Id("mailbox:password")).Clear();
            driver.FindElement(By.Id("mailbox:password")).SendKeys(password);
            driver.FindElement(By.XPath("//label[@id='mailbox:submit']/input")).Click();
            string currentURL = driver.Url;
            
            var isInboxPageUrl = currentURL.StartsWith(inboxPageUrl);            
            Console.WriteLine("url " + currentURL);

            Assert.IsTrue(isInboxPageUrl);
            Assert.AreEqual(login + "@mail.ru", driver.FindElement(By.Id("PH_user-email")).Text);
        }
    }
}
