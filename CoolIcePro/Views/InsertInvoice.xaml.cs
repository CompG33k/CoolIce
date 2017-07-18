using CoolIcePro.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class InsertInvoice : Page
    {
        public InsertInvoice()
        {
            InitializeComponent();

            //_dataGrid.ItemsSource = new List<Models.Invoice>(){
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "1138676",TotalAmount = 12.33 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "1133335",TotalAmount = 212.33 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "11333576",TotalAmount = 123.44 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "1133333",TotalAmount = 124.23 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "1133333",TotalAmount = 12.33 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "11333daa",TotalAmount = 212.33 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "11333a3",TotalAmount = 123.44 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "113ad53333",TotalAmount = 124.23 },
            //     new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "1138676",TotalAmount = 12.33 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "1133335",TotalAmount = 212.33 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "11333576",TotalAmount = 123.44 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "1133333",TotalAmount = 124.23 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "1133333",TotalAmount = 12.33 },
            //    new Models.Invoice()  {Date = new DateTime(),Description = "DESCRIPTION",InvoiceNumber = "11334443",TotalAmount = 912.39 }
            //};
        }
        public InsertInvoice(ViewModels.InsertInvoiceViewModel insertVm)
        {
            InitializeComponent();
            DataContext = insertVm;
        }
        private void CancelButtonEventHandler(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow(this);
            w.Close();
        }

        private void SaveButtonEventHandler(object sender, RoutedEventArgs e)
        {
            ////Company company = ProjectManager.Instance.CurrentCompany;

            ////if(company == null)
            ////{
            ////    MessageBox.Show("if(company == null) line 58  Views\\InsertInvoice.xaml.cs line 58");
            ////    return;
            ////}
            //////Retrieve CompanyId... from DB
            ////var foreignKey = ProjectManager.Instance.CoolIceProDBHelper.GetContactForeignKey(company);
            var foreignKey = 20;
            if(foreignKey == null)
            {
                MessageBox.Show("var foreignKey = ProjectManager.Instance.CoolIceProDBHelper.GetContactForeignKey(company);  Views\\InsertInvoice.xaml.cs line 61 ");
                return;
            }

           
          

             // Verify All UI
            if(!ValidateUI())
            {
                // tell user
                // to check the input
                return;
            }

            Invoice invoice = GetInvoiceFromUI();

            if (invoice == null)
            {
                // Something went wrong
                MessageBox.Show("Invoice GETINVOICEFROMUI()  is NULL");
                return;
            }
            // update Database
            if(!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(foreignKey,invoice))
            {
                // somethign in the DB went wrong
                MessageBox.Show("if(!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(foreignKey,invoice)) Views\\InsertInvoice.xaml.cs line 82");
                return;
            }


            MessageBox.Show("Success!");
            Window.GetWindow(this).Close();
        }


        private Invoice GetInvoiceFromUI()
        {
            Invoice _invoice = null;
            try
            {
                _invoice = new Invoice();
                _invoice.Check = _checkRadioButton.IsChecked ?? false;
                _invoice.Date = _datePicker.SelectedDate.Value;
                _invoice.Description = _descriptionTxtBox.Text;
                _invoice.ServicePerfomanceOn = _servicePerformedOn.Text;
                _invoice.TotalAmount = double.Parse(_totalAmountTxtBox.Text);
                _invoice.Warranty = _warrantyCheckBox.IsChecked ?? false;

                //return new Invoice(){
                //    Check =  _checkRadioButton.IsChecked?? false,
                //    Date = _datePicker.SelectedDate.Value,
                //    Description = _descriptionTxtBox.Text,
                //    ServicePerfomanceOn =_servicePerformedOn.Text,
                //    TotalAmount = double.Parse(_totalAmountTxtBox.Text),
                //    Warranty = _warrantyCheckBox.IsChecked ?? false
                //};
            }
            catch (InvalidOperationException invalidOperationEx)
            {
              var t =  invalidOperationEx.Message;
              _invoice = null;
            }
        
            return _invoice;
        }
        private bool ValidateUI()
        {
            bool validationCheck = true;
            int i = 1;
            StringBuilder validationMessage = new StringBuilder("Please make sure the following :\n\n");
            //Checkradio
            if (_checkRadioButton.IsChecked == true)
            {
                if(!string.IsNullOrEmpty(_checkNumberTxtBox.Text ))
                {
                    // Something went wrong this isn't suppose to be empty
                    validationMessage.Append(i.ToString() + ".) Cash/Check box is not checked.\n");
                    i++;
                    validationCheck = false;
                    //Confirm to user check box here
                } 
            }

            if(string.IsNullOrEmpty(_descriptionTxtBox.Text))
            {
                validationMessage.Append(i.ToString() + ".) Nothing in description box (empty).\n");
                i++;
                validationCheck = false;
            }

            if (string.IsNullOrEmpty(_totalAmountTxtBox.Text) || _totalAmountTxtBox.Text == "_______")
            {
                validationMessage.Append(i.ToString() + ".) No total Amount not filled in.\n");
                i++;
                validationCheck = false;
            }

            if(_datePicker.SelectedDate==null)
            {
                validationMessage.Append(i.ToString() + ".) No date selected.\n");
                validationCheck = false;
            }
           // if(_totalAmountTxtBox.Text)
            if(validationCheck != true)
                MessageBox.Show(validationMessage.ToString());
            return validationCheck;
        }

        private void CashRadioCheckedEventHandler(object sender, RoutedEventArgs e)
        {
            if (_checkNumberLabel != null && _checkNumberTxtBox != null)
            {
                _checkNumberTxtBox.Visibility = Visibility.Collapsed;
                _checkNumberLabel.Visibility = Visibility.Collapsed;
            }
        }

        private void CheckRadioCheckedEventHandler(object sender, RoutedEventArgs e)
        {
            _checkNumberTxtBox.Visibility = Visibility.Visible;
            _checkNumberLabel.Visibility = Visibility.Visible;
        }
    }
}
