using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebAutomation.DriverFactory
{
    public class ChromeFactory : IBrowserFactory
    {
        public IWebDriver CreateWebDriver()
        {
            return new ChromeDriver();
        }
    }
}
