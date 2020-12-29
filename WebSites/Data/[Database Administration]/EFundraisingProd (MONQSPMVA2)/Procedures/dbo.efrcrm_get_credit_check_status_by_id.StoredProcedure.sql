USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_check_status_by_id]    Script Date: 02/14/2014 13:04:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for credit_check_status
CREATE  PROCEDURE [dbo].[efrcrm_get_credit_check_status_by_id] 
                   @credit_check_status_id int   
AS
begin

select credit_check_status_id, 
       description
from credit_check_status
where credit_check_status_id = @credit_check_status_id

end
GO
