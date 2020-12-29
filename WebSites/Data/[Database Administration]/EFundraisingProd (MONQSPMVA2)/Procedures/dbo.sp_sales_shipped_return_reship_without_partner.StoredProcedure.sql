USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_sales_shipped_return_reship_without_partner]    Script Date: 02/14/2014 13:09:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[sp_sales_shipped_return_reship_without_partner] (@date_from VARCHAR(10) = '', @date_to VARCHAR(10) = '') AS
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

-- sales shipped
SELECT  'shipped sales' AS type, dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, 
	dbo.Client.Client_ID, dbo.Sale.Sales_ID, 
        dbo.Sale.Actual_Ship_Date AS action_date, dbo.Sale.Total_Amount
FROM    dbo.Client INNER JOIN
        dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
        dbo.Lead ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
        dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID
WHERE   dbo.sale.actual_ship_date BETWEEN @date_from_temp AND @date_to_temp 
	AND Sale.Actual_Ship_Date IS NOT NULL AND Sale.Reship_Date IS NULL AND Sale.Box_Return_Date IS NULL
UNION 
-- sales box return 
SELECT  'returned sales' AS type, dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, 
	dbo.Client.Client_ID, dbo.Sale.Sales_ID, 
        dbo.sale.Box_return_date AS action_date, dbo.Sale.Total_Amount
FROM    dbo.Client INNER JOIN
        dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
        dbo.Lead ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
        dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID
WHERE   dbo.sale.box_return_date BETWEEN @date_from_temp AND @date_to_temp
	AND Sale.Actual_Ship_Date IS NOT NULL AND Sale.Reship_Date IS NULL AND Sale.Box_Return_Date IS NOT NULL
UNION
-- sales box reship
SELECT  'reshipped sales' AS type, dbo.Client.Lead_ID, dbo.Client.Client_Sequence_Code, 
	dbo.Client.Client_ID, dbo.Sale.Sales_ID, 
        dbo.sale.reship_date AS action_date, dbo.Sale.Total_Amount
FROM    dbo.Client INNER JOIN
        dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
        dbo.Lead ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
        dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID
WHERE  dbo.sale.reship_date BETWEEN @date_from_temp AND @date_to_temp
	AND Sale.Actual_Ship_Date IS NOT NULL AND Sale.Reship_Date IS NOT NULL AND Sale.Box_Return_Date IS NOT NULL;
GO
