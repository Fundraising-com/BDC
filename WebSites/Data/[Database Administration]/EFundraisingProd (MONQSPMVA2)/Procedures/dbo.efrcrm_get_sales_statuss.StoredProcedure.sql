USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_statuss]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sales_Status
CREATE PROCEDURE [dbo].[efrcrm_get_sales_statuss] AS
begin

select Sales_Status_ID, Description from Sales_Status

end
GO
