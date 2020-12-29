USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_customer_status]    Script Date: 02/14/2014 13:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Customer_status
CREATE PROCEDURE [dbo].[efrcrm_insert_customer_status] @Customer_status_id int OUTPUT, @Customer_status_desc varchar(100), @Create_date datetime AS
begin

insert into Customer_status(Customer_status_desc, Create_date) values(@Customer_status_desc, @Create_date)

select @Customer_status_id = SCOPE_IDENTITY()

end
GO
