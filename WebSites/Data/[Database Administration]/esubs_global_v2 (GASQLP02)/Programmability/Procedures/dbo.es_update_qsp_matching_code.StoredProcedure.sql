USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_qsp_matching_code]    Script Date: 02/14/2014 13:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Qsp_matching_code
CREATE PROCEDURE [dbo].[es_update_qsp_matching_code] @Qsp_matching_code_id int, @Account_id int, @Cust_billing_matching_code varchar(10), @Cust_shipping_matching_code varchar(10), @Account_matching_code varchar(10) AS
begin

update Qsp_matching_code set Account_id=@Account_id, Cust_billing_matching_code=@Cust_billing_matching_code, Cust_shipping_matching_code=@Cust_shipping_matching_code, Account_matching_code=@Account_matching_code where Qsp_matching_code_id=@Qsp_matching_code_id

end
GO
