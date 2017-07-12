using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoolIcePro.ViewModels
{
    public class CustomersViewModel
    {
        ICommand mouseDoubleClickCommand;
        IEnumerable<Models.Company> customerList;
       

        public IEnumerable<Models.Company> CustomerList
        {
            get
            {
                var tempList = ProjectManager.Instance.CoolIceProDBHelper.GetAllCompanies();
                if (customerList == null || customerList != tempList)
                {
                    customerList = tempList;
                }
                return customerList;
            }
            set
            {
                if (customerList != value)
                {
                    customerList = value;
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

    }
}
