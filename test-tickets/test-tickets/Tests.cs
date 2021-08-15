using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_tickets
{
    [TestFixture]
    [AllureNUnit]
    public class Tests : Methods
    {
        [Test(Author = "marina.grigoreva")]
        [Category("Tickets")]
        [Description("ticket selection test")]
        [AllureTag("Nunit", "Debug")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Core")]

        public void SelectTicketsTest()
        {
            SelectCity(); //Выбрать город
            ClosePopup(); //закрыть рекламу
            Schedule();  //перейти к расписанию
            ClosePopup(); //закрыть рекламу
            SelectTomorow();  //выбрать респисание на завтра
            SelectSession(); //выбрать первый из доступных сеансов
            List<string> seats = SelectSeat(2); //выбрать билеты и записать в коллецию название фильма, данные о сеансе и выбранные места
            CheckBoxOn(); //включить чек-бокс согласия с условиями
            CheckOut(); //нажать кнопку оплаты

            Assert.IsTrue(GetFilmName().Equals(seats.ElementAt(0))); //убедиться что название фильма на момент выбора билетов и на момент оплаты одинаковое
            Assert.IsTrue(GetSessionsInfo().Equals(seats.ElementAt(1))); //убедиться что данные о сеансе на момент выбора билетов и на момент оплаты одинаковое
            Assert.IsTrue(GetResultSeats().Contains(seats.ElementAt(2))); //убедиться что в коллекции с местами отображенными на странице оплаты, есть одно из выбранных мест
            Assert.IsTrue(GetResultSeats().Contains(seats.ElementAt(3))); //убедиться что в коллекции с местами отображенными на странице оплаты, есть второе из выбранных мест

        }

        
    }
}
