USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[lead_unassignment_view]    Script Date: 02/14/2014 13:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[lead_unassignment_view]
as
SELECT Lead.lead_id, Lead_Qualification_Type.Description AS Qualification_Type_Desc, Lead_Status.Description AS Status, Lead.state_code, Lead.country_code, Promotion_Type.Description AS Promo_Type, Promotion.description AS Promotion, Web_Site.Web_Site_Name AS Web_Site, Lead.participant_count AS Part, Group_Type.description AS Group_Type, Organization_Type.organization_type_desc, Lead.day_phone, Lead.comments, Promotion.promotion_type_code, Lead.lead_entry_date, Organization_Type.organization_type_id, Lead.lead_assignment_date, Lead.lead_status_id, Lead.channel_code AS Channel
FROM ((((((Lead INNER JOIN Promotion ON Lead.promotion_id = Promotion.promotion_id) INNER JOIN Promotion_Type ON Promotion.promotion_type_code = Promotion_Type.Promotion_Type_Code) LEFT JOIN Group_Type ON Lead.group_type_id = Group_Type.group_type_id) LEFT JOIN Web_Site ON Lead.web_site_id = Web_Site.Web_Site_Id) INNER JOIN Lead_Status ON Lead.lead_status_id = Lead_Status.Lead_Status_ID) INNER JOIN Organization_Type ON Lead.organization_type_id = Organization_Type.organization_type_id) LEFT JOIN Lead_Qualification_Type ON Lead.lead_qualification_type_id = Lead_Qualification_Type.Lead_Qualification_Type_ID
WHERE (((Lead.consultant_id) = 0))
GO
