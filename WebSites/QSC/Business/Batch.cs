using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	///<summary>Batch</summary>
	public class Batch : QBusinessObject
	{

		protected int IDM=-1;
		[DAL.DataColumn("OrderId")]
		public int OrderId
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}

		///<summary>default constructor</summary>
		public Batch(){}

		public DataTable GetBatchByOrderId(int OrderIdP,string FMID)
		{
			DAL.BatchDataAccess oBatch = new BatchDataAccess();
			return oBatch.GetByOrderId(OrderIdP, FMID);
		}

		public DataTable GetBatchDetailsByOrderId(int OrderIdP)
		{
			DAL.BatchDataAccess oBatch = new BatchDataAccess();
			return oBatch.GetDetailsByOrderId(OrderIdP);
		}

		public DataTable GetCOHsByOrderId(int OrderIdP)
		{
			DAL.BatchDataAccess oBatch = new BatchDataAccess();
			return oBatch.GetCOHByOrderId(OrderIdP);
		}
		
		public DataTable GetCOHByCOHId(int COHIdP)
		{
			DAL.BatchDataAccess oBatch = new BatchDataAccess();
			return oBatch.GetCOHByCOHId(COHIdP);
		}
		
		public DataTable GetCODsByCOHId(int COHIdP)
		{
			DAL.BatchDataAccess oBatch = new BatchDataAccess();
			return oBatch.GetCODsByCOHId(COHIdP);
		}

		public DataTable GetCODByCOHIdTransId(int COHIdP, int TransIdP)
		{
			DAL.BatchDataAccess oBatch = new BatchDataAccess();
			return oBatch.GetCODByCOHIdTransId(COHIdP, TransIdP);
		}

		public DataTable GetBatchesByCampaignID(int CampaignID, string FMID)
		{
			DAL.BatchDataAccess oBatch = new BatchDataAccess();
			return oBatch.GetBatchesByCampaignID(CampaignID, FMID);
		}

        public DataTable GetBatchesByAccountID(int AccountID, string FMID)
        {
            DAL.BatchDataAccess oBatch = new BatchDataAccess();
            return oBatch.GetBatchesByAccountId(AccountID, FMID);
        }

        public DataTable GetBatchesByAccountName(string name, string FMID)
        {
            DAL.BatchDataAccess oBatch = new BatchDataAccess();
            return oBatch.GetBatchesByAccountName(name, FMID);
        }

		public bool CloseOrder(int OrderID) 
		{
			DAL.OrderHistoryDataAccess oOrderHistory = new OrderHistoryDataAccess();
			return oOrderHistory.CloseOrder(OrderID);
		}

		public bool OrderApproveDisapprove(int approvalType, int OrderID) 
		{
			if (approvalType==0) //Approve Order
			{
				DAL.OrderHistoryDataAccess oOrderHistory = new OrderHistoryDataAccess();
				return oOrderHistory.CloseOrder(OrderID);
			}
			else //Disapprove Order
			{
				DAL.OrderHistoryDataAccess oOrderHistory = new OrderHistoryDataAccess();
				oOrderHistory.CancelOrder(OrderID);
				return true;
			}
		}

		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				//this.ErrorBI = "this.Validate() returned true, lets try a save !<br>" + this.ErrorBI;
				return this.Save();
			}
			else
			{
				return false;
			}
		}

		///<summary>Check for compliance with biz rules</summary>
		public bool Validate()
		{
			return false;
		}

		public bool Save()
		{
			return false;
		}

		private string	ErrorGUIM;
		///<summary>Gets or sets error string associatated with user interface level validation</summary>
		public string	ErrorGUI	{ get{ return this.ErrorGUIM; } set{this.ErrorGUIM = value; } }
		
		private string	ErrorBIM;
		///<summary>Gets or sets error string associatated with biz intelligence level validation</summary>
		public string	ErrorBI		{ get{ return this.ErrorBIM;  } set{this.ErrorBIM  = value; } }


	}
}
