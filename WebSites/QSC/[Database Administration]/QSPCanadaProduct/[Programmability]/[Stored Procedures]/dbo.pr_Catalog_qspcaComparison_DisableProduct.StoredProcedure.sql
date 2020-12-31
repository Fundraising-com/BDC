USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_Catalog_qspcaComparison_DisableProduct]    Script Date: 06/07/2017 09:17:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Catalog_qspcaComparison_DisableProduct]

AS

DECLARE	@SeasonID	INT
SELECT	@SeasonID = ID
FROM	QSPCanadaCommon..Season seas
WHERE	DATEADD(mm, -1, GETDATE()) BETWEEN seas.StartDate AND seas.EndDate
AND		seas.Season IN ('F', 'S')

UPDATE		cid
SET			cid.Deleted = 1,
			cid.Update_Date = GETDATE(),
			cid.Update_User_ID = 101690
FROM		COM_OLTP1.QSPFulfillment.dbo.Catalog_Item ci
JOIN		COM_OLTP1.QSPFulfillment.dbo.Catalog_Item_Detail cid
				ON	cid.catalog_item_id = ci.catalog_item_id
JOIN		COM_OLTP1.QSPFulfillment.dbo.Catalog cat
				ON	cat.catalog_id = ci.catalog_id
JOIN		COM_OLTP1.QSPFulfillment.dbo.Catalog_Group catGroup
				ON	catGroup.catalog_group_id = cat.catalog_group_id
JOIN		COM_OLTP1.QSPEcommerce.dbo.Site_Catalog_Group siteCatGroup
				ON	siteCatGroup.Catalog_Group_ID = catGroup.Catalog_Group_ID
LEFT JOIN	(Product prod
JOIN			Pricing_Details pd
					ON	pd.Product_Instance = prod.Product_Instance
					AND	pd.Status IN (30600, 30603) --Active, Unremittable
JOIN			ProgramSection ps
					ON	ps.ID = pd.ProgramSectionID
JOIN			Program_Master pm
					ON	pm.Program_ID = ps.Program_ID
					AND	pm.Season = @SeasonID)
				ON	(CASE ISNULL(prod.RemitCode, '')
						WHEN '' THEN	prod.Product_Code
						ELSE			prod.RemitCode
					END = ci.Catalog_Item_Code
				OR	ci.Catalog_Item_Code = '8212')
				AND	pd.QSP_Price = cid.Price
				AND	pd.Nbr_Of_Issues = cid.Term
				AND	prod.Status IN (30600, 30603) --Active, Unremittable
				AND	catGroup.Catalog_Group_Name LIKE	CASE pd.TaxRegionID
															WHEN 1 THEN '%GST'
															ELSE		'%HST'
														END
				AND	pm.Code LIKE	CASE
										WHEN catGroup.Catalog_Group_Name LIKE '%Faculty%' THEN	'SRP%'
										ELSE													'MAG%'
									END

WHERE		ci.Deleted = 0
AND			cid.Deleted = 0
AND			cat.Deleted = 0
AND			catGroup.Deleted = 0
--AND			cat.Start_Date BETWEEN @SeasonStartDate AND @SeasonEndDate
--AND			catGroup.Catalog_Group_Name LIKE 'QSP Canada%'
AND			siteCatGroup.Site_ID = 4 --4: qsp.ca
AND			prod.RemitCode IS NULL
GO
