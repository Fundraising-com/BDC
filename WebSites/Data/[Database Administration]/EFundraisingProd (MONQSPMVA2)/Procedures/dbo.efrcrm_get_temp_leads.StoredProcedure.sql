USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_temp_leads]    Script Date: 02/14/2014 13:06:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Temp_Lead
CREATE PROCEDURE [dbo].[efrcrm_get_temp_leads] AS
begin

select Division_ID, Promotion_ID, Temp_Lead_Id, Channel_Code, Lead_Status_ID, Consultant_ID, Lead_Entry_Date, Salutation, First_Name, Last_Name, Title, Organization, Street_Address, City, State_Code, Country_Code, Zip_Code, Day_Phone, Day_Time_Call, Evening_Phone, Evening_Time_Call, Fax, Email, Group_Type_ID, Member_Count, Participant_Count, Fund_Raising_Goal, Decision_Date, Decision_Maker, Committee_Meeting_Required, Committee_Meeting_Date, Fund_Raiser_Start_Date, OnEmailList, FaxKit, EmailKit, Comments, Hear_Id, Kit_to_send, Kit_sent, Kit_sent_date, Old_Lead_Id, Lead_Assignment_Date, Interests, Has_Been_Contacted, Fk_Kit_Type_ID, Lead_Priority_Id, Day_Phone_Ext, Evening_Phone_Ext, Rejection_reason, Other_Phone, Other_Phone_Ext, Group_Web_Site, Organization_Type_Id, Campaign_Reason_Id, Title_Id, Cookie_Content, Campaign_Reason, Web_Site_Id, IsNew from Temp_Lead

end
GO
