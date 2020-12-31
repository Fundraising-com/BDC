USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_StaffOrdersForTime]    Script Date: 06/07/2017 09:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 09/06/2006
-- Update:		10/20/2006 - Includes Loonie orders for Time too.
-- Description:	Staff Orders for Time Remit Test
-- =============================================
CREATE PROCEDURE [dbo].[pr_RemitTest_StaffOrdersForTime] 
	
	@iRunID		int = 0

AS
	SELECT		codrh.CustomerOrderHeaderInstance,
				codrh.TransID,
				crh.FirstName,
				crh.LastName,
				codrh.TitleCode,
				codrh.MagazineTitle,
				codrh.Quantity,
				codrh.CatalogPrice,
				crh.Address1,
				COALESCE(crh.Address2, '') AS Address2,
				crh.City,
				crh.State AS Province,
				crh.Zip AS PostalCode,
				cod.CreationDate
	FROM		CustomerRemitHistory crh
	JOIN		CustomerOrderDetailRemitHistory codrh
					ON	codrh.CustomerRemitHistoryInstance = crh.Instance
					AND	codrh.Status IN (42000, 42001)
	JOIN		CustomerOrderHeader coh
					ON	coh.Instance = codrh.CustomerOrderHeaderInstance
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
					AND	cod.TransID = codrh.TransID
	JOIN		QSPCanadaCommon..Campaign c
					ON	c.ID = coh.CampaignID
	LEFT JOIN	QSPCanadaCommon..CampaignProgram cp
					ON	cp.CampaignID = c.ID
					AND	cp.ProgramID = 24 -- Loonie
					AND	cp.DeletedTF = 0
	JOIN		QSPCanadaProduct..Pricing_Details pd
					ON	pd.MagPrice_Instance = cod.PricingDetailsID
	JOIN		QSPCanadaProduct..Product p
					ON	p.Product_Instance = pd.Product_Instance
					AND	p.Pub_Nbr IN (57, 58)
	JOIN		RemitBatch rb
					ON	rb.ID = codrh.RemitBatchID
					AND	rb.RunID = @iRunID
	WHERE		c.IsStaffOrder = 1
	OR			COALESCE(cp.CampaignID, 0) <> 0
GO
