USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_TechnicalProblemLetterReportSimple]    Script Date: 06/07/2017 09:20:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_TechnicalProblemLetterReportSimple]

AS

SELECT	DISTINCT
		ltrim(rtrim(crh.LastName)) AS LastName,
    	ltrim(rtrim(crh.FirstName)) AS FirstName,
     	coalesce(crh.Address1, '') AS Address1,
		coalesce(crh.Address2, '') AS Address2,
		crh.City AS City,
		crh.state AS Province,
		crh.zip AS PostalCode,
		codrh.MagazineTitle,
		codrh.NumberOfissues as NbIssues,	
		--convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as Amount
		convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) as Amount
		
FROM	CustomerOrderHeader coh,
		CustomerOrderDetail cod,
		CustomerRemitHistory crh,
		CustomerOrderDetailRemitHistory codrh,
		QSPCanadaCommon..Campaign ca,
		Batch b
WHERE	cod.CustomerOrderHeaderInstance = coh.Instance
AND		codrh.CustomerOrderHeaderInstance = coh.Instance
AND		codrh.TransID = cod.TransID
AND		crh.Instance = codrh.CustomerRemitHistoryInstance
AND		ca.ID = coh.CampaignID
AND		coh.OrderBatchID = b.ID AND coh.OrderBatchDate = b.Date
AND		b.OrderID in (506819, 506806)
GO
