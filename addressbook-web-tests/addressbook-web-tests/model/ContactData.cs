using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;

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

        public string Lastname {get; set;}

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }


        private string Cleanup(string phone)
        {
            if (phone==null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-","").Replace("(","").Replace(")", "")+"\r\n";
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
                    return (Email + "\r\n" + Email2 + "\r\n" + Email3 + "\r\n").Trim();
                }
            }
            set
            {
                allEmails = value;
            }

        }




    }
}
