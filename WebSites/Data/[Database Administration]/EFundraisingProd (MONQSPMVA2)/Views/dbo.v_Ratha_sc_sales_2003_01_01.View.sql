USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_Ratha_sc_sales_2003_01_01]    Script Date: 02/14/2014 13:02:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_Ratha_sc_sales_2003_01_01]
AS
SELECT     TOP 100 PERCENT dbo.Sale.Sales_ID, dbo.Client_Address.State_Code, dbo.Client_Address.Zip_Code, dbo.Client_Address.Address_Type, 
                      dbo.Sale.Actual_Ship_Date, dbo.Sale.Total_Amount, dbo.Carrier.Description AS Carrier, dbo.Carrier_Shipping_Option.Description AS Shipping_Option, 
                      dbo.Payment_Term.Description AS Payment_Term, SUM(dbo.Sales_Item.Quantity_Sold) AS Qty
FROM         dbo.Sale INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Client ON dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code AND dbo.Sale.Client_ID = dbo.Client.Client_ID INNER JOIN
                      dbo.Client_Address ON dbo.Client.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Client.Client_ID = dbo.Client_Address.Client_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Scratch_Book.Product_Class_ID = dbo.Product_Class.Product_Class_ID INNER JOIN
                      dbo.Carrier ON dbo.Sale.Carrier_ID = dbo.Carrier.Carrier_ID INNER JOIN
                      dbo.Payment_Term ON dbo.Sale.Payment_Term_ID = dbo.Payment_Term.Payment_Term_ID INNER JOIN
                      dbo.Carrier_Shipping_Option ON dbo.Sale.Shipping_Option_ID = dbo.Carrier_Shipping_Option.Shipping_Option_ID
GROUP BY dbo.Sale.Sales_ID, dbo.Client_Address.State_Code, dbo.Client_Address.Zip_Code, dbo.Client_Address.Address_Type, dbo.Sale.Actual_Ship_Date, 
                      dbo.Sale.Total_Amount, dbo.Carrier.Description, dbo.Payment_Term.Description, dbo.Carrier_Shipping_Option.Description
HAVING      (dbo.Client_Address.Address_Type = 'st') AND (dbo.Sale.Actual_Ship_Date > CONVERT(DATETIME, '2003-01-01 00:00:00', 102))
ORDER BY dbo.Sale.Actual_Ship_Date
GO
