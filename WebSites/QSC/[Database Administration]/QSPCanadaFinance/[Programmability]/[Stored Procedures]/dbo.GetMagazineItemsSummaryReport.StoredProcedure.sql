USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetMagazineItemsSummaryReport]    Script Date: 06/07/2017 09:17:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMagazineItemsSummaryReport]
	@OrderID	int,
	@CampaignID   int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 8/19/2004 
--   Get Magazine Items Summary Report For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON


SELECT B.CampaignID,
	S.LastName + ', ' + S.FirstName as ParticipentName, 
	cod.ProductCode as TitleCode, 
	cod.ProductName as MagazineTitle, 	
	cod.Recipient as RecipientName, 
	1 as Qty,
	cod.Quantity as  NumberOfIssues,
	COD.Price as ItemPriceTotal,
	ISNULL(T.LastName, 'Unknown') as TeacherName,
	Classroom,
	CASE COD.ProductType
		WHEN 46001 Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	 
		WHEN 46002 Then (CASE C.Lang
					WHEN 'EN' Then 'Gift'  	   --Gift
					WHEN 'FR' Then 'Cadeau'   --French
					ELSE 'Gift'			
				   END) 
		WHEN 46003 Then (CASE C.Lang
					WHEN 'EN' Then 'WFC'  	   --WFC
					WHEN 'FR' Then 'Chocolat Le Meilleur au Monde'   --French
					ELSE 'WFC'			
				   END) 
		WHEN 46005 Then (CASE C.Lang
					WHEN 'EN' Then 'Food'  	   --Food
					WHEN 'FR' Then 'Produit alimentaire'   --French
					ELSE 'Food'			
				   END) 
		WHEN 46006 Then (CASE C.Lang
					WHEN 'EN' Then 'Book'   --Mag
					WHEN 'FR' Then 'Libre'   --French
					ELSE 'Magazine'			
				   END)  --'Book'
		WHEN 46007 Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END)  --'Music'
		ELSE ''				
	END as ProductTypeName
FROM    QSPCanadaOrderManagement..CustomerOrderDetail COD
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.Instance = COD.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..Student S ON S.Instance = COH.StudentInstance
	INNER JOIN QSPCanadaOrderManagement..Batch B on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	LEFT JOIN QSPCanadaOrderManagement..Teacher T on T.Instance =  S.TeacherInstance
WHERE  cod.ProductType in (46001,46006)-- only magz
	--and COD.StatusInstance in (507, 508, 509, 510, 511,512) --Sent To Remit, Shipped, Pendingtotpl, Pickable, Picked,unremittable	 
	and COD.StatusInstance NOT IN  (506) --  Voided Due To Error added by John P per Saqib 
	and B.ORDERID = @OrderID
ORDER BY Classroom,TeacherName,ParticipentName, RecipientName,MagazineTitle


/* remarked by saqib on 13 oct 2004

SELECT S.LastName + ', ' + S.FirstName as ParticipentName, 
	TitleCode, 
	MagazineTitle, 	
	Recipient as RecipientName, 
	COD.Price as Price,
	MAX(NumberOfIssues) as NumberOfIssues,
	T.FirstName + ', ' + T.LastName as TeacherName,
	Classroom,
	CASE COD.ProductType
		WHEN 46001 Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	 
		WHEN 46002 Then (CASE C.Lang
					WHEN 'EN' Then 'Gift'  	   --Gift
					WHEN 'FR' Then 'Cadeau'   --French
					ELSE 'Gift'			
				   END) 
		WHEN 46003 Then (CASE C.Lang
					WHEN 'EN' Then 'WFC'  	   --WFC
					WHEN 'FR' Then 'Chocolat Le Meilleur au Monde'   --French
					ELSE 'WFC'			
				   END) 
		WHEN 46005 Then (CASE C.Lang
					WHEN 'EN' Then 'Food'  	   --Food
					WHEN 'FR' Then 'Produit alimentaire'   --French
					ELSE 'Food'			
				   END) 
		WHEN 46006 Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END)  --'Book'
		WHEN 46007 Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Mag
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END)  --'Music'
		ELSE ''				
	END as ProductTypeName,
	CASE COD.ProductType
		WHEN 46008 Then (Max(PrizeL.Level_Code)) 	 
		WHEN 46013 Then (Max(PrizeL.Level_Code)) 	
		WHEN 46014 Then (Max(PrizeL.Level_Code)) 	
		WHEN 46015 Then (Max(PrizeL.Level_Code)) 	
	END as Incentive
INTO #Temp
FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory CODRH
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderDetail COD on COD.TransID = CODRH.TransID
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.Instance = COD.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..Student S ON S.Instance = COH.StudentInstance
	INNER JOIN QSPCanadaOrderManagement..CustomerRemitHistory CRH on COD.CustomerOrderHeaderInstance = CODRH.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..Batch B on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	LEFT JOIN QSPCanadaOrderManagement..Teacher T on T.Instance =  S.TeacherInstance
	LEFT JOIN QSPCanadaProduct.dbo.PROGRAM_MASTER ProgramM 
	INNER JOIN QSPCanadaProduct.dbo.PRICING_DETAILS PricingD ON ProgramM.Program_ID = PricingD.Program_ID 
	INNER JOIN QSPCanadaProduct.dbo.Prize_Level PrizeL ON ProgramM.Code = PrizeL.Catalog_Code 
	ON COD.PricingDetailsID = PricingD.MagPrice_Instance
WHERE B.CampaignID = @CampaignID 
	AND B.ORDERID = @OrderID
GROUP BY S.LastName, S.FirstName, TitleCode, MagazineTitle, Recipient, COD.Price, T.FirstName, T.LastName, Classroom, COD.ProductType, C.Lang
ORDER BY MagazineTitle,  RecipientName

SELECT ParticipentName, 
	TitleCode, 
	MagazineTitle, 
	RecipientName, 
	COUNT(*) as Qty,
	NumberOfIssues,
	(Price*Count(*)) as ItemPriceTotal,
	ISNULL(TeacherName, 'Unknown') as TeacherName,
	Classroom,
	ProductTypeName
FROM #Temp
GROUP BY  ParticipentName, TitleCode, MagazineTitle, RecipientName, Price, NumberOfIssues, TeacherName, Classroom, ProductTypeName
ORDER BY Classroom,ParticipentName, MagazineTitle

DROP TABLE #Temp
*/


SET NOCOUNT OFF
GO
