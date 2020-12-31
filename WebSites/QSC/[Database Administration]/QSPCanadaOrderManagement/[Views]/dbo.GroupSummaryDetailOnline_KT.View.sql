USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[GroupSummaryDetailOnline_KT]    Script Date: 06/07/2017 09:18:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  view [dbo].[GroupSummaryDetailOnline_KT] as
SELECT     Od.TransID,  (CASE od.ProductType WHEN 46001 THEN 1 ELSE od.Quantity END) AS Quantity, od.Price AS TotalPrice, 
                                                         od.ProductType, od.ProductCode, t .FirstName AS TeacherFirstName, t .LastName AS TeacherLastName, 
                                                                                                                      t .Instance, t .Classroom,  b.CampaignID, OnlineOrderID, b.ID AS BatchId, b.[Date] AS BatchDate, 
                                                                                                                      progd.Premium_Code, progd.QSPPremiumID AS PremiumCount, 
                                                                                                                      (CASE od.ProductType WHEN 46008 THEN CASE od.ProductCode WHEN 'PRA' THEN SUM(od.Quantity) 
                                                                                                                      WHEN 'PRB' THEN SUM(od.Quantity) WHEN 'PRC' THEN SUM(od.Quantity) 
                                                                                                                      WHEN 'PRD' THEN SUM(od.Quantity) WHEN 'PRE' THEN SUM(od.Quantity) 
                                                                                                                      WHEN 'PRF' THEN SUM(od.Quantity) WHEN 'PRG' THEN SUM(od.Quantity) ELSE 0 END ELSE 0 END) 
                                                                                                                      IncentiveCount, oh.Instance CustomerHeaderInstance, S.LastName, S.FirstName, s.Instance StudentInstance,b.OrderQualifierId
FROM    
	QSPCanadaCommon.dbo.CampaignProgram cp INNER JOIN
	dbo.OnLineOrderMappingTable OMT inner join
                      dbo.Student S On S.Instance=OMT.StudentInstance   INNER JOIN
                      dbo.Teacher T ON S.TeacherInstance = T.Instance INNER JOIN
                      dbo.CustomerOrderHeader Oh ON S.Instance = Oh.StudentInstance  and OH.Instance = OMT.CustomerOrderHeaderInstance INNER JOIN
                      dbo.CustomerOrderDetail Od ON Oh.Instance = Od.CustomerOrderHeaderInstance INNER JOIN
                      dbo.Batch B ON Oh.OrderBatchID = B.ID AND Oh.OrderBatchDate = B.[Date] ON cp.CampaignID = B.CampaignID 
		      LEFT OUTER JOIN
                      QSPCanadaProduct.dbo.PROGRAM_DETAILS ProgD INNER JOIN
                      QSPCanadaProduct.dbo.PRICING_DETAILS Pr ON ProgD.Offer_ID = Pr.Offer_Code ON Od.PricingDetailsID = Pr.MagPrice_Instance
Where cp.Programid <> 1 --Magazine Program
And cp.DeletedTF <> 1
GROUP BY oh.Instance, Od.TransID, B.CampaignID, B.ID, B.[Date], OnlineOrderID, T .Instance, T .LastName, T .FirstName, s.Instance,
      T .Classroom,S.FirstName, S.LastName, Od.ProductType, Od.ProductCode, ProgD.Premium_Code, 
      ProgD.QSPPremiumID,Od.Quantity, Od.Price,b.OrderQualifierId
GO
