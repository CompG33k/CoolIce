using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolIcePro.Models
{
    /// <summary>
    /// This is the Customer class for data.
    /// </summary>
    public class Customer: IModel
    {
        public Customer() { Contact = new Contact(); }
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string AddressExt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Telephone { get; set; }
       // public string CellNumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public Contact Contact { get; set; }
    }
}
