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
    /// Interaction logic for Invoices.xaml
    /// </summary>
    public partial class Invoices : Page
    {
        public Invoices()
        {
            InitializeComponent();
            _dataGrid.ItemsSource = ProjectManager.Instance.CoolIceProDBHelper.GetInvoices();

        }
        private void resultDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                //Models.Company value = (Models.Company)_dataGrid.SelectedValue;
                //PopupWindow p = new PopupWindow("Customer Details", new Customer());
                //p.Show();
            }
        }
    }
}
