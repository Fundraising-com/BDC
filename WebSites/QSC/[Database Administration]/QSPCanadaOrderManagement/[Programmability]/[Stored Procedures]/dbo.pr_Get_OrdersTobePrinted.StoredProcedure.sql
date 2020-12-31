USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_OrdersTobePrinted]    Script Date: 06/07/2017 09:19:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_OrdersTobePrinted]

	@pOrderID int,
	@pCampaignID int,
	@pAccountID int,
	@pOrderQualifier int,
	@pFromDateReceived datetime,
	@pToDateReceived datetime,
	@pShipmentGroupID int,
	@pHasShipment bit

AS

SET NoCount ON

IF @pOrderID = 0  
BEGIN
	SET @pOrderID = NULL
END

IF @pCampaignID = 0  
BEGIN
	SET @pCampaignID = NULL
END

IF @pAccountID = 0  
BEGIN
	SET @pAccountID = NULL
END

IF @pOrderQualifier = 0  
BEGIN
	SET @pOrderQualifier = NULL
END

IF (@pShipmentGroupID = 0)
BEGIN
	SET @pShipmentGroupID = NULL
END

	SELECT top 500
		  A.OrderID
		, A.CampaignID
		, C.[Name] As 'AccountName'
		, E.Description 'OrderType'
		, G.Description 'OrderQualifier'
		, UPPER(C.Lang) As 'Lang'
		, QspcanadaOrderManagement.dbo.UDF_ProgramsbyCampaign ( a.campaignid) as Programs
		, CONVERT(char(12), ISNULL(A.OrderDeliveryDate, A.date), 101) AS 'OrderDeliveryDate'
		, CONVERT(char(12), DATEADD(dd, -(prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep), ISNULL(A.OrderDeliveryDate, A.date)), 101) AS 'OrderShippingDate'
		--, CONVERT(char(12), ISNULL((SELECT MIN(ost.ReceiptDate) FROM OrderStageTracking ost WHERE (ost.OrderID = A.OrderID OR ost.ToteID > 0 AND ost.ToteID IN (SELECT ToteID FROM CustomerOrderHeader coh WHERE coh.OrderBatchDate = A.Date and coh.OrderBatchID = A.ID)) AND ost.Stage = 59000), A.date), 101) AS 'OrderReceivedDate'
		, CONVERT(char(12), A.Date, 101) AS 'OrderReceivedDate'
		, sg.ShipmentGroupID
		, sg.ShipmentGroupName
		, CASE rrbp.ID WHEN NULL THEN 'No' ELSE 'Yes' END AS HasShipment
	FROM
		Batch A
		LEFT OUTER JOIN CodeDetail B ON B.Instance = A.StatusInstance
		LEFT OUTER JOIN QSPCanadaCommon..CAccount C ON A.AccountId = C.Id
		LEFT OUTER JOIN CodeDetail D ON D.Instance = A.OriginalStatusInstance
		LEFT OUTER JOIN CodeDetail E ON E.Instance = A.OrderTypeCode
		LEFT OUTER JOIN CodeDetail F ON F.Instance = A.IncentiveCalculationStatus
		LEFT OUTER JOIN CodeDetail G ON G.Instance = A.OrderQualifierId
		JOIN			ReportRequestBatch rrb on rrb.BatchOrderId = A.OrderID
		LEFT OUTER JOIN	ShipmentGroup sg ON sg.ShipmentGroupID = rrb.ShipmentGroupID
		LEFT OUTER JOIN	QSPCanadaCommon..Address ad ON ad.AddressListID = C.AddressListID AND ad.address_type = 54001
		LEFT OUTER JOIN	QSPCanadaCommon..Province prov ON prov.Province_Code = ad.stateProvince
		LEFT OUTER JOIN	ReportRequestBatch_PrintPickList rrbp ON rrbp.ReportRequestBatchID = rrb.ID AND rrbp.pReportType = 1
    WHERE	rrb.IsPrinted = 0
	AND		rrb.IsQSPPrint = 1
	AND		dbo.UDF_PDFGenerationStatus(BatchOrderID) = 1
				/*A.OrderID IN 
          		( Select BatchOrderId from QspCanadaOrderManagement.dbo.ReportRequestBatch 
           		  Where IsPrinted = 0 and IsQSPPrint = 1
				  and dbo.UDF_PDFGenerationStatus(BatchOrderID) = 1)
			      --and createdate <= dateadd(mi,-30,getdate()) and A.OrderQualifierID <> 39008)*/
    AND		A.Orderid = IsNull(@pOrderID, A.OrderID)
    AND		A.StatusInstance <> 40005
    AND		A.CampaignID = IsNull(@pCampaignID, A.CampaignID)
    AND		A.AccountID = IsNull(@pAccountID, A.AccountID)
    AND		A.OrderQualifierID = IsNull(@pOrderQualifier, A.OrderQualifierID)
	AND		A.DateReceived >= IsNull(@pFromDateReceived, A.DateReceived)    
	AND		A.DateReceived <= IsNull(DATEADD(dd, 1, @pToDateReceived), A.DateReceived)
    AND		(@pShipmentGroupID IS NULL OR sg.ShipmentGroupID = @pShipmentGroupID)
	AND		(@pHasShipment IS NULL OR @pHasShipment = 1 AND rrbp.ID IS NOT NULL OR @pHasShipment = 0 AND rrbp.ID IS NULL)
    ORDER BY DATEADD(dd, -(prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep), ISNULL(A.OrderDeliveryDate, A.date)), A.OrderID --A.OrderDeliveryDate, A.OrderID
GO
