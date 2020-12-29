USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_lead_info]    Script Date: 02/14/2014 13:03:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[crm_get_lead_info] --516577
           @lead_id as int
   
           
as
SELECT     *, efrcommon.dbo.promotion.promotion_name AS Promotion, efrcommon.dbo.Promotion_Type.promotion_type_name AS Promotion_Type, dbo.Web_Site.Web_Site_Name AS from_web_site, 
                      dbo.Lead_Channel.Description AS channel2, dbo.consultant.name AS consultant, consultant_2.name AS assigner, consultant_1.name AS fm, 
                      dbo.Lead_Status.Description AS status, dbo.group_type.description AS group_type, dbo.organization_type.organization_type_desc AS org_type, 
                      dbo.campaign_reason.campaign_reason_desc AS campaign_reason, dbo.hear_about_us.name AS hear_about_us, 
                      dbo.Kit_Type.Description AS kit_type, dbo.Country.Country_Name AS country, dbo.State.State_Name AS state, dbo.title.title_desc AS title,
                      case when dbo.lead.day_time_call is null then 'n/a' else dbo.lead.day_time_call end as best_time_to_call
FROM         dbo.title RIGHT OUTER JOIN
                      dbo.lead ON dbo.title.title_id = dbo.lead.title_id LEFT OUTER JOIN
                      dbo.State ON dbo.lead.state_code = dbo.State.State_Code LEFT OUTER JOIN
                      dbo.Country ON dbo.lead.country_code = dbo.Country.Country_Code LEFT OUTER JOIN
                      dbo.Kit_Type ON dbo.lead.fk_kit_type_id = dbo.Kit_Type.Kit_Type_ID LEFT OUTER JOIN
                      dbo.hear_about_us ON dbo.lead.hear_id = dbo.hear_about_us.hear_id LEFT OUTER JOIN
                      dbo.campaign_reason ON dbo.lead.campaign_reason_id = dbo.campaign_reason.campaign_reason_id LEFT OUTER JOIN
                      dbo.organization_type ON dbo.lead.organization_type_id = dbo.organization_type.organization_type_id LEFT OUTER JOIN
                      dbo.group_type ON dbo.lead.group_type_id = dbo.group_type.group_type_id LEFT OUTER JOIN
                      dbo.Lead_Status ON dbo.lead.lead_status_id = dbo.Lead_Status.Lead_Status_ID LEFT OUTER JOIN
                      dbo.consultant consultant_2 ON dbo.lead.assigner_id = consultant_2.consultant_id LEFT OUTER JOIN
                      dbo.consultant consultant_1 ON dbo.lead.ext_consultant_id = consultant_1.consultant_id LEFT OUTER JOIN
                      dbo.consultant ON dbo.lead.consultant_id = dbo.consultant.consultant_id LEFT OUTER JOIN
                      dbo.Lead_Channel ON dbo.lead.channel_code = dbo.Lead_Channel.Channel_Code LEFT OUTER JOIN
                      efrcommon.dbo.Promotion_Type RIGHT OUTER JOIN
                      efrcommon.dbo.promotion ON efrcommon.dbo.Promotion_Type.Promotion_Type_Code = efrcommon.dbo.promotion.promotion_type_code ON 
                      dbo.lead.promotion_id = efrcommon.dbo.promotion.promotion_id LEFT OUTER JOIN
                      dbo.Web_Site ON dbo.lead.web_site_id = dbo.Web_Site.Web_Site_Id

where lead.lead_id = @lead_id
GO
