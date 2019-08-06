using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace WebAddressbookTests

{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase

    {


        public static IEnumerable<ContactData> RandomGroupDataProvider()
        {
            List<ContactData> contact = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contact.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Middlename = GenerateRandomString(20),
                    Nickname = GenerateRandomString(20),
                    Title = GenerateRandomString(100),
                    Company = GenerateRandomString(30),
                    Address= GenerateRandomString(100),
                    HomePhone= GenerateRandomString(10),
                    WorkPhone= GenerateRandomString(10),
                    MobilePhone= GenerateRandomString(10),
                    Fax= GenerateRandomString(10),
                    Email= GenerateRandomString(30),
                    Email2 = GenerateRandomString(30),
                    Email3 = GenerateRandomString(30),
                    Homepage = GenerateRandomString(50)
                });
            }
            return contact;
        }


        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));

        }



        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {            
       //     ContactData contact = new ContactData("Firstname", "Lastname");

            List<ContactData> oldContacts = app.Contacts.CleanRemovedContacts(ContactData.GetAll());

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.CleanRemovedContacts(ContactData.GetAll());

            oldContacts.Add(contact); 
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }



        [Test]

        public void TestDBConnectivityContact()
        {
            DateTime start = DateTime.Now;
            List<ContactData> fromUi = app.Contacts.GetContactDataList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<ContactData> fromDb = ContactData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }
    }
}
