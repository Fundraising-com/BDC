USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerOrderDetailCount_Single]    Script Date: 06/07/2017 09:18:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerOrderDetailCount_Single]

	@iMagPriceInstance		int

AS

SELECT	COUNT(*)
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
WHERE	cod.PricingDetailsID = @iMagPriceInstance
GO
