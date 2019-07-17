using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {

        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable =app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verifications

            Assert.AreEqual(fromTable,fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }


        [Test]
        public void TestContactInformationEditFormAndDetails()
        {
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verifications
            Console.WriteLine(fromDetails.AllInfo);
            Console.WriteLine(fromForm.AllInfo);
            Console.WriteLine("111");
            Assert.AreEqual(fromDetails.AllInfo, fromForm.AllInfo);
 //           Assert.AreEqual(fromTable.Address, fromForm.Address);
//            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
 //           Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }


    }
}
