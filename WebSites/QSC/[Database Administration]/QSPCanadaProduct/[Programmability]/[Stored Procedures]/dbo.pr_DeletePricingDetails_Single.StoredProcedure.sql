USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeletePricingDetails_Single]    Script Date: 06/07/2017 09:17:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DeletePricingDetails_Single]

	@iMagPriceInstance		int

AS
	DELETE FROM Pricing_Details WHERE MagPrice_Instance = @iMagPriceInstance
GO
