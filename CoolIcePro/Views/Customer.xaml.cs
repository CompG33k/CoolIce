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
using Microsoft.Office.Interop.Excel;
using CoolIcePro.ViewModels;
using System.Text.RegularExpressions;

namespace CoolIcePro.Views
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : System.Windows.Controls.Page
    {
        private readonly Regex _regex;
        public Customer(CustomerViewModel company)
        {
            InitializeComponent();
            _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
            this.DataContext = company;
        }

        private void ClickEventHandler(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook ;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet ;
            object misValue = System.Reflection.Missing.Value;
            
        //    xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";

            xlWorkBook.SaveAs("csharp-Excel.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook);
            //releaseObject(xlApp);

            MessageBox.Show("Excel file created , you can find the file c:\\csharp-Excel.xls");
        }

        private void ButtonClickedEventHandler(object sender, RoutedEventArgs e)
        {
            foreach (TextBlock tb in CoolIcePro.Util.UiUtil.FindVisualChildren<TextBlock>(this))
            {
                tb.FontStyle = new FontStyle();
            }
        }

        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

    }
}
