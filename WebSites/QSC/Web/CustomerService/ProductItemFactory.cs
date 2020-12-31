using System;
using System.Web;
using System.Web.UI.WebControls;
using QSPFulfillment.DataAccess.Common.ActionObject;
using QSP.WebControl;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for MagazineItemFactory.
	/// </summary>
	public class ProductItemFactory
	{
		private static ProductItemFactory singletonInstance;

		public static ProductItemFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductItemFactory();
				}

				return singletonInstance;
			}
		}

		public ProductItem GetProductItem(DataGridItem item) 
		{
			ProductItem productItem = new ProductItem();

			try 
			{
				productItem.MagPrice_instance = Convert.ToInt32(((Label) item.FindControl("lblMagInstance")).Text);
			} 
			catch { }

			productItem.Product_code = ((Label) item.FindControl("lblProductCode")).Text;
			productItem.Product_sort_name = ((Label) item.FindControl("lblTitle")).Text;
			
			try 
			{
				productItem.Term = Convert.ToInt32(((Label) item.FindControl("lblTerm")).Text);
			} 
			catch { }

			try 
			{
				productItem.Quantity = Convert.ToInt32(((System.Web.UI.WebControls.TextBox) item.FindControl("tbxQuantity")).Text);
			} 
			catch { }

			try 
			{
				productItem.Price = Convert.ToSingle(((Label) item.FindControl("lblPrice")).Text);
			}
			catch { }

			productItem.Lang = ((Label) item.FindControl("lblLang")).Text;
			productItem.Catalog_Name = ((Label) item.FindControl("lblCatalogName")).Text;
			
			try 
			{
				productItem.ProductType = Convert.ToInt32(((Label) item.FindControl("lblProductType")).Text);
			} 
			catch { }

			try 
			{
				productItem.EnterredPrice = Convert.ToSingle(((System.Web.UI.WebControls.TextBox) item.FindControl("tbxPrice")).Text);
			} 
			catch { }

			/*try 
			{
				productItem.PriceOverrideReason = Convert.ToInt32(((DropDownListReq) item.FindControl("ddlPriceOverrideReason")).SelectedValue);
			} 
			catch { }*/
			productItem.PriceOverrideReason = 45005;
            
            try
            {
                productItem.ProductReplacementReason = Convert.ToInt32(((DropDownListReq)item.FindControl("ddlProductReplacementReason")).SelectedValue);                
            }
            catch { }
            
			return productItem;
		}
	}
}
