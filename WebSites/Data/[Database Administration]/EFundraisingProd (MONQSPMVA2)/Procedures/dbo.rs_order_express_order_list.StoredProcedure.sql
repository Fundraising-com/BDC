USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_order_express_order_list]    Script Date: 02/14/2014 13:08:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[rs_order_express_order_list] -- '2008-2-1', '2008-2-29'
	  @start_date datetime 
	, @end_date datetime 
AS
BEGIN 


SELECT sale.sales_id, 
       sale.ext_order_id, 
       sale.qsp_order_id, 
       Production_Status.Description as production_status, 
       sale.sales_date,
       sale.actual_ship_date, 
       sale.total_amount, 
       [quantity_sold]+[quantity_free] AS qty, 
       Scratch_Book.description as product,
       c.name
FROM  sale INNER JOIN 
      Production_Status ON sale.production_status_id = Production_Status.Production_Status_ID INNER JOIN
      Sales_Item ON sale.sales_id = Sales_Item.sales_id INNER JOIN 
      Scratch_Book ON Sales_Item.scratch_book_id = Scratch_Book.scratch_book_id inner join
      consultant c on sale.consultant_id = c.consultant_id

WHERE (sale.ext_order_id > 0 or sale.production_status_id = 17)
      and sale.sales_date between @start_date and @end_date
ORDER BY sale.sales_date DESC, sale.sales_id desc


END
GO
