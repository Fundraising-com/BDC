using System;
using System.Data.SqlClient;
using System.Data;
using efundraising.Diagnostics;
using efundraising.EFundraisingCRM;

namespace EFundraisingCRMWeb.Components.Server.DataGrid.SaleItems
{
	/// <summary>
	/// Summary description for SaleItemsDataGrid.
	/// </summary>
	public class SaleItemsDataGrid
	{
		public SaleItemsDataGrid()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static string IsNullReturnEmpty(string sValue)
		{
			if (sValue == null)
				return string.Empty;
			return sValue.Trim();
		}
		//Buid a dataTable for displaying all the lad visits
		public static DataTable CreateDataTableSaleItems(SalesItemCollection saleItems)
		{
			DataTable dt = CreateEmptyDataTableSaleItems();
			try
			{		
				//for every visits
				foreach (SalesItem salesItem in saleItems)
				{
					ScratchBook sc = ScratchBook.GetScratchBookByID(salesItem.ScratchBookId);
    				//EFundraisingCRM.Package p = EFundraisingCRM.Package.GetPackageByID(sc.PackageId);

                    decimal profitPercentage = ManageSaleScreen.GetSalesItemProfitPercentage(salesItem);
                    decimal totalProfit = ManageSaleScreen.GetSalesItemTotalProfit(salesItem);
				
					DataRow dr = dt.NewRow();
					dr["SalesItemId"] = salesItem.SalesItemNo;
					dr["ScratchBookID"] = sc.ScratchBookId;
					dr["Product"] = IsNullReturnEmpty(sc.Description);
					dr["Profit"] = profitPercentage.ToString("C").Substring(1); //margin.ToString() + "%";
					dr["ProductCode"] = IsNullReturnEmpty(sc.ProductCode);
					dr["Quantity"] = salesItem.QuantitySold;
					dr["QuantityFree"] = salesItem.QuantityFree;
					dr["Price"] = salesItem.UnitPriceSold.ToString("C"); 
					decimal dtotalAmount = Convert.ToDecimal(salesItem.UnitPriceSold*salesItem.QuantitySold);
					dr["TotalAmount"] = dtotalAmount.ToString("C") ;
					dr["TotalProfit"] = totalProfit.ToString("C");
					dr["GroupName"] = IsNullReturnEmpty(salesItem.GroupName);
						
					
					dr["FixedProfit"]= "";
					dt.Rows.Add(dr);
				}
				
				
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in CreateDataTableSaleItems", ex);
			}

			return dt;
		}

		public static DataTable CreateEmptyDataTableSaleItems()
		{
			DataTable dt = new DataTable();
			// Build our data table for binding
			
			dt.Columns.Add("SalesItemId");
			dt.Columns.Add("ScratchbookID");
			dt.Columns.Add("Product");
			dt.Columns.Add("Profit");
			dt.Columns.Add("ProductCode");
			dt.Columns.Add("Quantity");
			dt.Columns.Add("QuantityFree");
			dt.Columns.Add("Price");
			dt.Columns.Add("TotalAmount");
			dt.Columns.Add("TotalProfit");
			dt.Columns.Add("FixedProfit");
			dt.Columns.Add("GroupName");

	
			return dt;
		}

		public static string[] GetColumnsName(DataTable dt)
		{
			if (dt == null)
				return null;
			System.Collections.ArrayList ar = new System.Collections.ArrayList();
			for (int i=0; i< dt.Columns.Count; i++)
				ar.Add(dt.Columns[i].ColumnName);

			return (string[])ar.ToArray(typeof(string));
		}

		public static DataTable AddNewRow(DataTable dt)
		{

			DataRow dr = dt.NewRow();
			dr["Quantity"] = 1;
			dr["QuantityFree"] = 0;
			dr["TotalProfit"] = "$0";
			dr["TotalAmount"] = "$0";
			dt.Rows.Add(dr);

			return dt;
		}

	}
}
