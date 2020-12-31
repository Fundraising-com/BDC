USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerOrderDetailCount_GST_HST]    Script Date: 06/07/2017 09:18:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerOrderDetailCount_GST_HST]

	@iMagPriceInstanceGST	int,
	@iMagPriceInstanceHST	int

AS

DECLARE	@iCustomerOrderDetailCount	int

SELECT	@iCustomerOrderDetailCount = COUNT(*)
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
WHERE	cod.PricingDetailsID = @iMagPriceInstanceGST

SELECT	@iCustomerOrderDetailCount = @iCustomerOrderDetailCount + COUNT(*)
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
WHERE	cod.PricingDetailsID = @iMagPriceInstanceHST

SELECT	@iCustomerOrderDetailCount
GO
