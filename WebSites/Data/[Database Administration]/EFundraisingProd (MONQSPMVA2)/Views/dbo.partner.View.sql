USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[partner]    Script Date: 02/14/2014 13:02:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE               VIEW [dbo].[partner]
AS

SELECT pvt.partner_id
	, p.partner_type_id as partner_group_type_id
	, 0 as partner_subgroup_type_id
	--, [7] as country_id
	, p.partner_name
	, [1] as partner_path
	, [2] as esubs_url
	, '' as estore_url
	, [8] as free_kit_url
	, [4] as logo
	, [9] as phone_number
	, '' as email_ext
	, [10] as url
	, p.guid
	, 1 as prize_eligible
	, p.has_collection_site
FROM 
(SELECT partner_id , partner_attribute_id, value
from efrcommon..partner_attribute_value ) pav
PIVOT
(
  MIN( [value])
FOR partner_attribute_id IN
(  [1], [2],[8],[4] ,[9] , [10])
) AS pvt
inner join efrcommon..partner p on pvt.partner_id = p.partner_id
--ORDER BY pvt.partner_id;
GO
