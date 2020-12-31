using System;
using DAL;

namespace Business
{
	/// <summary>
	/// Class:  FulfillmentBatch
	/// Description:  
	/// </summary>
	public class FulfillmentBatch :  QBusinessObject
	{
		#region Class Members
		// Column fields
		protected DateTime batchDateM;
		[DAL.DataColumn("BatchDate")]
		public DateTime BatchDate
		{
			get{ return this.batchDateM; }
			set{ this.batchDateM = value; }
		}
		protected int IDM=-1;
		[DAL.DataColumn("ID")]
		public int ID
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}		
		protected int fulfillmentIDM=-1;
		[DAL.DataColumn("FulfillmentID")]
		public int FulfillmentID
		{
			get{ return this.fulfillmentIDM; }
			set{ this.fulfillmentIDM=value;  }
		}		
		
		

		#endregion
		public FulfillmentBatch()
		{
			
		}
	}
}
