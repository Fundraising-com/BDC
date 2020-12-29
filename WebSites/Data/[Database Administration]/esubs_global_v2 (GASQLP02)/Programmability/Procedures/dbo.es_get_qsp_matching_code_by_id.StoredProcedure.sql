USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_qsp_matching_code_by_id]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Qsp_matching_code
CREATE PROCEDURE [dbo].[es_get_qsp_matching_code_by_id] @Qsp_matching_code_id int AS
begin

select Qsp_matching_code_id, Account_id, Cust_billing_matching_code, Cust_shipping_matching_code, Account_matching_code from Qsp_matching_code where Qsp_matching_code_id=@Qsp_matching_code_id

end
GO
