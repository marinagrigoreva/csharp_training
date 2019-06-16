using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests 
{
    public class ContactModificationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {

            ContactData newData = new ContactData("Test");
            newData.Lastname = "Test";
//            newData.Photo = "C:\\img\\1.png";
            app.Contacts.Modify(1, newData);




            
            
        }
    }
}
