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
        ICommand createInvoiceButtonClickedCommand;

        string _servicePerformedOn = string.Empty;
        string _checkNumber = string.Empty;
        string _description = string.Empty;
        string _invoiceNumber = string.Empty;
        double _totalAmount;
        bool _isCheck;

        Models.Invoice _invoice;

        public InsertInvoiceViewModel(InsertInvoiceViewModel lhs)
        {
            this.ServicePerformedOn = lhs.ServicePerformedOn;
            this.CheckNumber = lhs.CheckNumber;
            this.TotalAmount = lhs.TotalAmount;
            this.Description = lhs.Description;
            this.InvoiceNumber = lhs.InvoiceNumber;
            this._invoice = lhs.GetInvoice();
        }
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
            this._isCheck = vm.Check;
            this._invoiceNumber = vm.InvoiceNumber;
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

        public bool IsCheck
        {
            set
            {
                if (_isCheck != value)
                {
                    _isCheck = value;
                    OnPropertyChanged("IsCheck");
                }
            }
            get
            {
                return _isCheck;
            }
        }

        public string CheckNumber
        {
            set
            {
                if (_checkNumber != value)
                {
                    _checkNumber = value;
                    OnPropertyChanged("CheckNumber");
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
                if (_description != value)
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
        public string InvoiceNumber
        {
            set
            {
                if (_invoiceNumber != value)
                {
                    _invoiceNumber = value;
                    OnPropertyChanged("InvoiceNumber");
                }
            }
            get
            {
                return _invoiceNumber;
            }
        }
        
        public Models.Invoice GetInvoice()
        {
            return _invoice;
        }

        public ICommand EditButtonClickedCommand
        {
            get
            {
                if (saveButtonClickedCommand == null)
                {
                    saveButtonClickedCommand = new RelayCommand<object>(
                        sender =>
                        {
                            Models.Invoice invoice = GetInvoiceInformationUI();
                            if (!ProjectManager.Instance.CoolIceProDBHelper.UpdateInvoice(invoice))
                            {
                                //somethign in the DB went wrong
                                MessageBox.Show("if(!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(foreignKey,invoice)) Views\\InsertInvoice.xaml.cs line 82");
                                return;
                            }

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
                return saveButtonClickedCommand;
            }
        }

        public ICommand CreateInvoiceButtonClickedCommand
        {
            get
            {
                if (createInvoiceButtonClickedCommand == null)
                {
                    createInvoiceButtonClickedCommand = new RelayCommand<object>(
                        sender =>
                        {
                            Models.Invoice invoice = GetInvoiceInformationUI();

                            if (!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(invoice))
                            {
                                //somethign in the DB went wrong
                                MessageBox.Show("if(!ProjectManager.Instance.CoolIceProDBHelper.InsertInvoice(foreignKey,invoice)) Views\\InsertInvoice.xaml.cs line 82");
                                return;
                            }

                            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            if (window != null)
                            {
                                window.Close();
                            }
                        });
                }
                return createInvoiceButtonClickedCommand;
            }
        }

        private Models.Invoice GetInvoiceInformationUI()
        {
            Models.Invoice invoice = new Models.Invoice();
            invoice.Check = this.IsCheck;
            invoice.InvoiceNumber = this.InvoiceNumber;
            invoice.CompanyId = GetInvoice().CompanyId;
            invoice.CheckNumber = this.CheckNumber?? string.Empty;
            invoice.Description = this.Description?? string.Empty;
            invoice.ServicePerfomanceOn = this.ServicePerformedOn?? string.Empty;
            invoice.TotalAmount = this.TotalAmount;
            return invoice;
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
