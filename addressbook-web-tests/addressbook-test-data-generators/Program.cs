using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string filename = args[1];
            string format = args[2];
            string dataType = args[3];
            dynamic objectData = null;
            if (dataType == "groups")
            {
                objectData = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    objectData.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)

                    });
                }

            }
            else if (dataType == "contacts")
            {

                objectData = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    objectData.Add(new ContactData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10))
                    {
                      //  Firstname = TestBase.GenerateRandomString(10),
                      //  Lastname = TestBase.GenerateRandomString(10)
                        //Address = TestBase.GenerateRandomString(10)

                    });
                }

            }
            
            if (format == "excel")
            {
                writeGroupsToExcelFile(objectData, filename);

            }
            else {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeGroupsToCsvFile(objectData, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(objectData, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(objectData, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format" + format);
                }

                writer.Close();
            }

                   
        }

        static void writeGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            //    sheet.Cells[1, 1] = "test";
            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name,
                    group.Header,
                    group.Footer));
            }
        }

        static void writeGroupsToXmlFile<Data>(List<Data> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<Data>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile<Data>(List<Data> objectData, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(objectData, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
