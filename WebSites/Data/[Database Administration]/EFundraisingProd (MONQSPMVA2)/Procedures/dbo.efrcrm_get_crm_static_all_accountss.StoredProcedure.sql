USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_crm_static_all_accountss]    Script Date: 02/14/2014 13:04:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Crm_static_all_accounts
CREATE PROCEDURE [dbo].[efrcrm_get_crm_static_all_accountss] AS
begin

select Account_no, Account_name, Qsp_cust_billing_matching_code, Qsp_cust_shipping_matching_code, Qsp_account_matching_code from Crm_static_all_accounts

end
GO
