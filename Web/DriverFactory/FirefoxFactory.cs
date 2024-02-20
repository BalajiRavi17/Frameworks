using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.DriverFactory
{
    public class FirefoxFactory : IBrowserFactory
    {
        public IWebDriver CreateWebDriver()
        {
            return new FirefoxDriver();
        }
    }
}
