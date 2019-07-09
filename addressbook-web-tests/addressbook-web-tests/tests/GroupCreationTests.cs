using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests

{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase

    {


        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("aaa");
            group.Header = "ddd";
            group.Footer = "fff";

            List<GroupData> oldGrops = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGrops.Count + 1, app.GetGroupCount());

            List<GroupData> newGrops = app.Groups.GetGroupList();
            oldGrops.Add(group);
            oldGrops.Sort();
            newGrops.Sort();
            Assert.AreEqual(oldGrops, newGrops);   
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGrops = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGrops.Count + 1, app.GetGroupCount());

            List<GroupData> newGrops = app.Groups.GetGroupList();
            oldGrops.Add(group);
            oldGrops.Sort();
            newGrops.Sort();
            Assert.AreEqual(oldGrops, newGrops);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGrops = app.Groups.GetGroupList();

            app.Groups.Create(group);

            List<GroupData> newGrops = app.Groups.GetGroupList();
            oldGrops.Add(group);
            oldGrops.Sort();
            newGrops.Sort();
            Assert.AreEqual(oldGrops, newGrops);
        }

    }
}
