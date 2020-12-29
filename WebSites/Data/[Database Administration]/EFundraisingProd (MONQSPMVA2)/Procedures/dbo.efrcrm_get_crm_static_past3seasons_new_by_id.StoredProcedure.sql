USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_crm_static_past3seasons_new_by_id]    Script Date: 02/14/2014 13:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Crm_static_past3seasons_new
CREATE PROCEDURE [dbo].[efrcrm_get_crm_static_past3seasons_new_by_id] @Crm_static_past3seasons_new_id int AS
begin

select Crm_static_past3seasons_new_id, Account_no, Account_name, Total_sold, Qsp_cust_billing_matching_code, Qsp_cust_shipping_matching_code, Qsp_account_matching_code, Fm_id, Status, Email, First_name, Last_name, Home_phone, Work_phone, Mobile_phone, Create_date from Crm_static_past3seasons_new where Crm_static_past3seasons_new_id=@Crm_static_past3seasons_new_id

end
GO
