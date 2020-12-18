using System;
using System.Collections;
using efundraising.EFundraisingCRM;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for Product_Business_Rules.
	/// </summary>
	public class ProductBusinessRules: EFundraisingCRMDataObject 
	{

		private int productBusinessRuleID;
		private int productClassID;
		private int productID;
		private int minOrder;
		private Decimal free;
		private int averageDeliveryTime;
	
	
		public ProductBusinessRules()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Data Source Methods
		public static ProductBusinessRules GetProductBusinessRulesByProductID(int scratchBookID)
		{
			//check if product id is individually in the table, if not check package and then product class is used
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			ProductBusinessRules result = dbo.GetProductBusinessRulesByProductID(scratchBookID);
			if (result == null)
			{
                EFundraisingCRM.ScratchBook sb = EFundraisingCRM.ScratchBook.GetScratchBookByID(scratchBookID);

                result = dbo.GetProductBusinessRulesByPackageID(sb.PackageId);
                if (result == null)
                {
                    result = dbo.GetProductBusinessRulesByProductClassID(sb.ProductClassId);
                }
            }

			return result;
		}


        //this overload will tell the caller if the rule was based on the package or the product class
        public static ProductBusinessRules GetProductBusinessRulesByProductID(int scratchBookID, ref string ruleBaseLevel)
        {
            ruleBaseLevel = "Product";
            //check if product id is individually in the table, if not check package and then product class is used
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            ProductBusinessRules result = dbo.GetProductBusinessRulesByProductID(scratchBookID);
            if (result == null)
            {
                EFundraisingCRM.ScratchBook sb = EFundraisingCRM.ScratchBook.GetScratchBookByID(scratchBookID);
                result = dbo.GetProductBusinessRulesByPackageID(sb.PackageId);
                ruleBaseLevel = "Package";
                if (result == null)
                {
                    result = dbo.GetProductBusinessRulesByProductClassID(sb.ProductClassId);
                    ruleBaseLevel = "ProductClass";
                }
            }

            return result;
        }

        public static string GetProductBaseLevel(int scratchBookID)
        {
            string ruleBaseLevel = "Product";
            //check if product id is individually in the table, if not check package and then product class is used
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            ProductBusinessRules result = dbo.GetProductBusinessRulesByProductID(scratchBookID);
            if (result == null)
            {
                EFundraisingCRM.ScratchBook sb = EFundraisingCRM.ScratchBook.GetScratchBookByID(scratchBookID);
                result = dbo.GetProductBusinessRulesByPackageID(sb.PackageId);
                ruleBaseLevel = "Package";
                if (result == null)
                {
                    ruleBaseLevel = "ProductClass";
                }
            }

            return ruleBaseLevel;
        }



		public static ProductBusinessRules GetProductBusinessRulesByProductIdOnly(int scratchBookID)
		{
			//check if product id is individually in the table, if not the product class is used
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			ProductBusinessRules result = dbo.GetProductBusinessRulesByProductID(scratchBookID);
			
			return result;
		}

        public static ProductBusinessRules GetProductBusinessRulesByPackageIdOnly(int packageID)
        {
            //check if package id is individually in the table, if not the product class is used
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            ProductBusinessRules result = dbo.GetProductBusinessRulesByPackageID(packageID);

            return result;
        }

		public static ProductBusinessRules GetProductBusinessRulesByProductClass(int productClassID)
		{
			//check if product id is individually in the table, if not the product class is used
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			ProductBusinessRules result = dbo.GetProductBusinessRulesByProductClassID(productClassID);
			
			return result;
		}



		public static DateTime GetNextBusinessDay(DateTime date, int nbDays)
		{
			//check if product id is individually in the table, if not the product class is used
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
		    
			return dbo.GetNextBusinessDay(date, nbDays);
		
		}

		public static DateTime GetPreviousBusinessDay(DateTime date, int nbDays)
		{
			//check if product id is individually in the table, if not the product class is used
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPreviousBusinessDay(date, nbDays);
		
		}


		


		#endregion

		#region Properties
		public int ProductBusinessRuleID
		{
			set { productBusinessRuleID = value; }
			get { return productBusinessRuleID; }
		}


		public int ProductClassID
		{
			set { productClassID = value; }
			get { return productClassID; }
		}

		public int ProductID
		{
			set { productID = value; }
			get { return productID; }
		}

		public int MinOrder 
		{
			set { minOrder = value; }
			get { return minOrder; }
		}

		public Decimal Free 
		{
			set { free = value; }
			get { return free; }
		}


		public int AverageDeliveryTime
		{
			set { averageDeliveryTime = value; }
			get { return averageDeliveryTime; }
		}



		#endregion
	}
}
