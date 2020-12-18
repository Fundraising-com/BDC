using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QSP.OrderExpress.Web.Code;

using NHibernate;
using NHibernate.Expression;

using QSP.Business.Fulfillment;

namespace QSP.OrderExpress.Web {
    public partial class Test : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            ////int paId = 1510;
            ////int paId = 1502;
            ////QSP.Business.Fulfillment.ProgramAgreement pa = QSP.Business.Fulfillment.ProgramAgreement.GetProgramAgreement(paId);

            //// int orderId = 2613927;   // Pine Valley
            //int orderId = 2619367;   // Otis Spunkmeyer

            ////QSPForm.Business.Communication.Notifications notifications = new QSPForm.Business.Communication.Notifications();
            ////QSPForm.Business.Controller.OrderController oc = new QSPForm.Business.Controller.OrderController();
            ////oc.GenerateCharges(orderId, notifications);

            //this.ChargeList2.OrderId = orderId;
            //this.ChargeList2.LoadData();
            //this.ChargeList2.BindList();

            //Response.Write("Done!");

            //List<QSP.Business.Fulfillment.Campaign> campaignList = QSP.Business.Fulfillment.Campaign.GetCampaignList(2010, 357982, 7);


            //int productId = 13857;
            //int catalogId = 132;
            
            //ICriteria criteria = CatalogItem.CreateCriteria2();

            //criteria.Add(Expression.Eq(CatalogItem.ProductIdProperty, productId));
            //criteria.Add(Expression.Eq(CatalogItem.CatalogIdProperty, catalogId));
            
            //List<CatalogItem> catalogItemList = CatalogItem.GetCatalogItemList(criteria);

            //List<CatalogItem> catalogItemList = CatalogItem.GetCatalogItemListByCatalogId(catalogId);


            int a = 0;
        }
    } 
}