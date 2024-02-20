using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.DriverFactory
{
    public class CreateBrowsers
    {
        private IBrowserFactory browserFactory;
        private IWebDriver driver;
        public CreateBrowsers(IBrowserFactory factory)
        {
            this.browserFactory = factory;
        }
        public IWebDriver Initialize()
        {
            // Create WebDriver instance using the factory
            this.driver = browserFactory.CreateWebDriver();
            return this.driver;
        }

        public void Navigate(string url)
        {
            if (this.driver == null)
            {
                throw new InvalidOperationException("WebDriver is not initialized. Call Initialize() first.");
            }

            this.driver.Navigate().GoToUrl(url);

        }

        public void Cleanup()
        {
            // Close and quit WebDriver
            if (this.driver != null)
            {
                this.driver.Quit();
            }
        }
    }
}