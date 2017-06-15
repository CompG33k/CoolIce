using CoolIcePro.Views;
using MahApps.Metro.Controls;
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

namespace CoolIcePro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //Check if the event is not raised by the visual studio designer
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
            _frame.Navigate(new Customers());
            //_listView.ItemsSource = new List<String>() { "Customer", "Account Receivables", "Account Payables","Insert Company", "Insert Invoice" };
         //   _listView.ItemsSource = new List<String>() { "Customers", "Account Receivables", "Account Payables" };
        }
       
        private void ListViewSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            var l = sender as ListView;
            var listItemView = l.SelectedItem as ListViewItem;

            var sp = listItemView.Content as StackPanel;
            var label = sp.Children[1] as Label;
            
            var page = label.Content as string;
            var type = _frame.Content.GetType();
            
            if (type.Name == page) 
                return;
            switch (page)
            {
                case "Customers":
                    _frame.Navigate(new Customers());
                    break;
                case "Invoices":
                    _frame.Navigate(new Invoices());
                    break;
                //case "Insert Invoice":
                       
                //   // CoolIcePro.Controls.PopupWindow win = new Controls.PopupWindow();
                //    _frame.Navigate(new InsertInvoice());
                //  //  win.ShowDialog();
                        
                //    break;
                //case "Account Receivables":
                //    _frame.Navigate(new AccountReceivables());
                //    break;
                //case "Account Payables":
                //    _frame.Navigate(new AccountPayables());
                //    break;
                //case "Insert Company":
                //    _frame.Navigate(new InsertCompany());
                //    break;
            }
        }

        private void GroupButtonClickedEventHandler(object sender, RoutedEventArgs e)
        {
            if(FirstColumn.Width.Value == 135)
            {
                GridLength g = new GridLength(38);
                FirstColumn.Width = g;
            }
            else{
                GridLength g = new GridLength(135);
                FirstColumn.Width = g;
            }
        }

    }
}
