USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_sales_shipped]    Script Date: 02/14/2014 13:09:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_sales_shipped] (@date_from VARCHAR(10) = '', @date_to VARCHAR(10) = '', @partner_id INTEGER = 0) AS

	If @partner_id = -1
		EXEC dbo.sp_sales_shipped_without_partner @date_from, @date_to
	Else
		EXEC dbo.sp_sales_shipped_with_partner @date_from, @date_to, @partner_id
GO
