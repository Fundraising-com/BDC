using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	/// <summary>
	/// Summary description for CatalogSection.
	/// </summary>
	public class CatalogSection : QBusinessObject
	{
        #region Class Members
        // Column fields

        protected int IDM=-1;
        [DAL.DataColumn("ID")]
        public int ID
        {
            get{ return this.IDM; }
            set{ this.IDM=value;  }
        }
        protected int typeM;
        [DAL.DataColumn("Type")]
        public int Type
        {
            get{ return this.typeM; }
            set{ this.typeM = value; }
        }
        protected string catalogCodeM;
        [DAL.DataColumn("CatalogCode")]
        public string CatalogCode
        {
            get{ return this.catalogCodeM; }
            set{ this.catalogCodeM = value; }
        }

        protected string nameM;
        [DAL.DataColumn("Name")]
        public string Name
        {
            get{ return this.nameM; }
            set{ this.nameM = value; }
        }

        #endregion

        protected CatalogSectionDataTable aTable;

		public CatalogSection() 
		{
            IDM = -1;
            try
            {
               
                aTable = new CatalogSectionDataTable();

            }
            catch(COMException e)
            {
                int x = e.ErrorCode;
            }
		}

        override public bool ValidateAndSave()
        {
            // Do any validation
            aTable.Insert(1,1,Name, out IDM);
            
            return true;
        }
        /// <summary>
        /// See if it exists
        /// </summary>

        protected bool Exists(int nIDM)
        {
            bool bExists = false;
            DataTable a = aTable.Exists(nIDM );
            if(a.Rows.Count > 0)
            {
                DataRow dr = a.Rows[0]; 
                Fill(dr);
                bExists = true;
            }            
            return bExists;
        }

	}
}
