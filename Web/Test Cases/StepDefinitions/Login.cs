using log4net;
using NUnit.Framework;
using WebAutomation.Hooks;
using WebAutomation.Support.Reporting;
using WebAutomations.SwagLabs.PageObjects;

namespace WebAutomation.SwagLabs.StepDefinitions
{
    [Binding]
    public sealed class Login:Hook
    {
        private ScenarioContext _scenarioContext;
        LoginPageObjects loginPageObjects;
        LogGenerator logGenerator;
        ILog Logger;

        public Login(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            logGenerator= LogGenerator.getInstance();
            Logger = logGenerator.GenerateLogs(typeof(Login));
            loginPageObjects = LoginPageObjects.getInstance();
        }

        [Given(@"User is in Swag Labs login page")]
        public void GivenUserIsInSwagLabsLoginPage()
        {
            try
            {
                Logger.Info("Successfully Initialized the drivers");

            }catch (Exception ex)
            {
            Logger.Fatal("Failed to Initialize the drivers "+ex.StackTrace);
            }
        }

        [When(@"I enter valid username as ""([^""]*)""")]
        public void WhenIEnterValidUsernameAs(string uname)
        {
            _scenarioContext.Add("username", uname);
        }

        [When(@"valid password as ""([^""]*)""")]
        [Obsolete]
        public void WhenValidPasswordAs(string pwd)
        {
            _scenarioContext.Add("Password", pwd);
        }

        [When(@"when I clicked Login button")]
        public void WhenWhenIClickedLoginButton()
        {
            try { 
            string uname = _scenarioContext.Get<string>("username");
            string pwd = _scenarioContext.Get<string>("Password");
            loginPageObjects.login(uname, pwd);
                Logger.Info("Successfully logged In");
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to loging with Error" + ex.StackTrace);
            }
        }

        [Then(@"user should be navigated to home screen")]
        public void ThenUserShouldBeNavigatedToHomeScreen()
        {
                if (!loginPageObjects.verifyLoggedIn())
                {
                    Assert.Fail();
                }
            }
        [Then(@"User will click on Logout link to log out")]
        public void ThenUserWillClickOnLogoutLinkToLogOut()
        {
            loginPageObjects.logout();
        }

        [Then(@"user should be not navigated to home screen")]
        public void ThenUserShouldBeNotNavigatedToHomeScreen()
        {
            if (loginPageObjects.verifyLoggedIn())
            {
                Assert.Fail();
            }
        }

    }


}

