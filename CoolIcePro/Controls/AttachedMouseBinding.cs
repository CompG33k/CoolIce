using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoolIcePro.Controls
{

public sealed class RowDoubleClickHandler : FrameworkElement
{
   public sealed class RowDoubleClickHandler : FrameworkElement
	{
		public RowDoubleClickHandler(DataGrid dataGrid)
		{
			MouseButtonEventHandler handler = (sender, args) =>
			{
				var row = sender as DataGridRow;
				if (row != null && row.IsSelected)
				{
					var methodName = GetMethodName(dataGrid);

					var dataContextType = dataGrid.DataContext.GetType();
					var method = dataContextType.GetMethod(methodName);
					if (method == null)
					{
						throw new MissingMethodException(methodName);
					}

					method.Invoke(dataGrid.DataContext, null);
				}
			};

			dataGrid.LoadingRow += (s, e) =>
				{
					e.Row.MouseDoubleClick += handler;
				};

			dataGrid.UnloadingRow += (s, e) =>
				{
					e.Row.MouseDoubleClick -= handler;
				};
		}

		public static string GetMethodName(DataGrid dataGrid)
		{
			return (string)dataGrid.GetValue(MethodNameProperty);
		}

		public static void SetMethodName(DataGrid dataGrid, string value)
		{
			dataGrid.SetValue(MethodNameProperty, value);
		}

		public static readonly DependencyProperty MethodNameProperty = DependencyProperty.RegisterAttached(
			"MethodName",
			typeof(string),
			typeof(RowDoubleClickHandler),
			new PropertyMetadata((o, e) =>
			{
				var dataGrid = o as DataGrid;
				if (dataGrid != null)
				{
					new RowDoubleClickHandler(dataGrid);
				}
			}));
	}
}

 
    public class AttachedMouseBinding
    {
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(AttachedMouseBinding),
            new FrameworkPropertyMetadata(CommandChanged));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InputBinding inputBinding = d as InputBinding;
            ICommand command = e.NewValue as ICommand;
            if (inputBinding != null)
            {
                inputBinding.Command = command;
            }
        }
    } 
}
