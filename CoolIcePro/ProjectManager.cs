using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolIcePro.Models;
using System.Windows;
using CoolIcePro.Models;
using CoolIcePro.Windows;

namespace CoolIcePro
{
    class ProjectManager
    {
        private static ProjectManager _instance;
        private readonly CoolIceProHelper _coolIceProDBHelper;
        private List<GenericWindow> _windowsOpen;

        private ProjectManager() {
            _coolIceProDBHelper = new CoolIceProHelper();
            _windowsOpen = new List<GenericWindow>();
        }

        public static ProjectManager Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new ProjectManager();
                }
                return _instance; }
        }

        public CoolIceProHelper CoolIceProDBHelper
        {
            get { return _coolIceProDBHelper; }
        }

        public Customer CurrentCompany { get; set; }
              

        public bool RemoveOpenWindow(string Id)
        {
            var window = _windowsOpen.FirstOrDefault(x => x.Tag.ToString().Equals(Id));
            if (window != null)
            {
                return _windowsOpen.Remove(window);
            }
            return false;
        }

        public bool AddWindow(GenericWindow newWindow)
        {
            if (_windowsOpen.Count > 5)
            {
                MessageBox.Show("Too many windows open, close some windows.");
                return false;
            }
            _windowsOpen.Add(newWindow);
            return true;
        }

        public string[] States
        {
            get { return _states; }
        }

        static string[] _states =
        {
                "Alabama",
                "Alaska",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "Florida",
                "Georgia",
                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Maryland",
                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",
                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Pennsylvania",
                "Rhode Island",
                "South Carolina",
                "South Dakota",
                "Tennessee",
                "Texas",
                "Utah",
                "Vermont",
                "Virginia",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming"
        };
    }
}
