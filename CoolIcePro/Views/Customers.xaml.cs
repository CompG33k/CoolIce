using CoolIcePro.Controls;
using CoolIcePro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoolIcePro.Views
{
    /// <summary>
    /// Interaction logic for AllCustomers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        public Customers()
        {
            InitializeComponent();
            _dataGrid.ItemsSource = ProjectManager.Instance.CoolIceProDBHelper.GetAllCompanies();
            
        }
        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                var company = _dataGrid.SelectedItem as Models.Company;
                PopupWindow p = new PopupWindow("Customer Details", new Customer(new CompanyViewModel(company)));
                p.Show();
            }
        }
      
    }
}
