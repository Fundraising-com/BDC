using System;
using System.Xml;

namespace efundraising.EFundraisingCRM
{
    public class SalesSAPStatus : EFundraisingCRMDataObject
    {
        private int salesSAPStatusID;
        private string description;


        public SalesSAPStatus() : this(int.MinValue) { }
        public SalesSAPStatus(int salesSAPStatusID) : this(salesSAPStatusID, null) { }
        public SalesSAPStatus(int salesSAPStatusID, string description)
        {
            this.salesSAPStatusID = salesSAPStatusID;
			this.description = description;
		}


        #region Data Source Methods

        public static SalesSAPStatus GetSAPSalesStatusByID(int id)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetSAPSalesStatusByID(id);
        }

       
        #endregion





        #region Static Data
        public static SalesSAPStatus InProcess
        {
            get { return new SalesSAPStatus(1, "IN PROCESS FOR SAP"); }
        }

        public static SalesSAPStatus OpNoPay
        {
            get { return new SalesSAPStatus(2, "OPEN TO SAP NO PAYMENT"); }
        }

        public static SalesSAPStatus OpWithOutPay
        {
            get { return new SalesSAPStatus(3, "OPEN TO SAP WOUT PAYMENT"); }
        }

        public static SalesSAPStatus OpWithpay
        {
            get { return new SalesSAPStatus(4, "OPEN TO SAP WITH PAYMENT"); }
        }

        public static SalesSAPStatus ReleaseInSap
        {
            get { return new SalesSAPStatus(5, "RELEASED IN SAP"); }
        }

        public static SalesSAPStatus ShipInSap
        {
            get { return new SalesSAPStatus(6, "SHIPPED IN SAP"); }
        }

        
        #endregion












        #region Properties
        public int SalesSAPStatusID
        {
            set { salesSAPStatusID = value; }
            get { return salesSAPStatusID; }
        }

        public string Description
        {
            set { description = value; }
            get { return description; }
        }
        #endregion



    }
}
