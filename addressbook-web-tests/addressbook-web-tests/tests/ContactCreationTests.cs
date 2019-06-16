using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests

{
    [TestFixture]
    public class ContactCreationTests : TestBase

    {


        [Test]
        public void ContactCreationTest()
        {            
            ContactData contact = new ContactData("New");
            contact.Lastname = "Contact";
//            contact.Photo = "C:\\img\\1.png";
            app.Contacts.Create(contact);
        }
    }
}
