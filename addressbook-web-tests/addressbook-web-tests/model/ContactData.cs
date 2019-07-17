using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allInfo;


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

        public string Firstname {get; set;}

        public string Middlename { get; set; }

        public string Lastname {get; set;}

        public string Nickname { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string Homepage { get; set; }




        private string Cleanup(string phone)
        {
            if (phone==null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") +"\r\n";
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





        public string AllInfo
        {
            get
            {
               if(allInfo != null)
                {
                    return allInfo;
                }else
                {
                    return Cleanup(Firstname) + Cleanup(Middlename)
                        + Cleanup(Lastname) + Cleanup(Nickname) + Cleanup(Title) + Cleanup(Company) + Cleanup(Address)

                        + Cleanup(HomePhone) + Cleanup(MobilePhone) + Cleanup(WorkPhone) + Cleanup(Fax)

                       + Cleanup(Email) + Cleanup(Email2) + Cleanup(Email3) + Regex.Replace(Cleanup(Homepage), "http://", "");

                }
            }
            set {
                allInfo = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(value, "Homepage:", ""), "F: ", ""), "W: ", ""), "M: ", ""), "H: ", ""), " ", "\r\n"), "\r\n\r\n", "\r\n")+ "\r\n";
            }
        }




    }
}
