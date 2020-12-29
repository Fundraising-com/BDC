USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[lead_view]    Script Date: 02/14/2014 13:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[lead_view]
AS
SELECT
	l.Consultant_ID
	, CASE 
		WHEN ISNULL(la.lead_activity_date, 0) <> 0 
		THEN dbo.udf_evaluate_lead(la.lead_activity_type_id, DATEDIFF(dd, GETDATE(), la.lead_activity_date)) 
		ELSE NULL 
	  END AS Evaluation
	, l.Lead_ID
	, c.[Name] Consultant
	, c.email_address consultant_email
	, l.Lead_Entry_Date
	, la.Lead_Activity_Type_Id
	, la.Lead_Activity_Id
	, la.Lead_Activity_Date
	, la.Completed_Date
	, l.Lead_Assignment_Date
	, ls.[Description] Status
	, l.Salutation
	, l.First_Name
	, l.Last_Name
	, l.Organization
	, l.Street_Address
	, l.City
	, l.State_Code
	, l.Zip_Code
	, l.Day_Phone
	, l.Day_Time_Call
	, l.Evening_Phone
	, l.Fax, l.Email
	, l.Group_Type_ID
	, l.Participant_Count
	, l.Fund_Raising_Goal
	, l.Decision_Maker
	, l.Fund_Raiser_Start_Date
	, l.Comments
	, l.Country_Code
	, l.Has_Been_Contacted
	, l.Lead_Priority_Id
	, p.Promotion_Type_Code
	, pt.[promotion_type_name] as PromoType
	, p.[promotion_name] as Promotion
	, l.fk_Kit_Type_ID Kit_Type_ID
	, kt.[Description] KitType
	, l.Day_Phone_Ext
	, l.Evening_Phone_Ext
	, s.Time_Zone_Difference
	, l.Promotion_ID
	, l.kit_sent_date
	, pp.Partner_ID
	, l.Title_Id
	, l.Campaign_Reason_Id
	, l.Organization_Type_Id
	, l.Web_Site_Id
	, l.Group_Web_Site
	, l.Other_Phone
	, w.Web_Site_Name
	, l.Interests
	, l.Day_Phone_Is_Good
	, l.Evening_Phone_Is_Good
	, l.Account_Number
	, l.Activity_Closing_Reason_ID
FROM	dbo.Lead l
	INNER JOIN dbo.Lead_Activity la 
		ON l.Lead_ID = la.Lead_Id 
	INNER JOIN dbo.Lead_Status ls 
		ON l.Lead_Status_ID = ls.Lead_Status_ID 
	INNER JOIN dbo.Consultant c 
		ON l.Consultant_ID = c.Consultant_ID 
	INNER JOIN efrcommon.dbo.Promotion p 
		ON l.Promotion_ID = p.Promotion_ID 
	INNER JOIN efrcommon.dbo.Promotion_Type pt 
		ON p.Promotion_Type_Code = pt.Promotion_Type_Code 
	INNER JOIN efrcommon.dbo.partner_promotion pp
		ON p.promotion_id = pp.promotion_id
	INNER JOIN dbo.Kit_Type kt 
		ON l.fk_Kit_Type_ID = kt.Kit_Type_ID 
	INNER JOIN dbo.State s 
		ON l.State_Code = s.State_Code 
	INNER JOIN dbo.Lead_Activity_Type lat 
		ON la.Lead_Activity_Type_Id = lat.Lead_Activity_Type_Id 
	INNER JOIN dbo.Web_Site w 
		ON l.Web_Site_Id = w.Web_Site_Id
WHERE
	c.is_active = 1
 AND	c.department_id IN ( 7, 4, 18, 17 )
GO
