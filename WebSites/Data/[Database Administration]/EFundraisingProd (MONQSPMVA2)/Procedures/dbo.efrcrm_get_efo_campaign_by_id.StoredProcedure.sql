USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_campaign_by_id]    Script Date: 02/14/2014 13:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Campaign
CREATE PROCEDURE [dbo].[efrcrm_get_efo_campaign_by_id] @Campaign_ID int AS
begin

select Campaign_ID, Group_Type_ID, QSP_Program_ID, Campaign_Image_ID, Organizer_ID, Group_Name, Creation_Date, Financial_Goal, Fund_Raising_Reason, Background_Info, Comments, Is_Launched, Is_Over, Account_Number from EFO_Campaign where Campaign_ID=@Campaign_ID

end
GO
