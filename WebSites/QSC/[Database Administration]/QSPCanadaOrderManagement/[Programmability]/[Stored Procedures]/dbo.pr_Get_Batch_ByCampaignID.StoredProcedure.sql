USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Batch_ByCampaignID]    Script Date: 06/07/2017 09:19:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_Batch_ByCampaignID]
  @CampaignID int,
  @FMID varchar(4) = '9999'
AS

IF @FMID = '9999'
BEGIN
	SELECT
		  A.OrderID
		, A.CampaignID
		--, A.*
		--, B.Description 'BatchStatus'
		, C.[Name] As 'AccountName'
		--, D.Description 'OriginalBatchStatus'
		, E.Description 'OrderTypeCodeDesc'
		--, F.Description 'IncentiveCalculationStatusDesc'
		, G.Description 'OrderQualifierDesc'
		, UPPER(C.Lang) As 'Language'
		, A.[ID]        As 'BatchID'
		, Convert(varchar(10),A.[Date],101) AS 'BatchDate'
		, rrb.ShipmentGroupID
		, sg.ShipmentGroupName
	FROM
		Batch A
		--LEFT OUTER JOIN                   CodeDetail B ON B.Instance  = A.StatusInstance
		LEFT OUTER JOIN QSPCanadaCommon.dbo.CAccount C ON A.AccountId = C.Id
		--LEFT OUTER JOIN                   CodeDetail D ON D.Instance  = A.OriginalStatusInstance
		LEFT OUTER JOIN                   CodeDetail E ON E.Instance  = A.OrderTypeCode
		--LEFT OUTER JOIN                   CodeDetail F ON F.Instance  = A.IncentiveCalculationStatus
		LEFT OUTER JOIN                   CodeDetail G ON G.Instance  = A.OrderQualifierId
		JOIN			ReportRequestBatch rrb on rrb.BatchOrderId = A.OrderID
		LEFT OUTER JOIN	ShipmentGroup sg ON sg.ShipmentGroupID = rrb.ShipmentGroupID
	WHERE
		CampaignID = @CampaignID
		and A.StatusInstance NOT IN (40005)
	--AND	A.OrderQualifierID <> 39009		-- added by Ben - 11/02/2005
END
ELSE
BEGIN
	SELECT
		  A.OrderID
		, A.CampaignID
		, C.[Name]      As 'AccountName'
		, E.Description As 'OrderTypeCodeDesc'
		, G.Description As 'OrderQualifierDesc'
		, UPPER(C.Lang) As 'Language'
		, A.[ID]        As 'BatchID'
		, Convert(varchar(10),A.[Date],101) AS 'BatchDate'
		, rrb.ShipmentGroupID
		, sg.ShipmentGroupName
	FROM	                QSPCanadaOrderManagement.dbo.Batch      A
		LEFT OUTER JOIN          QSPCanadaCommon.dbo.CAccount   C ON A.AccountId   = C.Id
		LEFT OUTER JOIN QSPCanadaOrderManagement.dbo.CodeDetail E ON E.Instance    = A.OrderTypeCode
		LEFT OUTER JOIN QSPCanadaOrderManagement.dbo.CodeDetail G ON G.Instance    = A.OrderQualifierId
		     INNER JOIN          QSPCanadaCommon.dbo.Campaign  CP ON (A.CampaignID = CP.[ID] AND CP.FMID = @FMID)
		JOIN			ReportRequestBatch rrb on rrb.BatchOrderId = A.OrderID
		LEFT OUTER JOIN	ShipmentGroup sg ON sg.ShipmentGroupID = rrb.ShipmentGroupID
	WHERE
		CampaignID = @CampaignID
		and A.StatusInstance NOT IN (40005)
	--AND	A.OrderQualifierID <> 39009		-- added by Ben - 11/02/2005
END
GO
