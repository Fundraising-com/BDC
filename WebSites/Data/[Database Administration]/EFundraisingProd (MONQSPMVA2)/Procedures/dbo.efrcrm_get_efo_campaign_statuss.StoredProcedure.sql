USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_campaign_statuss]    Script Date: 02/14/2014 13:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Campaign_Status
CREATE PROCEDURE [dbo].[efrcrm_get_efo_campaign_statuss] AS
begin

select Campaign_ID, Date_To_Change, Status_ID, Email_Type_ID from EFO_Campaign_Status

end
GO
