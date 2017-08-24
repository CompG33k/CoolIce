using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using AutoMapper;
using CoolIcePro.SqlLiteResources;


namespace CoolIcePro.Models
{
    public class CoolIceProHelper : SQLiteDatabase
    {
        static string SELECTCUSTOMERQUERY = "select Id,CompanyName,Address,AddressExt,City,State,Zipcode,Telephone,Fax,Email,Website from company";
        static string SELECTCONTACTQUERY = "select Id, Fname,Lname,Telephone as contactTelephone,Cellphone as contactCellphone,Position,fk_id from Contact";
        static string SELECTINVOICEQUERY = "select Id,CompanyId, Date,Description,InvoiceNumber,ServicePerformanceOn,TotalAmount,Warranty,IsCheck from Invoice";

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

        public IEnumerable<CustomerSearch> GetAllSearchableCustomers()
        {
            string query = string.Format("select c.Id,c.CompanyName,c.Address,c.AddressExt,c.City,c.State,c.Zipcode,c.Telephone,c.Fax,c.Email,c.Website, con.Fname, con.Lname, con.Telephone as contactTelephone, con.Position, con.fk_id, con.Cellphone as contactCellPhone from company c LEFT JOIN Contact con ON c.Id =con.fk_id");
            using (DataTable table = this.GetDataTable(query))
            {
                 return from DataRow row in table.Rows
                        select GetSearchableCustomerClassInstance(row);
            }
        }

        private CustomerSearch GetSearchableCustomerClassInstance(DataRow row)
        {
            Customer customer = GetCustomerClassInstance(row);
            customer.Contact = GetContactClassInstance(row);
            return new CustomerSearch(customer);
        }

        public Models.Customer GetCustomer(long Id)
        {
            string query = string.Format("{0} where id = '{1}'", SELECTCUSTOMERQUERY, Id);
            using (DataTable table = this.GetDataTable(query))
            {
                return (from DataRow row in table.Rows
                        select GetCustomerClassInstance(row)).SingleOrDefault();
            }
        }
        

        public Models.Contact GetCustomerContacts(long customerId)
        {
            string query = string.Format("{0} where fk_id = '{1}'",SELECTCONTACTQUERY, customerId);
            using (DataTable table = this.GetDataTable(query))   
            {
                return (from DataRow row in table.Rows
                        select GetContactClassInstance(row)).FirstOrDefault();   
            }           
        }

        public IEnumerable<Invoice> GetCustomerInvoices(long companyId)
        {
            string query = string.Format("{0} where CompanyId ='{1}'",SELECTINVOICEQUERY, companyId.ToString());
            using(DataTable table = this.GetDataTable(query))
            {
                return (from DataRow row in table.Rows
                       select GetInvoiceClassInstance(row));
            }
        }

        public Models.Invoice GetInvoice(long invoiceId)
        {
            string query = string.Format("{0} where Id ='{1}'", SELECTINVOICEQUERY, invoiceId.ToString());
            using (DataTable table = this.GetDataTable(query))
            {
                return (from DataRow row in table.Rows
                        select GetInvoiceClassInstance(row)).FirstOrDefault();
            }
        }

        public IEnumerable<Models.InvoiceSearch> GetAllInvoices()
        {
            string query = string.Format("select i.Id,i.CompanyId, i.Date, c.CompanyName ,i.Warranty,i.Description,i.InvoiceNumber,i.ServicePerformanceOn,i.TotalAmount , con.Fname,con.Lname from invoice i Left JOIN Company c ON  i.CompanyId = c.Id Left Join Contact con ON i.CompanyId = con.fk_id");
            using (DataTable table = this.GetDataTable(query))
            {
                return (from DataRow row in table.Rows 
                        select new InvoiceSearch{
                            Id = row.Field<long>("Id"),
                            CompanyId = row.Field<long>("CompanyId"),
                            CompanyName = row.Field<string>("CompanyName"),
                            ContactFName = row.Field<string>("Fname"),
                            ContactLName = row.Field<string>("Lname"),
                            Date = DateTime.Parse(row["Date"].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                            Description = row["Description"] as string,
                            InvoiceNumber = row["InvoiceNumber"] as string,
                            ServicePerfomanceOn = row["ServicePerformanceOn"] as string, 
                            TotalAmount = row.Field<double>("TotalAmount"),
                            Warranty = (row.Field<long>("Warranty")  == 1) ? true: false
                        });
            }
        }

        public IEnumerable<Models.CustomerSearch> SearchCustomers(string searchText)
        {
            string query = string.Format("select c.Id,c.CompanyName,c.Address,c.AddressExt,c.City,c.State,c.Zipcode,c.Telephone,c.Fax,c.Email,c.Website, con.Fname, con.Lname, con.Telephone as contactTelephone, con.Position from company c LEFT JOIN Contact con ON c.Id =con.fk_id where c.CompanyName like '%{0}%' or c.Address like '%{0}%' or c.AddressExt like '%{0}%' or c.City like '%{0}%' or c.State like '%{0}%' or c.Zipcode like '%{0}%' or c.Telephone like '%{0}%' or con.Fname like '%{0}%' or con.Lname like '%{0}%' or con.Telephone like '%{0}%'", searchText);
            using (DataTable table = this.GetDataTable(query))
            {
                return (from DataRow row in table.Rows
                        select GetSearchableCustomerClassInstance(row));
            }
        }

        public IEnumerable<Models.InvoiceSearch> SearchInvoices(string searchText)
        {
            // Break query into spaces 
            // now loop through and add to a distinct list search result
            //Start loop searchText.SPlit();
            string query = string.Format("select i.Id,i.CompanyId, i.Date,c.CompanyName ,i.Warranty,i.Description,i.InvoiceNumber,i.ServicePerformanceOn,i.TotalAmount, con.Fname, con.Lname from invoice i Left JOIN Company c ON  i.CompanyId = c.Id Left Join Contact con ON i.CompanyId = con.fk_id where i.Description like '%{0}%' or i.ServicePerformanceOn like '%{0}%' or i.TotalAmount like '%{0}%' or i.Date like '%{0}%' or c.CompanyName like '%{0}%'", searchText);
            
            using(DataTable table = this.GetDataTable(query))
            using (DataTableReader dtr = table.CreateDataReader())
            {
                return (from DataRow row in table.Rows
                        select new InvoiceSearch
                        {
                            Id = row.Field<long>("Id"),
                            CompanyId = row.Field<long>("CompanyId"),
                            CompanyName = row.Field<string>("CompanyName"),
                            ContactFName = row.Field<string>("Fname"),
                            ContactLName = row.Field<string>("Lname"),
                            Date = DateTime.Parse(row["Date"].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                            Description = row["Description"] as string,
                            InvoiceNumber = row["InvoiceNumber"] as string,
                            ServicePerfomanceOn = row["ServicePerformanceOn"] as string,
                            TotalAmount = row.Field<double>("TotalAmount"),
                            Warranty = (row.Field<long>("Warranty") == 1) ? true : false
                        });
            }
        }

        private static Contact GetContactClassInstance(DataRow row)
        {
            return new Models.Contact()
            {
                Id = row.Field<long>("Id"),
                CompanyId = row.Field<long>("fk_Id"),
                FirstName = row.Field<string>("Fname"),
                LastName = row.Field<string>("Lname"),
                Telephone = row.Field<string>("contactTelephone"),
                Cellphone = row.Field<string>("contactCellphone"),
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
                Date = DateTime.Parse(row["Date"].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                Description = row["Description"] as string,
                InvoiceNumber = row["InvoiceNumber"] as string,
                ServicePerfomanceOn = row["ServicePerformanceOn"] as string, 
                TotalAmount = row.Field<double>("TotalAmount"),
                Warranty = (row.Field<long>("Warranty")  == 1) ? true: false,
                Check = (row.Field<long>("IsCheck") == 1) ? true : false
            };
        }

    }
}
