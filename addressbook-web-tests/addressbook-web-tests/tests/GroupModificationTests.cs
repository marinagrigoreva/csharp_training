using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.tests
{

    [TestFixture]
    public class GroupModificationTests : GroupTestBase
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

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeMod = oldGroups[0];
      //      GroupData oldData1 = oldGroups[0];

            app.Groups.Modify(toBeMod, newData);

            Assert.AreEqual(oldGroups.Count, app.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeMod.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }

        }




    }
}
