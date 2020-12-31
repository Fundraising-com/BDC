USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_PublisherContactProduct_SelectAllByPublisherContactID]    Script Date: 06/07/2017 09:17:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_PublisherContactProduct_SelectAllByPublisherContactID]

	@iPublisherContactID	int

AS

	SELECT	pcp.ID,
			p.Product_Code,
			p.Product_Sort_Name,
			p.Lang,
			cdStatus.Description AS Status
	
	FROM		Product p,
			PublisherContact_Product pcp,
			QSPCanadaCommon..CodeDetail cdStatus
	
	WHERE	pcp.PublisherContactID = @iPublisherContactID
	AND		p.Product_Code = pcp.Product_Code
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
