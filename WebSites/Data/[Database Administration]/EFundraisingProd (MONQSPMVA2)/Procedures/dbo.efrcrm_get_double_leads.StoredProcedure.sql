USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_double_leads]    Script Date: 02/14/2014 13:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Double_Lead
CREATE PROCEDURE [dbo].[efrcrm_get_double_leads] AS
begin

select Division_ID, Promotion_ID, Temp_Lead_Id, Channel_Code, Lead_Status_ID, Consultant_ID, Lead_Entry_Date, Salutation, First_Name, Last_Name, Organization, Street_Address, City, State_Code, Country_Code, Zip_Code, Day_Phone, Day_Time_Call, Evening_Phone, Fax, Email, Group_Type_ID, Participant_Count, Fund_Raising_Goal, Decision_Date, Decision_Maker, Fund_Raiser_Start_Date, OnEmailList, Comments, Hear_Id, Kit_to_send, Kit_sent, Kit_sent_date, Day_Phone_Ext, Evening_Phone_Ext, Rejection_reason, Other_Phone, Cookie_Content, Group_Web_Site, Organization_Type_Id, Title_Id, Other_Phone_Ext, Campaign_Reason_Id, Web_Site_Id from Double_Lead

end
GO
