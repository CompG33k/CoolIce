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
