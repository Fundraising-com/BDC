USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_check_status]    Script Date: 02/14/2014 13:04:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for credit_check_status
create  PROCEDURE [dbo].[efrcrm_get_credit_check_status] 
                   
AS
begin

select credit_check_status_id, 
       description
from credit_check_status


end
GO
