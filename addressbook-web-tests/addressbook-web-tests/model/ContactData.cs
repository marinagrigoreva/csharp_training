using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allInfo;

        public ContactData()
        {

        }
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }


        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname && Lastname == other.Lastname ;

        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode()+Lastname.GetHashCode();

        }

        public override string ToString()
        {
            return "Firstname= " + Firstname + ", Lastname= " + Lastname ;

        }
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Lastname.CompareTo(other.Lastname) == 0)
            {
                return Firstname.CompareTo(other.Firstname);
            }
            else
            {
                return Lastname.CompareTo(other.Lastname);
            } 

        }

        /*       public int CompareTo(ContactData other)
               {
                   if (Object.ReferenceEquals(other, null))
                   {
                       return 1;
                   }
                   return Firstname.CompareTo(other.Firstname)+Lastname.CompareTo(other.Lastname);

               } */

        [Column(Name = "firstname")]
        public string Firstname {get; set;}

   //     [Column(Name = "middlename")]
        public string Middlename { get; set; }

        [Column(Name = "lastname")]
        public string Lastname {get; set;}

        [Column(Name = "id"), PrimaryKey, Identity] //уникальный ключ, идентификатор
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        //     [Column(Name = "nickname")]
        public string Nickname { get; set; }

   //     [Column(Name = "title")]
        public string Title { get; set; }

   //     [Column(Name = "company")]
        public string Company { get; set; }

  //      [Column(Name = "address")]
        public string Address { get; set; }

  //      [Column(Name = "home")]
        public string HomePhone { get; set; }

  //      [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

 //       [Column(Name = "work")]
        public string WorkPhone { get; set; }

  //      [Column(Name = "fax")]
        public string Fax { get; set; }

  //      [Column(Name = "email")]
        public string Email { get; set; }

  //      [Column(Name = "email2")]
        public string Email2 { get; set; }

    //    [Column(Name = "email3")]
        public string Email3 { get; set; }

      //  [Column(Name = "homepage")]
        public string Homepage { get; set; }




        private string Cleanup(string phone)
        {
            if (phone==null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") +"\r\n";
        }


        private string CheckNotNull(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone + "\r\n";
        }

        private string CheckHomePhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return "H: "+phone + "\r\n";
        }

        private string CheckWorkPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return "W: " + phone + "\r\n";
        }

        private string CheckMobilePhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return "M: " + phone + "\r\n";
        }

        private string CheckFaxPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return "F: " + phone + "\r\n";
        }

        private string CheckHomepage(string page)
        {
            if (page == null || page == "")
            {
                return "";
            }
            return "Homepage:\r\n" + Regex.Replace(page, "http://", "")+"\r\n";
        }

        private string CheckName(string name)
        {
            if (name == null || name == "")
            {
                return "";
            }
            return name + " ";
        }



        public string AllPhones {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return Cleanup(HomePhone) + Cleanup(MobilePhone) + Cleanup(WorkPhone).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }



        public string AllEmails {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (Cleanup(Email)  + Cleanup(Email2 ) + Cleanup(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }

        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            if (Source == null || Source == "")
            {
                return "";
            }
            else {
                int place = Source.LastIndexOf(Find);

                if (place == -1)
                {
                    return Source;
                }

                string result = Source.Remove(place, Find.Length).Insert(place, Replace);
                return result;

            }            
        }



        public string AllInfo
        {
            get
            {
               if(allInfo != null)
                {
                    return allInfo;
                }else
                {

                    string res= CheckNotNull(CheckName(Firstname) + CheckName(Middlename)
                        + CheckNotNull(Lastname) + CheckNotNull(Nickname) + CheckNotNull(Title) + CheckNotNull(Company) + CheckNotNull(Address))

                        + CheckNotNull(CheckHomePhone(HomePhone) + CheckMobilePhone(MobilePhone) + CheckWorkPhone(WorkPhone) + CheckFaxPhone(Fax))

                       + CheckNotNull(Email) + CheckNotNull(Email2) + CheckNotNull(Email3) + CheckHomepage(Homepage);

                    return ReplaceLastOccurrence(res, "\r\n","");

                }
            }
            set {
                allInfo =value;
            }
        }

        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from g in db.Contacts select g).ToList();
            }
        }


    }
}
