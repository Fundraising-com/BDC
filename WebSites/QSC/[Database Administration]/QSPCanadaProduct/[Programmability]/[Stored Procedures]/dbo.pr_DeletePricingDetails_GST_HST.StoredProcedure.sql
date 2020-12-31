USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeletePricingDetails_GST_HST]    Script Date: 06/07/2017 09:17:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DeletePricingDetails_GST_HST]
	@iMagPriceInstanceGST	int,
	@iMagPriceInstanceHST	int
AS
	DELETE FROM Pricing_Details WHERE MagPrice_Instance = @iMagPriceInstanceGST
	DELETE FROM Pricing_Details WHERE MagPrice_Instance = @iMagPriceInstanceHST
GO
