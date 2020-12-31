USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetMagazineItemsSummaryReport]    Script Date: 06/07/2017 09:19:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetMagazineItemsSummaryReport]
	@ReportRequestID int,
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
	and COD.StatusInstance NOT IN  (506)
	and B.ORDERID = @OrderID
ORDER BY Classroom,TeacherName,ParticipentName, RecipientName,MagazineTitle



--following lines are written by saqib on 13-Apr-2005 to update data driven subscription support tables
IF @ReportRequestID <> 0  -- if the value is not zero it means the report is called from a data driven subscription
BEGIN
     
   UPDATE Qspcanadaordermanagement.dbo.ReportRequestBatch_MagazineItemsSummary
   set  RunDateStart = getdate()
   where [id]  = @ReportRequestID

END

SET NOCOUNT OFF
GO
