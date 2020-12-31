USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetMagazineItemsSummaryReportByProduct]    Script Date: 06/07/2017 09:17:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetMagazineItemsSummaryReportByProduct]
	@OrderID	int,
	@CampaignID	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 8/19/2004 
--   Get Magazine Items Summary Report By Product For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

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
	END as ProductTypeName
INTO #Temp
FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory CODRH
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderDetail COD on COD.TransID = CODRH.TransID
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.Instance = COD.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..Student S ON S.Instance = COH.StudentInstance
	INNER JOIN QSPCanadaOrderManagement..CustomerRemitHistory CRH on COD.CustomerOrderHeaderInstance = CODRH.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..Batch B on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	LEFT JOIN    QSPCanadaOrderManagement..Teacher T on T.Instance =  S.TeacherInstance
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
INTO #Temp2
FROM #Temp
GROUP BY  ParticipentName, TitleCode, MagazineTitle, RecipientName, Price, NumberOfIssues, TeacherName, Classroom, ProductTypeName
ORDER BY ParticipentName, MagazineTitle

SELECT ParticipentName, 
	ProductTypeName,
	SUM(Qty) as TotalItems, 
	SUM(ItemPriceTotal) as TotalSales
FROM #Temp2
GROUP BY  ParticipentName, ProductTypeName
ORDER BY ParticipentName


DROP TABLE #Temp
DROP TABLE #Temp2


SET NOCOUNT OFF
GO
