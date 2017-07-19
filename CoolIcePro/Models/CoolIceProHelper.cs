using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace CoolIcePro.Models
{
    public class CoolIceProHelper : SQLiteDatabase
    {
        private static string CONNECTIONSTRING;
          

        /// <summary>
        /// Logic for connecting to databae and retrieveing invoice information.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static Invoice GetInvoice(string searchString)
        {
            return new Invoice();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public bool InsertCompany(Company company)
        {
            try
            {
                return Insert("'Company'",
                       new Dictionary<string, string>()
                    {
                        {"CompanyName" ,company.CompanyName},
                        {"Address" ,company.Address},
                        {"AddressExt" ,company.AddressExt},
                        {"City" ,company.City},
                        {"State" ,company.State},
                        {"Zipcode" ,company.Zipcode},
                        {"Telephone" ,company.Telephone},
                        {"Fax" ,company.Fax},
                        {"Email" ,company.Email},
                        {"Website" ,company.Website}
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="foreignKey"></param>
        /// <returns></returns>
        public bool InserContact(Contact contact, long foreignKey)
        {
            try
            {
                return Insert("'Contact'",
                      new Dictionary<string, string>()
                    {
                        {"Fname" ,contact.Fname},
                        {"Lname" ,contact.Lname},
                        {"Telephone" ,contact.Telephone},
                        {"Cellphone" ,contact.Cellphone},
                        {"Position" ,contact.Position},
                       {"fk_Id" ,foreignKey.ToString()}
                    }
                      );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public long GetContactForeignKey(Company company)
        {
            long foreignKey = ExecuteScalar(string.Format("Select Id from 'Company' where CompanyName = '{0}' AND Address ='{1}' AND AddressExt = '{2}' AND City = '{3}' AND Telephone = '{4}' AND Fax = '{5}' AND Email = '{6}' AND Website = '{7}'",
                   company.CompanyName,
                   company.Address,
                   company.AddressExt,
                   company.City,
                   company.State,
                   company.Zipcode,
                   company.Telephone,
                   company.Fax,
                   company.Email,
                   company.Website
                ));
            return foreignKey;
        }

        public bool InsertInvoice(long companyId,Invoice invoice)
        {
            try
            {
                return Insert("'Invoice'",
                      new Dictionary<string, string>()
                    {
                        {"Date" ,invoice.Date.ToShortDateString()},
                        {"Description" ,invoice.Description},
                        {"InvoiceNumber" ,invoice.InvoiceNumber},
                        {"ServicePerformanceOn" ,invoice.ServicePerfomanceOn},
                        {"TotalAmount" ,invoice.TotalAmount.ToString()},
                        {"CompanyId" ,companyId.ToString()}
                    }
                      );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            
            DataTable table = this.GetDataTable("select * from company");
            //var buffer2 = table["Company"];
            DataTableReader dtr = table.CreateDataReader();
            List<Company> _list = new List<Company>();
            while (dtr.Read())
            {
                _list.Add(new Company()
                {
                    Id = (long)dtr["Id"],
                    CompanyName = dtr["CompanyName"] as string,
                    Address = dtr["Address"] as string,
                    AddressExt = dtr["AddressExt"] as string,
                    City = dtr["City"] as string,
                    State = dtr["State"] as string,
                    Zipcode = dtr["Zipcode"] as string,
                    Telephone = dtr["Telephone"] as string,
                    Fax = dtr["Fax"] as string,
                    Email = dtr["Email"] as string,
                    Website = dtr["Website"] as string

                });
            }
            return _list;
        }
        internal IEnumerable<Invoice> GetCustomerInvoices(long companyId)
        {

            DataTable table = this.GetDataTable(string.Format("select Id,CompanyId, datetime(Date) as Date,Description,InvoiceNumber,ServicePerformanceOn,TotalAmount,Warranty,IsCheck from Invoice where CompanyId ='{0}'", companyId.ToString()));

            DataTableReader dtr = table.CreateDataReader();
            List<Invoice> _list = new List<Invoice>();
            while (dtr.Read())
            {
                _list.Add(new Invoice()
                {
                    Id = (long)dtr["Id"],
                    CompanyId = (long)dtr["CompanyId"],
                 //   Date = DateTime.Parse( dtr["Date"])
                    Description = dtr["Description"] as string,
                    InvoiceNumber = dtr["InvoiceNumber"] as string,
                    ServicePerfomanceOn = dtr["ServicePerformanceOn"] as string,
                    TotalAmount = (double)dtr["TotalAmount"],
                  //  Warranty = ((int)dtr["Warranty"] == 1) as int? true: false,
                  //  Check =  ((int)dtr["IsCheck"]== 1)? true: false
                });
            }
            return _list;
        }
        internal IEnumerable<Invoice> GetInvoices()
        {
            //select strftime('%m/%d/%Y',substr(colName,0,20)) from tablename

            DataTable table = this.GetDataTable("select Id,CompanyId, datetime(Date) as Date,Description,InvoiceNumber,ServicePerformanceOn,TotalAmount,Warranty,IsCheck  from invoice");
            //var buffer2 = table["Company"];
            DataTableReader dtr = table.CreateDataReader();
            List<Invoice> _list = new List<Invoice>();
            while (dtr.Read())
            {
                _list.Add(new Invoice()
                {
                    Id = (long)dtr["Id"],
                    CompanyId = (long)dtr["CompanyId"],
                  //  Date = DateTime.Parse( dtr["Date"].ToString()),
                    Description = dtr["Description"] as string,
                    InvoiceNumber = dtr["InvoiceNumber"] as string,
                    ServicePerfomanceOn = dtr["ServicePerformanceOn"] as string,
                    TotalAmount = (double)dtr["TotalAmount"],
                  //  Warranty = (bool)dtr["Warranty"],
                  //  Check = (bool)dtr["Check"]
                });
            }
            return _list;
        }
    }
}
