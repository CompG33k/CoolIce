using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoolIcePro.ViewModels
{
    public class InsertCustomerViewModel: INotifyPropertyChanged
    {
        string _customerName;
        string _customerAddress;
        string _customerAddressExt;
        string _customerCity;
        string _customerState;
        string _customerZipCode;
        string _customerTelephone;
        string _customerFax;
        string _customerEmail;
        string _customerWebsite;
        string _contactFirstName;
        string _contactLastName;
        string _contactTelephone;
        string _contactCellphone;
        string _contactPosition;
        IEnumerable<string> _states;
        string[] _positions = { "MANAGER", "OWNER", "EMPLOYEE" };
        ICommand saveButtonPressedCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        public InsertCustomerViewModel()
        {
            States = ProjectManager.Instance.States;
        }

        public String[] Positions
        {
            get
            {
                return _positions;
            }
            set
            {
                if (_positions != value)
                {
                    _positions = value;
                    OnPropertyChanged("Positions");
                }
            }
        }

        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged("CustomerName");
                }
            }
        }

        public string CustomerAddress
        {
            get
            {
                return _customerAddress;
            }
            set
            {
                if (_customerAddress != value)
                {
                    _customerAddress = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public string CustomerAddressExt
        {
            get
            {
                return _customerAddressExt;
            }
            set
            {
                if (_customerAddressExt != value)
                {
                    _customerAddressExt = value;
                    OnPropertyChanged("AddressExt");
                }
            }
        }

        public string CustomerCity
        {
            get
            {
                return _customerCity;
            }
            set
            {
                if (_customerCity != value)
                {
                    _customerCity = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public string CustomerState
        {
            get
            {
                return _customerState;
            }
            set
            {
                if (_customerState != value)
                {
                    _customerState = value;
                    OnPropertyChanged("CustomerState");
                }
            }
        }

        public string CustomerZipCode
        {
            get
            {
                return _customerZipCode;
            }
            set
            {
                if (_customerZipCode != value)
                {
                    _customerZipCode = value;
                    OnPropertyChanged("ZipCode");
                }
            }
        }

        public string CustomerTelephone
        {
            get
            {
                return _customerTelephone;
            }
            set
            {
                if (_customerTelephone != value)
                {
                    _customerTelephone = value;
                    OnPropertyChanged("CustomerTelephone");
                }
            }
        }

        public string CustomerFax
        {
            get
            {
                return _customerFax;
            }
            set
            {
                if (_customerFax != value)
                {
                    _customerFax = value;
                    OnPropertyChanged("Fax");
                }
            }
        }

        public string CustomerEmail
        {
            get
            {
                return _customerEmail;
            }
            set
            {
                if (_customerEmail != value)
                {
                    _customerEmail = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public string CustomerWebsite
        {
            get
            {
                return _customerWebsite;
            }
            set
            {
                if (_customerWebsite != value)
                {
                    _customerWebsite = value;
                    OnPropertyChanged("Website");
                }
            }
        }

        public string ContactFirstName
        {
            get
            {
                return _contactFirstName;
            }
            set
            {
                if (_contactFirstName != value)
                {
                    _contactFirstName = value;
                    OnPropertyChanged("ContactFirstName");
                }
            }
        }

        public string ContactLastName
        {
            get
            {
                return _contactLastName;
            }
            set
            {
                if (_contactLastName != value)
                {
                    _contactLastName = value;
                    OnPropertyChanged("ContactLastName");
                }
            }
        }

        public string ContactTelephone
        {
            get
            {
                return _contactTelephone;
            }
            set
            {
                if (_contactTelephone != value)
                {
                    _contactTelephone = value;
                    OnPropertyChanged("ContactTelephone");
                }
            }
        }

        public string ContactCellphone
        {
            get
            {
                return _contactCellphone;
            }
            set
            {
                if (_contactCellphone != value)
                {
                    _contactCellphone = value;
                    OnPropertyChanged("ContactCellPhone");
                }
            }
        }

        public string ContactPosition
        {
            get
            {
                return _contactPosition;
            }
            set
            {
                if (_contactPosition != value)
                {
                    _contactPosition = value;
                    OnPropertyChanged("ContactPosition");
                }
            }
        }

        public IEnumerable<string> States
        {
            get
            {
                return _states;
            }
            set
            {
                if (_states != value)
                {
                    _states = value;
                    OnPropertyChanged("States");
                }
            }
        }

        public ICommand SaveButtonPressedCommand
        {
            get
            {
                if (saveButtonPressedCommand == null)
                {
                    saveButtonPressedCommand = new RelayCommand(
                        () =>
                        {
                            InsertCustomerLogic();
                            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            
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

                            if (window != null)
                            {
                                window.Close();
                            }
                        });
                }
                return saveButtonPressedCommand;
            }
        }

        private void UpdateListLogic()
        {
            throw new NotImplementedException();
        }

        private void InsertCustomerLogic()
        {
            Models.Customer customer = GetCutomerLogic();
            Models.Contact contact = GetCustomerContactLogic();

            long customerId = ProjectManager.Instance.CoolIceProDBHelper.InsertCustomer(customer);
            ProjectManager.Instance.CoolIceProDBHelper.InserCustomerContact(customerId, contact);
        }

        private Models.Contact GetCustomerContactLogic()
        {
            Models.Contact cContact = new Models.Contact();
            cContact.Cellphone = this.ContactCellphone;
            cContact.FirstName = this.ContactFirstName;
            cContact.LastName = this.ContactLastName;
            cContact.Position = this.ContactPosition;
            cContact.Telephone = this.ContactTelephone;
            return cContact;
        }

        private Models.Customer GetCutomerLogic()
        {
            Models.Customer c = new Models.Customer();
            c.Address = this.CustomerAddress;
            c.AddressExt = this.CustomerAddressExt;
            c.City = this.CustomerCity;
            c.CompanyName = this.CustomerName;
            c.Email = this.CustomerEmail;
            c.Fax = this.CustomerFax;
            c.State = this.CustomerState;
            c.Telephone = this.CustomerTelephone;
            c.Website = this.CustomerWebsite;
            c.Zipcode = this.CustomerZipCode;
            return c;
        }     

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
