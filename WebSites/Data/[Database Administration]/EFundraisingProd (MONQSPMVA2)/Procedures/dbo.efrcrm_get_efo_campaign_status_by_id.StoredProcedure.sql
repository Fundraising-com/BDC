USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_campaign_status_by_id]    Script Date: 02/14/2014 13:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Campaign_Status
CREATE PROCEDURE [dbo].[efrcrm_get_efo_campaign_status_by_id] @Campaign_ID int AS
begin

select Campaign_ID, Date_To_Change, Status_ID, Email_Type_ID from EFO_Campaign_Status where Campaign_ID=@Campaign_ID

end
GO
