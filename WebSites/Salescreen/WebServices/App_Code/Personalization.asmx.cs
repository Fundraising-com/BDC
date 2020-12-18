using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Drawing;
//using Microsoft.Web.Services2;
//using Microsoft.Web.Services2.Dime;
using System.Net;
using QSPForm.Business;
using QSPForm.Common;
using System.Web.Security;

namespace QSPFormWebServices
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	[WebService(Namespace="http://www.OrderExpress.com/QSPFormWebServices/")]
	public class Personalization : BaseWebService
	{
		public Personalization()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion


		/// <summary>
		/// This method returns information about all order detail for one order
		/// </summary>
		/// <param name="AccountTraksUniqueID">Order ID </param>
		/// <returns>AccountTrackOrder Object
		/// [
		///		AccountTrakUniqueID,
		///		AS400_OrderNumber,
		///		OrderStatus,
		///		OrderDetailList(Personalized item number, detail id)	
		/// ] 
		/// </returns>
		[WebMethod]
		public DataSet GetCategories()
		{
			
			VerifyAuthentification();
			DataSet dts = new DataSet("Categories");
			DataTable dtbl = new DataTable("Category");
			dtbl.Columns.Add("ID");
			dtbl.Columns.Add("Description");

			DataRow row = dtbl.NewRow();
			row[0] = 1;
			row[1] = "National";
			dtbl.Rows.Add(row);

			row = dtbl.NewRow();
			row[0] = 2;
			row[1] = "Regional";
			dtbl.Rows.Add(row);

			
			row = dtbl.NewRow();
			row[0] = 3;
			row[1] = "Personal";
			dtbl.Rows.Add(row);
			
			dts.Tables.Add(dtbl);
			return dts;
		}


		/// <summary>
		/// This method returns information about all order detail for one order
		/// </summary>
		/// <param name="AccountTraksUniqueID">Order ID </param>
		/// <returns>AccountTrackOrder Object
		/// [
		///		AccountTrakUniqueID,
		///		AS400_OrderNumber,
		///		OrderStatus,
		///		OrderDetailList(Personalized item number, detail id)	
		/// ] 
		/// </returns>
		[WebMethod]
		public DataSet Orphans(int orderID)
		{
			
			VerifyAuthentification();
			OrderSystem or = new OrderSystem();			
			DataSet dts = new DataSet("Order");
			DataTable dtbl = new DataTable("Order_Detail");
			dtbl = or.AV_SelectAllDetailByOrder(orderID);
			dts.Tables.Add(dtbl);
			return dts;
		}

		/// <summary>
		/// This method returns information about all order details for one order
		/// </summary>
		/// <param name="AS400_Number">fulf_number</param>
		/// <returns>AccountTrackOrder Object
		/// [
		///		AccountTrakUniqueID,
		///		AS400_OrderNumber,
		///		OrderStatus,
		///		OrderDetailList(Personalized item number, detail id)	
		/// ] 
		/// </returns>
		[WebMethod]
		public DataSet Standard(string fulfillmentOrderID)
		{
			VerifyAuthentification();
			OrderSystem or = new OrderSystem();			
			DataSet dts = new DataSet("Order");
			DataTable dtbl = new DataTable("Order_Detail");
			dtbl = or.AV_SelectAllDetailByAS400Number(fulfillmentOrderID);
			dts.Tables.Add(dtbl);
			return dts;
		}

		/// <summary>
		/// This method grabs the AccountTrackUniqueID and return a list
		/// of all available promos (type 1) or logos (type 2).
		/// </summary>
		/// <param name="AccountTracksUniqueID">
		///	 OrderID
		/// </param>
		/// <param name="Type">
		/// 1: Promo
		/// 2: Logo
		/// </param>
		[WebMethod]
		public DataSet AssetMgmt_List(int OrderID, int Type)
		{
			VerifyAuthentification();
			DataSet dts = new DataSet("AssetMgmt_List");
			switch(Type)
			{
				case 1:			//PROMO
					PromoSystem prmSys = new PromoSystem();
					dts.Tables.Add(prmSys.SelectAllPromoURLByOrderID(OrderID));
					break;
				case 2:			//LOGO
					LogoSystem lsys = new LogoSystem();
					dts.Tables.Add(lsys.SelectAllLogoURLByOrderID(OrderID));
					break;
				default:
					throw new Exception("You must choose your type between 1 for Promos and 2 for Logos");
			}
			return dts;
		}

        [WebMethod]
        public DataSet AssetMgmt_List_Formated(int OrderID, int Type, int Format)
        {
            try
            {
                VerifyAuthentification();
                DataSet dts = new DataSet("AssetMgmt_List");
                switch (Type)
                {
                    case 1:			//PROMO
                        //PromoSystem prmSys = new PromoSystem();
                        //dts.Tables.Add(prmSys.SelectAllPromoURLByOrderID(OrderID));
                        QSPForm.Business.PromoImageSystem prmSys = new PromoImageSystem();
                        dts.Tables.Add(prmSys.SelectAllPromoURLByOrderID(OrderID, Format));
                        break;
                    case 2:			//LOGO
                        LogoSystem lsys = new LogoSystem();
                        dts.Tables.Add(lsys.SelectAllLogoURLByOrderID(OrderID));
                        break;
                    default:
                        throw new Exception("You must choose your type between 1 for Promos and 2 for Logos");
                }
                return dts;
            }
            catch (Exception ex)
            {
                string x;
                x = ex.Message;
                return null;
            }

        }

		/// <summary>
		/// This method returns an array of byte that represents the selected image.
		/// </summary>
		/// <param name="AssetID">Promo or Logo ID</param>
		/// <param name="Type">1: Promo, 2: Logo</param>
		/// <param name="Resolution">1: Low Rez, 2 Hi Rez</param>
		/// <returns></returns>
		[WebMethod]
		public byte[] AssetMgmt_Image(int AssetID, int Type, int Resolution)
		{
			//Image img;
			VerifyAuthentification();

			//string img;
			byte [] img;

			if( (Resolution != 1) && (Resolution != 2))
			{
				throw new Exception("You must choose your type between 1 for Low Resolution (72dpi) and 2 for Hi Resolution (300dpi) ");
			}

			switch(Type)
			{
				case 1:			//PROMO
					PromoSystem prmSys = new PromoSystem();
					img = prmSys.GetImage(AssetID, Resolution);
					break;
				case 2:			//LOGO
					LogoSystem lsys = new LogoSystem();
					img = lsys.GetImage(AssetID, Resolution);
					break;
				default:
					throw new Exception("You must choose your type between 1 for Promos and 2 for Logos");
			}
			return img;
		}

        [WebMethod]
        public byte[] AssetMgmt_Image_Formated(int AssetID, int Type, int Resolution, int Format)
        {
            //Image img;
            VerifyAuthentification();

            //string img;
            byte[] img;

            if ((Resolution != 1) && (Resolution != 2))
            {
                throw new Exception("You must choose your type between 1 for Low Resolution (72dpi) and 2 for Hi Resolution (300dpi) ");
            }

            switch (Type)
            {
                case 1:			//PROMO_IMAGE
                    //PromoSystem prmSys = new PromoSystem();
                    //img = prmSys.GetImage(AssetID, Resolution);
                    QSPForm.Business.PromoImageSystem prmSys = new PromoImageSystem();
                    img = prmSys.GetImage(AssetID, Resolution, Format);
                    break;
                case 2:			//LOGO
                    LogoSystem lsys = new LogoSystem();
                    img = lsys.GetImage(AssetID, Resolution);
                    break;
                default:
                    throw new Exception("You must choose your type between 1 for Promos and 2 for Logos");
            }
            return img;
        }


		/// <summary>
		/// This method is used to update the personalizationID of all order detail in batch. 
		/// The PersonalizationDetail is an array of string that must be in this format :
		/// "OrderDetailID;PersonalizationID"
		/// </summary>
		/// <param name="OrderID">OrderID</param>
		/// <param name="PersonalizedDetail">Array of string containing the OrderDetailID and the PersonalizationID seperate by ; </param>
		[WebMethod]
		public string UpdatePersonalization(int OrderID, string [] PersonalizedDetail)
		{
            try
            {
                VerifyAuthentification();
                OrderSystem os = new OrderSystem();
                return os.UpdatePersonalization(OrderID, PersonalizedDetail); ;
            }
            catch (Exception ex)
            {
                return "failed : System error";
            }
		}
        /// <summary>
        /// This method is used to update the personalizationID of all order detail in batch. 
        /// The PersonalizationDetail is an array of string that must be in this format :
        /// "OrderDetailID;PersonalizationID"
        /// </summary>
        /// <param name="OrderID">OrderID</param>
        /// <param name="PersonalizedDetail">Array of string containing the OrderDetailID and the PersonalizationID seperate by ; </param>
        [WebMethod]
        public string UpdatePE(DataSet dts)
        {
            try
            {
                VerifyAuthentification();
                OrderSystem os = new OrderSystem();
                return os.UpdatePE(dts, this.UserID); ;
            }
            catch (Exception ex)
            {
                return "failed : System error";
            }
        }

		

	}
}
