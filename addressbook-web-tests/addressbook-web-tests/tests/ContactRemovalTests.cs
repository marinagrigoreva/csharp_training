using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    class ContactRemovalTests : AuthTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            ContactData oldData = new ContactData("Firstname", "Lastname");
 //           oldData.Lastname = "Test123";

            app.Contacts.IfContactNotPresent(oldData);

            List<ContactData> oldContacts = app.Contacts.GetContactDataList();

            app.Contacts.Remove(0);

            List<ContactData> newContacts = app.Contacts.GetContactDataList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            

        }
    }
}
