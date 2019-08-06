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

            List<ContactData> oldContacts = app.Contacts.CleanRemovedContacts(ContactData.GetAll());
            ContactData toBeMod = oldContacts[0];

            app.Contacts.Modify(toBeMod, newData);

            List<ContactData> newContacts = app.Contacts.CleanRemovedContacts(ContactData.GetAll());

            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            //перенесено по аналогии из групп, не особо тут нужно
            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeMod.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                }
            }





        }
    }
}
