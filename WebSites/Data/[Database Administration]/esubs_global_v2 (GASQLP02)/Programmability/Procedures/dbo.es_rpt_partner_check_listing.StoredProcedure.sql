USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_check_listing]    Script Date: 02/14/2014 13:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rpt_partner_check_listing]
    @start_date as datetime 
  , @end_date as datetime
  , @partner_id int
AS
BEGIN

/*	declare @start_date datetime
    declare @end_date datetime
    declare @partner_id int

	set @start_date = '2008-09-01 00:00:00'
	set @end_date = getdate()--'2008-11-01 00:00:00'
	set @partner_id = 8 
	
NOTE: 
11/18/2009 DP -- Generate listing of checks for a specified partner
	
*/


declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)

  -- header du rapport
    select '-Partner ID'
           , '-Group ID'
		   , 'Group Name'
           , '-Event ID'
           , '-Payment Period ID'
           , '-Payment ID'
           , 'Cheque Date'
	   , 'Cheque Number'
	   , '$Check Amount'
	   , 'Payment Name'
	   , 'Address'
	   , 'Country-State'
	   , 'Postal Code'


SELECT   dbo.[group].partner_id as partner_id,  
	 dbo.[group].group_id	as group_id, 
	 group_name		as group_name, 
	 event_id		as event_id, 
	 payment_period_id	as payment_period_id, 
	 payment_id		as payment_id, 
	 cheque_date		as cheque_date, 
	 cheque_number		as cheque_number,  
	 paid_amount		as paid_amount, 
	 payment_name		as payment_name, 
	 address_1		as address, 
	 subdivision_code	as country_state, 
	 zip_code		as postal_code 
FROM dbo.payment INNER JOIN
     dbo.payment_info ON dbo.payment.payment_info_id = dbo.payment_info.payment_info_id INNER JOIN
     dbo.[group] ON dbo.payment_info.group_id = dbo.[group].group_id
WHERE (dbo.[group].partner_id = @partner_id)
and cheque_date between @start_date and @end_date
and cheque_number > 1
order by cheque_date


end
GO
