USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_qsp_get_online_account_by_id]    Script Date: 02/14/2014 13:06:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***************************************************************
Return all online accounts info
	definition:
	food account = WFC

Created by 	mcote
ALTER  date 	Feb 05, 2008
***************************************************************/
CREATE  PROCEDURE [dbo].[es_qsp_get_online_account_by_id] 
		@onlineAccountID int
AS
begin
select
	O.business_division_id
	, O.organization_id
	, A.account_id
	, A.fulf_account_id
	, A.account_name
	
	, [manage_fundraiser_pwd] = MLOGIN.[Password]
	, A.fm_id
	, PAA.postal_address_type_id
	, PADDR.postal_address_id
	, PADDR.address1
	, PADDR.address2
	, PADDR.city
	, PADDR.subdivision_code
	, PADDR.zip
	, PADDR.zip4
	, PADDR.county
	, PADDR.first_name + ' ' + PADDR.last_name as sponsor_name
	, ee.email_address as sponsor_email

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

left join QSPFulFillment.dbo.email_account ea 
	on A.account_id = ea.account_id
left join QSPFulFillment.dbo.email ee
	on ea.email_id =  ee.email_id

where
	 A.account_id = @onlineAccountID
	AND O.business_division_id = 1
	AND O.[deleted] = 0
	AND A.[deleted] = 0

END
GO
