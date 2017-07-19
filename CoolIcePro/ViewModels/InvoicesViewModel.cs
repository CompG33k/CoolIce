using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoolIcePro.ViewModels
{
    public class InvoicesViewModel : INotifyPropertyChanged, IPageViewModel
    {
        ICommand rowDoubleClickCommand;
        ICommand customerMenuItemClickCommand;
        ICommand invoiceMenuItemClickCommand;
        IEnumerable<Models.IModel> _list;

        //    _dataGrid.ItemsSource = ProjectManager.Instance.CoolIceProDBHelper.GetInvoices();
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<Models.IModel> List
        {
            get
            {
                var list = ProjectManager.Instance.CoolIceProDBHelper.GetInvoices();

                if (_list == null || _list != list)
                {
                    _list = list;
                }
                return _list;
            }
            set
            {
                if (_list != value)
                {
                    _list = value;
                    OnPropertyChanged("List");
                }
            }
        }

        public ICommand RowDoubleClickCommand
        {
            get
            {
                if (rowDoubleClickCommand == null)
                {
                    rowDoubleClickCommand = new RelayCommand<Models.Invoice>(
                        sender =>
                        {
                            var inv =sender as Models.Invoice;
                            System.Windows.MessageBox.Show(string.Format("Invoice id: {0}\nDouble row Clicked",inv.Id));
                            //var company = item as Models.Company;
                            //var p = new CoolIcePro.Controls.PopupWindow("Customer Details", new CoolIcePro.Views.Customer(new CompanyViewModel(company)));
                            //p.Show();
                        });
                }
                return rowDoubleClickCommand;
            }
        }

        public ICommand CustomerMenuItemClickCommand
        {
            get
            {
                if (customerMenuItemClickCommand == null)
                {
                    customerMenuItemClickCommand = new RelayCommand<Object>(
                        sender =>
                        {
                            var inv =sender as Models.Invoice;
                            if (inv != null)
                            {
                                System.Windows.MessageBox.Show(string.Format("CustomerMenuItemClickCommand id: {0}\nDouble row Clicked", inv.Id));
                            }
                            //var company = item as Models.Company;
                            //var p = new CoolIcePro.Controls.PopupWindow("Customer Details", new CoolIcePro.Views.Customer(new CompanyViewModel(company)));
                            //p.Show();
                        });
                }
                return customerMenuItemClickCommand;
            }
        }

        public ICommand InvoiceMenuItemClickCommand
        {
            get
            {
                if (invoiceMenuItemClickCommand == null)
                {
                    invoiceMenuItemClickCommand = new RelayCommand<Models.Invoice>(
                        sender =>
                        {
                            var inv =sender as Models.Invoice;
                            if (inv != null)
                            {
                                System.Windows.MessageBox.Show(string.Format("InvoiceMenuItemClickCommand id: {0}\nDouble row Clicked", inv.Id));
                            }

                            var invoice = sender as Models.Invoice;
                            var p = new CoolIcePro.Controls.PopupWindow("Invoice Details", new CoolIcePro.Views.InsertInvoice(new CoolIcePro.ViewModels.InsertInvoiceViewModel(invoice)));
                            p.Show();
                        });
                }
                return invoiceMenuItemClickCommand;
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
