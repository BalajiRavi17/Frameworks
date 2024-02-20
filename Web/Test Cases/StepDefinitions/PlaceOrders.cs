using log4net;
using NUnit.Framework;
using WebAutomation.Hooks;
using WebAutomation.Support.Reporting;
using WebAutomation.SwagLabs.PageObjects;
using WebAutomations.SwagLabs.PageObjects;

namespace WebAutomation.SwagLabs.StepDefinitions
{
    [Binding]
    public class PlaceOrders : Hook
    {
        private ScenarioContext _scenarioContext;
        ProductsPageObjects productsPageObjects;
        LogGenerator logGenerator;
        ILog Logger;

        public PlaceOrders(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            logGenerator = LogGenerator.getInstance();
            Logger =logGenerator.GenerateLogs(typeof(PlaceOrders));
        }

        [Given(@"Login https://www\.saucedemo\.com/")]
        public void GivenLoginHttpsWww_Saucedemo_Com()
        {
            try
            {
                productsPageObjects = new ProductsPageObjects(driver);
                Logger.Info("Successfully Initialized");
            }
            catch (Exception ex)
            {
                Logger.Error("Filed to initialize due to error " + ex);
            }
    }


        [When(@"Select any item and note the price from the inventroy page and add it to cart")]
        public void WhenSelectAnyItemAndNoteThePriceFromTheInventroyPageAndAddItToCart()
        {
            try { 
            _scenarioContext.Add("ExpectedPrice",productsPageObjects.addAItemToCart());
                Logger.Info("Added first item to cart and noted price of that item");
            }
            catch (Exception ex)
            {
                Logger.Error("Filed to add item to cart due to error " + ex);
            }
        }

        [When(@"Navigate to cart page and verify same price as above noted displayed.")]
        public void WhenNavigateToCartPageAndVerifySamePriceAsAboveNotedDisplayed_()
        {
            try { 
            productsPageObjects.NaviagteToCart();
                Logger.Info("Navigated to cart");
            }
            catch (Exception ex)
            {
                Logger.Error("Filed to Naviagte due to error " + ex);
            }
        }

        [When(@"Click on checkout and enter the sample details and click continue")]
        public void WhenClickOnCheckoutAndEnterTheSampleDetailsAndClickContinue()
        {
            try { 
            productsPageObjects.fillDetails();
                Logger.Info("Checkout and filled required details");
            }
            catch (Exception ex)
            {
                Logger.Error("Filed to Fill details due to error " + ex);
            }
        }

        [Then(@"Verify the Item and Price on chekout page and click finish\.")]
        public void ThenVerifyTheItemAndPriceOnChekoutPageAndClickFinish_()
        {
            try { 
            _scenarioContext.Add("ActualPrice",productsPageObjects.GetItemPrice());
            if(!_scenarioContext.Get<string>("ExpectedPrice").Equals(_scenarioContext.Get<string>("ActualPrice")))
            {
                Assert.Fail();
            }
                Logger.Info("Successfully verifed the item proce");
            }
            catch (Exception ex)
            {
                Logger.Error("Filed to verify due to error " + ex);
            }
        }
    }
}
