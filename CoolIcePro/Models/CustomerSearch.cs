using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolIcePro.Models
{
    public class CustomerSearch: IModel
    {
        Customer _customer;
        Invoice _invoice;
        public CustomerSearch(Customer customer, Invoice invoice)
        {
            _customer = customer;
            _invoice = invoice;
        }
        public CustomerSearch(Customer customer)
        {
            _customer = customer;
        }
        public long Id { get { return _customer.Id; } set { _customer.Id = value; } }
        public string CompanyName { get{return _customer.CompanyName;}}
        public string Address { get{ return _customer.Address;}}
        public string AddressExt { get{ return _customer.AddressExt;}}
        public string City { get{ return _customer.City; }}
        public string State { get{ return _customer.State;}}
        public string Zipcode { get{ return _customer.Zipcode; }}
        public string Telephone { get{return _customer.Telephone;}}
        // public string CellNumber { get; set; }
        public string Fax { get{ return _customer.Fax; }}
        public string Email { get{return _customer.Email;}}
        public string Website { get { return _customer.Website; } }
        public Contact Contact { get { return _customer.Contact; } }


        public DateTime Date { get { return _invoice.Date; } }
        public long InvoiceId { get { return _invoice.Id; } }
        public string InvoiceNumber { get { return _invoice.InvoiceNumber; } }
        public string ServicePerfomanceOn { get { return _invoice.ServicePerformanceOn; } }
        public string Description { get { return _invoice.Description; } }
        public double TotalAmount { get { return _invoice.TotalAmount; } }
        public bool Warranty { get { return _invoice.Warranty; } }
        public bool Check { get { return _invoice.Check; } }
    }
}
