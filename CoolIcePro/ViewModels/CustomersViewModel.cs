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
    public class CustomersViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ICommand mouseDoubleClickCommand;
        ICommand customerMenuItemClickCommand;
        IEnumerable<Models.Company> customerList;
       

        public IEnumerable<Models.Company> CustomerList
        {
            get
            {
                var tempList = ProjectManager.Instance.CoolIceProDBHelper.GetAllCompanies();
                if (customerList == null || customerList != tempList)
                {
                    customerList = tempList;
                    //OnPropertyChanged("CustomerList");
                }
                return customerList;
            }
            set
            {
                if (customerList != value)
                {
                    customerList = value;
                    OnPropertyChanged("CustomerList");
                }
            }
        }

        public ICommand MouseDoubleClickCommand
        {
            get
            {
                if (mouseDoubleClickCommand == null)
                {
                    mouseDoubleClickCommand = new RelayCommand<Models.Company>(
                        item =>
                        {
                            var company = item as Models.Company;
                            var p = new CoolIcePro.Controls.PopupWindow("Customer Details", new CoolIcePro.Views.Customer(new CompanyViewModel(company)));
                            p.Show();
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
                    customerMenuItemClickCommand = new RelayCommand<Object>(
                        sender =>
                        {
                            var inv = sender as Models.Invoice;
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
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
