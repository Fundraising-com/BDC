IF @@servername NOT IN ('GASQLT02', 'MONQSPMVA2')
BEGIN
	RAISERROR('Wrong server!', 20, 1) WITH LOG
END
GO
use efundraisingprod;
GO
IF EXISTS (SELECT NULL
           FROM sys.objects
           WHERE ([object_id] = OBJECT_ID(N'report_gross_profit')) AND
                 (OBJECTPROPERTY([object_id], N'IsProcedure') = 1))
   BEGIN
      DROP PROCEDURE report_gross_profit;
   END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE report_gross_profit
  @actualShipStartDate1 DATETIME
, @actualShipEndDate1 DATETIME
, @actualShipStartDate2 DATETIME
, @actualShipEndDate2 DATETIME
AS

BEGIN
	SELECT
		'T1' AS 'T',
		T1.name AS 'Name',
		T1.Product_Description AS 'Product Class',
		COUNT(T1.sales_id) AS 'Sales',
		CONVERT(DECIMAL(10,2), SUM(T1.total_amount)) AS 'Amount Sold',
		CONVERT(DECIMAL(10,2), CASE WHEN (SUM(T1.cost_of_goods) = 0 OR SUM(T1.total_amount) = 0) THEN 0 ELSE (SUM(T1.cost_of_goods) * 100.0) / SUM(T1.total_amount) END) AS 'GP%',
		CONVERT(DECIMAL(10,2), SUM(T1.cost_of_goods)) AS 'GP Amount'
	FROM (
		SELECT
			Consultant.name,
			dbo.sale.sales_id,
			dbo.sale.total_amount,
			product_class.description as [Product_Description], 
			SUM(isnull(cost_of_goods.cost, 0) * sales_item.quantity_sold) as 'cost_of_goods'
		FROM
 
			  Lead (NOLOCK)
			  INNER JOIN Consultant (NOLOCK)  ON Lead.consultant_id = Consultant.consultant_id
			  INNER JOIN Promotion (NOLOCK) ON Lead.promotion_id = Promotion.promotion_id
			  INNER JOIN Partner (NOLOCK) ON Promotion.partner_id = Partner.partner_id
			  LEFT JOIN Client (NOLOCK) ON Lead.lead_id = Client.lead_id
			  LEFT JOIN sale (NOLOCK) on sale.client_id = client.client_id AND sale.client_sequence_code = Client.client_sequence_code
			  LEFT JOIN Sales_Item (NOLOCK) ON dbo.sale.sales_id = Sales_Item.sales_id
			  LEFT JOIN Scratch_Book (NOLOCK) ON Sales_Item.scratch_book_id = Scratch_Book.scratch_book_id
			  LEFT JOIN product_class (NOLOCK) ON Scratch_Book.product_class_id = product_class.product_class_id
			  LEFT JOIN (select country_code, scratch_book_id, product_class_id from scratch_book_price_info group by country_code, scratch_book_id, product_class_id) AS prices ON prices.scratch_book_id = Scratch_Book.scratch_book_id AND prices.product_class_id = product_class.product_class_id
			  LEFT JOIN cost_of_goods (NOLOCK) ON prices.scratch_book_id = cost_of_goods.scratch_book_id AND prices.country_code = cost_of_goods.country AND (Sales_Item.quantity_sold BETWEEN cost_of_goods.quantity_from AND cost_of_goods.quantity_to) AND (dbo.sale.actual_ship_date BETWEEN cost_of_goods.effective_start_date AND cost_of_goods.effective_end_date)
		WHERE
			  dbo.sale.actual_ship_date BETWEEN @actualShipStartDate1 AND @actualShipEndDate1
 
		GROUP BY consultant.name, dbo.sale.sales_id, dbo.sale.total_amount, product_class.description) T1
	GROUP BY T1.name, T1.Product_Description
	UNION
	SELECT
		'T2' AS 'T',
		T2.name AS 'Name',
		T2.Product_Description AS 'Product Class',
		COUNT(T2.sales_id) AS 'Sales',
		CONVERT(DECIMAL(10,2), SUM(T2.total_amount)) AS 'Amount Sold',
		CONVERT(DECIMAL(10,2), CASE WHEN (SUM(T2.cost_of_goods) = 0 OR SUM(T2.total_amount) = 0) THEN 0 ELSE (SUM(T2.cost_of_goods) * 100.0) / SUM(T2.total_amount) END) AS 'GP%',
		CONVERT(DECIMAL(10,2), SUM(T2.cost_of_goods)) AS 'GP Amount'
	FROM (
		SELECT
			Consultant.name,
			dbo.sale.sales_id,
			dbo.sale.total_amount,
			product_class.description as [Product_Description], 
			SUM(isnull(cost_of_goods.cost, 0) * sales_item.quantity_sold) as 'cost_of_goods'
		FROM
 
			  Lead (NOLOCK)
			  INNER JOIN Consultant (NOLOCK)  ON Lead.consultant_id = Consultant.consultant_id
			  INNER JOIN Promotion (NOLOCK) ON Lead.promotion_id = Promotion.promotion_id
			  INNER JOIN Partner (NOLOCK) ON Promotion.partner_id = Partner.partner_id
			  LEFT JOIN Client (NOLOCK) ON Lead.lead_id = Client.lead_id
			  LEFT JOIN sale (NOLOCK) on sale.client_id = client.client_id AND sale.client_sequence_code = Client.client_sequence_code
			  LEFT JOIN Sales_Item (NOLOCK) ON dbo.sale.sales_id = Sales_Item.sales_id
			  LEFT JOIN Scratch_Book (NOLOCK) ON Sales_Item.scratch_book_id = Scratch_Book.scratch_book_id
			  LEFT JOIN product_class (NOLOCK) ON Scratch_Book.product_class_id = product_class.product_class_id
			  LEFT JOIN (select country_code, scratch_book_id, product_class_id from scratch_book_price_info group by country_code, scratch_book_id, product_class_id) AS prices ON prices.scratch_book_id = Scratch_Book.scratch_book_id AND prices.product_class_id = product_class.product_class_id
			  LEFT JOIN cost_of_goods (NOLOCK) ON prices.scratch_book_id = cost_of_goods.scratch_book_id AND prices.country_code = cost_of_goods.country AND (Sales_Item.quantity_sold BETWEEN cost_of_goods.quantity_from AND cost_of_goods.quantity_to) AND (dbo.sale.actual_ship_date BETWEEN cost_of_goods.effective_start_date AND cost_of_goods.effective_end_date)
		WHERE
			  dbo.sale.actual_ship_date BETWEEN @actualShipStartDate2 AND @actualShipEndDate2
 
		GROUP BY consultant.name, dbo.sale.sales_id, dbo.sale.total_amount, product_class.description) T2
	GROUP BY T2.name, T2.Product_Description

END