USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_campaign_status]    Script Date: 02/14/2014 13:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Campaign_Status
CREATE PROCEDURE [dbo].[efrcrm_update_efo_campaign_status] @Campaign_ID int, @Date_To_Change smalldatetime, @Status_ID int, @Email_Type_ID int AS
begin

update EFO_Campaign_Status set Date_To_Change=@Date_To_Change, Status_ID=@Status_ID, Email_Type_ID=@Email_Type_ID where Campaign_ID=@Campaign_ID

end
GO
