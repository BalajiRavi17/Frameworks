using OpenQA.Selenium;

namespace WebAutomation.Support.Reporting
{
    public class CaptureScreenshots
    {
        public static string screenshotCpature(IWebDriver driver)
        {
            ITakesScreenshot tcakesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = tcakesScreenshot.GetScreenshot();
            return screenshot.AsBase64EncodedString;

        }
    }
}
