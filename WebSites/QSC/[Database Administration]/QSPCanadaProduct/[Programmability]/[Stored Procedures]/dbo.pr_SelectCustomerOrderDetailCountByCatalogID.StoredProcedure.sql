USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerOrderDetailCountByCatalogID]    Script Date: 06/07/2017 09:18:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerOrderDetailCountByCatalogID]

@iCatalogID	int

AS

SELECT		COUNT(*)
FROM		
		ProgramSection PS INNER JOIN 
		Pricing_Details PD ON PS.[ID] = PD.ProgramSectionID INNER JOIN
		QSPCanadaOrderManagement..CustomerOrderDetail COD ON PD.MagPrice_Instance = COD.PricingDetailsID
		
WHERE		PS.Program_ID = @iCatalogID-- AND ISNULL(COD.DelFlag,0) <> 0
GO
