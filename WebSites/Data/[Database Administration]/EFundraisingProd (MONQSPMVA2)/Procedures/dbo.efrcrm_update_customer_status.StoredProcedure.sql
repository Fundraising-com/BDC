USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_customer_status]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Customer_status
CREATE PROCEDURE [dbo].[efrcrm_update_customer_status] @Customer_status_id int, @Customer_status_desc varchar(100), @Create_date datetime AS
begin

update Customer_status set Customer_status_desc=@Customer_status_desc, Create_date=@Create_date where Customer_status_id=@Customer_status_id

end
GO
