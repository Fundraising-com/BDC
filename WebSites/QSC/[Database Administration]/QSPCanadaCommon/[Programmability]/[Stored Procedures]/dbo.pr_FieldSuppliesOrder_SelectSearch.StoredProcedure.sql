USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_FieldSuppliesOrder_SelectSearch]    Script Date: 06/07/2017 09:33:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_FieldSuppliesOrder_SelectSearch]

	@iCampaignID int

AS

SELECT	b.OrderID,
		ps.Name AS ProgramSectionName,
		p.Product_Code,
		p.Product_Sort_Name,
		cod.Quantity,
		cd.Description AS Status
		
FROM		QSPCanadaOrderManagement..Batch b,
		QSPCanadaOrderManagement..CustomerOrderHeader coh,
		QSPCanadaOrderManagement..CustomerOrderDetail cod,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Product p,
		QSPCanadaCommon..CodeDetail cd

WHERE	b.CampaignID = @iCampaignID
AND		b.OrderQualifierID = 39007
AND		coh.OrderBatchID = b.ID
AND		coh.OrderBatchDate = b.Date
AND		cod.CustomerOrderHeaderInstance = coh.Instance
AND		pd.MagPrice_Instance = cod.PricingDetailsID
AND		ps.ID = pd.ProgramSectionID
AND		p.Product_Instance = pd.Product_Instance
AND		cd.Instance = cod.StatusInstance
AND		b.StatusInstance <> 40005
AND		cod.DelFlag = 0
GO
