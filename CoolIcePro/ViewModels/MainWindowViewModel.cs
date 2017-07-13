using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CoolIcePro.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        System.Windows.Controls.Page mainWindowPage;
        ICommand listViewSelectionChangedCommand;

        public event PropertyChangedEventHandler PropertyChanged;        
     
        public MainWindowViewModel()
        {
            mainWindowPage = new CoolIcePro.Views.Customers();
        }

        public ICommand ListViewSelectionChangedCommand
        {
            get
            {
                if (listViewSelectionChangedCommand == null)
                {
                    listViewSelectionChangedCommand = new RelayCommand<System.Windows.Controls.SelectionChangedEventArgs>(
                        args =>
                        {
                            var pageSelectedTitle = GetPageSelectedTitle(args);
                            
                            if (MainWindowPage.Title == pageSelectedTitle)
                                    return;

                            switch (pageSelectedTitle)
                            {
                                case "Customers":
                                    MainWindowPage =new CoolIcePro.Views.Customers();
                                    break;
                                case "Invoices":
                                    MainWindowPage =new CoolIcePro.Views.Invoices();
                                    break;
                            }
                        });
                }
                return listViewSelectionChangedCommand;
            }
        }

        public System.Windows.Controls.Page MainWindowPage
        {
            get { return mainWindowPage; }
            set
            {
                if (mainWindowPage.Equals(value))
                    return;
                mainWindowPage = value;
                OnPropertyChanged("MainWindowPage");
            }
        }

        private static string GetPageSelectedTitle(SelectionChangedEventArgs args)
        {
            var sender = args.Source as ListView;
            var l = sender as ListView;
            var listItemView = l.SelectedItem as ListViewItem;

            var sp = listItemView.Content as StackPanel;
            var label = sp.Children[1] as Label;

            return label.Content as string;
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
