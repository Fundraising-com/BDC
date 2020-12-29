USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_sales_informations]    Script Date: 02/14/2014 13:02:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_sales_informations]
AS
SELECT     TOP 100 PERCENT dbo.Sale.Sales_ID AS Sale_Id, dbo.Sale.Client_Sequence_Code AS Client_Seq, dbo.Sale.Client_ID AS Id, 
                      dbo.Sale.Sales_Date AS Sale_Date, dbo.AR_Status.Description AS AR_Description, dbo.Sale.Scheduled_Delivery_Date AS Scheduled_Delivery, 
                      dbo.Consultant.Name AS Consultant, dbo.Consultant.Consultant_ID AS Consultant_Id, dbo.Sales_Status.Description AS Sale_Status, 
                      dbo.Sales_Status.Sales_Status_ID AS Sale_Status_ID, dbo.Production_Status.Description AS Production_Status, 
                      dbo.Payment_Method.Description AS Method, dbo.Payment_Term.Payment_Term_ID AS Payment_Term_ID, dbo.Payment_Term.Description AS Term, 
                      dbo.Sale.PO_Number AS PO, dbo.PO_Status.Description AS PO_Status, dbo.Sale.Comment, dbo.Carrier.Description AS Carrier, 
                      dbo.Carrier_Shipping_Option.Description AS Shipping_Option, dbo.Billing_Company.Billing_Company_Code, dbo.Sale.Total_Amount, 
                      dbo.Sale.Shipping_Fees, dbo.Sale.Shipping_Fees_Discount, dbo.Sale.Waybill_No, dbo.Sale.Actual_Ship_Date, dbo.Sale.Confirmed_Date, 
                      dbo.Sale.Payment_Due_Date
FROM         dbo.Sales_Status INNER JOIN
                      dbo.Consultant INNER JOIN
                      dbo.Production_Status INNER JOIN
                      dbo.PO_Status RIGHT OUTER JOIN
                      dbo.Payment_Term INNER JOIN
                      dbo.Payment_Method INNER JOIN
                      dbo.Carrier RIGHT OUTER JOIN
                      dbo.Sale LEFT OUTER JOIN
                      dbo.Carrier_Shipping_Option ON dbo.Sale.Shipping_Option_ID = dbo.Carrier_Shipping_Option.Shipping_Option_ID LEFT OUTER JOIN
                      dbo.Billing_Company ON dbo.Sale.Billing_Company_ID = dbo.Billing_Company.Billing_Company_ID ON 
                      dbo.Carrier.Carrier_ID = dbo.Sale.Carrier_ID ON dbo.Payment_Method.Payment_Method_ID = dbo.Sale.Payment_Method_ID ON 
                      dbo.Payment_Term.Payment_Term_ID = dbo.Sale.Payment_Term_ID ON dbo.PO_Status.PO_Status_ID = dbo.Sale.PO_Status_ID ON 
                      dbo.Production_Status.Production_Status_ID = dbo.Sale.Production_Status_ID ON dbo.Consultant.Consultant_ID = dbo.Sale.Consultant_ID ON 
                      dbo.Sales_Status.Sales_Status_ID = dbo.Sale.Sales_Status_ID LEFT OUTER JOIN
                      dbo.AR_Status ON dbo.Sale.AR_Status_ID = dbo.AR_Status.AR_Status_ID
ORDER BY dbo.Sale.Sales_ID DESC
GO
