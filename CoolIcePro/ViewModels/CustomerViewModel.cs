﻿using CoolIcePro.Views;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;

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

        long contactId;
        string contactFname;
        string contactLname;
        string contactTelephone;
        string contactCellphone;
        string contactPosition;

        private bool canExecute = true;


        private ICommand insertButtonClicked;
        private ICommand rowDoubleClickCommand;
        private ICommand invoiceMenuItemClickCommand;
        private ICommand updateButtonClickCommand;
        private ICommand newInvoiceButtonClickCommand;
        
        IEnumerable<CoolIcePro.Models.Contact> contacts;
        IEnumerable<string> states;
        ObservableCollection<Models.Invoice> invoices;


        public long Id
        {
            get { return id; }

            set
            {
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
        public string Address
        {
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
        public string AddressExt
        {
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
        public string City
        {
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

        public string State
        {
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

        public string Zipcode
        {
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

        public string Telephone
        {
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
        public string CellNumber
        {
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
        public string Fax
        {
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
        public string Email
        {
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
        public string Website
        {
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

        //public IEnumerable<CoolIcePro.Models.Contact> Contacts
        //{
        //    get { return contacts; }
        //    set
        //    {
        //        if (contacts != value)
        //        {
        //            contacts = value;
        //            OnPropertyChanged("Contacts");
        //        }
        //    }
        //}

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

        public ObservableCollection<Models.Invoice> Invoices
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
        Models.Customer _resetViewModel;
        public Models.Customer GetResetViewModel() { return _resetViewModel; }
        
        public CustomerViewModel(CoolIcePro.Models.Customer customer)
        {
            _resetViewModel = customer;

            Id = customer.Id;
            invoices = new ObservableCollection<Models.Invoice>(ProjectManager.Instance.CoolIceProDBHelper.GetCustomerInvoices(Id));
            //    Observable.Create<Models.Invoice>(current =>{
            //    foreach (var cur in ProjectManager.Instance.CoolIceProDBHelper.GetCustomerInvoices(Id))
            //    {
            //        current.OnNext(cur);
            //    }
            //    return Disposable.Create(() => { });
            //});

            SetViewModel(customer);
        }

        public void SetViewModel(CoolIcePro.Models.Customer customer)
        {
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

            var contact = customer.Contact;
            contactId = contact.Id;
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

        public ICommand UpdateButtonClickCommand
        {
            get
            {
                if (updateButtonClickCommand == null)
                {
                    updateButtonClickCommand = new RelayCommand<Page>(
                        page =>
                        {
                            UpdateCustomerLogic();
                            var customerPage = (Views.Customer)page;
                            if (customerPage != null)
                            {
                                customerPage._editButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                Window parentWindow = Application.Current.MainWindow;
                                var vm = parentWindow.DataContext as MainWindowViewModel;
                                if (vm != null)
                                {
                                    var vMm = vm.MainWindowPage.DataContext as IPageViewModel;
                                    if (vMm != null)
                                    {
                                        vMm.ResetList();
                                    }
                                }
                            }
                        });
                }
                return updateButtonClickCommand;
            }
        }

        public ICommand NewInvoiceButtonClickCommand
        {
            get
            {
                if (newInvoiceButtonClickCommand == null)
                {
                    newInvoiceButtonClickCommand = new RelayCommand(
                        () =>
                        {
                            var page = new CoolIcePro.Views.InsertInvoice(PAGE_STATE.NEW,new CoolIcePro.ViewModels.InsertInvoiceViewModel(Id));
                            Windows.GenericWindow gw = new Windows.GenericWindow(685, 625, string.Format("New Invoice for {0}", CompanyName), page);
                            gw.ShowDialog();
                            this.Invoices = new ObservableCollection<Models.Invoice>(ProjectManager.Instance.CoolIceProDBHelper.GetCustomerInvoices(Id));
                        });
                }
                return newInvoiceButtonClickCommand;
            }
        }
        

        private void UpdateCustomerLogic()
        {
            Models.Customer customer = GetCutomerLogic();
            Models.Contact contact = GetCustomerContactLogic();

            if (ProjectManager.Instance.CoolIceProDBHelper.UpdateCustomer(customer))
            {
                ProjectManager.Instance.CoolIceProDBHelper.UpdateCustomerContact(contact);
                MessageBox.Show("Success");
                return;
            }
            
        }

        private Models.Contact GetCustomerContactLogic()
        {
            Models.Contact cContact = new Models.Contact();
            cContact.Id = this.contactId;
            cContact.Cellphone = this.contactCellphone;
            cContact.FirstName = this.contactFname;
            cContact.LastName = this.ContactLname;
            cContact.Position = this.contactPosition;
            cContact.Telephone =  ContactTelephone;
            cContact.Cellphone =  ContactCellphone;
            cContact.CompanyId = this.Id;
            return cContact;
        }

        private Models.Customer GetCutomerLogic()
        {
            Models.Customer c = new Models.Customer();
            c.Id = this.Id;
            c.Address = this.Address;
            c.AddressExt = this.AddressExt;
            c.City = this.City;
            c.CompanyName = this.CompanyName;
            c.Email = this.Email;
            c.Fax = this.Fax;
            c.State = this.State;
            c.Telephone = this.Telephone;
            c.Website = this.Website;
            c.Zipcode = this.Zipcode;
            return c;
        }     
        private void InvoiceWindowLogic(Models.Invoice invoice)
        {
            var page = new CoolIcePro.Views.InsertInvoice(PAGE_STATE.UPDATE, new CoolIcePro.ViewModels.InsertInvoiceViewModel(invoice));
            var customer = ProjectManager.Instance.CoolIceProDBHelper.GetCustomer(invoice.CompanyId);
            Windows.GenericWindow gw = new Windows.GenericWindow(685, 625, string.Format("Invoice for {0}", customer.CompanyName), page);
            gw.ShowDialog();
            this.Invoices = new ObservableCollection<Models.Invoice>(ProjectManager.Instance.CoolIceProDBHelper.GetCustomerInvoices(Id));
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
