USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_sponsor_check_report]    Script Date: 02/14/2014 13:06:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Jiro Hidaka>
-- Create date: <12-08-2009>
-- Description:	<Broke 'es_rpt_campaign_check_report' into 2 separate sp's:
--                1) es_rpt_sponsor_profit_report
--                2) es_rpt_sponsor_check_report >
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_sponsor_check_report]
    -- Add the parameters for the stored procedure here
	@event_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select p.cheque_number, Convert(varchar(10),Isnull(pp.end_date, p.cheque_date), 101)  as check_date, p.paid_amount, 
           DATENAME(month,Isnull(pp.end_date, p.cheque_date)) + ' ' + DATENAME(year,Isnull(pp.end_date, p.cheque_date)) as check_period
			from payment_info pin
				inner join payment p on pin.payment_info_id = p.payment_info_id
			left join
			(
				select pps.payment_id, pps.payment_status_id
				from payment_payment_status pps
				   inner join
				   (
				select payment_id, max(create_date) as create_date
				from payment_payment_status
				group by payment_id
				   ) ppsNew on ppsNew.payment_id = pps.payment_id and ppsNew.create_date = pps.create_date 
			) pcancel
			on pcancel.payment_id = p.payment_id
				left join payment_period pp
					 on pp.payment_period_id = p.payment_period_id
			where (event_id = @event_id and isnull(pcancel.payment_status_id, 2) <> 9)
			order by Isnull(pp.end_date, p.cheque_date)
END
GO
