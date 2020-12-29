USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_sales_shipped_return_reship]    Script Date: 02/14/2014 13:09:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_sales_shipped_return_reship] (@date_from VARCHAR(10) = '', @date_to VARCHAR(10) = '', @Partner_ID INT = 0) AS
-- debut sp_

	IF @Partner_ID = -1
		EXEC sp_sales_shipped_return_reship_without_partner @date_from, @date_to
	ELSE
		EXEC sp_sales_shipped_return_reship_with_partner @date_from, @date_to, @partner_id
GO
