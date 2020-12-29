USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_qsp_get_online_account_by_food_account_id]    Script Date: 02/14/2014 13:06:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***************************************************************
Return all internet accounts id linked to the same organization
as an food account id
	definition:
	internet account = MMB
	food account = WFC

Created by 	mcote
Create date 	Feb 05, 2008

exec  es_qsp_get_online_account_by_food_account_id 312374
***************************************************************/
CREATE  PROCEDURE [dbo].[es_qsp_get_online_account_by_food_account_id] 
		@foodAccountID int
AS
begin

declare @organizationID int

select @organizationID = O.organization_id
from QSPFulfillment.dbo.Organization O
inner join QSPFulfillment.dbo.Account A 
	on O.organization_id = A.organization_id
where  A.account_id = @foodAccountID

select
	--O.business_division_id
	-- O.organization_id
	A.account_id
	, A.fulf_account_id
	, A.account_name
	/*, product_line_id = 
	case when O.business_division_id = 1 
	then 
		case 		 
		when A.fulf_account_id between  30000000 and 30999999 --WFC
		then 4
		when A.fulf_account_id between 425000000 and 425099999 --MMB
		then 1
		when A.fulf_account_id between 425200000 and 425299999 --MMB
		then 1
		when A.fulf_account_id between 425400000 and 425499999 --MMB
		then 1
		when A.fulf_account_id between 425900000 and 425999999 --MMB
		then 1
		else 0
		end
	else 0
	end
	*/
	/*, product_line_name = 
	case when O.business_division_id = 1 
	then 
		case
		when A.fulf_account_id between  30000000 and 30999999 --WFC
		then 'WFC'
		when A.fulf_account_id between 425000000 and 425099999 --MMB
		then 'MMB'
		when A.fulf_account_id between 425200000 and 425299999 --MMB
		then 'MMB'
		when A.fulf_account_id between 425400000 and 425499999 --MMB
		then 'MMB'
		when A.fulf_account_id between 425900000 and 425999999 --MMB
		then 'MMB'
		else ''
		end
	else ''
	end
	*/
	/*
	, inet_accessible = 
	case when O.business_division_id = 1 
	then 
		case
		when A.fulf_account_id between  30000000 and 30999999 --WFC
		then cast(0 as bit)
		when A.fulf_account_id between 425000000 and 425099999 --MMB
		then cast(1 as bit)
		when A.fulf_account_id between 425200000 and 425299999 --MMB
		then cast(1 as bit)
		when A.fulf_account_id between 425400000 and 425499999 --MMB
		then cast(1 as bit)
		when A.fulf_account_id between 425900000 and 425999999 --MMB
		then cast(1 as bit)
		else cast(0 as bit)
		end
	else cast(0 as bit)
	end
	*/
	--, [manage_fundraiser_pwd] = MLOGIN.[Password]
	, A.fm_id
	--, PAA.postal_address_type_id
	--, PADDR.postal_address_id
	--, PADDR.address1
	--, PADDR.address2
	--, PADDR.city
	--, PADDR.subdivision_code
	--, PADDR.zip
	--, PADDR.zip4
	--, PADDR.county
	, PADDR.first_name + ' ' + PADDR.last_name as sponsor_name
	
from QSPFulfillment.dbo.Organization O
inner join QSPFulfillment.dbo.Account A 
	on O.organization_id = A.organization_id
left join QSPECommerce.dbo.AccountReportingUsers MLOGIN
	on O.[business_division_id] = MLOGIN.[x_business_division_id]
	AND A.[fulf_account_id] = MLOGIN.[fulf_account_id]
	AND MLOGIN.[DeletedTF] = 0 
left join QSPFulfillment.dbo.postal_address_account PAA
	on A.account_id = PAA.account_id
	AND PAA.postal_address_type_id = 1 --Billing
	AND PAA.[deleted] = 0 
left join QSPFulfillment.dbo.postal_address PADDR
	on PAA.postal_address_id = PADDR.postal_address_id
	AND PADDR.[deleted] = 0 
where
	O.organization_id = @organizationID
	AND O.business_division_id = 1
	AND O.[deleted] = 0
	AND A.[deleted] = 0 
	AND 
	(
		-- A.fulf_account_id between  30000000 and  30999999 --WFC 
		-- OR 
		A.fulf_account_id between 425000000 and 425099999 --MMB
		OR A.fulf_account_id between 425200000 and 425299999 --MMB
		OR A.fulf_account_id between 425400000 and 425499999 --MMB
		OR A.fulf_account_id between 425900000 and 425999999 --MMB
	)
	AND A.fulf_account_id NOT IN
	(
	425888812 --D2C MAGAZINE ORDERS (rolls up to same flagpole as 428999999)
	, 425888813 --RD AGENCY TEST - ATTACHED MAIL -AmericanMagazineOutlet.com
	, 425888814 --QSP RETENTION BLITZ FREE OFFER
	, 428999999 --QDS MAGAZINE VOUCHER ORDERS
	, 710111111  --Gift Spree: Early Signing 2007
	)
END
GO
