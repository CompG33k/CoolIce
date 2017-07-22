using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoolIcePro.ViewModels
{
    public interface IGenericWindowViewModel
    {
        double Height { get; set; }
        double Width { get; set; }
        string Title { get; set; }
        Page CurrentPage { get; set; } 
    }
}
