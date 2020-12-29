USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_qsp_matching_code]    Script Date: 02/14/2014 13:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Qsp_matching_code
CREATE PROCEDURE [dbo].[es_insert_qsp_matching_code] @Qsp_matching_code_id int OUTPUT, @Account_id int, @Cust_billing_matching_code varchar(10), @Cust_shipping_matching_code varchar(10), @Account_matching_code varchar(10) AS
begin

insert into Qsp_matching_code(Account_id, Cust_billing_matching_code, Cust_shipping_matching_code, Account_matching_code) values(@Account_id, @Cust_billing_matching_code, @Cust_shipping_matching_code, @Account_matching_code)

select @Qsp_matching_code_id = SCOPE_IDENTITY()

end
GO
