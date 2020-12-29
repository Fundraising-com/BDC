USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_checksystem_payments_without_exceptions_with_status]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment
CREATE PROCEDURE [dbo].[es_get_checksystem_payments_without_exceptions_with_status] @paymentStatusID as int
AS
BEGIN
SELECT 
-- Payment
p.Payment_id, p.Payment_type_id, p.Payment_info_id, p.Payment_period_id, p.Cheque_number, p.Cheque_date, 
p.Paid_amount, p.[Name], p.Create_date as pCreate_date,
p.[name] as pname,
p.phone_number as pphone_number,
p.address_1 as paddress_1,
p.address_2 as paddress_2,
p.City as pcity,
p.Zip_Code as pzip_code,
p.Country_Code as pcountry_code,
p.Subdivision_Code as psubdivision_code,
ps.Payment_status_id,
ps.[Description],
-- Payment Info
pif.payment_info_id
          , pif.group_id
          ,pif. event_id
          , pif.payment_name
          , pif.on_behalf_of_name
          , pif.ship_to_name, pif.create_date
          , pn.phone_number_id
          , pn.phone_number
          , pif.ssn
          , pif.active
          , pa.postal_address_id
          , pa.address_1
          , pa.address_2
          , pa.city
          , pa.zip_code
          , pa.country_code
          , pa.subdivision_code
-- Payment Period
,pp.Payment_period_id, 
pp.Start_date as ppStart_date, 
pp.End_date as ppEnd_date, 
pp.Create_date as ppCreate_date
-- Partner
,Isnull(prt.partner_id, ppinfo.partner_id) as partner_id
	, prt.partner_type_id
	, prt.partner_name
	, prt.has_collection_site
	, prt.guid
-- Group
, grp.group_id
	, grp.parent_group_id
	, grp.sponsor_id
	, grp.partner_id
	, grp.lead_id
	, grp.external_group_id
	, Isnull(grp.group_name, '') as group_name
	, grp.test_group
	, grp.expected_membership
	, grp.redirect
	, grp.group_url
	, grp.comments
	, grp.create_date
-- Payment Type
,pyt.Payment_type_id, pyt.Payment_type_name, pyt.Create_date as pytcreate_date
-- Group Status
,gs.Group_status_id, gs.[Description] as gsDescription
FROM 
payment p 
INNER JOIN Payment_info pif 
	ON p.Payment_info_id = pif.Payment_info_id
INNER JOIN 
	(
	select pps.payment_id, pps.payment_status_id
	from payment_payment_status pps
	inner join (
		select payment_id, max(create_date) as create_date 
		from payment_payment_status 
		group by payment_id
		) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
	)
pps
	ON pps.payment_id = p.payment_id
INNER JOIN Payment_status ps 
	ON pps.payment_status_id = ps.payment_status_id
INNER JOIN payment_period pp
	ON pp.payment_period_id = p.payment_period_id
LEFT JOIN postal_address as pa
	ON pa.postal_address_id = pif.postal_address_id
LEFT JOIN phone_number as pn
	ON pn.phone_number_id = pif.phone_number_id
--LEFT JOIN event ev 
--	On ev.event_id = pif.event_id
LEFT JOIN [group] grp
	On grp.group_id = pif.group_id
LEFT JOIN partner prt
	ON prt.partner_id = grp.partner_id
LEFT JOIN Payment_type pyt 
	On pyt.Payment_type_id= p.Payment_type_id
LEFT JOIN 
	(select ggs.group_id, ggs.group_status_id
	from group_group_status ggs
	inner join (
		select group_id, max(create_date) as create_date 
		from group_group_status 
		group by group_id
		) ggs2 on ggs.group_id = ggs2.group_id and ggs.create_date = ggs2.create_date
	) ggs
	On ggs.group_id = grp.group_id
LEFT JOIN Group_status gs
	ON gs.group_status_id = ggs.group_status_id
LEFT JOIN partner_payment_info ppinfo
              ON (ppinfo.payment_info_id = pif.payment_info_id and ppinfo.active = 1)
--LEFT JOIN partner_payment_config ppc
--             ON ppc.partner_payment_info_id = ppinfo.partner_payment_info_id
WHERE 
p.Payment_id NOT IN (SELECT Payment_id FROM Payment_exception_type WHERE Isnull(Is_Corrected, 0)=0)
and pps.payment_status_id = @paymentStatusID

END
GO
