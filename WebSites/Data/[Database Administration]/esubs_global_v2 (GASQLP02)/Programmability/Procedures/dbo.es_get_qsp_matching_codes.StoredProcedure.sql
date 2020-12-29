USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_qsp_matching_codes]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Qsp_matching_code
CREATE PROCEDURE [dbo].[es_get_qsp_matching_codes] AS
begin

select Qsp_matching_code_id, Account_id, Cust_billing_matching_code, Cust_shipping_matching_code, Account_matching_code from Qsp_matching_code

end
GO
