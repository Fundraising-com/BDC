USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_status_email]    Script Date: 02/14/2014 13:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Status_Email
CREATE PROCEDURE [dbo].[efrcrm_update_efo_status_email] @Email_Type_ID int, @Status_ID int AS
begin

update EFO_Status_Email set Status_ID=@Status_ID where Email_Type_ID=@Email_Type_ID

end
GO
