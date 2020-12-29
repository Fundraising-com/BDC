USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_temp_lead]    Script Date: 02/14/2014 13:04:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[efr_get_temp_lead]
	@temp_lead_id int
AS
SELECT
	[Division_ID],
	[Promotion_ID],
	[Temp_Lead_ID],
	[Channel_Code],
	[Lead_Status_ID],
	[Consultant_ID],
	[Lead_Entry_Date],
	[Salutation],
	[First_Name],
	[Last_Name],
	[Organization],
	[Street_Address],
	[City],
	[State_Code],
	[Country_Code],
	[Zip_Code],
	[Day_Phone],
	[Day_Time_Call],
	[Evening_Phone],
	[Fax],
	[Email],
	[Group_Type_ID],
	[Participant_Count],
	[Fund_Raising_Goal],
	[Decision_Date],
	[Decision_Maker],
	[Fund_Raiser_Start_Date],
	[OnEmailList],
	[Comments],
	[Hear_ID],
	[kit_to_send],
	[kit_sent],
	[kit_sent_date],
	[Day_Phone_Ext],
	[Evening_Phone_Ext],
	[Rejection_reason],
	[Other_Phone],
	[Cookie_Content],
	[Group_Web_Site],
	[Organization_Type_ID],
	[Title_ID],
	[Campaign_Reason_ID],
	[Web_Site_ID],
	[Other_Phone_Ext],
	[IsNew],
	[Opt_In_Sweepstakes],
	[Group_ID]

FROM
	temp_lead
WHERE
	temp_lead_id = @temp_lead_id
GO
