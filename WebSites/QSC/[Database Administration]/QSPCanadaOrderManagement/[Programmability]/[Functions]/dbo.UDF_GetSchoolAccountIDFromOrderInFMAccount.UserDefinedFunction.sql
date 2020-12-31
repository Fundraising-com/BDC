USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetSchoolAccountIDFromOrderInFMAccount]    Script Date: 06/07/2017 09:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_GetSchoolAccountIDFromOrderInFMAccount] (@OrderID INT)

RETURNS INT

AS

BEGIN	

DECLARE		@SchoolAccountID INT

SELECT		@SchoolAccountID = accSchool.Id
FROM		Batch b
JOIN		QSPCanadaCommon..Campaign cFM ON cFM.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount accFM ON accFM.Id = cFM.BillToAccountID
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaOrderManagement.dbo.Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
JOIN		(SELECT MIN(Id) Id, Name, CAccountCodeClass, postal_code PostalCode FROM QSPCanadaCommon..CAccount acc JOIN QSPCanadaCommon..[Address] ad ON ad.AddressListID = acc.AddressListID AND ad.address_type = 54001 GROUP BY acc.Name, acc.CAccountCodeClass, ad.postal_code) AS accSchool ON accSchool.Name = cust.FirstName AND accSchool.PostalCode = cust.Zip AND ISNULL(accSchool.CAccountCodeClass, '') <> 'FM'
WHERE		accFM.CAccountCodeClass = 'FM'
AND			b.OrderID = @OrderID

RETURN (@SchoolAccountID)

END
GO
