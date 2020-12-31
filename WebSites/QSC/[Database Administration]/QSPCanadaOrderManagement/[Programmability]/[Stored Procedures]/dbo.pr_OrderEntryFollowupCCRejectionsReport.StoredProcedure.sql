USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_OrderEntryFollowupCCRejectionsReport]    Script Date: 06/07/2017 09:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_OrderEntryFollowupCCRejectionsReport]

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT		batch.campaignid AS CA,
			batch.OrderID,
			fm.FMID,
			fm.FirstName AS FMFirstName,
			fm.LastName AS FMLastName,
			coh.Instance AS COH,
			cod.TransID,
			teach.Classroom,
			teach.LastName,
			ISNULL(stu.FirstName, '') + ' ' + ISNULL(stu.LastName, '') AS ParticipantName,
			cod.Recipient AS SubscriberName,
			ISNULL(CustBill.FirstName, '') + ' ' + ISNULL(CustBill.LastName, '') AS PurchaserName,
			CustBill.Phone AS CustomerPhone,
			CustBill.Address1 AS CustomerAddress1,
			CustBill.Address2 AS CustomerAddress2,
			CustBill.City AS CustomerCity,
			CustBill.State AS CustomerState,
			CustBill.Zip AS CustomerZip,
			cod.ProductCode AS TitleCode,
			cod.productName AS MagazineTitle,
			cod.Quantity AS Numberofissues,
			cod.Price,
			cp.CreditCardNumber,
			cp.ExpirationDate,
			ISNULL(coh.ToteID,0) AS ToteID,
			CASE WHEN batch.OrderQualifierID in (39001,39002) THEN lo.LandedOrderID
				ELSE 0 	END AS CustomerOrderID	
FROM		CreditCardPayment cp
JOIN		CustomerPaymentHeader ph
				ON	ph.Instance = cp.CustomerPaymentHeaderInstance
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = ph.CustomerOrderHeaderInstance
JOIN		QSPCanadaCommon..Campaign ca
				ON	ca.ID = coh.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = ca.FMID
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		Student stu
				ON	stu.instance = coh.StudentInstance
JOIN		Teacher teach
				ON	teach.Instance = stu.teacherInstance
JOIN		Customer custBill
				ON	custBill.Instance = coh.CustomerBillToInstance
JOIN		Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon.dbo.Season s  
				ON	GetDate() BETWEEN s.StartDate AND s.EndDate  
				AND	s.Season = 'Y'  
				AND	batch.Date BETWEEN s.StartDate AND s.EndDate  
LEFT JOIN	LandedOrder lo
				ON batch.OrderQualifierID IN (39001,39002) AND lo.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
WHERE		coh.PaymentMethodInstance IN (50003, 50004, 50005)
AND			cp.StatusInstance IN (19001, 19002, 19005)
AND			batch.OrderQualifierID NOT IN (39009, 39011, 39014) --Internet, Internet Fix, CC Reprocess Courtesy
AND			cod.DelFlag <> 1
AND			cod.ProductType NOT IN (46017, 46021)
AND			batch.StatusInstance NOT IN (40001, 40002, 40003, 40004, 40005, 40006)
AND			NOT EXISTS		(SELECT	1
							FROM	incident inc
							JOIN	incidentAction incA
										ON	inc.IncidentInstance = incA.IncidentInstance
							WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
							AND		incA.ActionInstance IN (18, 27, 22) --Update Credit Card Info, CC Call Attempt 5, Remove From OEFU Report
							)
AND			NOT EXISTS		(SELECT	1
							FROM	incident inc
							JOIN	incidentAction incA
										ON	inc.IncidentInstance = incA.IncidentInstance
							WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
							AND		inc.TransID = cod.TransID
							AND		incA.ActionInstance IN (150, 151) --New Sub to Invoice, New Item to Invoice
							)
ORDER BY	batch.OrderID,
			coh.Instance,
			cod.TransID,
			teach.LastName,
			teach.Classroom,
			ParticipantName,
			PurchaserName

END
GO
