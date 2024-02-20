using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.SwagLabs.PageObjects
{
    public class ProductsPageObjects
    {
        IWebDriver driver;
        public ProductsPageObjects(IWebDriver driver) { 
            this.driver = driver;
        
        }
        IEnumerable<IWebElement> HomePageProdsLinks => driver.FindElements(By.XPath("//div[@class='inventory_item_description']//button"));
        IEnumerable<IWebElement> HomePageProdsPrice => driver.FindElements(By.XPath("//div[@class='inventory_item_price']"));

        IWebElement CartButton => driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));
        IWebElement CheckOutButton => driver.FindElement(By.XPath("//button[@class='btn btn_action btn_medium checkout_button ']"));

        IWebElement firsNameText => driver.FindElement(By.Id("first-name"));
        IWebElement lastNameText => driver.FindElement(By.Id("last-name"));
        IWebElement PostalCodeText => driver.FindElement(By.Id("postal-code"));
        IWebElement continueButton => driver.FindElement(By.Id("continue"));
        IWebElement finishButton => driver.FindElement(By.Id("finish"));
        public string addAItemToCart()
        {
            HomePageProdsLinks.First().Click();
            return HomePageProdsPrice.First().Text;
        }
        public void NaviagteToCart() 
        {
            CartButton.Click();
        }

        public void fillDetails()
        {
            Random random = new Random();
            CheckOutButton.Click(); 
            firsNameText.SendKeys(random.Next().ToString());
            lastNameText.SendKeys(random.Next().ToString());
            PostalCodeText.SendKeys(random.Next().ToString());
            continueButton.Click();
        }

        public string GetItemPrice()
        {
            return HomePageProdsPrice.First().Text;
        }
    }
}
