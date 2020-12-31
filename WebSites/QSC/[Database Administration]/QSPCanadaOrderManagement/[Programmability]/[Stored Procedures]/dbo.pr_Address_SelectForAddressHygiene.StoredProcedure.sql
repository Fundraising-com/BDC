USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Address_SelectForAddressHygiene]    Script Date: 06/07/2017 09:19:43 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Address_SelectForAddressHygiene]

AS
CREATE TABLE #Address(
	[CustomerID] [int] NOT NULL,
	[BatchID] [int] NOT NULL,
	[BatchDate] [datetime] NOT NULL
) ON [PRIMARY]


INSERT INTO	#Address(
			[customerID],
			[BatchID],
			[BatchDate])
SELECT		DISTINCT
			CASE 
				WHEN cod.CustomerShipToInstance = 0 THEN custCoh.Instance
													ELSE custCod.Instance
			END AS CustomerID,
			b.[ID] AS BatchID,
			b.[Date] AS BatchDate
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchDate = b.[date]
				AND	coh.OrderBatchID = b.ID
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.instance
JOIN		Customer custCoh
				ON	custCoh.Instance = coh.CustomerBillToInstance
JOIN		Customer custCod
				ON	custCod.Instance = cod.CustomerShipToInstance
LEFT JOIN	AddressHygiene ah
				ON	ah.BatchDate = b.[date]
				AND	ah.BatchID = b.ID
WHERE		ah.BatchID is null --Ensure it has not already been hygiened
AND			b.OrderQualifierID in (39001, 39002)
AND			b.StatusInstance in (500, 501, 502)
AND			b.CampaignID in (SELECT	campaignID FROM AddressHygieneCampaignException)


INSERT INTO	AddressHygiene(
			BatchID,
			BatchDate,
			DateRun)
SELECT		BatchID,
			BatchDate,
			GETDATE()
FROM		#Address
GROUP BY	BatchDate, BatchID
ORDER BY	BatchDate, BatchID


SELECT		addr.CustomerID,
			ISNULL(cust.Address1, '') AS Address1, 
			ISNULL(cust.Address2, '') AS Address2, 
			ISNULL(cust.City, '') AS City, 
			ISNULL(cust.County, '') AS County,
			ISNULL(cust.State, '') AS Region, 
			ISNULL(cust.Zip, '') AS PostCode,
			ISNULL(cust.ZipPlusFour, '') AS PostCode2,
			'CA' AS Country
FROM		#Address addr
JOIN		Customer cust
				ON	cust.Instance = addr.CustomerID
ORDER BY	BatchDate, BatchID


DROP TABLE #Address
GO
