USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_crm_static_past3seasons_new]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Crm_static_past3seasons_new
CREATE PROCEDURE [dbo].[efrcrm_update_crm_static_past3seasons_new] @Crm_static_past3seasons_new_id int, @Account_no int, @Account_name varchar(50), @Total_sold decimal, @Qsp_cust_billing_matching_code varchar(9), @Qsp_cust_shipping_matching_code varchar(9), @Qsp_account_matching_code varchar(9), @Fm_id varchar(4), @Status int, @Email varchar(50), @First_name varchar(20), @Last_name varchar(30), @Home_phone varchar(20), @Work_phone varchar(20), @Mobile_phone varchar(20), @Create_date datetime AS
begin

update Crm_static_past3seasons_new set Account_no=@Account_no, Account_name=@Account_name, Total_sold=@Total_sold, Qsp_cust_billing_matching_code=@Qsp_cust_billing_matching_code, Qsp_cust_shipping_matching_code=@Qsp_cust_shipping_matching_code, Qsp_account_matching_code=@Qsp_account_matching_code, Fm_id=@Fm_id, Status=@Status, Email=@Email, First_name=@First_name, Last_name=@Last_name, Home_phone=@Home_phone, Work_phone=@Work_phone, Mobile_phone=@Mobile_phone, Create_date=@Create_date where Crm_static_past3seasons_new_id=@Crm_static_past3seasons_new_id

end
GO
