USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_status_emails]    Script Date: 02/14/2014 13:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Status_Email
CREATE PROCEDURE [dbo].[efrcrm_get_efo_status_emails] AS
begin

select Email_Type_ID, Status_ID from EFO_Status_Email

end
GO
