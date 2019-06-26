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
            ContactData oldData = new ContactData("Test123");
            oldData.Lastname = "Test123";
            app.Contacts.Remove(1, oldData);
        }
    }
}
