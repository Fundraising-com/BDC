USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_sponsor_check_summary_report]    Script Date: 02/14/2014 13:06:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec es_rpt_sponsor_check_summary_report 1236204 
/*declare @end_date datetime
set @end_date = '1900-01-01 00:00:00.000'
Select '/20/'
select  Convert(varchar(20), Month(DateAdd(month,1, @end_date))) + '/20/'  + Convert(varchar(10), year(@end_date)) 
*/	
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: April 23, 2010
-- Description:	PROC FOR VARIABLE PROFIT CHECK SUMMARY REPORT.
-- =============================================
--CREATE PROCEDURE [dbo].[es_rpt_sponsor_check_summary_report] 
CREATE PROCEDURE [dbo].[es_rpt_sponsor_check_summary_report] 
	@event_id int
AS
BEGIN
	
    select (case  when pit.profit_range_id = 1 then convert(varchar(20), p.cheque_number) + ' *'
			when p.cheque_number = 1 then 'N/A' else convert(varchar(20), p.cheque_number) end) as cheque_number
		, LEFT(DATENAME(month, pp.start_date), 3) + ' ' + DATENAME(day, pp.start_date) + ', ' + DATENAME(year, pp.start_date) + ' to ' +
			 LEFT(DATENAME(month, pp.end_date), 3) + ' ' + DATENAME(day, pp.end_date) + ', ' + DATENAME(year, pp.end_date) as check_period
		, sum(od.quantity) as total_item_count
		, Sum(pit.order_detail_amount) as total_amount_purchase
		, sum(pit.profit_amount) as total_profit_amount
        , convert(decimal(8, 3), pit.profit_percentage) as profit_percentage
		, p.paid_amount  as paid_amount
		, pps.payment_status_id
		, (case when confirmation_date is null then Convert(varchar(10),'N/A')
			when  confirmation_date = '1900-01-01 00:00:00.000'  then Convert(varchar(10), Month(DateAdd(month,1, pp.end_date))) + '/20/'  + Convert(varchar(10), year(pp.end_date)) 
			else Convert(varchar(10),confirmation_date, 101) end) as date_check_sent
        , pit.profit_id
		, pit.profit_range_id
	from payment_info pin
	inner join payment p on pin.payment_info_id = p.payment_info_id
	left join payment_period pp on pp.payment_period_id = p.payment_period_id
    inner join payment_item pit on pit.payment_id = p.payment_id 
	inner join qspfulfillment.dbo.[order_detail]  od on pit.qsp_order_detail_id = od.order_detail_id 
	inner join payment_payment_status pps on pps.payment_id = p.payment_id
	inner join (
		select payment_id, max(create_date) as create_date
		from payment_payment_status
		group by payment_id
		   ) ppsNew on ppsNew.payment_id = pps.payment_id and ppsNew.create_date = pps.create_date 	and payment_status_id in (2, 10)
	left join dbo.payment_batch pb on pb.payment_batch_id = p.payment_batch_id and confirmation_date is not null
	where event_id = @event_id
	group by  (case  when pit.profit_range_id = 1 then convert(varchar(20), p.cheque_number) + ' *'
			when p.cheque_number = 1 then 'N/A' else convert(varchar(20), p.cheque_number) end) 
		, LEFT(DATENAME(month, pp.start_date), 3) + ' ' + DATENAME(day, pp.start_date) + ', ' + DATENAME(year, pp.start_date) + ' to ' +
		 LEFT(DATENAME(month, pp.end_date), 3) + ' ' + DATENAME(day, pp.end_date) + ', ' + DATENAME(year, pp.end_date)
		, confirmation_date
		 , pit.profit_percentage 
		--, pit.profit_amount
		, p.paid_amount
		, pp.start_date
		, pp.end_date--,p.cheque_date 
		, pps.payment_status_id
	    , pit.profit_id
	, pit.profit_range_id
	order by pp.end_date, pit.profit_percentage
END
GO
