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
    public class CustomersViewModel : INotifyPropertyChanged, IPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ICommand mouseDoubleClickCommand;
        ICommand customerMenuItemClickCommand;
        IEnumerable<Models.IModel> _list;

        public CustomersViewModel()
        {
            _list = ProjectManager.Instance.CoolIceProDBHelper.GetAllSearchableCustomers(); 
        }

        public IEnumerable<Models.IModel> List
        {
            get
            {
                return _list;
            }
            set
            {

                _list = value;
                OnPropertyChanged("List");
            }
        }

        public ICommand MouseDoubleClickCommand
        {
            get
            {
                if (mouseDoubleClickCommand == null)
                {
                    mouseDoubleClickCommand = new RelayCommand<Models.IModel>(
                        selectedItem =>
                        {
                            var customer = selectedItem as Models.CustomerSearch;
                            if (customer != null)
                            {
                                CustomerWindowLogic(customer);
                            }
                        });
                }
                return mouseDoubleClickCommand;
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
                            var customer = selectedItem as Models.CustomerSearch;
                            if (customer != null)
                            {
                                CustomerWindowLogic(customer);
                            }
                        });
                }
                return customerMenuItemClickCommand;
            }
        }
        public void FilterList(string searchText)
        {
            List = ProjectManager.Instance.CoolIceProDBHelper.SearchCustomers(searchText.Trim());       
        }
     
        public void ResetList()
        {
            List = ProjectManager.Instance.CoolIceProDBHelper.GetAllSearchableCustomers();
        }
        private static void CustomerWindowLogic(Models.CustomerSearch customerSearch)
        {
            var customer = ProjectManager.Instance.CoolIceProDBHelper.GetCustomer(customerSearch.Id);
            var contact = ProjectManager.Instance.CoolIceProDBHelper.GetCustomerContacts(customerSearch.Id);
            customer.Contact = contact;
            var page = new CoolIcePro.Views.Customer(new CustomerViewModel(customer));
           
            Windows.GenericWindow gw = new Windows.GenericWindow(685, 625, string.Format("{0}", customerSearch.CompanyName), page);
            gw.ShowDialog();
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
