using CoolIcePro.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoolIcePro.Views
{
    /// <summary>
    /// Interaction logic for InsertCompanyConfirmation.xaml
    /// </summary>
    public partial class InsertCompanyConfirmation : Page
    {
        public InsertCompanyConfirmation()
        {
            InitializeComponent();
        }

        public InsertCompanyConfirmation(dynamic customerView)
        {
            InitializeComponent();
            LoadUI(customerView);
        }
      
        private void ButtonHandler(object sender, RoutedEventArgs e)
        {
            try{
                var button = sender as Button;
                if(button.Tag.ToString() == "Confirm"){

                    Company company = GetCompanyFromUI();

                    if(!ProjectManager.Instance.CoolIceProDBHelper.InsertCompany(company))
                        MessageBox.Show("NO INSERT PERFORMED");

                    long foreignKey = ProjectManager.Instance.CoolIceProDBHelper.GetContactForeignKey(company);

                    if (foreignKey == null)
                        MessageBox.Show("No Foreign Key");
                    
                    Contact contact =  GetContactUI();
                    ProjectManager.Instance.CoolIceProDBHelper.InserContact(contact,foreignKey);
                }

                MainWindow w = Window.GetWindow(this) as MainWindow;
                var g = w.Content as Grid;
                var page = w._frame.Content as InsertCompany;
                g.IsEnabled = true;
                page._popup.IsOpen = false;
            }
            catch (DbEntityValidationException dbEx){
                foreach (var validationErrors in dbEx.EntityValidationErrors){
                        foreach (var validationError in validationErrors.ValidationErrors)
                            System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        
                    }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadUI(dynamic customerView)
        {
            //_companyNameLabel.Content = customerView.Name;
            //_addressLabel.Content = customerView.Address;
            //_addressExtLabel.Content = customerView.AddressExt;
            //_cityLabel.Content = customerView.City;
            //_stateLabel.Content = customerView.State;
            //_zipcodeLabel.Content = customerView.Zipcode;
            //_telephoneLabel.Content = customerView.Telephone;
            //_faxLabel.Content = customerView.Fax;
            //_emailLabel.Content = customerView.Email;
            //_websiteLabel.Content = customerView.Website;

            //_fNameLabel.Content = customerView.ContactFname;
            //_lNameLabel.Content = customerView.ContactLname;
            //_contactTelephoneLabel.Content = customerView.ContactTelephone;
            //_cellLabel.Content = customerView.ContactCell;
            //_positionLabel.Content = customerView.ContactPosition;
            _companyNameLabel.Content = "TESTCOMPANY NAME";
            _addressLabel.Content = "123 NAME";
            _addressExtLabel.Content = customerView.AddressExt;
            _cityLabel.Content = "MOORPARK";
            _stateLabel.Content = "CA";
            _zipcodeLabel.Content = "93021";
            _telephoneLabel.Content = "3102111234";
            _faxLabel.Content = customerView.Fax;
            _emailLabel.Content = "BOB@HOTMAIL.COM";
            _websiteLabel.Content = customerView.Website;

            _fNameLabel.Content = "FNAMETEST";
            _lNameLabel.Content = "LNAMETEST";
            _contactTelephoneLabel.Content = "3102331234";
            _cellLabel.Content = customerView.ContactCell;
            _positionLabel.Content = "OWNER";
        }

        private Contact GetContactUI()
        {
            Contact contact = new Contact();
            contact.Fname = _fNameLabel.Content as string;
            contact.Lname = _lNameLabel.Content as string;
            contact.Telephone = _contactTelephoneLabel.Content as string;
            contact.Cellphone = _cellLabel.Content as string;
            contact.Position = _positionLabel.Content as string;

            if (contact.Fname == null || contact.Position == null)
            {
                MessageBox.Show("Not all required fields filled in!");
                return null;
            }
            return contact;
        }

        private Company GetCompanyFromUI()
        {
            Company company = new Company();
            company.CompanyName = _companyNameLabel.Content as string;
            company.Address = _addressLabel.Content as string;
            company.AddressExt = _addressExtLabel.Content as string;
            company.City = _cityLabel.Content as string;
            company.State = _stateLabel.Content as string;
            company.Zipcode = _zipcodeLabel.Content as string;
            company.Telephone = _telephoneLabel.Content as string;
            company.Fax = _faxLabel.Content as string;
            company.Email = _emailLabel.Content as string;
            company.Website = _websiteLabel.Content as string;

            if (company.CompanyName == null || company.Address == null || company.Zipcode == null || company.Telephone == null)
            {
                MessageBox.Show("Not all fields are fill in!");
                return null;
            }
            return company;
        }
    }
}
