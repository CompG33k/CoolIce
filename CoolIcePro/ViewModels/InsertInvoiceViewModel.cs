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
    public class InsertInvoiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ICommand saveButtonClickedCommand;
        ICommand cancelButtonClickedCommand;

        string _servicePerformedOn;
        string _checkNumber;
        string _totalAmount;
        string _description;
        
        public InsertInvoiceViewModel() { }

        public InsertInvoiceViewModel(Models.Invoice vm) {
            if (vm == null)
                return;
            this._checkNumber = vm.InvoiceNumber;
            this._description = vm.Description;
            this._servicePerformedOn = vm.ServicePerfomanceOn;
            this._totalAmount = vm.TotalAmount.ToString();
        }

        public string ServicePerformedOn
        {
            set
            {
                if (_servicePerformedOn != value)
                {
                    _servicePerformedOn = value;
                    OnPropertyChanged("ServicePerformedOn");
                }
            }
            get
            {
                return _servicePerformedOn;
            }
        }

        public string CheckNumber
        {
            set
            {
                if (_checkNumber != null)
                {
                    _checkNumber = value;
                    OnPropertyChanged("_checkNumber");
                }
            }
            get
            {
                return _checkNumber;
            }
        }

        public string TotalAmount
        {
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChanged("TotalAmount");
                }
            }
            get
            {
                return _totalAmount;
            }
        }

        public string Description
        {
            set
            {
                if (_description != null)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
            get
            {
                return _description;
            }
        }

        public ICommand SaveButtonClickedCommand
        {
            get
            {
                if (saveButtonClickedCommand == null)
                {
                    saveButtonClickedCommand = new RelayCommand<object>(
                        sender =>
                        {
                            int i = 0;
                           // sender.Close();
                           // MessageBox.Show("Save Button Clicked");
                            ////Company company = ProjectManager.Instance.CurrentCompany;

                            ////if(company == null)
                            ////{
                            ////    MessageBox.Show("if(company == null) line 58  Views\\InsertInvoice.xaml.cs line 58");
                            ////    return;
                            ////}
                            //////Retrieve CompanyId... from DB
                            ////var foreignKey = ProjectManager.Instance.CoolIceProDBHelper.GetContactForeignKey(company);
                            //var foreignKey = 20;
                            //if (foreignKey == null)
                            //{
                            //    MessageBox.Show("var foreignKey = ProjectManager.Instance.CoolIceProDBHelper.GetContactForeignKey(company);  Views\\InsertInvoice.xaml.cs line 61 ");
                            //    return;
                            //}




                            // Verify All UI
                            //if (!ValidateUI())
                            //{
                            //     tell user
                            //     to check the input
                            //    return;
                            //}

                            //Invoice invoice = GetInvoiceFromUI();

                            //if (invoice == null)
                            //{
                            //     Something went wrong
                            //    MessageBox.Show("Invoice GETINVOICEFROMUI()  is NULL");
                            //    return;
                            //}
                            // update Database
                            //if (!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(foreignKey, invoice))
                            //{
                            //     somethign in the DB went wrong
                            //    MessageBox.Show("if(!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(foreignKey,invoice)) Views\\InsertInvoice.xaml.cs line 82");
                            //    return;
                            //}


                            //MessageBox.Show("Success!");
                            //Window.GetWindow(this).Close();
                            //var company = item as Models.Company;
                            //var p = new CoolIcePro.Controls.PopupWindow("Customer Details", new CoolIcePro.Views.Customer(new CompanyViewModel(company)));
                            //p.Show();
                        });
                }
                return saveButtonClickedCommand;
            }
        }
        public ICommand CancelButtonClickedCommand
        {
            get
            {
                if (cancelButtonClickedCommand == null)
                {
                    cancelButtonClickedCommand = new RelayCommand<object>(
                        sender =>
                        {
                        
                           // Window w = Window.GetWindow(this);
                           // w.Close();
                            //var company = item as Models.Company;
                            //var p = new CoolIcePro.Controls.PopupWindow("Customer Details", new CoolIcePro.Views.Customer(new CompanyViewModel(company)));
                            //p.Show();
                        });
                }
                return cancelButtonClickedCommand;
            }
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
