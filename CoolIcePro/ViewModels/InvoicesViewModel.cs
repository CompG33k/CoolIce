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
                    rowDoubleClickCommand = new RelayCommand<Models.IModel>(
                        selectedItem =>
                        {
                            var invoice = selectedItem as Models.Invoice;
                            if (invoice == null)
                                return;

                            var p = new CoolIcePro.Controls.PopupWindow("Invoice Details", new CoolIcePro.Views.InsertInvoice(new CoolIcePro.ViewModels.InsertInvoiceViewModel(invoice)));
                            p.Show();
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
                    customerMenuItemClickCommand = new RelayCommand<Models.IModel>(
                        selectedItem =>
                        {
                            var invoice =selectedItem as Models.Invoice;
                            if (invoice == null)
                                return;

                            var company = ProjectManager.Instance.CoolIceProDBHelper.GetCompany(invoice.CompanyId);
                            if (company == null)
                                return;
                                                        
                            var p = new CoolIcePro.Controls.PopupWindow("Customer Details", new CoolIcePro.Views.Customer(new CompanyViewModel(company)));
                            p.Show();
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
                    invoiceMenuItemClickCommand = new RelayCommand<Models.IModel>(
                        selectedItem =>
                        {
                            var invoice = selectedItem as Models.Invoice;
                            if (invoice == null)
                                return;

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
