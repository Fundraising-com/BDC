USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerOrderDetailCountByCatalogSectionID]    Script Date: 06/07/2017 09:18:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerOrderDetailCountByCatalogSectionID]

	@iCatalogSectionID		int

AS

SELECT	COUNT(*)
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod,
		Pricing_Details pd
WHERE	pd.MagPrice_Instance = cod.PricingDetailsID
AND		pd.ProgramSectionID = @iCatalogSectionID
GO
