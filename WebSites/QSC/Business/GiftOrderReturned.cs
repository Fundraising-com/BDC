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
	public class GiftOrderReturned : QBusinessObject
	{
		#region Class Members
		protected int IDM ;
		[DAL.DataColumn("Order_Id")]
		public int Order_Id
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		#endregion Class Members

	
		public string pSearchFieldType;
		public string pSearchBoxValue;

		protected DAL.GiftOrderReturnedData aTable;

		public GiftOrderReturned()
		{
			try
			{
				aTable = new DAL.GiftOrderReturnedData();
			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}
	
		
		/// <summary>
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

		public DataTable GetGiftOrderData()
		{
			return aTable.Exists(IDM);
		}

		public bool Validate( string pSearchFieldType, string pSearchBoxValue)
		{
			bool bValid = true;
			return bValid; 
		}
	

	
		
		
		
	}
	//new class for Batch and Code header 
	public class BatchAndCodeHeader : QBusinessObject
	{
		#region Class Members
		// Column fields
		
		
		protected int OrderIDM=-1;
		[DAL.DataColumn("OrderId")]
		public int OrderId
		{
			get{ return this.OrderIDM; }
			set{ this.OrderIDM=value;  }
		}

		protected int cohIDM=-1;
		[DAL.DataColumn("Coh")]
		public int Coh
		{
			get{ return this.cohIDM; }
			set{ this.cohIDM=value;  }
		}

		protected DateTime BatchdateIdM;
		[DAL.DataColumn("BatchDate")]
		public DateTime BatchDate
		{
			get{ return this.BatchdateIdM; }
			set{ this.BatchdateIdM=value;  }
		}


		protected int BilltoacctIdM;
		[DAL.DataColumn("BilltoacctId")]
		public int BilltoacctId
		{
			get{ return this.BilltoacctIdM; }
			set{ this.BilltoacctIdM=value;  }
		}


		protected int ShiptoacctIdM;
		[DAL.DataColumn("ShiptoacctId")]
		public int ShiptoacctId
		{
			get{ return this.ShiptoacctIdM; }
			set{ this.ShiptoacctIdM=value;  }
		}


		protected int CampaignIdM;
		[DAL.DataColumn("Campaignid")]
		public int Campaignid
		{
			get{ return this.CampaignIdM; }
			set{ this.CampaignIdM = value; }
		}

		protected int StatusIdM;
		[DAL.DataColumn("StatusId")]
		public int StatusId
		{
			get{ return this.StatusIdM; }
			set{ this.StatusIdM = value; }
		}


		protected int OrdertypecodeIdM;
		[DAL.DataColumn("OrdertypecodeId")]
		public int OrdertypecodeId
		{
			get{ return this.OrdertypecodeIdM; }
			set{ this.OrdertypecodeIdM = value; }
		}

		protected int OrderqualifierIdM;
		[DAL.DataColumn("OrderqualifierId")]
		public int OrderqualifierId
		{
			get{ return this.OrderqualifierIdM; }
			set{ this.OrderqualifierIdM = value; }
		}

		protected int CustomerinstanceIdM ;
		[DAL.DataColumn("CustomerinstanceId")]
		public int CustomerinstanceId
		{
			get{ return this.CustomerinstanceIdM; }
			set{ this.CustomerinstanceIdM = value; }
		}

		protected string ChangeUser_IdM;
		[DAL.DataColumn("ChangeUser_Id")]
		public string ChangeUser_Id
		{
			get{ return this.ChangeUser_IdM; }
			set{ this.ChangeUser_IdM = value; }
		}


		override public bool ValidateAndSave()
		{
			bool bOk=true;
			// Do any validation
			if(!Validate())
			{
			}
			else
			{
				if(OrderIDM == -1 && cohIDM ==-1)
				{
					bOk= aTable.InsertBatchOrderHeader(BatchdateIdM,BilltoacctIdM,ShiptoacctIdM,CampaignIdM,StatusIdM,OrdertypecodeIdM,OrderqualifierIdM,CustomerinstanceIdM,ChangeUser_IdM,out OrderIDM, out cohIDM);
				}
					
			}
            
			return bOk;
		}
		

		protected DAL.BatchOrderHeaderData aTable;

		public BatchAndCodeHeader() 
		{
			OrderIDM = -1;
			cohIDM=-1;
			try
			{
               
				aTable = new DAL.BatchOrderHeaderData();

			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}
		#endregion
		public bool Validate()
		{
			bool bValid = true;



			return bValid;
		}
	}


	//CustomerOrderDetail

	//new class for Batch and Code header 
	public class OrderDetail : QBusinessObject
	{
		#region Class Members
		// Column fields
		
		
		protected int cohIDM;
		[DAL.DataColumn("Coh")]
		public int Coh
		{
			get{ return this.cohIDM; }
			set{ this.cohIDM=value;  }
		}

		protected string productcode_IdM;
		[DAL.DataColumn("productcode_Id")]
		public string productcode_Id
		{
			get{ return this.productcode_IdM; }
			set{ this.productcode_IdM = value; }
		}

		
		protected int quantityIdM;
		[DAL.DataColumn("quantity")]
		public int quantity
		{
			get{ return this.quantityIdM; }
			set{ this.quantityIdM=value;  }
		}


		protected double priceIdM;
		[DAL.DataColumn("price")]
		public double price
		{
			get{ return this.priceIdM; }
			set{ this.priceIdM=value;  }
		}


		protected double catalogpriceIdM;
		[DAL.DataColumn("catalogprice")]
		public double catalogprice
		{
			get{ return this.catalogpriceIdM; }
			set{ this.catalogpriceIdM = value; }
		}

		protected int producttypeIdM;
		[DAL.DataColumn("producttype")]
		public int producttype
		{
			get{ return this.producttypeIdM; }
			set{ this.producttypeIdM = value; }
		}


		protected int statusIdM;
		[DAL.DataColumn("status")]
		public int status
		{
			get{ return this.statusIdM; }
			set{ this.statusIdM = value; }
		}

		


		override public bool ValidateAndSave()
		{
			bool bOk=true;
			// Do any validation
			if(!Validate())
			{
			}
			else
			{
				{
					bOk= aTable.InsertOrderItemDetail(cohIDM,productcode_IdM,quantityIdM,priceIdM,catalogpriceIdM,producttypeIdM,statusIdM);
				}
					
			}
            
			return bOk;
		}
		

		protected DAL.OrderDetailData aTable;

		public OrderDetail() 
		{
			//OrderIDM = -1;
			//cohIDM=-1;
			try
			{
               
				aTable = new DAL.OrderDetailData();

			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}
		#endregion
		public bool Validate()
		{
			bool bValid = true;
			return bValid;
		}
	}






	//new class for customer (ACCOUNT)
	public class CustomerAcc : QBusinessObject
	{
		#region Class Members
		// Column fields
		
		protected int IDM=-1;
		[DAL.DataColumn("CustomerInstance")]
		public int CustomerInstance
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		
		protected int Account_IdM;
		[DAL.DataColumn("Account_Id")]
		public int Account_Id
		{
			get{ return this.Account_IdM; }
			set{ this.Account_IdM=value;  }
		}
		protected string ChangeUser_IdM;
		[DAL.DataColumn("ChangeUser_Id")]
		public string ChangeUser_Id
		{
			get{ return this.ChangeUser_IdM; }
			set{ this.ChangeUser_IdM = value; }
		}


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
					bOk= aTable.InsertCustomer(Account_IdM,ChangeUser_IdM,out IDM);
				}
					
			}
            
			return bOk;
		}
		

		protected DAL.CustomerAccData aTable;

		public CustomerAcc() 
		{
			IDM = -1;
			try
			{
               
				aTable = new DAL.CustomerAccData();

			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}
		#endregion
		public bool Validate()
		{
			bool bValid = true;
			return bValid;
		}
	}
		
	












}