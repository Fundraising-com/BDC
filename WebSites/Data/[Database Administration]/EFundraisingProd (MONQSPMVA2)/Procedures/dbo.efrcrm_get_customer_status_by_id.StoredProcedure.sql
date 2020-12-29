USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_customer_status_by_id]    Script Date: 02/14/2014 13:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Customer_status
CREATE PROCEDURE [dbo].[efrcrm_get_customer_status_by_id] @Customer_status_id int AS
begin

select Customer_status_id, Customer_status_desc, Create_date from Customer_status where Customer_status_id=@Customer_status_id

end
GO
