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
	/// Summary description for Shipment.
	/// </summary>
	public class Shipment : QBusinessObject
	{
		public Shipment()
		{
			//
			// TODO: Add constructor logic here
			//, 
		}

		public DataTable GetShippableOrders(Int32 lTopP, string zProductLine, int nIndividualOrders, string zOrderIDS, int zPrintStatus, string zProductCode, int nBackOrderOnly, int nShipmentGroupID)
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();
			return oShipment.GetShippableOrders(lTopP, zProductLine, nIndividualOrders,zOrderIDS,zPrintStatus,zProductCode,nBackOrderOnly,nShipmentGroupID);
		}

		public DataTable GetShipmentInfoByOrderID(int lOrderIdP)
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();
			return oShipment.GetShipmentInfoByOrderID(lOrderIdP);
		}

		public DataTable GetCarriers()
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();
			return oShipment.GetCarriers();
		}

		public DataTable GetShipmentVariationInfo(int lOrderIdP, string sSessionIdP, int? lShipmentGroupID)
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();
			return oShipment.GetShipmentVariationInfo(lOrderIdP, sSessionIdP, lShipmentGroupID);
		}

		public void InsertShipmentVariation(
				string sSessionIdP, 
				int lCustomerOrderHeaderInstanceP, 
				int lTransIdP,
				int lQuantityShippedP,
				int lQuantityReplacedP,
				int lReplacementItemIdP,
				bool bShipTFP,
				string sCommentP,
				string sCustomerCommentP,
				string sModifiedByP)
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();
			oShipment.InsertShipmentVariation(
					sSessionIdP
					, lCustomerOrderHeaderInstanceP
					, lTransIdP
					, lQuantityShippedP
					, lQuantityReplacedP
					, lReplacementItemIdP
					, bShipTFP
					, sCommentP
					, sCustomerCommentP
					, sModifiedByP
				);
		}


		
		public void SplitCOD(
			int lCustomerOrderHeaderInstanceP, 
			int lTransIdP,
			int lSplitQuantityP,
			string sModifiedByP)
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();
			oShipment.SplitCOD(
				lCustomerOrderHeaderInstanceP
				, lTransIdP
				, lSplitQuantityP
				, sModifiedByP
				);
		}

		public int ShipBatch(
			string sOrderIdsP, 
			int lDistributionCenterIdP,
			int lCarrierIdP,
			string sShipDateP,
			string sExpectedDeliveryDate,
			int lNumberOfBoxesP,
			float fWeightP,
			int lNumberOfSkidsP,
			string sWeightUnitOfMeasureP,
			string sCommentP,
			string sSessionIdP, 
			int lUserIdP,
			string sWayBillP,
            string zProductLine,
            int? lShipmentGroupIDP

            )
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();

			return oShipment.ShipBatches(
				sOrderIdsP
				, lDistributionCenterIdP
				, lCarrierIdP
				, sShipDateP
				, sExpectedDeliveryDate
				, lNumberOfBoxesP
				, fWeightP
				, lNumberOfSkidsP
				, sWeightUnitOfMeasureP
				, sCommentP
                , lUserIdP
                , 0
                , sSessionIdP				
                , zProductLine
                , lShipmentGroupIDP);

			//return Convert.ToInt32(oTable.Rows[1][1].ToString());

			// Waybill insert here.



		}

        public int ShipOrderItem(
            int  nCustomerOrderHeaderInstance, 
            int  nTransID,
            int lDistributionCenterIdP,
            int lCarrierIdP,
            string sShipDateP,
            string sExpectedDeliveryDate,
            int lNumberOfBoxesP,
            float fWeightP,
            int lNumberOfSkidsP,
            string sWeightUnitOfMeasureP,
            string sCommentP,
            string sSessionIdP, 
            int lUserIdP,
            string sWayBillP

            )
        {
            DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();

            return oShipment.ShipOrderItem(
                nCustomerOrderHeaderInstance
                , nTransID
                , lDistributionCenterIdP
                , lCarrierIdP
                , sShipDateP
                , sExpectedDeliveryDate
                , lNumberOfBoxesP
                , fWeightP
                , lNumberOfSkidsP
                , sWeightUnitOfMeasureP
                , sCommentP
                , lUserIdP
                , 0
                , sSessionIdP);

            //return Convert.ToInt32(oTable.Rows[1][1].ToString());

            // Waybill insert here.



        }



		public void DeleteShipmentVariations(string sSessionIdP)
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();
			oShipment.DeleteShipmentVariations(sSessionIdP);
		}

		public void InsertShipmentWaybill(int lShipmentIdP, string sWaybillNumberP)
		{
			DAL.ShipmentDataAccess oShipment = new ShipmentDataAccess();
			oShipment.InsertShipmentWaybill(lShipmentIdP, sWaybillNumberP);
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
