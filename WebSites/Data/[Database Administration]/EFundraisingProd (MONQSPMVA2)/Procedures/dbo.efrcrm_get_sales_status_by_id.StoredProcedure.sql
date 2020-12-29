USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_status_by_id]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Sales_Status
CREATE PROCEDURE [dbo].[efrcrm_get_sales_status_by_id] @Sales_Status_ID int AS
begin

select Sales_Status_ID, Description from Sales_Status where Sales_Status_ID=@Sales_Status_ID

end
GO
