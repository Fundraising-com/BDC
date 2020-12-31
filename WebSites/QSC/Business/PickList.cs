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
	/// Summary description for PickList.
	/// </summary>
	public class PickList : QBusinessObject
	{
		public PickList()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataTable GetUnpickedOrdersByList(int lDistributionCenterIdP, string sOrderListP)
		{
			DAL.PickListDataAccess oPick = new PickListDataAccess();
			return oPick.GetUnpickedOrdersByList(lDistributionCenterIdP, sOrderListP);
		}

		public DataTable GetOrderBreakdown(int lDistributionCenterIdP, int lOrderIdP)
		{
			DAL.PickListDataAccess oPick = new PickListDataAccess();
			return oPick.GetOrderBreakdown(lDistributionCenterIdP, lOrderIdP);
		}

		public void InsertBatchDistributionList(int lDistributionCenterIdP, int lOrderIdP)
		{
			DAL.PickListDataAccess oPick = new PickListDataAccess();
			oPick.InsertBatchDistributionCenter(lDistributionCenterIdP, lOrderIdP);
		}
		
		public void ReserveQuantitiesAndUpdateStatus(int lDistributionCenterIdP, int lOrderIdP)
		{
			DAL.PickListDataAccess oPick = new PickListDataAccess();
			oPick.ReserveQuantitiesAndUpdateStatus(lDistributionCenterIdP, lOrderIdP);
		}

		public DataTable GetMagQueueOrders()
		{
			DAL.PickListDataAccess oPick = new PickListDataAccess();
			return oPick.GetMagQueueOrders();
		}

		
		public DataTable GetMagQueueOrdersByList(string sOrderListP)
		{
			DAL.PickListDataAccess oPick = new PickListDataAccess();
			return oPick.GetMagQueueOrdersByList(sOrderListP);
		}

		public void ProcessMagQueue(int lOrderIdP)
		{
			DAL.PickListDataAccess oPick = new PickListDataAccess();
			oPick.ProcessMagQueue(lOrderIdP);
		}

		///<summary>check it then submit it</summary>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
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
			/* setup variables to track validation */
			bool blValid = true;

			return blValid;
		}


		///<summary>Save to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			bool blSave = false;
			
			return blSave;
		}
	}
}
