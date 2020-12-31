USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerOrderDetailCountByProductInstance]    Script Date: 06/07/2017 09:18:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerOrderDetailCountByProductInstance]

	@iProductInstance	int

AS

SELECT	COUNT(*)
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod,
		Pricing_Details pd
WHERE	pd.MagPrice_Instance = cod.PricingDetailsID
AND		pd.Product_Instance = @iProductInstance
GO
