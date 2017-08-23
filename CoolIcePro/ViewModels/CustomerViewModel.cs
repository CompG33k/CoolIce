using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoolIcePro.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        long id;
        string companyName;
        string address;
        string addressExt;
        string city;
        string state;
        string zipcode;
        string telephone;
        string cellNumber;
        string fax;
        string email;
        string website;

        string contactFname;
        string contactLname;
        string contactTelephone;
        string contactCellphone;
        string contactPosition;
        private bool canExecute = true;

        
        private ICommand insertButtonClicked;
        private ICommand rowDoubleClickCommand;
        private ICommand invoiceMenuItemClickCommand;
        
        IEnumerable<CoolIcePro.Models.Contact> contacts;
        IEnumerable<string> states;
        IEnumerable<Models.Invoice> invoices;


        public long Id
        {
            get { return id; }
            
            set{
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                if (companyName != value)
                {
                    companyName = value;
                    OnPropertyChanged("CompanyName");
                }
            }
        }
        public string Address {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }
        public string AddressExt{ 
            get { return addressExt; }
            set
            {
                if (addressExt != value)
                {
                    addressExt = value;
                    OnPropertyChanged("AddressExt");
                }
            }
        }
        public string City { 
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }
        public string State{ 
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    OnPropertyChanged("State");
                }
            }
        }
        public string Zipcode{ 
            get { return zipcode; }
            set
            {
                if (zipcode != value)
                {
                    zipcode = value;
                    OnPropertyChanged("Zipcode");
                }
            }
        }
        public string Telephone{ 
            get { return telephone; }
            set
            {
                if (telephone != value)
                {
                    telephone = value;
                    OnPropertyChanged("Telephone");
                }
            }
        }
        public string CellNumber{ 
            get { return cellNumber; }
            set
            {
                if (cellNumber != value)
                {
                    cellNumber = value;
                    OnPropertyChanged("CellNumber");
                }
            }
        }
        public string Fax{ 
            get { return fax; }
            set
            {
                if (fax != value)
                {
                    fax = value;
                    OnPropertyChanged("Fax");
                }
            }
        }
        public string Email{ 
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string Website{ 
            get { return website; }
            set
            {
                if (website != value)
                {

                    website = value;
                    OnPropertyChanged("Website");
                }
            }
        }

        public string ContactFname
        {
            get { return contactFname; }
            set
            {
                if (contactFname != value)
                {
                    contactFname = value;
                    OnPropertyChanged("ContactFname");
                }
            }
        }
        public string ContactLname 
        {
            get { return contactLname; }
            set
            {
                if (contactLname != value)
                {
                    contactLname = value;
                    OnPropertyChanged("ContactLname");
                }
            }
        }
        public string ContactTelephone 
        {
            get { return contactTelephone; }
            set
            {
                if (contactTelephone != value)
                {
                    contactTelephone = value;
                    OnPropertyChanged("ContactTelephone");
                }
            }
        }
        public string ContactCellphone 
        {
            get { return contactCellphone; }
            set
            {
                if (contactCellphone != value)
                {
                    contactCellphone = value;
                    OnPropertyChanged("ContactCellphone");
                }
            }
        }
        public string ContactPosition 
        {
            get { return contactPosition; }
            set
            {
                if (contactPosition != value)
                {
                    contactPosition = value;
                    OnPropertyChanged("ContactPosition");
                }
            }
        }

        public IEnumerable<CoolIcePro.Models.Contact> Contacts
        { 
            get { return contacts; }
            set
            {
                if (contacts != value)
                {
                    contacts = value;
                    OnPropertyChanged("Contacts");
                }
            }
        }

        public IEnumerable<string> States
        {
            get
            {
                if (states == null)
                {
                    states = ProjectManager.Instance.States;
                }
                return states;
            }
        }

        public IEnumerable<Models.Invoice> Invoices
        {
            get { return invoices; }
            set
            {
                if (invoices != value)
                {
                    invoices = value;
                    OnPropertyChanged("Invoices");
                }
            }
        }

        public CustomerViewModel(CoolIcePro.Models.Customer customer)
        {
            Id = customer.Id;
            invoices = ProjectManager.Instance.CoolIceProDBHelper.GetCustomerInvoices(Id);
           
            CompanyName = customer.CompanyName;
            Address = customer.Address;
            AddressExt = customer.AddressExt;
            City = customer.City;
            State = customer.State;
            Zipcode = customer.Zipcode;
            Telephone = customer.Telephone;
            Fax = customer.Fax;
            Email = customer.Email;
            Website = customer.Website; 
            
            if (customer.Contact == null)
                return;
            var contact  = customer.Contact;
            ContactFname = contact.FirstName;
            ContactLname = contact.LastName;
            ContactTelephone = contact.Telephone;
            ContactCellphone = contact.Cellphone;
            ContactPosition = contact.Position;
           
        }
     
        public ICommand RowDoubleClickCommand
        {
            get
            {
                if (rowDoubleClickCommand == null)
                {
                    rowDoubleClickCommand = new RelayCommand<Models.IModel>(
                        selectedItem =>
                        {
                            var invoice = selectedItem as Models.Invoice;
                            if (invoice == null)
                                return;

                            InvoiceWindowLogic(invoice);
                        });
                }
                return rowDoubleClickCommand;
            }
        }

        public ICommand InvoiceMenuItemClickCommand
        {
            get
            {
                if (invoiceMenuItemClickCommand == null)
                {
                    invoiceMenuItemClickCommand = new RelayCommand<Models.IModel>(
                        selectedItem =>
                        {
                            var invoice = selectedItem as Models.Invoice;
                            if (invoice == null)
                                return;

                            InvoiceWindowLogic(invoice);
                        });
                }
                return invoiceMenuItemClickCommand;
            }
        }

        private static void InvoiceWindowLogic(Models.Invoice invoice)
        {
            var page = new CoolIcePro.Views.InsertInvoice(new CoolIcePro.ViewModels.InsertInvoiceViewModel(invoice));
            var customer = ProjectManager.Instance.CoolIceProDBHelper.GetCustomer(invoice.CompanyId);
            Windows.GenericWindow gw = new Windows.GenericWindow(685, 625, string.Format("Invoice for {0}", customer.CompanyName), page);
            gw.ShowDialog();
        }
     
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
