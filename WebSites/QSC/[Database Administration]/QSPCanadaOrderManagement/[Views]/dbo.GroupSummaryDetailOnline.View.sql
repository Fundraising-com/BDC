USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[GroupSummaryDetailOnline]    Script Date: 06/07/2017 09:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  view [dbo].[GroupSummaryDetailOnline] as
SELECT   Od.TransID, 
	  (CASE od.ProductType WHEN 46001 THEN 1 ELSE od.Quantity END) AS Quantity, od.Price AS TotalPrice, 
                                                         od.ProductType, od.ProductCode, t .FirstName AS TeacherFirstName, t .LastName AS TeacherLastName, 
                                                                                                                      t .Instance, t .Classroom,  b.CampaignID, OnlineOrderID,b.OrderId, b.ID AS BatchId, b.[Date] AS BatchDate, 
                                                                                                                      --progd.Premium_Code, progd.QSPPremiumID AS PremiumCount, 
									pr.QSPPremiumID As Premium_Code, pr.QSPPremiumID AS PremiumCount, 
                                                                                                                      (CASE od.ProductType WHEN 46008 THEN CASE od.ProductCode     WHEN 'PRA' THEN SUM(od.Quantity) 
									        WHEN 'PRB' THEN SUM(od.Quantity) WHEN 'PRC' THEN SUM(od.Quantity) 
					                                                              WHEN 'PRD' THEN SUM(od.Quantity) WHEN 'PRE' THEN SUM(od.Quantity) 
                                                                 					         WHEN 'PRF' THEN SUM(od.Quantity) WHEN 'PRG' THEN SUM(od.Quantity) 
									         ELSE 0 
							END 
				   WHEN 46013 THEN  CASE od.ProductCode WHEN 'DCA' THEN SUM(od.Quantity) 
		                                                                 WHEN 'DCB' THEN SUM(od.Quantity) WHEN 'DCC' THEN SUM(od.Quantity) 
                           		                                      WHEN 'DCD' THEN SUM(od.Quantity) WHEN 'DCE' THEN SUM(od.Quantity) 
                                                     			           WHEN 'DCF' THEN SUM(od.Quantity) WHEN 'DCG' THEN SUM(od.Quantity) 
						           ELSE 0 
						           END 
	 ELSE 0 
	END)  IncentiveCount,  oh.Instance CustomerHeaderInstance, S.LastName, S.FirstName, s.Instance StudentInstance,b.OrderQualifierId
/*FROM         QSPCanadaCommon.dbo.CampaignProgram cp INNER JOIN
                      dbo.OnlineOrderMappingTable OMT INNER JOIN
                      dbo.Student S ON S.Instance = OMT.StudentInstance INNER JOIN
                      dbo.Teacher T ON S.TeacherInstance = T.Instance INNER JOIN
                      dbo.CustomerOrderHeader Oh ON S.Instance = Oh.StudentInstance AND Oh.Instance = OMT.CustomerOrderHeaderInstance INNER JOIN
                      dbo.CustomerOrderDetail Od ON Oh.Instance = Od.CustomerOrderHeaderInstance INNER JOIN
                      dbo.Batch B ON Oh.OrderBatchID = B.ID AND Oh.OrderBatchDate = B.[Date] AND OMT.OnlineOrderID = B.OrderID ON    --- Added
                      cp.CampaignID = B.CampaignID LEFT OUTER JOIN
                      QSPCanadaProduct.dbo.PROGRAM_DETAILS ProgD INNER JOIN
                      QSPCanadaProduct.dbo.PRICING_DETAILS Pr 
		ON ProgD.MagProgram_Instance = Pr.MagProgram_Instance --ON ProgD.Offer_ID = Pr.Offer_Code    Changed :April 11, 2005
		ON Od.PricingDetailsID = Pr.MagPrice_Instance
*/
FROM            QSPCanadaCommon.dbo.CampaignProgram cp INNER JOIN
                      dbo.OnlineOrderMappingTable OMT INNER JOIN
                      dbo.Student S ON S.Instance = OMT.StudentInstance INNER JOIN
                      dbo.Teacher T ON S.TeacherInstance = T.Instance INNER JOIN
                      dbo.CustomerOrderHeader Oh ON S.Instance = Oh.StudentInstance AND Oh.Instance = OMT.CustomerOrderHeaderInstance INNER JOIN
                      dbo.CustomerOrderDetail Od ON Oh.Instance = Od.CustomerOrderHeaderInstance INNER JOIN
                      dbo.Batch B ON Oh.OrderBatchID = B.ID AND Oh.OrderBatchDate = B.[Date] AND OMT.OnlineOrderID = B.OrderID ON 
                      cp.CampaignID = B.CampaignID LEFT OUTER JOIN
                      QSPCanadaProduct.dbo.PRICING_DETAILS Pr ON Od.PricingDetailsID = Pr.MagPrice_Instance        
WHERE     (cp.ProgramID <> 1)
And cp.DeletedTF <> 1
And od.DelFlag=0
GROUP BY Oh.Instance, Od.TransID, B.CampaignID, B.ID, B.[Date], OMT.OnlineOrderID, B.OrderID, T.Instance, T.LastName, T.FirstName, S.Instance, 
                      T.Classroom, S.FirstName, S.LastName, Od.ProductType, Od.ProductCode,
	-- ProgD.Premium_Code, ProgD.QSPPremiumID,
Pr.QSPPremiumID, Od.Quantity, Od.Price,            B.OrderQualifierID
GO
