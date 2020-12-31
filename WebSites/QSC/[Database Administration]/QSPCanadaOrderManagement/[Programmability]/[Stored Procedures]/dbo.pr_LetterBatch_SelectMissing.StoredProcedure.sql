USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_SelectMissing]    Script Date: 06/07/2017 09:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_LetterBatch_SelectMissing]

AS

DECLARE	@CurrentSeasonStartDate	DATETIME
SELECT	@CurrentSeasonStartDate = seas.StartDate
FROM	QSPCanadaCommon..Season seas
WHERE	GETDATE() BETWEEN seas.StartDate AND seas.EndDate
AND		seas.Season IN ('Y')

DECLARE	@ToDate		DATETIME
SET @ToDate = DATEADD(DAY, -7, GETDATE())
PRINT @ToDate

SELECT		im.CustomerOrderHeaderInstance,
			im.TransID,
			'Inactive Magazines' AS LetterTemplateName,
			im.MagazineTitle,
			im.ProductCode,
			im.Price,
			im.NbrOfIssues,
			im.[Language],
			im.RunID AS RemitRunID,
			im.CreationDate AS OrderDate,
			im.RecipientFirstName,
			im.RecipientLastName
FROM		vw_GetInactiveMagazines im
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = im.CustomerOrderHeaderInstance
				AND	cod.TransID = im.TransID
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
JOIN		Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = coh.CampaignID
LEFT JOIN	(LetterBatchCustomerOrderDetail lbcod
JOIN		LetterBatch lb
					ON	lb.ID = lbcod.LetterBatchID
					AND	lb.DeletedTF = 0
					AND	lb.LetterTemplateID = 4)
				ON	lbcod.CustomerOrderHeaderInstance = im.CustomerOrderHeaderInstance
				AND	lbcod.TransID = im.TransID
LEFT JOIN	(Incident incCancel
JOIN		IncidentAction incActCancel
					ON	incActCancel.IncidentInstance = incCancel.IncidentInstance)
				ON	incCancel.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	incCancel.TransID = cod.TransID
				AND	incActCancel.ActionInstance = 1 --1: Cancel
LEFT JOIN	(Incident incNewSub
JOIN		IncidentAction incActNewSub
					ON	incActNewSub.IncidentInstance = incNewSub.IncidentInstance)
				ON	incNewSub.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	incNewSub.TransID = cod.TransID
				AND	incActNewSub.ActionInstance = 2 --2: New Order
WHERE		lbcod.LetterBatchID IS NULL
AND			incActCancel.IncidentInstance IS NULL
AND			incActNewSub.IncidentInstance IS NULL
AND			cod.DelFlag <> 1
AND			b.StatusInstance <> 40005
AND			codrh.Status IN (42010) --42010: Magazine Inactive
AND			camp.StartDate >= @CurrentSeasonStartDate
AND			im.CreationDate <= @ToDate

UNION

SELECT		im.CustomerOrderHeaderInstance,
			im.TransID,
			'TV Week - Outside BC' AS LetterTemplateName,
			im.MagazineTitle,
			'11BD' AS ProductCode,
			im.Price,
			im.NbrOfIssues,
			im.[Language],
			im.RunID AS RemitRunID,
			im.CreationDate AS OrderDate,
			im.RecipientFirstName,
			im.RecipientLastName
FROM		vw_GetTVWeekSubs im
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = im.CustomerOrderHeaderInstance
				AND	cod.TransID = im.TransID
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = coh.CampaignID
LEFT JOIN	(LetterBatchCustomerOrderDetail lbcod
JOIN		LetterBatch lb
					ON	lb.ID = lbcod.LetterBatchID
					AND	lb.DeletedTF = 0
					AND	lb.LetterTemplateID = 5)
				ON	lbcod.CustomerOrderHeaderInstance = im.CustomerOrderHeaderInstance
				AND	lbcod.TransID = im.TransID
LEFT JOIN	(Incident incCancel
JOIN		IncidentAction incActCancel
					ON	incActCancel.IncidentInstance = incCancel.IncidentInstance)
				ON	incCancel.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	incCancel.TransID = cod.TransID
				AND	incActCancel.ActionInstance = 1 --1: Cancel
LEFT JOIN	(Incident incNewSub
JOIN		IncidentAction incActNewSub
					ON	incActNewSub.IncidentInstance = incNewSub.IncidentInstance)
				ON	incNewSub.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	incNewSub.TransID = cod.TransID
				AND	incActNewSub.ActionInstance = 2 --2: New Order
WHERE		lbcod.LetterBatchID IS NULL
AND			incActCancel.IncidentInstance IS NULL
AND			incActNewSub.IncidentInstance IS NULL
AND			camp.StartDate >= @CurrentSeasonStartDate
AND			im.CreationDate <= @ToDate

UNION

--Subs that are tied to a previous season that couldn't be automatically updated to a current season offer
SELECT		cod.CustomerOrderHeaderInstance,
			cod.TransID,
			'Current Offer Not Found' AS LetterTemplateName,
			cod.ProductName AS MagazineTitle,
			cod.ProductCode,
			cod.Price,
			cod.Quantity AS NbrOfIssues,
			p.Lang AS Language,
			rb.RunID AS RemitRunID,
			cod.CreationDate AS OrderDate,
			crh.FirstName AS RecipientFirstName,
			crh.LastName AS RecipientLastName
FROM		CustomerOrderDetail cod
JOIN		QSPCanadaProduct..PRICING_DETAILS pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p 
				ON	p.Product_Instance = pd.Product_Instance
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
JOIN		CustomerRemitHistory crh
				ON	crh.Instance = codrh.CustomerRemitHistoryInstance
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
JOIN		Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = coh.CampaignID
LEFT JOIN	(LetterBatchCustomerOrderDetail lbcod
JOIN		LetterBatch lb
					ON	lb.ID = lbcod.LetterBatchID
					AND	lb.DeletedTF = 0
					AND	lb.LetterTemplateID = 4)
				ON	lbcod.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	lbcod.TransID = cod.TransID
LEFT JOIN	(Incident incCancel
JOIN		IncidentAction incActCancel
					ON	incActCancel.IncidentInstance = incCancel.IncidentInstance)
				ON	incCancel.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	incCancel.TransID = cod.TransID
				AND	incActCancel.ActionInstance = 1 --1: Cancel
LEFT JOIN	(Incident incNewSub
JOIN		IncidentAction incActNewSub
					ON	incActNewSub.IncidentInstance = incNewSub.IncidentInstance)
				ON	incNewSub.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	incNewSub.TransID = cod.TransID
				AND	incActNewSub.ActionInstance = 2 --2: New Order
WHERE		lbcod.LetterBatchID IS NULL
AND			incActCancel.IncidentInstance IS NULL
AND			incActNewSub.IncidentInstance IS NULL
AND			cod.DelFlag <> 1
AND			b.StatusInstance <> 40005
AND			codrh.Status IN (42010) --42010: Magazine Inactive
AND			p.Status IN (30600, 30603)
AND			camp.StartDate >= @CurrentSeasonStartDate
AND			cod.CreationDate <= @ToDate

ORDER BY OrderDate
GO
