USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_BatchDetails_ByOrderId]    Script Date: 07/26/2011 09:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER   PROCEDURE [dbo].[pr_Get_BatchDetails_ByOrderId]

@BatchOrderId int

AS

/**************************************************************************************************************************************
-- 	8/2/2011 CRL
--	Added processing fees to output
**************************************************************************************************************************************/

DECLARE @BatchProcessingFees numeric(10,2)

SELECT @BatchProcessingFees = SUM(processingFees.Price)
FROM Batch AS batch
	LEFT OUTER JOIN CustomerOrderHeader COH ON batch.ID = COH.OrderBatchID  AND batch.Date = COH.OrderBatchDate
	LEFT OUTER JOIN CustomerOrderDetail processingFees on COH.Instance = processingFees.CustomerOrderHeaderInstance AND processingFees.ProductCode = 'PFEE'
WHERE 
	Batch.OrderId = @BatchOrderId

SELECT
	  A.*
	, B.Description 'BatchStatus'
	, C.[Name] As 'AccountName'
	, D.Description 'OriginalBatchStatus'
	, E.Description 'OrderTypeCodeDesc'
	, F.Description 'IncentiveCalculationStatusDesc'
	, G.Description 'OrderQualifierDesc'
	, UPPER(C.Lang) As 'Language'
	, @BatchProcessingFees as 'ProcessingFees'
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


