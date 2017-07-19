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
        string _searchText;
        System.Windows.Controls.Page mainWindowPage;
        ICommand listViewSelectionChangedCommand;
        ICommand enterKeyPressedCommand;
      
        public event PropertyChangedEventHandler PropertyChanged;
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
        public string SearchText
        {
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged("SearchText");
                }
            }
            get
            {
                return _searchText;
            }
        }
     
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

        public ICommand EnterKeyPressedCommand
        {
            get
            {
                    if (enterKeyPressedCommand == null)
                    {
                        enterKeyPressedCommand = new RelayCommand<CoolIcePro.ViewModels.IPageViewModel>(
                            iViewModel =>
                            {
                                if (string.IsNullOrWhiteSpace(SearchText))
                                {
                                    if (iViewModel != null)
                                        iViewModel.ResetList();
                                    return;
                                }
                                if (iViewModel == null)
                                    return;
                                if (iViewModel.List == null)
                                    return;

                                iViewModel.FilterList(SearchText);
                            });
                    }
                return enterKeyPressedCommand;
            }
        }     
      
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        private static string GetPageSelectedTitle(System.Windows.Controls.SelectionChangedEventArgs args)
        {
            var sender = args.Source as ListView;
            var l = sender as ListView;
            var listItemView = l.SelectedItem as ListViewItem;

            var sp = listItemView.Content as StackPanel;
            var label = sp.Children[1] as Label;

            return label.Content as string;
        }
    }
}
