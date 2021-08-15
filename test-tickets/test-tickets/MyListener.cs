using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace test_tickets
{
    public class MyListener : EventFiringWebDriver
    {
        /*     public MyListener(ChromeDriver chromeDriver)
                : base(chromeDriver)
             {
             } */


        public MyListener(IWebDriver parentDriver)
            : base(parentDriver)
        {
        }

   //     public ITakesScreenshot Driver { get; internal set; }

        /*  protected override void OnException(WebDriverExceptionEventArgs driver)
          {
              ITakesScreenshot screenshotDriver = driver.Driver as ITakesScreenshot;
              if (!ReferenceEquals(screenshotDriver, null))
              {
                  Screenshot screenshot = screenshotDriver.GetScreenshot();
                  screenshot.SaveAsFile(TestContext.CurrentContext.Test.Name + "_" + DateTime.Now.ToString("yyyy -MM-dd HH") + ".png", ScreenshotImageFormat.Png);
              }

          } */



    }
}