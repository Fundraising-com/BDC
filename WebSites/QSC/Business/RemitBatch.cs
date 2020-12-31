using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// class: RemitBatch.
	/// Description: Object responsible for generating remit information to be sent
	/// to fulfillment houses.
	/// </summary>
	public class RemitBatch : QBusinessObject
	{
		protected string fileNameM;
		[DAL.DataColumn("FileName")]
		public string FileName
		{
			get{ return this.fileNameM; }
			set{ this.fileNameM = value; }
		}
		protected string zOutputLocationM;
		/*FulfillmentHouse aFulfillHouseM;*/

		public RemitBatch()
		{
			
		}
		/// <summary>
		/// Method:  ValidateAndSave
		/// </summary>
		override public bool ValidateAndSave()
		{
			bool bOk=true;


			return bOk;

		}
		public bool Exists(DateTime aBatchDateP, int nIDP)
		{
			bool bExists = false;
			/*
			DataSet a = aTable.Exists(nIDP );

			if(a.Tables.Count>0 && a.Tables[0].Rows.Count > 0)
			{
				DataRow dr = a.Tables[0].Rows[0]; 
				Fill(dr);
				bExists = true;
			} 
			  */        
			return bExists;
		}
		

		/// <summary>
		/// Method:  RunRemit
		/// Description: Process any order/customer associated with this remit batch.
		/// </summary>
		public bool RunRemit()
		{
			bool bOk = true;
			/*
			aFulfillmentHouse = new FulfillmentHouse();
			if(aFulmentHouse.Exists(fulfillmentIDM ))
			{
				// Need the format file for this output

			}
			else
			{
				bOk = false;
				//SetError
			}
			*/

			return bOk;
		}
	}
}
