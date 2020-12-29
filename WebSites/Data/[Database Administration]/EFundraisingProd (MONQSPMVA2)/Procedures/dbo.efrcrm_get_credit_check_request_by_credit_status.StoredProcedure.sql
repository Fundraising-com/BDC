USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_check_request_by_credit_status]    Script Date: 02/14/2014 13:04:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Applicable_Adjustment_Tax
CREATE        PROCEDURE [dbo].[efrcrm_get_credit_check_request_by_credit_status]  
                 @credit_status_id int  AS
begin

select credit_check_id,
       lead_id,
       consultant_id,
       request_date,
       order_date,
       amount_requested,
       credit_score,
       credit_status_id,
       amount_approved,
       last_name,
       first_name,
       mid_init,
       address,
       city,
       state,
       zip,
       ssn,
       result_date, 
       result_confirmation_date,
       credit_report
from credit_check_request
where credit_status_id = @credit_status_id


end
GO
