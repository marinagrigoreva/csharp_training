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
        /// ����������� � ��������� ���� � �� �������
        ///  https://selenium.dev/selenium/docs/api/dotnet/html/T_OpenQA_Selenium_Support_Events_EventFiringWebDriver.htm  Events
        /// </summary>
        /// <param name="driver"></param>
        public static void Logging(MyListener driver)
        {

            driver.FindingElement += (sender, e) => { Console.WriteLine("����� ����� �������� : " + e.FindMethod); WriteLine("����� ����� �������� : " + e.FindMethod); };
            driver.FindElementCompleted += (sender, e) => { Console.WriteLine("������ ������� : " + e.FindMethod); WriteLine("������ ������� : " + e.FindMethod); };
            driver.ExceptionThrown += (sender, e) => { Console.WriteLine(e.ThrownException); WriteLine(e.ThrownException.ToString()); };
            driver.ElementValueChanging += (sender, e) => { Console.WriteLine("������ ��������� �������� ��: '" + e.Value + "'"); WriteLine("������ ��������� �������� ��: '" + e.Value + "'"); };
            driver.ElementValueChanged += (sender, e) => { Console.WriteLine("�������� �������� ��: '" + e.Value + "'"); WriteLine("�������� �������� ��: '" + e.Value + "'"); };
            driver.ElementClicked += (sender, e) => { Console.WriteLine("���������� ���� �� ��������"); WriteLine("���������� ���� �� ��������"); };
            driver.ElementClicking += (sender, e) => { Console.WriteLine("����� ������ �� ��������"); WriteLine("����� ������ �� ��������"); };
            driver.Navigating += (sender, e) => { Console.WriteLine("����� ������� �� ������: " + e.Url); WriteLine("����� ������� �� ������: " + e.Url); };
            driver.Navigated += (sender, e) => { Console.WriteLine("����������� ������� �� ������: " + e.Url); WriteLine("����������� ������� �� ������: " + e.Url); };
            driver.NavigatingBack += (sender, e) => { Console.WriteLine("����� ������� �� ������ �����: " + e.Url); WriteLine("����� ������� �� ������ �����: " + e.Url); };
            driver.NavigatingForward += (sender, e) => { Console.WriteLine("����������� ������� �� ������ �����: " + e.Url); WriteLine("����������� ������� �� ������ �����: " + e.Url); };
            driver.ScriptExecuting += (sender, e) => { Console.WriteLine("������ ���������� �������: " + e.Script); WriteLine("������ ���������� �������: " + e.Script); };
            driver.ScriptExecuted += (sender, e) => { Console.WriteLine("�������� ������: " + e.Script); WriteLine("�������� ������: " + e.Script); };

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
