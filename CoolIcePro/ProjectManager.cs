using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolIcePro.Models;
using System.Windows;
using CoolIcePro.Models;

namespace CoolIcePro
{
    class ProjectManager
    {
        private static ProjectManager _instance;
        private CoolIceProHelper _coolIceProDBHelper;

        private ProjectManager() {
            _coolIceProDBHelper = new CoolIceProHelper();
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
            set { _coolIceProDBHelper = value; }
        }

        public Company CurrentCompany { get; set; } 

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
