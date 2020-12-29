USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_card_refund_request_by_id]    Script Date: 02/14/2014 13:04:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[efrcrm_insert_credit_card_refund_request] 69747,111,null,10,''
--select * from dbo.credit_card_refund_request
-- Generate update stored proc for Local_Sponsor_Activity
CREATE  PROCEDURE [dbo].[efrcrm_get_credit_card_refund_request_by_id] --69747,111,null,10,''
                  @id int
                  
AS
begin

select credit_card_refund_request_id,
       sale_id ,
       bpps_id ,
       request_date,
       refund_amount,
       status_code,
	   credit_card_type_id
from dbo.credit_card_refund_request
where credit_card_refund_request_id = @id
      


end
GO
