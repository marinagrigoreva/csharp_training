using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.tests
{

    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("new group");
            newData.Header = null;
            newData.Footer = null;

            GroupData oldData = new GroupData("asd");
            oldData.Header = "asd";
            oldData.Footer = "dfg";

            app.Groups.IfGroupNotPresent(oldData);

            app.Groups.Modify(0, newData);

        }




    }
}
