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
            ContactData oldData = new ContactData("Test123");
            oldData.Lastname = "Test123";
            ContactData newData = new ContactData("Test");
            newData.Lastname = "Test";
//            newData.Photo = "C:\\img\\1.png";

            app.Contacts.IfContactNotPresent(oldData);

            app.Contacts.Modify(1, newData);




            
            
        }
    }
}
