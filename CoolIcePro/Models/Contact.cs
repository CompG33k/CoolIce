using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolIcePro.Models
{
    [Table("Contact")]
    public class Contact
    {
        [PrimaryKey]
        public long Id { get; set; }
        
        [Column("fk_id")]
        public long CompanyId { get; set; }
        [Column("Fname")]
        public string FirstName{get;set;}
        [Column("Lname")]
        public string LastName{get;set;}
        [Column("Telephone")]
        public string Telephone{get;set;}
        [Column("Cellphone")]
        public string Cellphone{get;set;}
        [Column("Position")]
        public string Position{get;set;}
    }
}
