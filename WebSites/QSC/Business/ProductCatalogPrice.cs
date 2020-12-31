using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
//using DAL;

namespace Business
{
	/// <summary>
	/// Summary description for CatalogSection.
	/// </summary>
	public class ProductCatalogPrice : QBusinessObject
	{
		#region Class Members
		protected int IDM ;
		[DAL.DataColumn("Campaign_Id")]
		public int Campaign_Id
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}

		protected string ProductCodeM;
		[DAL.DataColumn("Product_Code")]
		public string ProductCode
		{
			get{ return this.ProductCodeM; }
			set{ this.ProductCodeM = value; }
		}
		#endregion Class Members

	
		public string pSearchFieldType;
		public string pSearchBoxValue;

		protected DAL.ProductCatalogPriceData aTable;

		public ProductCatalogPrice()
		{
			try
			{
				aTable = new DAL.ProductCatalogPriceData();
			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}
	
		
		///<summary></summary>
		override public bool ValidateAndSave()
		{
			bool bOk =true;
			// Do any validation
			if(!Validate(pSearchFieldType, pSearchBoxValue))
			{
				// Always True bOk = false;
			}
		
			return bOk;
		}

		public DataTable GetProductCatalogPriceData()
		{
			return aTable.Exists(IDM,ProductCode);
		}


		public bool Validate( string pSearchFieldType, string pSearchBoxValue)
		{
			bool bValid = true;
			return bValid;
		}
	}
}