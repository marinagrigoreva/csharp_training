using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase

        
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {


        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.GoToHomePage();
            //     ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData, ContactData oldData)
        {
            IfContactNotPresent(oldData);
            SelectContact(v);
            InitContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            //      ReturnToHomePage();
            return this;
        }


        public ContactHelper Remove(int v, ContactData oldData)
        {
            IfContactNotPresent(oldData);
            SelectContact(v);
            RemoveContact();
            driver.SwitchTo().Alert().Accept();
            manager.Navigator.GoToHomePage();
     //       ReturnToHomePage();            
            return this;
        }


        public ContactHelper IfContactNotPresent(ContactData oldData)
        {
            if (CheckPresentContact()==false)
            {
                Create(oldData);
            }
            return this;
        }

        public bool CheckPresentContact()
        {
            return IsElementPresent(By.XPath("//tr[@name='entry'][1]//input[@name='selected[]']"));

        }




        private ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        private ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        private ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry'][" + v + "]//img[@title='Edit']")).Click();
            return this;
        }


        public ContactHelper RemoveContact() 
        {

            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry']["+v+"]//input[@name='selected[]']")).Click();
            return this;
        }



        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return this;
        }


        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }



        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
    }
}
