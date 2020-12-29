USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_product_order_form]    Script Date: 02/14/2014 13:08:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rs_product_order_form]  AS

SELECT top 100 
Sale.sales_id,Client.organization, 
Client.[First_name] + ' ' + [Last_Name] as [Name],Default_Ship_To_Address_View.Street_Address, Default_Ship_To_Address_View.[City] + ', ' + [State_Code] + ' ' + [Zip_Code] AS Ville, 
Sales_Item.quantity_sold + Sales_Item.quantity_free AS QTY, Product_Class.description AS ITEM, Sales_Item.unit_price_sold AS PRICE, 
(Sales_Item.quantity_sold + Sales_Item.quantity_free)*(Sales_Item.unit_price_sold) AS TOTAL, 
Scratch_Book.product_code, Client.day_phone, Scratch_Book.description, Sale.scheduled_delivery_date
FROM 
(Scratch_Book 
INNER JOIN (Client 
INNER JOIN ((Sale 
INNER JOIN Default_Ship_To_Address_View
ON (Sale.client_sequence_code = Default_Ship_To_Address_View.Client_Sequence_Code) AND (Sale.client_id = Default_Ship_To_Address_View.Client_ID)) 
INNER JOIN Sales_Item ON Sale.sales_id = Sales_Item.sales_id) ON (Client.client_sequence_code = Sale.client_sequence_code) AND (Client.client_id = Sale.client_id)) ON Scratch_Book.scratch_book_id = Sales_Item.scratch_book_id) 
INNER JOIN Product_Class ON Scratch_Book.product_class_id = Product_Class.product_class_id
WHERE (((Sales_Item.quantity_sold)>0))
ORDER BY Scratch_Book.product_code
GO
