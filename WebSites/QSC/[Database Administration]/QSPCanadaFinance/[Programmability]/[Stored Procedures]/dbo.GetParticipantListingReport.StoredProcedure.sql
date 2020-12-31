USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetParticipantListingReport]    Script Date: 06/07/2017 09:17:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE [dbo].[GetParticipantListingReport]
	@OrderID	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 9/8/2004 
--   Get Participant Listing Report For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT coh.StudentInstance,isnull(S.LastName,'') + ', ' + isnull(S.FirstName,'') as ParticipentName, 
	COD.ProductType,
	ProductCode, 	
	ProductName, 	
	Recipient as RecipientName, 
	COD.Price as ItemPriceTotal,
	CASE COD.ProductType
		WHEN 46001 Then (1) 	   --Mag
		WHEN 46002 Then (COD.Quantity) --Gift
		WHEN 46003 Then (COD.Quantity) --WFC
		WHEN 46005 Then (COD.Quantity) --Food
		WHEN 46006 Then (COD.Quantity) --Book 
		WHEN 46007 Then (COD.Quantity) --Music
		WHEN 46010 Then (COD.Quantity) --MMB
		WHEN 46011 Then (COD.Quantity) --National
		WHEN 46012 Then (COD.Quantity) --Video
	END AS 'QTY',
	ISNULL(T.LastName,'Unknown') as TeacherName,
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
		WHEN 46010  Then (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --MMB
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	 --MMB
		WHEN 46011 Then (CASE C.Lang
					WHEN 'EN' Then 'National'   --National
					WHEN 'FR' Then 'National'   --French
					ELSE 'National'			
				   END)	--National
		WHEN 46012 Then (CASE C.Lang
					WHEN 'EN' Then 'Video'   --Video
					WHEN 'FR' Then 'Video'   --French
					ELSE 'Video'			
				   END)	--Video
		ELSE ''				
	END as ProductTypeName,
	QSPCanadaOrderManagement.dbo.UDF_GetPrizeLevel(coh.StudentInstance, b.Orderid) as Incentive,
	QSPCanadaOrderManagement.dbo.UDF_GetInternetOrderItems(b.Orderid,coh.StudentInstance) as TotalInternetItems,
             'LANDED' AS Sale_Type	
FROM QSPCanadaOrderManagement..CustomerOrderDetail COD 
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.Instance = COD.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..Student S ON S.Instance = COH.StudentInstance
	INNER JOIN QSPCanadaOrderManagement..Batch B on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	LEFT JOIN QSPCanadaOrderManagement..Teacher T on T.Instance =  S.TeacherInstance
WHERE  B.ORDERID = @OrderID
	 and COD.StatusInstance <>  506 -- Voided Due To Error 
	and  cod.ProductType in (46001,  46002, 46003, 46005, 46006, 46007,46010, 46011,  46012)  


UNION ALL-- now add up the prizes info of those students who doesnt have regular ground sale but they sold only online

SELECT coh.StudentInstance, isnull(student.LastName,'Unknown') + ', ' + isnull(student.FirstName,'Unknown') as ParticipentName, 
	null as ProductType,
	null as ProductCode, 	
	null as ProductName, 	
	null as RecipientName, 
	null as ItemPriceTotal,
	null AS 'QTY',
	ISNULL(Teacher.LastName,'Unknown') as TeacherName,
	Teacher.Classroom,
	null as ProductTypeName,
	Substring(ProductCode,3,1) as Incentive,
	QSPCanadaOrderManagement.dbo.UDF_GetInternetOrderItems(batch.Orderid,coh.StudentInstance) as TotalInternetItems,
        'ONLINE' AS Sale_Type
FROM  QSPCanadaOrderManagement.dbo.customerorderdetail cod,
      QSPCanadaOrderManagement.dbo.customerorderheader coh,
      QSPCanadaOrderManagement.dbo.batch as batch,
      QSPCanadaOrderManagement.dbo.student as student,
      QSPCanadaOrderManagement.dbo.teacher as teacher
 where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.studentinstance = student.instance
 and coh.instance = cod.customerorderheaderinstance
 and student.teacherinstance = teacher.instance
  and COD.StatusInstance <>  506 -- Voided Due To Error 
 and  cod.ProductType in (46008,46013,46014,46015)  -- only prizes
 and  batch.OrderId = @OrderID

 and coh.StudentInstance IN --to validate in mapping table 
			    (select distinct studentinstance 
			     from    QSPCanadaOrderManagement.dbo.OnlineOrderMappingTable as map
			     where  map.LandedOrderID = @OrderID )


and coh.StudentInstance NOT IN  -- exclude those students who have sold items in ground sale
( Select distinct coh.StudentInstance
  FROM  QSPCanadaOrderManagement.dbo.customerorderdetail cod,
        QSPCanadaOrderManagement.dbo.customerorderheader coh,
        QSPCanadaOrderManagement.dbo.batch as batch
 where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.instance = cod.customerorderheaderinstance
 and batch.OrderId = @OrderID
 and cod.ProductType NOT IN (46008,46013,46014,46015) )

ORDER BY Classroom,TeacherName, ParticipentName , RecipientName,ProductTypeName, ProductName

SET NOCOUNT OFF
GO
