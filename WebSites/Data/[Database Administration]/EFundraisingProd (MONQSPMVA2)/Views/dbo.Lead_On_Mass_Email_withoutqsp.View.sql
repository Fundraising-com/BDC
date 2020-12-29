USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Lead_On_Mass_Email_withoutqsp]    Script Date: 02/14/2014 13:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    VIEW [dbo].[Lead_On_Mass_Email_withoutqsp]
AS
SELECT DISTINCT 
dbo.lead.lead_id, 
dbo.CleanStringOfComma(dbo.lead.email) As Email, 
--dbo.CleanStringOfComma(dbo.lead.first_name + ' ' + dbo.lead.last_name) as name, 
dbo.lead.first_name as name,
dbo.consultant.name as consultant,
--' + CAST(dbo.lead.lead_id AS varchar) AS CMLink,
dbo.CleanStringOfComma(dbo.lead.organization) AS Group_Name,
dbo.promotion.promotion_type_code As PromoTypeCode,
dbo.lead.lead_entry_date as EntryDate, 
dbo.promotion.description,
gt.description as group_type_desc, 
dbo.partner.partner_name,
dbo.promotion.partner_id
FROM dbo.lead 
INNER JOIN dbo.promotion ON dbo.lead.promotion_id = dbo.promotion.promotion_id 
INNER JOIN dbo.consultant ON dbo.lead.consultant_id = dbo.consultant.consultant_id
inner join dbo.group_type gt on dbo.lead.group_type_id = gt.group_type_id
inner join dbo.partner ON dbo.partner.partner_id = dbo.promotion.partner_id 
left outer join dbo.crm_static_past3seasons_new p3s on p3s.qsp_account_matching_code = dbo.lead.matching_code
--inner join lead_visit lv on lv.lead_id = lead.lead_id
--inner join promotion p2 on p2.promotion_id =lv.promotion_id
WHERE (dbo.lead.email IS NOT NULL) AND (dbo.promotion.promotion_type_code <> 'AG') AND (dbo.promotion.promotion_type_code <> 'QC') AND 
(dbo.promotion.promotion_type_code <> 'FB') AND (dbo.promotion.promotion_type_code <> 'GF') 
AND (dbo.lead.promotion_id <> 126) AND (dbo.lead.promotion_id <> 172) AND 
(dbo.lead.country_code = 'US') AND (dbo.lead.onemaillist = 1) 
AND (dbo.promotion.partner_id not in(3, 1,50, 503, 129, 500, 154, 124, 54, 686))
/*(dbo.promotion.partner_id in(500))*/ /*(dbo.promotion.partner_id not in(8,154))*/ /*(dbo.promotion.partner_id = 8)*//*(dbo.promotion.partner_id not in(8,54,124, 154))*/ 
AND (dbo.lead.valid_email = 1) AND 
(dbo.consultant.is_fm = 0) AND (dbo.consultant.is_agent = 0) AND (dbo.lead.ext_consultant_id is null) 
AND (dbo.lead.transfered_date is null) and (dbo.promotion.promotion_type_code <> 'OUT') 
and p3s.qsp_account_matching_code is null 
and (dbo.promotion.promotion_type_code <> 'ON' /*or p2.promotion_type_code <> 'ON'*/) 
--and dbo.lead.group_type_id in(6,4,2,13)--and group_type_id in(4,43,36)



--football, cheer, baseball & softball 
--select * from group_type order by 3
--
--select * from partner where partner_id  in(3, 8, 1, 503, 129, 500, 154, 124, 54, 686)
--select * from partner where partner_id in(8,54,124, 154)--= 154
--select * from partner where partner_name like 'asa%' '%%'

/*UNION
SELECT 0 as Lead_id
, n.Email as Email
, n.Fullname, --''as CMLink, 
'' as organization, 
'INT' as PromoTypeCode ,
'5/17/05' as EntryDate
, '' as description
, '' as group_type_desc
, 0 as partner_id
FROM newsLetter n where n.Unsubscribed = 0
*/
GO
