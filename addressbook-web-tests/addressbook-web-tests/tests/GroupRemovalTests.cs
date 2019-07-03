using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

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

            List<GroupData> oldGrops = app.Groups.GetGroupList();

            app.Groups.IfGroupNotPresent(oldData);
            app.Groups.Remove(0);

            List<GroupData> newGrops = app.Groups.GetGroupList();

            oldGrops.RemoveAt(0); 
            Assert.AreEqual(oldGrops, newGrops);
        }
    }
}
