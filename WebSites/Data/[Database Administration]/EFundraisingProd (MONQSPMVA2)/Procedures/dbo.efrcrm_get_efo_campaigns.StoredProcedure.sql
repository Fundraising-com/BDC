USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_campaigns]    Script Date: 02/14/2014 13:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Campaign
CREATE PROCEDURE [dbo].[efrcrm_get_efo_campaigns] AS
begin

select Campaign_ID, Group_Type_ID, QSP_Program_ID, Campaign_Image_ID, Organizer_ID, Group_Name, Creation_Date, Financial_Goal, Fund_Raising_Reason, Background_Info, Comments, Is_Launched, Is_Over, Account_Number from EFO_Campaign

end
GO
