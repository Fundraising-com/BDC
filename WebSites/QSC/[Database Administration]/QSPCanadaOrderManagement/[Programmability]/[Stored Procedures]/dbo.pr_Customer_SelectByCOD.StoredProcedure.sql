USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Customer_SelectByCOD]    Script Date: 06/07/2017 09:19:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Customer_SelectByCOD]

	@iCustomerOrderHeaderInstance	int,
	@iTransID						int

AS

DECLARE		@iCODShipToInstance		int

SELECT		@iCODShipToInstance = cod.CustomerShipToInstance
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
WHERE		cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND			cod.TransID = @iTransID


IF (@iCODShipToInstance = 0)
BEGIN
	SELECT		c.*
	FROM		QSPCanadaOrderManagement..Customer c
	JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
					ON	coh.CustomerBillToInstance = c.Instance
	WHERE		coh.Instance = @iCustomerOrderHeaderInstance
END
ELSE
BEGIN
	SELECT		c.*
	FROM		QSPCanadaOrderManagement..Customer c
	WHERE		c.Instance = @iCODShipToInstance
END
GO
