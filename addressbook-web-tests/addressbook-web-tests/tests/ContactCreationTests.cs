using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

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



        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {            
       //     ContactData contact = new ContactData("Firstname", "Lastname");

            List<ContactData> oldContacts = app.Contacts.GetContactDataList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactDataList();

            oldContacts.Add(contact); 
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
