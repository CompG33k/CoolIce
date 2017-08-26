using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolIcePro.Models
{
    public class InvoiceSearch : IModel
    {
        Customer _customer = new Customer();
        Invoice _invoice = new Invoice();

        public InvoiceSearch() { }

        //public InvoiceSearch(Customer customer, Invoice invoice)
        //{
        //    _customer = customer;
        //    _invoice = invoice;
        //}

        //public CustomerSearch(Customer customer)
        //{
        //    _customer = customer;
        //}

        public long Id { get { return _invoice.Id; } set { _invoice.Id = value; } }
        public string CompanyName { get { return _customer.CompanyName; } set { _customer.CompanyName = value; } }
        public long CompanyId { get { return _customer.Id; } set { _customer.Id = value; } }
        //public string Address { get{ return _customer.Address;}}
        //public string AddressExt { get{ return _customer.AddressExt;}}
        //public string City { get{ return _customer.City; }}
        //public string State { get{ return _customer.State;}}
        //public string Zipcode { get{ return _customer.Zipcode; }}
        //public string Telephone { get{return _customer.Telephone;}}
        // public string CellNumber { get; set; }
        //public string Fax { get{ return _customer.Fax; }}
        //public string Email { get{return _customer.Email;}}
        //public string Website { get { return _customer.Website; } }
        //public Contact Contact { get { return _customer.Contact; } }
        public string ContactFullName { get { return _customer.Contact.FirstName+ " " + _customer.Contact.LastName; } }
        public string ContactFName { get { return _customer.Contact.FirstName; } set { _customer.Contact.FirstName = value; } }
        public string ContactLName { get { return _customer.Contact.LastName; } set { _customer.Contact.LastName = value; } }
        public DateTime Date { get { return _invoice.Date; } set { _invoice.Date = value; } }

        public string InvoiceNumber { get { return _invoice.InvoiceNumber; } set { _invoice.InvoiceNumber = value; } }
        public string ServicePerformanceOn { get { return _invoice.ServicePerformanceOn; } set { _invoice.ServicePerformanceOn = value; } }
        public string Description { get { return _invoice.Description; } set { _invoice.Description = value; } }
        public double TotalAmount { get { return _invoice.TotalAmount; } set { _invoice.TotalAmount = value; } }
        public bool Warranty { get { return _invoice.Warranty; } set { _invoice.Warranty = value; } }
        //public bool Check { get { return _invoice.Check; } }
    }
}
