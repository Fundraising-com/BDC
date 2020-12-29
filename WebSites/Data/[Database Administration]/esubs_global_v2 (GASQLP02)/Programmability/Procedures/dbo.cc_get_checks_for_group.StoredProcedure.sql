USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_checks_for_group]    Script Date: 02/14/2014 13:04:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cc_get_checks_for_group]
	@event_id varchar(20)
AS
BEGIN
	SELECT  CONVERT(varchar(50),pp.start_date,101) +  ' to ' + CONVERT(varchar(50),pp.end_date,101) as period
		  	, p.cheque_number
			, p.paid_amount
			, [pi].payment_name
			, [pi].payment_info_id as PaymentInfoID
			,  p.payment_id as PaymentID
            , payment_status.description as payment_status
	FROM  dbo.payment_info [pi] INNER JOIN
          dbo.payment p ON [pi].payment_info_id = p.payment_info_id INNER JOIN
          dbo.payment_period pp ON p.payment_period_id = pp.payment_period_id
    inner join (select pps.payment_id, description
              	  from payment_payment_status pps inner join payment_status ps
                    on pps.payment_status_id = ps.payment_status_id
                   
			      inner join (
				           select payment_id, max(create_date) as create_date
				           from payment_payment_status 
				            group by payment_id
			       ) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date    
    
              ) payment_status on payment_status.payment_id = p.payment_id
	WHERE pi.event_id = @event_id
END
GO
