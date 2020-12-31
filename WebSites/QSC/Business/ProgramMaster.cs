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
	public class ProgramMaster : QBusinessObject
	{
		#region Class Members
		// Column fields

		protected int IDM=-1;
		[DAL.DataColumn("Program_ID")]
		public int Program_ID
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		protected string typeM;
		[DAL.DataColumn("Program_Type")]
		public string Program_Type
		{
			get{ return this.typeM; }
			set{ this.typeM = value; }
		}
		protected int subTypeM;
		[DAL.DataColumn("SubType")]
		public int SubType
		{
			get{ return this.subTypeM; }
			set{ this.subTypeM = value; }
		}

		protected int seasonM;
		[DAL.DataColumn("Season")]
		public int Season
		{
			get{ return this.seasonM; }
			set{ this.seasonM = value; }
		}

		protected string alphaM;
		[DAL.DataColumn("Alpha")]
		public string Alpha
		{
			get{ return this.alphaM; }
			set{ this.alphaM = value; }
		}

		protected string codeM;
		[DAL.DataColumn("Code")]
		public string Code
		{
			get{ return this.codeM; }
			set{ this.codeM = value; }
		}
		protected int statusM;
		[DAL.DataColumn("Status")]
		public int Status
		{
			get{ return this.statusM; }
			set{ this.statusM = value; }
		}
		protected string countryM;
		[DAL.DataColumn("Country")]
		public string Country
		{
			get{ return this.countryM; }
			set{ this.countryM = value; }
		}
		protected string languageM;
		[DAL.DataColumn("Lang")]
		public string Lang
		{
			get{ return this.languageM; }
			set{ this.languageM = value; }
		}
		protected string isReplacementM;
		[DAL.DataColumn("IsReplacement")]
		public string IsReplacement
		{
			get{ return this.isReplacementM; }
			set{ this.isReplacementM = value; }
		}
		protected string isNationalM;
		[DAL.DataColumn("IsNational")]
		public string IsNational
		{
			get{ return this.isNationalM; }
			set{ this.isNationalM = value; }
		}


		#endregion

        protected ProgramMasterDataAccess aTable;

		public ProgramMaster() 
		{
            IDM = -1;
            try
            {
               
                aTable = new ProgramMasterDataAccess();

            }
            catch(COMException e)
            {
                int x = e.ErrorCode;
            }
		}
        /// <summary>
        /// ValidateAndSave
        /// </summary>
        override public bool ValidateAndSave()
        {
            bool bOk=true;
            // Do any validation
            if(!Validate())
            {
            }
            else
            {
                if(IDM == -1)
                {
                    bOk= aTable.Insert(typeM, subTypeM,seasonM,alphaM,codeM,statusM,countryM,languageM,isReplacementM,isNationalM,UserIDChanged,out IDM);
                }
                else
                {
					bOk = aTable.Update( IDM,typeM, subTypeM,seasonM,alphaM,codeM,statusM,countryM,languageM,isReplacementM,isNationalM,userIDChangedM);
                }
            }
            
            return bOk;
        }
        /// <summary>
        /// See if it exists
        /// </summary>

        public bool Exists(int nIDM)
        {
            bool bExists = false;
            DataSet a = aTable.Exists(nIDM );

			if(a.Tables.Count>0 && a.Tables[0].Rows.Count > 0)
            {
                DataRow dr = a.Tables[0].Rows[0]; 
                Fill(dr,this.GetType());
                bExists = true;
            } 
			          
            return bExists;
        }

        /// <summary>
        /// Function:  Validate
        /// 
        /// </summary>
        public bool Validate()
        {
            bool bValid = true;



            return bValid;
        }
	}
}
