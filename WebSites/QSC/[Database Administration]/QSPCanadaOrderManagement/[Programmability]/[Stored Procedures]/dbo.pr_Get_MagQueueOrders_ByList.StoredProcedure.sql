USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_MagQueueOrders_ByList]    Script Date: 06/07/2017 09:19:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_MagQueueOrders_ByList]

@OrderIdList varchar(500)

AS

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempMagQueueOrders]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempMagQueueOrders]

CREATE TABLE [dbo].[#TempMagQueueOrders] (
	[OrderId] [int] NOT NULL ,
	[DateOrdered] [varchar] (10) NULL ,
	[CampaignId] [int] NOT NULL ,
	[OrderType] [varchar] (50) NULL ,
	[EnteredAmount] [money] NULL,
	[ItemQuantity] [int] NULL,
	[ItemTotalCost] [money] NULL,
	[IsPickable] [bit] NULL,
	[OrderQualifier] [varchar] (50),
	[OnHandOK] [bit] NULL
) ON [PRIMARY]


EXEC ('
INSERT INTO
	#TempMagQueueOrders
SELECT
	A.OrderId
	, Convert(varchar, A.Date,101)  As DateOrdered
	, CampaignId
	, B.Description As OrderType
	, A.EnterredAmount As EnteredAmount
	, 0
	, 0
	, 1
	, C.Description As OrderQualifier
	, 1
FROM
	QSPCanadaOrderManagement..Batch A
	INNER JOIN QSPCanadaOrderManagement..CodeDetail B ON A.OrderTypeCode = B.Instance
	INNER JOIN QSPCanadaOrderManagement..CodeDetail C ON A.OrderQualifierId = C.Instance
WHERE
	A.StatusInstance = 40013 AND
	A.PickDate Is NULL 
	AND A.OrderId IN (' + @OrderIdList +')')

---- Loop Through And Calculate Total Items and Determine whether the batch is entirely mag or not
DECLARE @OrderId int, @TotalQuantity int, @TotalCost money, @OnHandReturnValue int

DECLARE C1 CURSOR FOR
	SELECT
		OrderId
	FROM
		#TempMagQueueOrders

OPEN C1
	FETCH NEXT FROM C1 INTO
		@OrderId

WHILE @@Fetch_Status = 0
	BEGIN
			
		SELECT @TotalQuantity = 0, @TotalCost = 0

		--- Get totals for NON mag items, if no items then update ispickable flag to 0
		SELECT
			@TotalQuantity = Count(C.Quantity),
			@TotalCost = Sum(Price)		--- Removed Quantity Multiplication per Karen T.
		FROM
			Batch A
			INNER JOIN CustomerOrderHeader B ON A.[Date] = B.OrderBatchDate AND A.[Id] = B.OrderBatchId
			INNER JOIN CustomerOrderDetail C ON B.Instance = C.CustomerOrderHeaderInstance
		WHERE
			OrderId = @OrderId
			AND C.ProductType = 46001
		GROUP BY
			A.OrderId

			
		UPDATE #TempMagQueueOrders SET ItemQuantity = @TotalQuantity, ItemTotalCost = @TotalCost WHERE OrderId = @OrderId
		
		
		PRINT Convert(varchar,@TotalQuantity)
			
		

		FETCH NEXT FROM C1 INTO
			@OrderId

	END

CLOSE C1
DEALLOCATE C1






SELECT * FROM #TempMagQueueOrders WHERE IsPickable = 1
GO
