USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_check_request_awaiting_order]    Script Date: 02/14/2014 13:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--for credit check , gets the credit request for the windows service
--JF Lavigne

CREATE        PROCEDURE [dbo].[efrcrm_get_credit_check_request_awaiting_order]  
AS
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
where credit_status_id = 4 and  --Pending
      order_date is null 
      


end
GO
