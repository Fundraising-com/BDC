USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sap_sales_status_by_id]    Script Date: 02/14/2014 13:06:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jason farrell
-- Create date: july 19, 2012
-- Description:	get sap order status for salescreen
-- =============================================
create PROCEDURE [dbo].[efrcrm_get_sap_sales_status_by_id]
	-- Add the parameters for the stored procedure here
	@Ext_sales_status_id AS int
AS

SET NOCOUNT ON;
select Ext_sales_status_id, Description from [Ext_sales_status] where Ext_sales_status_id = @Ext_sales_status_id
GO
