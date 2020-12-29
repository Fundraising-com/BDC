USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[ef_GetRequest]    Script Date: 02/14/2014 13:04:28 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[ef_GetRequest] 
(
   @intTempLeadID INTEGER, 
   @intIsNew INTEGER
)
AS
SELECT dbo.Partner.Partner_ID, dbo.Partner.Partner_Name, dbo.Temp_Lead.Division_ID, dbo.Temp_Lead.Promotion_ID, dbo.Temp_Lead.Temp_Lead_ID,
       dbo.Temp_Lead.Channel_Code, dbo.Temp_Lead.Lead_Status_ID, dbo.Temp_Lead.Consultant_ID, dbo.Temp_Lead.Lead_Entry_Date,
       dbo.Temp_Lead.Salutation, dbo.Temp_Lead.First_Name, dbo.Temp_Lead.Last_Name, dbo.Temp_Lead.Organization, dbo.Temp_Lead.Street_Address,
       dbo.Temp_Lead.City, dbo.Temp_Lead.State_Code, dbo.Temp_Lead.Country_Code, dbo.Temp_Lead.Zip_Code, dbo.Temp_Lead.Day_Phone,
       dbo.Temp_Lead.Day_Time_Call, dbo.Temp_Lead.Evening_Phone, dbo.Temp_Lead.Fax, dbo.Temp_Lead.Email, dbo.Temp_Lead.Group_Type_ID,
       dbo.Temp_Lead.Participant_Count, dbo.Temp_Lead.Fund_Raising_Goal, dbo.Temp_Lead.Decision_Date, dbo.Temp_Lead.Decision_Maker,
       dbo.Temp_Lead.Fund_Raiser_Start_Date,dbo.Temp_Lead.OnEmailList, dbo.Temp_Lead.Comments, dbo.Temp_Lead.Hear_ID,
       dbo.Temp_Lead.kit_to_send, dbo.Temp_Lead.kit_sent, dbo.Temp_Lead.kit_sent_date, dbo.Temp_Lead.Day_Phone_Ext,
       dbo.Temp_Lead.Evening_Phone_Ext, dbo.Temp_Lead.Rejection_reason, dbo.Temp_Lead.Other_Phone, dbo.Temp_Lead.Cookie_Content,
       dbo.Temp_Lead.Group_Web_Site, dbo.Temp_Lead.Organization_Type_ID, dbo.Temp_Lead.Title_ID, dbo.Temp_Lead.Campaign_Reason_ID,
       dbo.Temp_Lead.Web_Site_ID, dbo.Temp_Lead.Other_Phone_Ext, dbo.Temp_Lead.IsNew, dbo.Temp_Lead.Opt_In_Sweepstakes
FROM dbo.Temp_Lead 
INNER JOIN dbo.Promotion ON dbo.Temp_Lead.Promotion_ID = dbo.Promotion.Promotion_ID 
INNER JOIN dbo.Partner ON dbo.Promotion.Partner_ID = dbo.Partner.Partner_ID 
WHERE dbo.Temp_Lead.Temp_Lead_ID = @intTempLeadID OR dbo.Temp_Lead.IsNew = @intIsNew
GO
