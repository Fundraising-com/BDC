USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Lead_On_Mass_Email]    Script Date: 02/14/2014 13:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Lead_On_Mass_Email    Script Date: 2003-02-22 20:34:17 *****
***** Object:  View dbo.Lead_On_Mass_Email    Script Date: 2/11/2003 12:27:44 PM *****
*/
CREATE              VIEW [dbo].[Lead_On_Mass_Email]
AS
SELECT DISTINCT 
               dbo.lead.lead_id, 
		dbo.CleanStringOfComma(dbo.lead.email) As Email, 
		dbo.CleanStringOfComma(dbo.lead.first_name + ' ' + dbo.lead.last_name) as name, 
               'http://mygrouppage2.efundraising.com/home.aspx?lid=' + CAST(dbo.lead.lead_id AS varchar) AS CMLink,
		dbo.CleanStringOfComma(dbo.lead.organization) AS Group_Name,
		dbo.promotion.promotion_type_code As PromoTypeCode,
		dbo.lead.lead_entry_date as EntryDate, 
		dbo.promotion.description
FROM  dbo.lead INNER JOIN
               dbo.promotion ON dbo.lead.promotion_id = dbo.promotion.promotion_id INNER JOIN
               dbo.consultant ON dbo.lead.consultant_id = dbo.consultant.consultant_id
WHERE (dbo.lead.email IS NOT NULL) AND (dbo.promotion.promotion_type_code <> 'AG') AND (dbo.promotion.promotion_type_code <> 'QC') AND 
               (dbo.promotion.promotion_type_code <> 'FB') AND (dbo.promotion.promotion_type_code <> 'GF') AND (dbo.lead.promotion_id <> 126) AND (dbo.lead.promotion_id <> 172) AND 
               (dbo.lead.country_code = 'US') AND (dbo.lead.onemaillist = 1) AND /*(dbo.promotion.partner_id = 1) */(dbo.promotion.partner_id not in(8,50,54,124)) AND (dbo.lead.valid_email = 1) AND 
               (dbo.consultant.is_fm = 0) AND (dbo.consultant.is_agent = 0) AND (dbo.lead.ext_consultant_id is null) AND (dbo.lead.transfered_date is null) and (dbo.promotion.promotion_type_code <> 'OUT') 
		
UNION

SELECT 0 as Lead_id, dbo.Newsletter.Email as Email, dbo.newsletter.Fullname, ''as CMLink, 
	'' as organization, 
	'INT' as PromoTypeCode ,
	'5/17/05' as EntryDate
	, '' as description
FROM dbo.newsLetter where Unsubscribed = 0
GO
