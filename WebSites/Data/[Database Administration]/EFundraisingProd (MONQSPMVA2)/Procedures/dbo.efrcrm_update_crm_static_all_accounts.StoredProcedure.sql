USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_crm_static_all_accounts]    Script Date: 02/14/2014 13:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Crm_static_all_accounts
CREATE PROCEDURE [dbo].[efrcrm_update_crm_static_all_accounts] @Account_no int, @Account_name varchar(50), @Qsp_cust_billing_matching_code varchar(9), @Qsp_cust_shipping_matching_code varchar(9), @Qsp_account_matching_code varchar(9) AS
begin

update Crm_static_all_accounts set Account_name=@Account_name, Qsp_cust_billing_matching_code=@Qsp_cust_billing_matching_code, Qsp_cust_shipping_matching_code=@Qsp_cust_shipping_matching_code, Qsp_account_matching_code=@Qsp_account_matching_code where Account_no=@Account_no

end
GO
