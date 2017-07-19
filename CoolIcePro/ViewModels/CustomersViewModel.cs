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
    public class CustomersViewModel : INotifyPropertyChanged, IPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ICommand mouseDoubleClickCommand;
        ICommand customerMenuItemClickCommand;
        IEnumerable<Models.IModel> _list;
        public CustomersViewModel()
        {
            _list = ProjectManager.Instance.CoolIceProDBHelper.GetAllCompanies(); 
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

        public ICommand MouseDoubleClickCommand
        {
            get
            {
                if (mouseDoubleClickCommand == null)
                {
                    mouseDoubleClickCommand = new RelayCommand<Models.IModel>(
                        selectedItem =>
                        {
                            var company = selectedItem as Models.Company;
                            if (company != null)
                            {
                                var p = new CoolIcePro.Controls.PopupWindow("Customer Details", new CoolIcePro.Views.Customer(new CompanyViewModel(company)));
                                p.Show();
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
                            var company = selectedItem as Models.Company;
                            if (company != null)
                            {
                                var p = new CoolIcePro.Controls.PopupWindow("Customer Details", new CoolIcePro.Views.Customer(new CompanyViewModel(company)));
                                p.Show();
                            }
                        });
                }
                return customerMenuItemClickCommand;
            }
        }
        public void FilterList(string searchText)
        {

            List = ProjectManager.Instance.CoolIceProDBHelper.SearchCompanies(searchText.Trim());       
        }
     
        public void ResetList()
        {
            List = ProjectManager.Instance.CoolIceProDBHelper.GetAllCompanies();
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
