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
	/// Summary description for OrderStageTracking.
	/// </summary>
	public class OrderStageTracking : QBusinessObject
	{
		protected int AcctId=-1;
		[DAL.DataColumn("GroupId")]
		public int AccountID
		{
			get{ return this.AcctId; }
			set{ this.AcctId=value;  }
		}

		protected DateTime DFrom;
		[DAL.DataColumn("SnapShotDate")]
		public DateTime FromDate
		{
			get{ return this.DFrom; }
			set{ this.DFrom = value; }
		}
		protected DateTime DTo;
		[DAL.DataColumn("SnapShotDate")]
		public DateTime ToDate
		{
			get{ return this.DTo; }
			set{ this.DTo = value; }
		}

		protected string AccountName;
		[DAL.DataColumn("GroupName")]
		public string Account
		{
			get{ return this.AccountName; }
			set{ this.AccountName = value; }
		}

		protected string FMId;
		[DAL.DataColumn("FMID")]
		public string FieldManager
		{
			get{ return this.FMId; }
			set{ this.FMId = value; }
		}

		protected int CAId=-1;
		[DAL.DataColumn("CampaignId")]
		public int Campaign
		{
			get{ return this.CAId; }
			set{ this.CAId=value;  }
		}


		protected string StatusId;
		[DAL.DataColumn("Stage")]
		public string Status
		{
			get{ return this.StatusId; }
			set{ this.StatusId = value; }
		}

		/*protected int TransSeq=0;
		[DAL.DataColumn("TransmissionSequence")]
		public int TransSequence
		{
			get{ return this.TransSeq; }
			set{ this.TransSeq=value;  }
		}*/

		protected int OrderID=0;
		[DAL.DataColumn("OrderId")]
		public int OrderId
		{
			get{ return this.OrderID; }
			set{ this.OrderID=value;  }
		}

        protected int orderQualifierID = 0;
        [DAL.DataColumn("OrderQualifierID")]
        public int OrderQualifierID
        {
            get { return this.orderQualifierID; }
            set { this.orderQualifierID = value; }
        }

        protected bool showOrdersPastStage = false;
        public bool ShowOrdersPastStage
        {
            get { return this.showOrdersPastStage; }
            set { this.showOrdersPastStage = value; }
        }

      protected int productType = 0;
      [DAL.DataColumn("ProductType")]
      public int ProductType
      {
         get { return this.productType; }
         set { this.productType = value; }
      }

		protected OrderStageTrackingDataAccess aTable;

		public OrderStageTracking()
		{
			try
			{
				aTable = new OrderStageTrackingDataAccess();
			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}

		override public bool ValidateAndSave()
		{
			bool bOk=true;
	
			            
			return bOk;
		}

		public bool ValidateDataType( string pSearchFieldType, string pSearchBoxValue)
		{
			bool bOk;
			
			Business.BankDeposit BD = new Business.BankDeposit();
			if (BD.Validate (pSearchFieldType,pSearchBoxValue))
			{ bOk=true;}
			else
			{bOk=false;}
			return bOk;
		}

		public DataSet GetTrackingFilesDataSet()
		{
			DataSet TrackingFilesDataSet = new DataSet();
			DataTable FileDT = new DataTable();
			DataTable OrderDT = new DataTable();
			DataTable OrderDetailDT = new DataTable();
				
			FileDT = aTable.GetAllOrderTrackingFiles(AcctId,AccountName,CAId,FMId,DFrom,DTo,OrderID,StatusId,ShowOrdersPastStage, OrderQualifierID, ProductType );
			TrackingFilesDataSet.Tables.Add(FileDT);
			TrackingFilesDataSet.Tables[0].TableName = "Files";


            OrderDT = aTable.GetTrackingFileDetail(AcctId, AccountName, CAId, FMId, DFrom, DTo, OrderID, StatusId, ShowOrdersPastStage, OrderQualifierID, ProductType);
			TrackingFilesDataSet.Tables.Add(OrderDT);
			TrackingFilesDataSet.Tables[1].TableName = "TrackingOrders";

            OrderDetailDT = aTable.GetTrackingOrderDetail(AcctId, AccountName, CAId, FMId, DFrom, DTo, OrderID, StatusId, ShowOrdersPastStage, OrderQualifierID, ProductType);
			TrackingFilesDataSet.Tables.Add(OrderDetailDT);
			TrackingFilesDataSet.Tables[2].TableName = "OrderDetail";

			return TrackingFilesDataSet;
		}

		public DataSet GetFilesDeatilaBySeq(int pSeq)
		{
			DataSet FilesDetDataSet = new DataSet();
			DataTable FileDetailDT = new DataTable();

            FileDetailDT = aTable.GetTrackingFileDetail(AcctId, AccountName, CAId, FMId, DFrom, DTo, OrderID, StatusId, ShowOrdersPastStage, OrderQualifierID, ProductType);
			FilesDetDataSet.Tables.Add(FileDetailDT);
			return FilesDetDataSet;
		}
	}
}
