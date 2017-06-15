using CoolIcePro.View.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoolIcePro.ViewModels
{
    public class CompanyViewModel : INotifyPropertyChanged
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
        private ICommand doubleClickCommand;
        private ICommand insertButtonClicked;
        
        List<CoolIcePro.Models.Contact> contacts;



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
                    companyName = value;
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

        public List<CoolIcePro.Models.Contact> Contacts{ 
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


        public CompanyViewModel(CoolIcePro.Models.Company company)
        {
            Id = company.Id;
            CompanyName = company.CompanyName;
            Address = company.Address;
            AddressExt = company.AddressExt;
            City = company.City;
            State = company.State;
            Zipcode = company.Zipcode;
            Telephone = company.Telephone;
            CellNumber = company.CellNumber;
            Fax = company.Fax;
            Email = company.Email;
            Website = company.Website; 
            DoubleClickCommand = new RelayCommand(ShowMessage, param => this.canExecute);
            InsertButtonClicked = new RelayCommand(InsertMessage);
            if (company.Contacts == null)
                return;
            var contact  = company.Contacts.FirstOrDefault();
            ContactFname = contact.Fname;
            ContactLname = contact.Lname;
            ContactTelephone = contact.Telephone;
            ContactCellphone = contact.Cellphone;
            ContactPosition = contact.Position;
           
        }
        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }
        public ICommand DoubleClickCommand
        {
            get
            {
                return doubleClickCommand;
            }
            set
            {
                doubleClickCommand = value;
            }
        }

        public ICommand InsertButtonClicked
        {
            get
            {
                return insertButtonClicked;
            }
            set
            {
                insertButtonClicked = value;
            }
        }

        public void InsertMessage(object obj)
        {
            System.Windows.MessageBox.Show("INSERT");
        }
        public void ShowMessage(object obj)
        {
            System.Windows.MessageBox.Show("SHOW");
        }
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        //===============================


        //public class DataGridAttachedBehaviors
        //{
        //    #region DoubleClick

        //    public static DependencyProperty OnDoubleClickProperty = DependencyProperty.RegisterAttached(
        //        "OnDoubleClick",
        //        typeof(ICommand),
        //        typeof(DataGridAttachedBehaviors),
        //        new UIPropertyMetadata(DataGridAttachedBehaviors.OnDoubleClick));

        //    public static void SetOnDoubleClick(DependencyObject target, ICommand value)
        //    {
        //        target.SetValue(DataGridAttachedBehaviors.OnDoubleClickProperty, value);
        //    }

        //    private static void OnDoubleClick(DependencyObject target, DependencyPropertyChangedEventArgs e)
        //    {
        //        var element = target as Control;
        //        if (element == null)
        //        {
        //            throw new InvalidOperationException("This behavior can be attached to a Control item only.");
        //        }

        //        if ((e.NewValue != null) && (e.OldValue == null))
        //        {
        //            element.MouseDoubleClick += MouseDoubleClick;
        //        }
        //        else if ((e.NewValue == null) && (e.OldValue != null))
        //        {
        //            element.MouseDoubleClick -= MouseDoubleClick;
        //        }
        //    }

        //    private static void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //    {
        //        UIElement element = (UIElement)sender;
        //        ICommand command = (ICommand)element.GetValue(DataGridAttachedBehaviors.OnDoubleClickProperty);
        //        command.Execute(null);
        //    }

        //    #endregion DoubleClick

        //    #region SelectionChanged
        //    //removed
        //    #endregion
        //}
    }
}
