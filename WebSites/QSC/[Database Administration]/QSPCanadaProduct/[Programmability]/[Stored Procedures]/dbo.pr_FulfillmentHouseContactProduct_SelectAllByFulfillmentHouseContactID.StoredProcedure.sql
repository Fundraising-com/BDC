USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_FulfillmentHouseContactProduct_SelectAllByFulfillmentHouseContactID]    Script Date: 06/07/2017 09:17:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_FulfillmentHouseContactProduct_SelectAllByFulfillmentHouseContactID]

	@iFulfillmentHouseContactID	int

AS

	SELECT	fhcp.ID,
			p.Product_Code,
			p.Product_Sort_Name,
			p.Lang,
			cdStatus.Description AS Status
	
	FROM		Product p,
			FulfillmentHouseContact_Product fhcp,
			QSPCanadaCommon..CodeDetail cdStatus
	
	WHERE	fhcp.FulfillmentHouseContactID = @iFulfillmentHouseContactID
	AND		p.Product_Code = fhcp.Product_Code
	AND		cdStatus.Instance = p.Status
	AND		p.Product_Instance =
			(SELECT	TOP 1
					p2.Product_Instance
			FROM		Product p2
			WHERE	p2.Product_Code = p.Product_Code
			ORDER BY	p2.Product_Instance DESC)
	
	ORDER BY	p.Product_Sort_Name,
			cdStatus.Description
GO
