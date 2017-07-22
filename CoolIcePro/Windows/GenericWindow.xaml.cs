using CoolIcePro.ViewModels;
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

namespace CoolIcePro.Windows
{
    /// <summary>
    /// Interaction logic for GenericWIndow.xaml
    /// </summary>
    public partial class GenericWindow : MetroWindow
    {
        public GenericWindow(double height,double width,string windowTitle, Page page)
        {
            InitializeComponent();
            DataContext = new GenericWindowViewModel(height, width, windowTitle, page);
        }
    }
}
