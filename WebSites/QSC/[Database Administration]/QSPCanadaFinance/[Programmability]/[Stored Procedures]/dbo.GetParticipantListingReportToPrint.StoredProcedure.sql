USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetParticipantListingReportToPrint]    Script Date: 06/07/2017 09:17:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetParticipantListingReportToPrint]
	@FromDate 	datetime,
	@ToDate 	datetime
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 8/29/2004 
--   Get Participant Listing Reports ready to print For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT OrderID,
	C.ID as CampaignID,
	A.Name,
	MAX(C.FMID) AS FMID, 
	MAX(FM.LastName) as LastName,
	MAX(FM.FirstName) as FirstName,
	MAX(C.Lang) as Lang
FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory CODRH
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderDetail COD on COD.TransID = CODRH.TransID
			AND COD.CustomerOrderHeaderInstance = CODRH.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.Instance = COD.CustomerOrderHeaderInstance
	--INNER JOIN QSPCanadaOrderManagement..CustomerRemitHistory CRH on COD.CustomerOrderHeaderInstance = CODRH.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..Batch B on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	INNER JOIN QSPCanadaCommon..FieldManager FM on FM.FMID = C.FMID
	INNER JOIN QSPCanadaCommon..CAccount A on C.BillToAccountID = A.ID 
WHERE StartDate BETWEEN  @FromDate AND @ToDate
GROUP BY OrderID, A.Name, C.ID
ORDER BY A.Name


SET NOCOUNT OFF
GO
