USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efr_leads]    Script Date: 02/14/2014 13:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFR_Lead
CREATE PROCEDURE [dbo].[efrcrm_get_efr_leads] AS
begin

select Lead_ID, First_Name, Last_Name, Organization_Name, Promotion_Description, Lead_Activity_Detail, Lead_Comment, Activity_Scheduled_Date, Consultant_ID, Consultant_Ext, Is_Done, Phone_Number, Phone_extension, Promotion_Type, 2ndPhone_Number, 2ndPhone_Extension from EFR_Lead

end
GO
