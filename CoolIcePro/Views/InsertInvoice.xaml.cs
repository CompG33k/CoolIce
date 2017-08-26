using CoolIcePro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public enum PAGE_STATE { NEW, UPDATE }
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class InsertInvoice : Page
    {
        ViewModels.InsertInvoiceViewModel _undoViewModel;
        PAGE_STATE _currentState;

        public InsertInvoice(PAGE_STATE currentState, ViewModels.InsertInvoiceViewModel insertVm)
        {
            InitializeComponent();
            DataContext = insertVm;
            _undoViewModel = new ViewModels.InsertInvoiceViewModel(insertVm);
            
            PageStateUILogic(currentState);
        }
        private void CancelButtonEventHandler(object sender, RoutedEventArgs e)
        {
            if (_currentState == PAGE_STATE.NEW)
            {
                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (window != null)
                {
                    window.Close();
                }
            }
            else
            {
                if (_undoViewModel != null)
                    this.DataContext = _undoViewModel;
                ToggleUIControlSwitch();
                //else something went wrong
            }
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
                _invoice.ServicePerformanceOn = _servicePerformedOn.Text;
                _invoice.TotalAmount = double.Parse(_totalAmountTxtBox.Text, System.Globalization.NumberStyles.Currency);  //double.Parse(_totalAmountTxtBox.Text);
                _invoice.Warranty = _warrantyCheckBox.IsChecked ?? false;
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

        private void PreviewTextInputEventHandler(object sender, TextCompositionEventArgs e)
        {
            Regex rgx = new Regex(@"^\$?(?:[0-9]+(?:\.[0-9]+)?|\.[0-9]+)$");
            e.Handled = !rgx.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void EditButtonClickedEventHandler(object sender, RoutedEventArgs e)
        {
            ToggleUIControlSwitch();
        }

        private void PageStateUILogic(PAGE_STATE currentState)
        {
            _currentState = currentState;
            if (currentState == PAGE_STATE.NEW)
            {
                _buttonStackPanel.Visibility = System.Windows.Visibility.Visible;
                _updateButton.Visibility = System.Windows.Visibility.Collapsed;
                _createButton.Visibility = System.Windows.Visibility.Visible;
                _editButton.Visibility = System.Windows.Visibility.Collapsed;
                ToggleUIControlSwitch();
            }
        }

        private void ToggleUIControlSwitch()
        {
            if (_datePicker.IsHitTestVisible == false)
            {
                _datePicker.IsHitTestVisible = true;
                _datePicker.FontWeight = FontWeights.Bold;
                _warrantyCheckBox.IsHitTestVisible = true;
                _warrantyCheckBox.FontWeight = FontWeights.Bold;
                _servicePerformedOn.IsReadOnly = false;
                _servicePerformedOn.FontWeight = FontWeights.Bold;
                _checkNumberTxtBox.IsReadOnly = false;
                _checkNumberTxtBox.FontWeight = FontWeights.Bold;
                _invoiceNumberTxtBox.IsReadOnly = false;
                _invoiceNumberTxtBox.FontWeight = FontWeights.Bold;
                _descriptionTxtBox.IsReadOnly = false;
                _descriptionTxtBox.FontWeight = FontWeights.Bold;
                _totalAmountTxtBox.IsReadOnly = false;
                _totalAmountTxtBox.FontWeight = FontWeights.Bold;
                _cashRadioButton.IsHitTestVisible = true;
                _cashRadioButton.FontWeight = FontWeights.Bold;
                _checkRadioButton.IsHitTestVisible = true;
                _checkRadioButton.FontWeight = FontWeights.Bold;
                _buttonStackPanel.Visibility = System.Windows.Visibility.Visible;
                _editButton.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                _datePicker.IsHitTestVisible = false;
                _datePicker.FontWeight = FontWeights.Normal;
                _warrantyCheckBox.IsHitTestVisible = false;
                _warrantyCheckBox.FontWeight = FontWeights.Normal;
                _servicePerformedOn.IsReadOnly = true;
                _servicePerformedOn.FontWeight = FontWeights.Normal;
                _checkNumberTxtBox.IsReadOnly = true;
                _checkNumberTxtBox.FontWeight = FontWeights.Normal;
                _invoiceNumberTxtBox.IsReadOnly = true;
                _invoiceNumberTxtBox.FontWeight = FontWeights.Normal;
                _descriptionTxtBox.IsReadOnly = true;
                _descriptionTxtBox.FontWeight = FontWeights.Normal;
                _totalAmountTxtBox.IsReadOnly = true;
                _totalAmountTxtBox.FontWeight = FontWeights.Normal;
                _cashRadioButton.IsHitTestVisible = true;
                _cashRadioButton.FontWeight = FontWeights.Bold;
                _checkRadioButton.IsHitTestVisible = true;
                _checkRadioButton.FontWeight = FontWeights.Bold;
                _buttonStackPanel.Visibility = System.Windows.Visibility.Collapsed;
                _editButton.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
