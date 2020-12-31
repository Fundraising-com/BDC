USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[GroupSummaryDetail]    Script Date: 06/07/2017 09:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[GroupSummaryDetail] 
AS
SELECT  (CASE od.ProductType WHEN 46001 THEN 1 
				ELSE od.Quantity 
				END) AS Quantity, 
	od.Price AS TotalPrice, 
	od.ProductType, 
	od.ProductCode, 
	t .FirstName AS TeacherFirstName, 
	t .LastName AS TeacherLastName, 
             t .Instance, t .Classroom, b.CampaignID, 
	b.OrderID, b.ID AS BatchId, 
	b.[Date] AS BatchDate, 
	pr.QSPPremiumID Premium_Code, 
	pr.QSPPremiumID AS PremiumCount, 
	--pr.QSPPremiumID Premium_Code, 
	--pr.QSPPremiumID AS PremiumCount, 
             (CASE od.ProductType WHEN 46008 THEN CASE od.ProductCode   
							WHEN 'PRA' THEN SUM(od.Quantity) 
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
	END)  IncentiveCount,  
	oh.Instance CustomerHeaderInstance, 
	s.LastName, 
	s.FirstName, 
	s.Instance StudentInstance,
	b.OrderQualifierId
FROM   QSPCanadaCommon.dbo.CampaignProgram cp INNER JOIN
             QSPCanadaOrderManagement.dbo.Student S INNER JOIN
             QSPCanadaOrderManagement.dbo.Teacher T ON S.TeacherInstance = T.Instance INNER JOIN
             QSPCanadaOrderManagement.dbo.CustomerOrderHeader Oh ON S.Instance = Oh.StudentInstance INNER JOIN
             QSPCanadaOrderManagement.dbo.CustomerOrderDetail Od ON Oh.Instance = Od.CustomerOrderHeaderInstance INNER JOIN
             QSPCanadaOrderManagement.dbo.Batch B ON Oh.OrderBatchID = B.ID AND Oh.OrderBatchDate = B.[Date] ON cp.CampaignID = B.CampaignID LEFT OUTER JOIN
             QSPCanadaProduct.dbo.PRICING_DETAILS Pr ON Od.PricingDetailsID = Pr.MagPrice_Instance
WHERE cp.Programid <> 1 --Magazine Program
AND       cp.DeletedTF <> 1
AND       od.DelFlag=0
GROUP BY oh.Instance,  B.CampaignID, B.ID, B.[Date], B.OrderID, T .Instance, T .LastName, T .FirstName, s.Instance,
                    T .Classroom, S.FirstName, S.LastName, Od.ProductType, Od.ProductCode, Pr.PrdPremiumCode, 
                    Pr.QSPPremiumID,Od.Quantity, Od.Price,b.OrderQualifierId,od.transid















/*SELECT  (CASE od.ProductType WHEN 46001 THEN 1 
				ELSE od.Quantity 
				END) AS Quantity, 
	od.Price AS TotalPrice, 
	od.ProductType, 
	od.ProductCode, 
	t .FirstName AS TeacherFirstName, 
	t .LastName AS TeacherLastName, 
             t .Instance, t .Classroom, b.CampaignID, 
	b.OrderID, b.ID AS BatchId, 
	b.[Date] AS BatchDate, 
             progd.Premium_Code, 
	progd.QSPPremiumID AS PremiumCount, 
              (CASE od.ProductType WHEN 46008 THEN CASE od.ProductCode   
							WHEN 'PRA' THEN SUM(od.Quantity) 
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
	END)  IncentiveCount,  
	oh.Instance CustomerHeaderInstance, 
	s.LastName, 
	s.FirstName, 
	s.Instance StudentInstance,
	b.OrderQualifierId
FROM    QSPCanadaCommon.dbo.CampaignProgram cp INNER JOIN
              QSPCanadaOrderManagement.dbo.Student S INNER JOIN
              QSPCanadaOrderManagement.dbo.Teacher T ON S.TeacherInstance = T.Instance INNER JOIN
              QSPCanadaOrderManagement.dbo.CustomerOrderHeader Oh ON S.Instance = Oh.StudentInstance INNER JOIN
              QSPCanadaOrderManagement.dbo.CustomerOrderDetail Od ON Oh.Instance = Od.CustomerOrderHeaderInstance INNER JOIN
              QSPCanadaOrderManagement.dbo.Batch B ON Oh.OrderBatchID = B.ID AND Oh.OrderBatchDate = B.[Date] ON cp.CampaignID = B.CampaignID LEFT OUTER JOIN
              QSPCanadaProduct.dbo.PROGRAM_DETAILS ProgD INNER JOIN
              QSPCanadaProduct.dbo.PRICING_DETAILS Pr ON ProgD.Offer_ID = Pr.Offer_Code AND ProgD.MagProgram_Instance = Pr.MagProgram_Instance ON Od.PricingDetailsID = Pr.MagPrice_Instance
WHERE cp.Programid <> 1 --Magazine Program
AND       cp.DeletedTF <> 1
GROUP BY oh.Instance,  B.CampaignID, B.ID, B.[Date], B.OrderID, T .Instance, T .LastName, T .FirstName, s.Instance,
                    T .Classroom, S.FirstName, S.LastName, Od.ProductType, Od.ProductCode, ProgD.Premium_Code, 
                    ProgD.QSPPremiumID,Od.Quantity, Od.Price,b.OrderQualifierId
*/
GO
