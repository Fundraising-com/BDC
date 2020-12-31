USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Batch_ByOrderId]    Script Date: 06/07/2017 09:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[pr_Get_Batch_ByOrderId]  

@BatchOrderId int,
  @FMID varchar(4) = '9999'

AS



IF (@FMID<>'9999')

Begin
	
	SELECT
		  A.OrderID
		, A.CampaignID
		, A.*
		, B.Description 'BatchStatus'
		, C.[Name] As 'AccountName'
		, D.Description 'OriginalBatchStatus'
		, E.Description 'OrderTypeCodeDesc'
--		, F.Description 'IncentiveCalculationStatusDesc'
		, G.Description 'OrderQualifierDesc'
		, UPPER(C.Lang) As 'Language'
		, A.[ID]        As 'BatchID'
		, Convert(varchar(10),A.[Date],101) AS 'BatchDate'
		, A.[ID]        As 'ID'
		, Convert(varchar(10),A.[Date],101) AS 'Date'
		, rrb.ShipmentGroupID
		, sg.ShipmentGroupName
	FROM
		Batch A
		LEFT OUTER JOIN CodeDetail B ON B.Instance = A.StatusInstance
		LEFT OUTER JOIN QSPCanadaCommon..CAccount C ON A.AccountId = C.Id
		LEFT OUTER JOIN CodeDetail D ON D.Instance = A.OriginalStatusInstance
		LEFT OUTER JOIN CodeDetail E ON E.Instance = A.OrderTypeCode
		LEFT OUTER JOIN CodeDetail F ON F.Instance = A.IncentiveCalculationStatus
		LEFT OUTER JOIN CodeDetail G ON G.Instance = A.OrderQualifierId
		JOIN			QSPCanadaCommon..Campaign camp ON camp.ID = A.CampaignID
		JOIN			ReportRequestBatch rrb on rrb.BatchOrderId = A.OrderID
		LEFT OUTER JOIN	ShipmentGroup sg ON sg.ShipmentGroupID = rrb.ShipmentGroupID
	WHERE
		OrderId = @BatchOrderId		
		and camp.FMID=@FMID
		and A.StatusInstance NOT IN (40005)
		--and A.OrderQualifierID <> 39009		-- added by Ben - 11/02/2005

end
else
begin

	SELECT
		  A.OrderID
		, A.CampaignID
		, A.*
		, B.Description 'BatchStatus'
		, C.[Name] As 'AccountName'
		, D.Description 'OriginalBatchStatus'
		, E.Description 'OrderTypeCodeDesc'
		, F.Description 'IncentiveCalculationStatusDesc'
		, G.Description 'OrderQualifierDesc'
		, UPPER(C.Lang) As 'Language'
		, A.[ID]        As 'BatchID'
		, Convert(varchar(10),A.[Date],101) AS 'BatchDate'
		, A.[ID]        As 'ID'
		, Convert(varchar(10),A.[Date],101) AS 'Date'
		, rrb.ShipmentGroupID
		, sg.ShipmentGroupName
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
	WHERE
		OrderId = @BatchOrderId
		and A.StatusInstance NOT IN (40005)
		--and A.OrderQualifierID <> 39009		-- added by Ben - 11/02/2005


End
GO
