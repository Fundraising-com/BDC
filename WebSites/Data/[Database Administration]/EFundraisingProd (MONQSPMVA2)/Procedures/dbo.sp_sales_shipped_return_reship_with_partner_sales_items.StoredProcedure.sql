USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_sales_shipped_return_reship_with_partner_sales_items]    Script Date: 02/14/2014 13:09:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   PROCEDURE [dbo].[sp_sales_shipped_return_reship_with_partner_sales_items] (@date_from VARCHAR(10) = '', @date_to VARCHAR(10) = '', @partner_id INTEGER = 0) AS
-- debut sp_
DECLARE @date_from_temp DATETIME;
DECLARE @date_to_temp DATETIME;

if @date_from = ''
	SET @date_from_temp = getdate();
else
	SET @date_from_temp = @date_from;

if @date_to = ''
	SET @date_to_temp = getdate();
else
	SET @date_to_temp = @date_to;


SELECT 'shipped sales' AS type, dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, dbo.Sale.Sales_ID, 
                      dbo.Sale.Actual_Ship_Date AS action_date, dbo.Sale.Total_Amount, dbo.Sales_Item.Sales_Item_No, dbo.Sales_Item.Sales_Amount AS sale, 
                      dbo.Product_Class.Description AS product_class_desc, dbo.Payment.Payment_Amount AS Payment_Amount, dbo.Payment.Payment_Entry_Date
	FROM         dbo.Client INNER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
                      dbo.Lead ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Scratch_Book.Product_Class_ID = dbo.Product_Class.Product_Class_ID LEFT OUTER JOIN
                      dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID
	WHERE   dbo.Promotion.partner_id = @partner_id and dbo.sale.actual_ship_date BETWEEN @date_from_temp AND @date_to_temp 
		AND Sale.Actual_Ship_Date IS NOT NULL AND Sale.Reship_Date IS NULL AND Sale.Box_Return_Date IS NULL
	GROUP BY dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, dbo.Sale.Sales_ID, dbo.Sale.Actual_Ship_Date, dbo.Sale.Total_Amount, 
                      dbo.Sales_Item.Sales_Item_No, dbo.Product_Class.Description, dbo.Sales_Item.Sales_Amount, dbo.Payment.Payment_Amount, 
                      dbo.Payment.Payment_Entry_Date, Sale.Box_Return_Date, Sale.Reship_Date
UNION 
-- sales box return 
SELECT  'returned sales' AS type, dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, 
	dbo.Client.Client_ID, dbo.Sale.Sales_ID, 
        dbo.sale.Box_return_date AS action_date, dbo.Sale.Total_Amount, dbo.Sales_Item.Sales_Item_No, dbo.Sales_Item.Sales_Amount AS sale, 
        dbo.Product_Class.Description AS product_class_desc, dbo.Payment.Payment_Amount AS Payment_Amount, dbo.Payment.Payment_Entry_Date
	FROM  dbo.Client INNER JOIN
              	      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
            	      dbo.Lead ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Scratch_Book.Product_Class_ID = dbo.Product_Class.Product_Class_ID LEFT OUTER JOIN
                      dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID
	WHERE   dbo.Promotion.partner_id = @partner_id and dbo.sale.box_return_date BETWEEN @date_from_temp AND @date_to_temp
	AND Sale.Actual_Ship_Date IS NOT NULL AND Sale.Reship_Date IS NULL AND Sale.Box_Return_Date IS NOT NULL
	GROUP BY dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, dbo.Sale.Sales_ID, dbo.Sale.Actual_Ship_Date, dbo.Sale.Total_Amount, 
                      dbo.Sales_Item.Sales_Item_No, dbo.Product_Class.Description, dbo.Sales_Item.Sales_Amount, dbo.Payment.Payment_Amount, 
                      dbo.Payment.Payment_Entry_Date, Sale.Box_Return_Date, Sale.Reship_Date
UNION
-- sales box reship
SELECT  'reshipped sales' AS type, dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, 
	dbo.Client.Client_ID, dbo.Sale.Sales_ID, 
        dbo.sale.reship_date AS action_date, dbo.Sale.Total_Amount, dbo.Sales_Item.Sales_Item_No, dbo.Sales_Item.Sales_Amount AS sale, 
                      dbo.Product_Class.Description AS product_class_desc, dbo.Payment.Payment_Amount AS Payment_Amount, dbo.Payment.Payment_Entry_Date
	FROM         dbo.Client INNER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
                      dbo.Lead ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Scratch_Book.Product_Class_ID = dbo.Product_Class.Product_Class_ID LEFT OUTER JOIN
                      dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID
	WHERE   dbo.Promotion.partner_id = @partner_id and 
	dbo.sale.reship_date BETWEEN @date_from_temp AND @date_to_temp
	AND Sale.Actual_Ship_Date IS NOT NULL AND Sale.Reship_Date IS NOT NULL AND Sale.Box_Return_Date IS NOT NULL
	GROUP BY dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, dbo.Sale.Sales_ID, dbo.Sale.Actual_Ship_Date, dbo.Sale.Total_Amount, 
                      dbo.Sales_Item.Sales_Item_No, dbo.Product_Class.Description, dbo.Sales_Item.Sales_Amount, dbo.Payment.Payment_Amount, 
                      dbo.Payment.Payment_Entry_Date, Sale.Box_Return_Date, Sale.Reship_Date;
GO
