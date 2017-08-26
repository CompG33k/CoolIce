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
        string _totalAmount;
        bool _isCheck;
        DateTime _date;

        Models.Invoice _invoice;

        public InsertInvoiceViewModel(InsertInvoiceViewModel lhs)
        {
            this.ServicePerformanceOn = lhs.ServicePerformanceOn;
            this.CheckNumber = lhs.CheckNumber;
            this.TotalAmount = lhs.TotalAmount;
            this.Description = lhs.Description;
            this.InvoiceNumber = lhs.InvoiceNumber;
            this.Date = lhs.Date;
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
            this.Date = vm.Date;
            this.CheckNumber = vm.CheckNumber;
            this.Description = vm.Description;
            this.ServicePerformanceOn = vm.ServicePerformanceOn;
            this.TotalAmount = String.Format("{0:C}",vm.TotalAmount.ToString());//;String.Format("{0:C}
            this.IsCheck = vm.Check;
            this.InvoiceNumber = vm.InvoiceNumber;
        }

        public string ServicePerformanceOn
        {
            set
            {
                if (_servicePerformedOn != value)
                {
                    _servicePerformedOn = value;
                    OnPropertyChanged("ServicePerformanceOn");
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

        public DateTime Date
        {
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
            get
            {
                return _date;
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
            invoice.Id = _invoice.Id;
            invoice.Date = this.Date;
            invoice.Check = this.IsCheck;
            invoice.InvoiceNumber = this.InvoiceNumber;
            invoice.CompanyId = GetInvoice().CompanyId;
            invoice.CheckNumber = this.CheckNumber?? string.Empty;
            invoice.Description = this.Description?? string.Empty;
            invoice.ServicePerformanceOn = this.ServicePerformanceOn ?? string.Empty;
            invoice.TotalAmount = double.Parse(this.TotalAmount, System.Globalization.NumberStyles.Currency); 
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
