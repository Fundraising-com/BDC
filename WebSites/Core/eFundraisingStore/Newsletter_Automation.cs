using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace GA.BDC.Core.eFundraisingStore
{
    public class Newsletter_Automation
    {

        private string email;
        private string name;


        public Newsletter_Automation() : this(null) { }
        public Newsletter_Automation(string email) : this(null, email) { }
        public Newsletter_Automation(string name, string email)  
        {
            this.email = email;
            this.name = name;
        }
     
        
        public void InsertDataFromCSVFile(string filename)
        {
        
           DataTable dt = CreateDatatableFromFile(filename);
           FillTempTable(dt);
        
        }


        public void UpdateDatabaseTables()
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            dbo.UpdateLeadNewsletterTables();      
        
        }


        public DataTable DownloadEfrMailingList(int partnerID, string beginDate, string endDate)
        {
            
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.RetrieveDataFromEFRMailListTables(partnerID, beginDate, endDate);
        }

        public DataTable DownloadFRMailingList()
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.RetrieveDataFromFRMailListTables();
        }




        public DataTable DownloadDataForEFR()
        {

            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.RetrieveDataFromEFRTables();
            
        
        }

        public DataTable DownloadDataForFR()
        {

            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.RetrieveDataFromFRTables();


        }
        
        
        private DataTable CreateDatatableFromFile(string filename)
           {
               DataTable dt = new DataTable();
               DataColumn dc;
               DataRow dr;

               dc = new DataColumn();
               dc.DataType = System.Type.GetType("System.String");
               dc.ColumnName = "email";
               dc.Unique = false;
               dt.Columns.Add(dc);
               dc = new DataColumn();
               dc.DataType = System.Type.GetType("System.String");
               dc.ColumnName = "name";
               dc.Unique = false;
               dt.Columns.Add(dc);
               //dc = new DataColumn();
               //dc.DataType = System.Type.GetType("System.String");
               //dc.ColumnName = "name";
               //dc.Unique = false;
               //dt.Columns.Add(dc);
               //dc = new DataColumn();
               //dc.DataType = System.Type.GetType("System.String");
               //dc.ColumnName = "consultant";
               //dc.Unique = false;
               //dt.Columns.Add(dc);
               //dc = new DataColumn();
               //dc.DataType = System.Type.GetType("System.String");
               //dc.ColumnName = "partner";
               //dc.Unique = false;
               //dt.Columns.Add(dc);

               StreamReader sr = new StreamReader(filename);
               string input;
               try
               {
                   while ((input = sr.ReadLine()) != null)
                   {
                       string[] s = input.Split(new char[] { ',' });
                       dr = dt.NewRow();
                       dr["email"] = s[0].Replace(@"""", "");
                       dr["name"] = s[1].Replace(@"""", "");
                       dt.Rows.Add(dr);
                   }
               sr.Close();
               }
               catch (Exception ex)
               {

                   throw ex;
               }
               
               return dt;
           }

           private void FillTempTable(DataTable dt)
           {
              try
               {
                   //Stopwatch sw = new Stopwatch();
                   DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
                   dbo.InsertToTempTable(dt);
                   //RunUpdateProcedure();
              }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
        
        
        
        public string Email
        {
            set { email = value;}
            get { return email; }
        }

        public string Name
        {
            set { name = value;}
            get { return name; }
        }





    }
}
