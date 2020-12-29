USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_crm_static_past3seasons_new]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Crm_static_past3seasons_new
CREATE PROCEDURE [dbo].[efrcrm_insert_crm_static_past3seasons_new] @Crm_static_past3seasons_new_id int OUTPUT, @Account_no int, @Account_name varchar(50), @Total_sold decimal, @Qsp_cust_billing_matching_code varchar(9), @Qsp_cust_shipping_matching_code varchar(9), @Qsp_account_matching_code varchar(9), @Fm_id varchar(4), @Status int, @Email varchar(50), @First_name varchar(20), @Last_name varchar(30), @Home_phone varchar(20), @Work_phone varchar(20), @Mobile_phone varchar(20), @Create_date datetime AS
begin

insert into Crm_static_past3seasons_new(Account_no, Account_name, Total_sold, Qsp_cust_billing_matching_code, Qsp_cust_shipping_matching_code, Qsp_account_matching_code, Fm_id, Status, Email, First_name, Last_name, Home_phone, Work_phone, Mobile_phone, Create_date) values(@Account_no, @Account_name, @Total_sold, @Qsp_cust_billing_matching_code, @Qsp_cust_shipping_matching_code, @Qsp_account_matching_code, @Fm_id, @Status, @Email, @First_name, @Last_name, @Home_phone, @Work_phone, @Mobile_phone, @Create_date)

select @Crm_static_past3seasons_new_id = SCOPE_IDENTITY()

end
GO
