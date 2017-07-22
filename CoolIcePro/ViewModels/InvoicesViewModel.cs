﻿using GalaSoft.MvvmLight.Command;
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

        public InvoicesViewModel()
        {
            _list = ProjectManager.Instance.CoolIceProDBHelper.GetAllInvoices();
        }

        public IEnumerable<Models.IModel> List
        {
            get
            {
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

                            InvoiceWindowLogic(invoice);
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

                            var customer = ProjectManager.Instance.CoolIceProDBHelper.GetCustomer(invoice.CompanyId);
                            if (customer == null)
                                return;

                            var page = new CoolIcePro.Views.Customer(new CustomerViewModel(customer));
                            Windows.GenericWindow gw = new Windows.GenericWindow(685, 625, string.Format("Invoice for {0}", customer.CompanyName), page);
                            gw.ShowDialog();
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

                            InvoiceWindowLogic(invoice);
                        });
                }
                return invoiceMenuItemClickCommand;
            }
        }

        public void FilterList(string searchText)
        {
            List = ProjectManager.Instance.CoolIceProDBHelper.SearchInvoices(searchText.Trim());
        }

        public void ResetList()
        {
            List = ProjectManager.Instance.CoolIceProDBHelper.GetAllInvoices();
        }

        private static void InvoiceWindowLogic(Models.Invoice invoice)
        {
            var page = new CoolIcePro.Views.InsertInvoice(new CoolIcePro.ViewModels.InsertInvoiceViewModel(invoice));
            var customer = ProjectManager.Instance.CoolIceProDBHelper.GetCustomer(invoice.CompanyId);
            Windows.GenericWindow gw = new Windows.GenericWindow(685, 625, string.Format("Invoice for {0}", customer.CompanyName), page);
            gw.ShowDialog();
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
