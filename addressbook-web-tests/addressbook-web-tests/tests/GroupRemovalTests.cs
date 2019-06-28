using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            GroupData oldData = new GroupData("asd");
            oldData.Header = "asd";
            oldData.Footer = "dfg";

            app.Groups.IfGroupNotPresent(oldData);
            app.Groups.Remove(1);            
        }
    }
}
