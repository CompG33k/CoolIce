using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolIcePro.Models
{
    /// <summary>
    /// This s the Invoice Class for data.
    /// </summary>
    public class Invoice
    {
        public DateTime Date { get; set; }
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string InvoiceNumber { get; set; }
        public string ServicePerfomanceOn { get;  set; }
        public string Description { get; set; }
        public double TotalAmount { get; set; }
        public bool Warranty { get; set; }
        public bool Check { get; set; }
    }
}
