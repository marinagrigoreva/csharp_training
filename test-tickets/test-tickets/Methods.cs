using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_tickets
{
    public class Methods : TestBase
    {


        /// <summary>
        /// Выбрать Барнаул)
        /// </summary>
        public void SelectCity()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='geo-chooser__cities']/div/a[contains(.,'Барнаул')]")));
            driver.FindElement(By.XPath("//div[@class='geo-chooser__cities']/div/a[contains(.,'Барнаул')]")).Click();
        }

        /// <summary>
        /// Закрыть всплывающее окно
        /// </summary>
        public void ClosePopup()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@class='popup-notification__closer']")));
            driver.FindElement(By.XPath("//a[@class='popup-notification__closer']")).Click();
        }

        /// <summary>
        /// Перейти в расписание
        /// </summary>
        public void Schedule()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li/a[contains(.,'Расписание')]")));
            driver.FindElement(By.XPath("//li/a[contains(.,'Расписание')]")).Click();
        }

        /// <summary>
        /// Выбрать первый досупный сеанс
        /// </summary>
        public void SelectSession()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@class='diagram__sessions__item js-diagram__sessions__item']/div[1]")));
            driver.FindElement(By.XPath("//a[@class='diagram__sessions__item js-diagram__sessions__item']/div[1]")).Click();
        }

        /// <summary>
        /// Выбрать указанное количество мест и записать в коллекцию название фильма, информацию о сеансе и выбранные места
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
        public List<string> SelectSeat(int seat)
        {
            //дождаться появления элементов мест и положить их в коллекцию
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[contains(@class,'hall__place')]")));
            List<IWebElement> listPeriods = driver.FindElements(By.XPath("//div[contains(@class,'hall__place')]")).ToList();

            //дождаться появления надписи с названием фильма и с информацией о сеансе
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='ticket-box__title']")));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='ticket-box__info']")));

            List<string> seats = new();
            seats.Add(driver.FindElement(By.XPath("//div[@class='ticket-box__title']")).Text);//записать название фильма
            seats.Add(driver.FindElement(By.XPath("//div[@class='ticket-box__info']")).Text);//записать инфорамацию о сеансе
            
            int i = 0; int selected = 0;
            while (i < listPeriods.Count & selected < seat)
            {
                if (listPeriods.ElementAt(i).GetCssValue("background-color").Equals("rgba(0, 0, 0, 0)"))
                {
                    listPeriods.ElementAt(i).Click(); 
                    selected++;
                    
                    seats.Add(listPeriods.ElementAt(i).GetAttribute("title")); // записать выбранное место
                }
                i++;
            }
            

            return seats;
        }

        /// <summary>
        /// Выбрать завтра и дождаться когда надпись станет красной
        /// </summary>
        public void SelectTomorow()
        {
            driver.FindElement(By.XPath("//a[contains(.,'Завтра')]")).Click();
            wait.Until(d=>d.FindElement(By.XPath("//a[@class='diagram__sessions__item js-diagram__sessions__item']/div[1]")).GetCssValue("color")== "rgba(80, 122, 173, 1)");

        }


        /// <summary>
        /// Включить чек-бокс 
        /// </summary>
        public void CheckBoxOn()
        {
            ClickOnInvisibleElement(driver.FindElement(By.XPath("//label[@for=\"agreement\"]/input")));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div/label[@for=\"agreement\"][@class=\"checked\"]")));
        }

        /// <summary>
        /// Перейти к оплате
        /// </summary>
        public void CheckOut()
        {            
            driver.FindElement(By.XPath("//button[@value=\"pay\"]")).Click();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//input[@value=\"Войти\"]")));
        }

        /// <summary>
        /// Получить информацию о сеансе на странице логина
        /// </summary>
        /// <returns></returns>
        public string GetSessionsInfo()
        {
            string name = driver.FindElement(By.XPath("//div[@class=\"ticket-box__info\"]")).Text;
            return name;
        }

        /// <summary>
        /// Получить название фильма на странице логина
        /// </summary>
        /// <returns></returns>
        public string GetFilmName()
        {
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class=\"ticket-box__title\"]")));
            string name = driver.FindElement(By.XPath("//div[@class=\"ticket-box__title\"]")).Text;
            return name;
        }



        /// <summary>
        /// Возвращает коллекцию строк из двух мест, формат записи подогнан под тот что отображен при выборе мест
        /// </summary>
        /// <returns></returns>
        public List<string> GetResultSeats()
        {
            List<IWebElement> rows = driver.FindElements(By.XPath("//div/section[2]//div[@class=\"chosen-tickets__row\"]")).ToList();
            List<IWebElement> seat = driver.FindElements(By.XPath("//div/section[2]//div[@class=\"chosen-tickets__place\"]")).ToList();
            List<string> result = new();
            if (GetText(rows).Count.Equals(1))
            {
                string text = GetText(seat).ElementAt(0);
                string[] words = text.Split(new char[] { ',' });

                result.Add("р." + GetText(rows).ElementAt(0) + ", м." + words[0]);
                result.Add("р." + GetText(rows).ElementAt(0) + ", м." + words[1]);
            }
            else if (GetText(rows).Count.Equals(2))
            {
                string text = GetText(seat).ElementAt(0);
                string[] words = text.Split(new char[] { ',' });

                result.Add("р." + GetText(rows).ElementAt(0) + ", м." + GetText(seat).ElementAt(0));
                result.Add("р." + GetText(rows).ElementAt(1) + ", м." + GetText(seat).ElementAt(1));
            }
            return result;
        }


        /// <summary>
        /// Получить текст с рядом и местом на странице логина
        /// </summary>
        /// <param name="list"></param> Коллекция элементов указывающих на записи о рядах или местах
        /// <returns></returns>
        public List<string> GetText(List<IWebElement> list)
        {

            List<string> text = new();
            int count = list.Count;
            int i = 0;
            while (i < count)
            {
                text.Add(list.ElementAt(i).Text.Replace("места", "").Replace("место", "").Replace("ряд", "").Replace("D-Box", "").Replace(" ", "").Replace(":", ""));
                i++;
            }
            return text;
        }

    }
}
