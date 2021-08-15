using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace test_tickets
{
    public class TestBase
    {
     //   protected IWebDriver driver;
        protected string baseURL;
        protected WebDriverWait wait;        
        private string filename;
        private string saveLocation;
        protected MyListener driver;



        [SetUp]
        public void SetupTest()
        {
            driver = new MyListener(new ChromeDriver());
            //new ChromeDriver();
            driver.ExceptionThrown += (sender, e) => { Console.WriteLine(e.ThrownException); };
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            baseURL = "http://www.kino-mir.ru/";
            Logging(driver);
            Go(baseURL);
        }



        [TearDown]
        public void TeardownTest()
        {


            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                Snap(driver);
                Console.WriteLine("Check out why the test failed, look here: " + filename);
            }
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

        }

        /// <summary>
        /// Логирование в текстовый файл и на консоль
        ///  https://selenium.dev/selenium/docs/api/dotnet/html/T_OpenQA_Selenium_Support_Events_EventFiringWebDriver.htm  Events
        /// </summary>
        /// <param name="driver"></param>
        public static void Logging(MyListener driver)
        {

            driver.FindingElement += (sender, e) => { Console.WriteLine("Начат поиск элемента : " + e.FindMethod); WriteLine("Начат поиск элемента : " + e.FindMethod); };
            driver.FindElementCompleted += (sender, e) => { Console.WriteLine("Найден элемент : " + e.FindMethod); WriteLine("Найден элемент : " + e.FindMethod); };
            driver.ExceptionThrown += (sender, e) => { Console.WriteLine(e.ThrownException); WriteLine(e.ThrownException.ToString()); };
            driver.ElementValueChanging += (sender, e) => { Console.WriteLine("Начато изменение значения на: '" + e.Value + "'"); WriteLine("Начато изменение значения на: '" + e.Value + "'"); };
            driver.ElementValueChanged += (sender, e) => { Console.WriteLine("Изменено значение на: '" + e.Value + "'"); WriteLine("Изменено значение на: '" + e.Value + "'"); };
            driver.ElementClicked += (sender, e) => { Console.WriteLine("Произведен клик по элементу"); WriteLine("Произведен клик по элементу"); };
            driver.ElementClicking += (sender, e) => { Console.WriteLine("Перед кликом по элементу"); WriteLine("Перед кликом по элементу"); };
            driver.Navigating += (sender, e) => { Console.WriteLine("Начат переход по ссылке: " + e.Url); WriteLine("Начат переход по ссылке: " + e.Url); };
            driver.Navigated += (sender, e) => { Console.WriteLine("Осуществлен переход по ссылке: " + e.Url); WriteLine("Осуществлен переход по ссылке: " + e.Url); };
            driver.NavigatingBack += (sender, e) => { Console.WriteLine("Начат переход по ссылке назад: " + e.Url); WriteLine("Начат переход по ссылке назад: " + e.Url); };
            driver.NavigatingForward += (sender, e) => { Console.WriteLine("Осуществлен переход по ссылке назад: " + e.Url); WriteLine("Осуществлен переход по ссылке назад: " + e.Url); };
            driver.ScriptExecuting += (sender, e) => { Console.WriteLine("Начато выполнение скрипта: " + e.Script); WriteLine("Начато выполнение скрипта: " + e.Script); };
            driver.ScriptExecuted += (sender, e) => { Console.WriteLine("Выполнен скрипт: " + e.Script); WriteLine("Выполнен скрипт: " + e.Script); };

        }

        public static void WriteLine(string message)
        {
            string filename = "//logs/" + TestContext.CurrentContext.Test.Name + "_LOGS_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + filename, true))
            {
                sw.WriteLine(String.Format("{0,-23} {1}", DateTime.Now.ToString() + ":", message));
            }
        }

        public void ClickOnInvisibleElement(IWebElement el)
        {
            IWebElement element = el;

            String script = "var object = arguments[0];"
                    + "var theEvent = document.createEvent(\"MouseEvent\");"
                    + "theEvent.initMouseEvent(\"click\", true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);"
                    + "object.dispatchEvent(theEvent);"
                    ;

            ((IJavaScriptExecutor)driver).ExecuteScript(script, element);
        }


        public void Snap(IWebDriver driver)
        {
            try
            {
                CreateTempDirectory();
                CreateFilename();
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(filename, ScreenshotImageFormat.Png);
            }
            catch (Exception) { }
        }

        public void CreateTempDirectory()
        {
            try
            {
                saveLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//screens/";
                bool dirExists = System.IO.Directory.Exists(saveLocation);
                if (!dirExists)
                    System.IO.Directory.CreateDirectory(saveLocation);
            }
            catch (Exception) { }
        }

        public void CreateFilename()
        {
            try
            {
                var timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                filename = TestContext.CurrentContext.Test.FullName;
                string ext = ".png";
                filename = filename + timeStamp;
                filename = saveLocation + filename + ext;
            }
            catch (Exception) { }
        }

        public void Go(String baseURL)
        {
            driver.Navigate().GoToUrl(baseURL);
            
        }
    }
}
