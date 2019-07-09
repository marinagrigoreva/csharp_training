using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests 
{
    public class ContactModificationTests : AuthTestBase
    {

        [Test]
        public void ContactModificationTest()
        {
            ContactData oldData = new ContactData("Firstname", "Lastname");
            ContactData newData = new ContactData("NewFirstname", "NewLastname");

            app.Contacts.IfContactNotPresent(oldData);

            List<ContactData> oldContacts = app.Contacts.GetContactDataList();

            app.Contacts.Modify(0, newData);

            List<ContactData> newContacts = app.Contacts.GetContactDataList();

            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);





        }
    }
}
