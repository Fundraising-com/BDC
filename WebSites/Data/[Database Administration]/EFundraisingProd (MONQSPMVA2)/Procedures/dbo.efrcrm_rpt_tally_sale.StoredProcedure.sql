USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_rpt_tally_sale]    Script Date: 02/14/2014 13:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_rpt_tally_sale] @sales_id int AS

select 
	s.sales_id
	,p.description as packageName
	,sb.description as product
	,
	CASE sb.raising_potential
		WHEN 0 
			THEN 0
		ELSE 100*(sb.raising_potential - si.unit_price_sold)/sb.raising_potential
	END AS profit
	, sb.product_code
	, si.quantity_sold
	, sb.raising_potential
	, sb.raising_potential*si.quantity_sold as Total_Amount
	, sb.raising_potential - si.unit_price_sold as Total_Profit
	--, Sum(sb.raising_potential*si.quantity_sold) as Total_Amount
	--, Sum(sb.raising_potential - si.unit_price_sold) as Total_Profit
from sale s
	inner join sales_item si 
		on s.sales_id = si.sales_id
	inner join scratch_book sb 
		on si.scratch_book_id = sb.scratch_book_id 
	inner join package p 
		on sb.package_id = p.package_id
where s.sales_id = @sales_id
group by 
	s.sales_id, 
	p.description, 
	sb.description, 
	sb.raising_potential, 
	sb.product_code, 
	si.quantity_sold, 
	si.unit_price_sold
GO
