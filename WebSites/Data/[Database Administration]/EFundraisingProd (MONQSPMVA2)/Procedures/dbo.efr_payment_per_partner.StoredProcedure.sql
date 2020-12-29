USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_payment_per_partner]    Script Date: 02/14/2014 13:03:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_payment_per_partner] 
(@date_from datetime, @date_to datetime)
AS
--declare @date_from datetime
--declare @date_to datetime

--set @date_from = '2005-01-01' 
--set @date_to = '2005-03-31 23:59:59'

select 

       	pa.partner_name
	, spc.product_class_desc
	--, s.sales_id
	, sum(pay.payment_amount) as total_payment
	--, si.sales_amount as amount
	--,s.sales_date
	
from
	promotion p 
	inner join lead l
		on l.promotion_id = p.promotion_id
	inner join partner pa
		on pa.partner_id = p.partner_id
	left outer join client c
		on c.lead_id = l.lead_id
       	inner join sale s
		on s.client_id = c.client_id 
		and s.client_sequence_code = c.client_sequence_code
       	inner join (select distinct si.sales_id,pc.Description  as product_class_desc 
		from sales_item si 
--			on si.sales_id = s.sales_id
       		inner join scratch_book sb
			on sb.scratch_book_id = si.scratch_book_id
	       	JOIN product_class pc
			on pc.product_class_id = sb.product_class_id
		where si.sales_amount <> 0
		) spc
		on spc.sales_id = s.sales_id 
	inner join payment pay
		on pay.sales_id = s.sales_id
	
where 
	s.Actual_Ship_Date is not null
	and s.sales_status_id = 2
	and pay.payment_entry_date between @date_from AND @date_to
	--and s.sales_date between @date_from AND @date_to
group by 
	pa.partner_name
	, spc.product_class_desc
order by 1
--SELECT * FROM dbo.partner_commission
GO
