using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using AutoMapper;


namespace CoolIcePro.Models
{
    public class CoolIceProHelper : SQLiteDatabase
    {
        //private static string CONNECTIONSTRING;
        static string SELECTCUSTOMERQUERY = "select Id,CompanyName,Address,AddressExt,City,State,Zipcode,Telephone,Fax,Email,Website from company";
        static string SELECTCONTACTQUERY = "select Id, Fname,Lname,Telephone,Cellphone,Position,fk_id from Contact";
        static string SELECTINVOICEQUERY = "select Id,CompanyId, datetime(Date) as Date,Description,InvoiceNumber,ServicePerformanceOn,TotalAmount,Warranty,IsCheck from Invoice";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool InsertCustomerEx(Customer customer)
        {
            try
            {
                return Insert("'Company'",
                       new Dictionary<string, string>()
                    {
                        {"CompanyName" ,customer.CompanyName},
                        {"Address" ,customer.Address},
                        {"AddressExt" ,customer.AddressExt},
                        {"City" ,customer.City},
                        {"State" ,customer.State},
                        {"Zipcode" ,customer.Zipcode},
                        {"Telephone" ,customer.Telephone},
                        {"Fax" ,customer.Fax},
                        {"Email" ,customer.Email},
                        {"Website" ,customer.Website}
                    });
            }
            catch 
            {
                throw;
            }
            return false;
        }
        
        /// <summary>
        /// Inserts the customer. and returns the PrimaryKey.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        public long InsertCustomer(Customer customer)
        {
            long number = -1;
            try
            {
                number = InsertGetLastRowId("'Company'",
                       new Dictionary<string, string>()
                    {
                        {"CompanyName" ,customer.CompanyName},
                        {"Address" ,customer.Address},
                        {"AddressExt" ,customer.AddressExt},
                        {"City" ,customer.City},
                        {"State" ,customer.State},
                        {"Zipcode" ,customer.Zipcode},
                        {"Telephone" ,customer.Telephone},
                        {"Fax" ,customer.Fax},
                        {"Email" ,customer.Email},
                        {"Website" ,customer.Website}
                    });
            }
            catch 
            {
                throw;
            }
            return number;
        }
          public bool InsertInvoice(long companyId, Invoice invoice)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="customerPrimaryId"></param>
        /// <returns></returns>
        public bool InserCustomerContact(long customerPrimaryId,Contact contact)
        {
            try
            {
                return Insert("'Contact'",
                      new Dictionary<string, string>()
                    {
                        {"Fname" ,contact.FirstName},
                        {"Lname" ,contact.LastName},
                        {"Telephone" ,contact.Telephone},
                        {"Cellphone" ,contact.Cellphone},
                        {"Position" ,contact.Position},
                       {"fk_Id" ,customerPrimaryId.ToString()}
                    }
                      );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        
        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                using (DataTable table = this.GetDataTable(SELECTCUSTOMERQUERY))
                {
                    return from DataRow row in table.Rows
                            select GetCustomerClassInstance(row);
                }
            }catch
            {
                throw;
            }
        }
        public Models.Customer GetCustomer(long Id)
        {
            try
            {
                string query = string.Format("{0} where id = '{1}'",SELECTCUSTOMERQUERY, Id);
                using (DataTable table = this.GetDataTable(query))
                {
                    return (from DataRow row in table.Rows
                           select GetCustomerClassInstance(row)).SingleOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }
        

        public async Task<IEnumerable<Models.Contact>> GetCustomerContacts(long customerId)
        {
            try
            {
                string query = string.Format("{0} where fk_id = '{0}'",SELECTCONTACTQUERY, customerId);
                using (DataTable table = this.GetDataTable(query))
                {
                    return (from DataRow row in table.Rows
                           select GetContactClassInstance(row));
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Invoice> GetCustomerInvoices(long companyId)
        {
             try
             {
                string query = string.Format("{0} where CompanyId ='{1}'",SELECTINVOICEQUERY, companyId.ToString());
                using(DataTable table = this.GetDataTable(query))
                {
                    return (from DataRow row in table.Rows
                       select GetInvoiceClassInstance(row));
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Invoice> GetAllInvoices()
        {
            try
             {
                using(DataTable table = this.GetDataTable(SELECTINVOICEQUERY))
                {
                    return (from DataRow row in table.Rows
                       select GetInvoiceClassInstance(row));                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Models.Customer> SearchCustomers(string searchText)
        {
            try
            {
                string query = string.Format("{0} where CompanyName like '%{1}%' or Address like '%{1}%' or AddressExt like '%{1}%' or City like '%{1}%' or State like '%{1}%'",SELECTCUSTOMERQUERY, searchText);
                using(DataTable table = this.GetDataTable(query))
                {
                    return (from DataRow row in table.Rows
                       select GetCustomerClassInstance(row));
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Invoice> SearchInvoices(string searchText)
        {
            List<Invoice> _list = new List<Invoice>();
            string query = string.Format("select Id,CompanyId, datetime(Date) as Date,Description,InvoiceNumber,ServicePerformanceOn,TotalAmount,Warranty,IsCheck from invoice where Date like '%{0}%' or Description like '%{0}%' or ServicePerformanceOn like '%{0}%' or TotalAmount like '%{0}%'", searchText);
            using(DataTable table = this.GetDataTable(query))
            using (DataTableReader dtr = table.CreateDataReader())
            {
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
            }
            return _list;
        }

      

        private static Contact GetContactClassInstance(DataRow row)
        {
            return new Models.Contact()
            {
                Id = row.Field<long>("Id"),
                CompanyId = row.Field<long>("fk_Id"),
                FirstName = row.Field<string>("Fname"),
                LastName = row.Field<string>("Lname"),
                Telephone = row.Field<string>("Telephone"),
                Cellphone = row.Field<string>("Cellphone"),
                Position = row.Field<string>("Position")
            };
        }

        private static Models.Customer GetCustomerClassInstance(DataRow row)
        {
            return new Models.Customer()
            {
                Id = row.Field<long>("Id"),
                CompanyName = row.Field<string>("CompanyName"),
                Address = row.Field<string>("Address"),
                AddressExt = row.Field<string>("AddressExt"),
                City = row.Field<string>("City"),
                State = row.Field<string>("State"),
                Zipcode = row.Field<string>("Zipcode"),
                Telephone = row.Field<string>("Telephone"),
                Fax = row.Field<string>("Fax"),
                Email = row.Field<string>("Email"),
                Website = row.Field<string>("Website")
            };
        }

        private static Models.Invoice GetInvoiceClassInstance(DataRow row)
        {
            return new Invoice()
            {
                Id = row.Field<long>("Id"),
                CompanyId = row.Field<long>("CompanyId"),
                //   Date = DateTime.Parse( dtr["Date"])
                Description = row["Description"] as string,
                InvoiceNumber = row["InvoiceNumber"] as string,
                ServicePerfomanceOn = row["ServicePerformanceOn"] as string, 
                TotalAmount = row.Field<double>("TotalAmount")
                //  Warranty = ((int)dtr["Warranty"] == 1) as int? true: false,
                //  Check =  ((int)dtr["IsCheck"]== 1)? true: false
            };
        }
      
    }
}
