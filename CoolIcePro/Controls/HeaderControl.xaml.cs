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

namespace CoolIcePro.Controls
{
    /// <summary>
    /// Interaction logic for HeaderControl.xaml
    /// </summary>
    public partial class HeaderControl : UserControl
    {

        // Register the routed event
        public static readonly RoutedEvent CustomerButtonClickEvent =
            EventManager.RegisterRoutedEvent("CustomerButton", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(HeaderControl));

        // Register the routed event
        public static readonly RoutedEvent InvoiceButtonClickEvent =
            EventManager.RegisterRoutedEvent("InvoiceButton", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(HeaderControl));

        // Register the routed event
        public static readonly RoutedEvent AnnualReportButtonClickEvent =
            EventManager.RegisterRoutedEvent("AnnualReportButton", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(HeaderControl));

        public static readonly RoutedEvent AccntReceivableButtonClickEvent =
            EventManager.RegisterRoutedEvent("AccntReceivableButton", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(HeaderControl));

        public static readonly RoutedEvent MonthlyInvoiceButtonClickEvent =
            EventManager.RegisterRoutedEvent("MonthlyInvoiceButton", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(HeaderControl));

        public static readonly RoutedEvent AccntPayablesButtonClickEvent =
            EventManager.RegisterRoutedEvent("AccntPayablesButton", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(HeaderControl));

        
        public HeaderControl()
        {
            InitializeComponent();
        }

       
        // .NET wrapper
        public event RoutedEventHandler CustomerButton
        {
            add { AddHandler(CustomerButtonClickEvent, value); } 
            remove { RemoveHandler(CustomerButtonClickEvent, value); }
        }

        public event RoutedEventHandler InvoiceButton
        {
            add { AddHandler(InvoiceButtonClickEvent, value); }
            remove { RemoveHandler(InvoiceButtonClickEvent, value); }
        }

        public event RoutedEventHandler AnnualReportButton
        {
            add { AddHandler(AnnualReportButtonClickEvent, value); }
            remove { RemoveHandler(AnnualReportButtonClickEvent, value); }
        }

        public event RoutedEventHandler AccntReceivableButton
        {
            add { AddHandler(AccntReceivableButtonClickEvent, value); }
            remove { RemoveHandler(AccntReceivableButtonClickEvent, value); }
        }

        public event RoutedEventHandler MonthlyInvoiceButton
        {
            add { AddHandler(MonthlyInvoiceButtonClickEvent, value); }
            remove { RemoveHandler(MonthlyInvoiceButtonClickEvent, value); }
        }

        public event RoutedEventHandler AccntPayablesButton
        {
            add { AddHandler(AccntPayablesButtonClickEvent, value); }
            remove { RemoveHandler(AccntPayablesButtonClickEvent, value); }
        }
        private void CustomerClickEventHandler(object sender, RoutedEventArgs e)
        {
            // Raise the routed event "selected"
            RaiseEvent(new RoutedEventArgs(HeaderControl.CustomerButtonClickEvent));
        }

        private void InvoiceClickEventHandler(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(HeaderControl.InvoiceButtonClickEvent));
        }

        private void AnnualReportEventHandler(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(HeaderControl.AnnualReportButtonClickEvent));
        }

        private void AccntReceivablesEventHandler(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(HeaderControl.AccntReceivableButtonClickEvent));
        }

        private void MonthlyInvoiceClickEventHndler(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(HeaderControl.MonthlyInvoiceButtonClickEvent));
        }

        private void AccntPayablesClickEventHandler(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(HeaderControl.AccntPayablesButtonClickEvent));
        }

       

        
    }
}
