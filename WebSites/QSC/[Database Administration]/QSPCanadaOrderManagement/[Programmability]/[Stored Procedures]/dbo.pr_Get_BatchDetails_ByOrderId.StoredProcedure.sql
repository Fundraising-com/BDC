USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_BatchDetails_ByOrderId]    Script Date: 06/07/2017 09:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[pr_Get_BatchDetails_ByOrderId]

@BatchOrderId int

AS

SELECT
	  A.*
	, B.Description 'BatchStatus'
	, C.[Name] As 'AccountName'
	, D.Description 'OriginalBatchStatus'
	, E.Description 'OrderTypeCodeDesc'
	, F.Description 'IncentiveCalculationStatusDesc'
	, G.Description 'OrderQualifierDesc'
	, UPPER(C.Lang) As 'Language'
	--, A.[ID]        As 'BatchID'
	--, Convert(varchar(10),A.[Date],101) AS 'BatchDate'
	--, A.OrderID
	--, A.CampaignID
	--, A.[ID]        As 'ID'
	--, Convert(varchar(10),A.[Date],101) AS 'Date'
FROM
	Batch A
	LEFT OUTER JOIN CodeDetail B ON B.Instance = A.StatusInstance
	LEFT OUTER JOIN QSPCanadaCommon..CAccount C ON A.AccountId = C.Id
	LEFT OUTER JOIN CodeDetail D ON D.Instance = A.OriginalStatusInstance
	LEFT OUTER JOIN CodeDetail E ON E.Instance = A.OrderTypeCode
	LEFT OUTER JOIN CodeDetail F ON F.Instance = A.IncentiveCalculationStatus
	LEFT OUTER JOIN CodeDetail G ON G.Instance = A.OrderQualifierId
WHERE
	OrderId = @BatchOrderId
GO
