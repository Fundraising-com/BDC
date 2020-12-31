USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[HRSummaryDetail]    Script Date: 06/07/2017 09:18:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[HRSummaryDetail] 
AS SELECT DISTINCT b.OrderID OrderId, 
	b.Id BatchId, 
	b.date BatchDate, 
	b.CampaignId CampaignId, 
	(CASE od.producttype WHEN 46001 THEN COUNT(*) ELSE 0 END) MagQuantity,
	 (CASE od.producttype WHEN 46001 THEN SUM(od.Price) ELSE 0 END) TotalMagPrice,
	 (CASE od.producttype WHEN 46002 THEN SUM(od.quantity) ELSE 0 END) GiftQuantity,
	 (CASE od.producttype WHEN 46002 THEN SUM(od.Price) ELSE 0 END) TotalGiftPrice,
	 (CASE od.producttype WHEN 46003 THEN SUM(od.quantity) ELSE 0 END) WFCQuantity, 
	 (CASE od.producttype WHEN 46003 THEN SUM(od.Price) ELSE 0 END) TotalWFCPrice,
	 (CASE od.producttype WHEN 46005 THEN SUM(od.quantity) ELSE 0 END) FoodQuantity, 
	(CASE od.producttype WHEN 46005 THEN SUM(od.Price) ELSE 0 END) TotalFoodPrice, 
	emap.EnvelopeID, 
	s.LastName AS StudentLastName, 
	s.FirstName AS StudentFirstName, 
	t .Classroom, t .FirstName AS TeacherFirstName, 
	t .MiddleInitial AS TeacherMiddleInitial, 
	t .LastName AS TeacherLastName
 FROM   dbo.Student s INNER JOIN dbo.Teacher t ON s.TeacherInstance = t .Instance INNER JOIN 
	dbo.Batch b INNER JOIN dbo.CustomerOrderHeader oh ON b.ID = oh.OrderBatchID AND b.[Date] = oh.OrderBatchDate INNER JOIN 
	dbo.OrderInEnvelopeMap emap ON oh.Instance = emap.CustomerOrderHeaderInstance INNER JOIN dbo.Envelope e ON emap.EnvelopeID = e.Instance AND e.IsIncentive = 'N' 
	INNER JOIN dbo.CustomerOrderDetail od ON oh.Instance = od.CustomerOrderHeaderInstance ON t .Instance = e.TeacherInstance
WHERE (od.ProductCode <> 9999) 
GROUP BY b.CampaignId, b.OrderID, b.Id, b.date, producttype, emap.EnvelopeID, s.LastName, s.FirstName, t .Classroom, t .FirstName, t .MiddleInitial, t .LastName, s.Instance
GO
