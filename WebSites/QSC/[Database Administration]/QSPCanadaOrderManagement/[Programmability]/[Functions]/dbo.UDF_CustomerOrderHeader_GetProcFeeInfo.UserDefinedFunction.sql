USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_CustomerOrderHeader_GetProcFeeInfo]    Script Date: 06/07/2017 09:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_CustomerOrderHeader_GetProcFeeInfo]
(	
	@CustomerOrderHeaderInstance	INT
)

RETURNS NUMERIC(12,2)

AS

BEGIN 

	DECLARE  @ReturnValue NUMERIC(12,2)

	SELECT  @ReturnValue = SUM(cod.Price)
	FROM	CustomerOrderDetail cod
	WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		cod.ProductType IN (46017) --46017: Processing Fee
	AND		cod.DelFlag = 0

	RETURN @ReturnValue
END
GO
