USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_crm_static_all_accounts]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Crm_static_all_accounts
CREATE PROCEDURE [dbo].[efrcrm_insert_crm_static_all_accounts] @Account_no int OUTPUT, @Account_name varchar(50), @Qsp_cust_billing_matching_code varchar(9), @Qsp_cust_shipping_matching_code varchar(9), @Qsp_account_matching_code varchar(9) AS
begin

insert into Crm_static_all_accounts(Account_name, Qsp_cust_billing_matching_code, Qsp_cust_shipping_matching_code, Qsp_account_matching_code) values(@Account_name, @Qsp_cust_billing_matching_code, @Qsp_cust_shipping_matching_code, @Qsp_account_matching_code)

select @Account_no = SCOPE_IDENTITY()

end
GO
