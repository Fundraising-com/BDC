USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Entertainment_IsShippedToAccount]    Script Date: 06/07/2017 09:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Entertainment_IsShippedToAccount] (@CustomerOrderHeaderInstance INT)

RETURNS INT

AS  

BEGIN

DECLARE @IsShippedToAccount BIT

SELECT	@IsShippedToAccount = 1
FROM	CustomerOrderHeader coh
WHERE	coh.Instance = @CustomerOrderHeaderInstance
AND		coh.FormCode IN ('0737') --Gift Order Form

RETURN ISNULL(@IsShippedToAccount, 0)
  
END
GO
