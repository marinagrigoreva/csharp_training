using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;

namespace WebAddressbookTests
{
    class ContactRemovalTests : GroupTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            ContactData oldData = new ContactData("Firstname", "Lastname");
 //           oldData.Lastname = "Test123";

            app.Contacts.IfContactNotPresent(oldData);

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];


            app.Contacts.Remove(toBeRemoved);

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            //неизвестно нужно ли это. перенесено из GroupRemovalTest
            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }



        }


    }
}
