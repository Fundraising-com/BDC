using System;

namespace CRMWeb.Classes
{
	/// <summary>
	/// Summary description for SaleInfo.
	/// </summary>
	public class SaleInfo
	{

		protected int saleID;
		protected decimal totalAmount;
		protected decimal totalPaid;
		protected decimal totalAdjustment;
        protected decimal balance;

	
		public SaleInfo()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public int SALE_ID {
			get {
				return saleID;
			}
			set {
				saleID = value;
			}
		}

		public decimal BALANCE {
			get {
				return balance;
			}
			set {
			    balance = value;
			}
		}


		public decimal TOTAL_AMOUNT {
			get {
				return totalAmount;
			}
			set {
				totalAmount = value;
			}
		}
		
		public decimal TOTAL_PAID {
			get {
				return totalPaid;
			}
			set {
				totalPaid = value;
			}
		}

		public decimal TOTAL_ADJUSTMENT {
			get {
				return totalAdjustment;
			}
			set {
				totalAdjustment = value;
			}
		}
	}
}
