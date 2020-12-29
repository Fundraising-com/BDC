USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_clients_sales_Informations]    Script Date: 02/14/2014 13:02:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_clients_sales_Informations]
AS
SELECT     TOP 100 PERCENT dbo.v_sales_informations.Sale_Id, dbo.v_sales_informations.Client_Seq, dbo.v_sales_informations.Id, 
                      dbo.v_sales_informations.Sale_Date, dbo.v_sales_informations.Scheduled_Delivery, dbo.v_sales_informations.Consultant, 
                      dbo.v_sales_informations.Sale_Status, dbo.v_sales_informations.Production_Status, dbo.v_sales_informations.Method, 
                      dbo.v_sales_informations.Term, dbo.v_sales_informations.PO, dbo.v_sales_informations.PO_Status, 
                      dbo.Client.Interested_In_Agent AS Interested_Being_Agent, dbo.v_sales_informations.Comment, dbo.v_sales_informations.Carrier, 
                      dbo.v_sales_informations.Shipping_Option, dbo.v_sales_informations.Billing_Company_Code, 
                      dbo.v_sales_coupons_assign.Coupon_Sheet_Assigned, dbo.v_sales_local_sponsor_found.Local_Sponsor_Found
FROM         dbo.Client LEFT OUTER JOIN
                      dbo.v_sales_local_sponsor_found RIGHT OUTER JOIN
                      dbo.v_sales_informations ON dbo.v_sales_local_sponsor_found.Sales_ID = dbo.v_sales_informations.Sale_Id LEFT OUTER JOIN
                      dbo.v_sales_coupons_assign ON dbo.v_sales_informations.Sale_Id = dbo.v_sales_coupons_assign.Sales_ID ON 
                      dbo.Client.Client_ID = dbo.v_sales_informations.Id AND dbo.Client.Client_Sequence_Code = dbo.v_sales_informations.Client_Seq
ORDER BY dbo.v_sales_informations.Sale_Id DESC
GO
