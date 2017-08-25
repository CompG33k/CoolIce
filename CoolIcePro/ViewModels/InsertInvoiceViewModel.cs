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
        double _totalAmount;
        string _description;
        Models.Invoice _invoice;
        public InsertInvoiceViewModel(long companyId) 
        {
            _invoice = new Models.Invoice();
            _invoice.CompanyId = companyId;
        }

        public InsertInvoiceViewModel(Models.Invoice vm) {
            if (vm == null)
                return;
            this._invoice = vm;
            this._checkNumber = vm.InvoiceNumber;
            this._description = vm.Description;
            this._servicePerformedOn = vm.ServicePerfomanceOn;
            this._totalAmount = vm.TotalAmount;
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

        public double TotalAmount
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

        public Models.Invoice GetInvoice()
        {
            return _invoice;
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
                            Models.Invoice invoice = GetInvoiceInformationUI();
                            if (!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(invoice))
                            {
                                 //somethign in the DB went wrong
                                MessageBox.Show("if(!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(foreignKey,invoice)) Views\\InsertInvoice.xaml.cs line 82");
                                return;
                            }
                         
                        });
                }
                return saveButtonClickedCommand;
            }
        }

        private Models.Invoice GetInvoiceInformationUI()
        {
            Models.Invoice invoice = new Models.Invoice();
            invoice.Check = false;
            invoice.CompanyId = GetInvoice().CompanyId;
            invoice.CheckNumber = this.CheckNumber;
            invoice.Description = this.Description;
            invoice.ServicePerfomanceOn = this._servicePerformedOn;
            invoice.TotalAmount = this.TotalAmount;
            return invoice;
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
