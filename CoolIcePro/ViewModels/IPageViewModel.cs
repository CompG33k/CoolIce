using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolIcePro.ViewModels
{
    public interface IPageViewModel
    {
        IEnumerable<Models.IModel> List { get; set; }
        void FilterList(string searchText);
        void ResetList();
    }
}
