using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Threading;

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
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {            
            SelectContact(v);
            InitContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }


        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            RemoveContact();
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(3000);            
            manager.Navigator.GoToHomePage();
          
            return this;
        }

        private List<ContactData> contactCache = null;
        public List<ContactData> GetContactDataList()
        {
            if (contactCache == null)
            {
                contactCache= new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tbody/tr[@name='entry']"));
                for (int k = 0; k < elements.Count; k++)
                {
                    string firstname = elements.ElementAt(k).FindElement(By.XPath("./td[not(@class)][2]")).Text;
                    string lastname = elements.ElementAt(k).FindElement(By.XPath("./td[not(@class)][1]")).Text;

                    contactCache.Add(new ContactData(firstname, lastname));
                    Debug.WriteLine(contactCache[k].Firstname);
                    Debug.WriteLine(contactCache[k].Lastname);
                }
            }          
            return contactCache;
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
            contactCache = null;
            return this;
        }

        private ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry'][" + (v + 1) + "]//img[@title='Edit']")).Click();
            return this;
        }


        public ContactHelper RemoveContact() 
        {

            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry']["+(v+1)+"]//input[@name='selected[]']")).Click();
            return this;
        }



        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
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
