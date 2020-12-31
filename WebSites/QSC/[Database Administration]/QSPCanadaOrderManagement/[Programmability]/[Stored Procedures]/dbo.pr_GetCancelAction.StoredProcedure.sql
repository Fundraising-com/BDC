USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetCancelAction]    Script Date: 06/07/2017 09:20:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GetCancelAction]

@iCustomerOrderHeaderInstance 	int = 0,
@iTransID 				int = 0

AS

DECLARE @ProductType INT
SELECT	@ProductType = ProductType
FROM	CustomerOrderDetail
WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		TransID = @iTransID

IF @ProductType = 46001
BEGIN

	DECLARE 	@iCount int,
			@iActionToReturn int

	SELECT  @iCount=COUNT(*) 
	   FROM  CustomerOrderDetailRemitHistory
	WHERE  CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
		  TransID = @iTransID AND
		  Status IN('42001', '42002', '42003', '42004', '42006', '42007')

	IF @iCount > 0
		SET @iActionToReturn = 1
	ELSE
		SET @iActionToReturn = 14
END
ELSE
BEGIN
	SET @iActionToReturn = 1
END

SELECT @iActionToReturn
GO
