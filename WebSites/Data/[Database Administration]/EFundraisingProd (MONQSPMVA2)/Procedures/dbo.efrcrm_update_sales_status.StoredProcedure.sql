USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sales_status]    Script Date: 02/14/2014 13:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sales_Status
CREATE PROCEDURE [dbo].[efrcrm_update_sales_status] @Sales_Status_ID int, @Description varchar(50) AS
begin

update Sales_Status set Description=@Description where Sales_Status_ID=@Sales_Status_ID

end
GO
