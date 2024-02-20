using OpenQA.Selenium;
using WebAutomation.Hooks;

namespace WebAutomations.SwagLabs.PageObjects
{
    public class LoginPageObjects : Hook
    {

        private static LoginPageObjects loginPageObjectsInstance;
        private static readonly object _lock = new object();
        private LoginPageObjects()
        {
            
        }

        public static LoginPageObjects getInstance()
        {
            if (loginPageObjectsInstance == null)
            {
                lock (_lock)
                {
                    if (loginPageObjectsInstance == null)
                    {
                        loginPageObjectsInstance = new LoginPageObjects();
                    }
                }
            }
                return loginPageObjectsInstance;
            
        }
        IWebElement userNameText => driver.FindElement(By.Id("user-name"));

         IWebElement passwordText => driver.FindElement(By.Id("password"));

         IWebElement loginButton => driver.FindElement(By.Id("login-button"));

        IWebElement BurgerMenuButtonIcon => driver.FindElement(By.Id("react-burger-menu-btn"));

        IWebElement LogoutButton => driver.FindElement(By.Id("logout_sidebar_link"));

        IEnumerable <IWebElement> HomePageProdsLinks => driver.FindElements(By.XPath("//div[@class='inventory_item_description']//button"));

        public void login( string username, string password)
        {
            userNameText.Clear();
            userNameText.SendKeys(username);
            passwordText.Clear();
            passwordText.SendKeys(password);
            loginButton.Click();
        }

        public bool verifyLoggedIn()
        {
            if (HomePageProdsLinks.Count() >0)
            {
                return true;
            }
            return false;
        }

        public void logout()
        {
            BurgerMenuButtonIcon.Click();
            LogoutButton.Click();
        }

    }
}
