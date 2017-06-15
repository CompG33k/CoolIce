using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoolIcePro.Views
{
    /// <summary>
    /// Interaction logic for InsertRecord.xaml
    /// </summary>
    public partial class InsertCompany : Page
    {
    
        public InsertCompany()
        {
            InitializeComponent();

            //Popup work
            Window w = Window.GetWindow(_addressExtTxtBox);
            _stateTxtBox.ItemsSource = ProjectManager.Instance.States;
           
            if (null != w)
            {
                w.LocationChanged += delegate(object sender, EventArgs args)
                {
                    if (_popup.IsOpen)
                    {
                        var offset = _popup.HorizontalOffset;
                        _popup.HorizontalOffset = offset + 1;
                        _popup.HorizontalOffset = offset;
                    }
                };
            }
        }
        
        private void PopupOpenedEventHandler(object sender, EventArgs e)
        {
            Window win = Window.GetWindow(this);
            win.LocationChanged += new EventHandler(WindowLocationChangedEventHandler);
        }

        private void WindowLocationChangedEventHandler(object sender, EventArgs e)
        {
            if (_popup.IsOpen)
            {
                var mi = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                mi.Invoke(_popup, null);
            }
        }

        private void SaveButtonHanlder(object sender, RoutedEventArgs e)
        {
            //Check UI Logic
            if (!RequiredFields())
                return;
            var c =_positionComboBox.SelectedItem as ComboBoxItem;
                       
            _popupFrame.Navigate(new InsertCompanyConfirmation(new {
            Name = _companyNameTxtBox.Text.Trim(),
            Address = _addressTxtBox.Text.Trim(),
            AddressExt = _addressExtTxtBox.Text.Trim(),
            City = _cityTxtBox.Text.Trim(),
            State = _stateTxtBox.Text.Trim(),
            Zipcode = _zipcodeTxtBox.Text.Trim(),
            Telephone = _telephoneTxtBox.Text.Trim(),
            Fax = _faxTxtBox.Text.Trim(),
            Email = _emailTxtBox.Text.Trim(),
            Website = _websiteTxtBox.Text.Trim(),

            ContactFname = _fNameTxtBox.Text.Trim(),
            ContactLname = _lNameTxtBox.Text.Trim(),
            ContactTelephone = _contactTelephoneTxtBox.Text.Trim(),
            ContactCell = _cellTxtBox.Text.Trim(),
            ContactPosition =  (c.Content )?? ""
            
            }));
            _popup.IsOpen = true;
            var w = Window.GetWindow(this);
            var p = w.Content as Grid;
            p.IsEnabled = false;
        }

        bool RequiredFields()
        {
            //Implement Logic to check UI (TextBoxes... let user know an error has occurred)
            bool passed = true;
                       
               if(string.Empty ==_companyNameTxtBox.Text.Trim())
               {
                   
                   passed = false;
               }
            
               if(string.Empty == _addressTxtBox.Text.Trim() )
               {
                   passed = false;
               }
                
                if(string.Empty == _addressExtTxtBox.Text.Trim() )
                {
                  //  passed = false;
                }

                if(string.Empty == _cityTxtBox.Text.Trim() )
                {

                    passed = false;
                }

                //if(string.Empty == _stateTxtBox.Text.Trim())
                //{
                //    passed = false;
                //}
                
                if(string.Empty == _zipcodeTxtBox.Text.Trim() )
                {
                    passed = false;
                }
              
                if(string.Empty == _telephoneTxtBox.Text.Trim())
                {
                    passed = false;
                }
                if(string.Empty == _faxTxtBox.Text.Trim() )
                {
                    
                   // passed = false;
                }
                
                if(string.Empty == _emailTxtBox.Text.Trim() )
                {
                  //  passed = false;
                }
                if(string.Empty == _websiteTxtBox.Text.Trim())
                {
                   // passed = false;
                }
                if(string.Empty == _fNameTxtBox.Text.Trim() )
                {
                    passed = false;
                }
                if(string.Empty == _lNameTxtBox.Text.Trim())
                {
                    passed = false;
                }
                if(string.Empty == _contactTelephoneTxtBox.Text.Trim())
                {
                   // passed = false;
                }
                if(string.Empty == _cellTxtBox.Text.Trim())
                {

                 //   passed = false;
                }
               
            return passed;
        }
    }

}
