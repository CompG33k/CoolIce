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
using System.Windows.Shapes;

namespace CoolIcePro.Controls
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : MetroWindow
    {
        //public PopupWindow()
        //{
        //    InitializeComponent();
            
        //    this.Activate(); 
        //}
        public PopupWindow(string windowTitle, Page page)
        {
            InitializeComponent();
            this.Title = windowTitle;
            _frame.Navigate(page);            
            this.Activate(); 
        }
    }
}
