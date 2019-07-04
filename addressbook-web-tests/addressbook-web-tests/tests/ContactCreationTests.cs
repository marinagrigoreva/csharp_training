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


        [Test]
        public void ContactCreationTest()
        {            
            ContactData contact = new ContactData("Firstname", "Lastname");

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
