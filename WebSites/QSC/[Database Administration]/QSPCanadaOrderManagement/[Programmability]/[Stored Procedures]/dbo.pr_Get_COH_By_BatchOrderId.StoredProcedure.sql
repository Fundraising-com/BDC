USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_COH_By_BatchOrderId]    Script Date: 06/07/2017 09:19:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_COH_By_BatchOrderId]

@BatchOrderId int	

 AS


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempCOH]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempCOH]

CREATE TABLE [dbo].[#TempCOH] (
	[COHInstance] [int] NOT NULL ,
	[ItemQuantity] [int] NULL,
	[ItemTotalCost] [money] NULL
) ON [PRIMARY]


INSERT INTO
	#TempCOH
SELECT
	A.Instance
	, Sum(B.Quantity)
	, Sum(B.Price) --- Removed Quantity Multiplication per Karen T.
FROM
	CustomerOrderHeader A
	INNER JOIN CustomerOrderDetail B ON B.CustomerOrderHeaderInstance = A.Instance
	INNER JOIN Batch C ON A.OrderBatchId = C.Id AND Convert(varchar, A.OrderBatchDate, 101)  = Convert(varchar, C.[Date], 101)
WHERE
	C.OrderId = @BatchOrderId
GROUP BY
	A.Instance


SELECT
	A.*, C.ItemQuantity, C.ItemTotalCost, D.Name As 'AccountName'
FROM
	CustomerOrderHeader A
	INNER JOIN Batch B ON A.OrderBatchId = B.Id AND Convert(varchar, A.OrderBatchDate, 101)  = Convert(varchar, B.[Date], 101)
	LEFT OUTER JOIN #TempCOH C ON C.COHInstance = A.Instance
	LEFT OUTER JOIN Account D ON D.Id = A.AccountId
WHERE
	B.OrderId = @BatchOrderId
GO
