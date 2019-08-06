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
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase

        
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {


        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails,
            //    AllInfo = allInfo
            };
        }



        public ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactViewDetails(index);
            String text = driver.FindElement(By.Id("content")).Text;
            Debug.WriteLine(text);
            return new ContactData("","")
            {
                AllInfo = text                
            };

            throw new NotImplementedException();
        }

        private ContactHelper InitContactViewDetails(int v)
        {
            driver.FindElement(By.XPath("//tr[@name='entry'][" + (v + 1) + "]//img[@title='Details']")).Click();
            return this;
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value"); 

            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value"); 

            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");

            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value"); 
            string title = driver.FindElement(By.Name("title")).GetAttribute("value"); 
            string company = driver.FindElement(By.Name("company")).GetAttribute("value"); 

            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value"); 

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value"); 

            return new ContactData(firstname, lastname)
            {
                Middlename = middlename,
                Nickname = nickname,
                Title = title,
                Company=company,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Fax = fax,
                Email=email,
                Email2=email2,
                Email3=email3,
                Homepage = homepage,
            };            
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

        public ContactHelper Modify(ContactData toBeMod, ContactData newData)
        {
     //       SelectContact(toBeMod);
            InitContactModification(toBeMod.Id);
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

        internal ContactHelper Remove(ContactData toBeRemoved)
        {
            SelectContact(toBeRemoved.Id);
            RemoveContact();
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(3000);
            manager.Navigator.GoToHomePage();

            return this;
        }



        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath("//input[@name='selected[]' and @value='" + id + "']")).Click();
            return this;
        }

        public List<ContactData> CleanRemovedContacts(List<ContactData> oldContacts)
        {
            int i = 0;
            List<ContactData> oldContactsClean = new List<ContactData>();
            while (i < oldContacts.Count)
            {
                if (oldContacts[i].Deprecated == "00.00.0000 0:00:00")
                {
                    oldContactsClean.Add(oldContacts[i]);
                    Debug.WriteLine(oldContacts[i].Deprecated);
                }
                i++;
            }
            return oldContactsClean;
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

        private ContactHelper InitContactModification(string toBeMod)
        {
            driver.FindElement(By.XPath("//input[contains(@id,'"+toBeMod+"')]/../..//img[@title='Edit']")).Click();
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

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
