USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_UnpickedOrders]    Script Date: 06/07/2017 09:20:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE [dbo].[pr_Get_UnpickedOrders]

@DistributionCenterId int = null

AS
set nocount on

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 01', getdate())

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempUnpickedOrders]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempUnpickedOrders]

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 02', getdate())

CREATE TABLE [dbo].[#TempUnpickedOrders] (
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

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 03', getdate())

INSERT INTO
	#TempUnpickedOrders
SELECT
	 A.OrderId
	, Convert(varchar, A.Date,101)  As 'DateOrdered'
	, CampaignId
	, B.Description As 'OrderType'
	, A.EnterredAmount As 'EnteredAmount'
	, 0
	, 0
	, 1
	, C.Description As 'OrderQualifier'
	, 1
FROM
	QSPCanadaOrderManagement..Batch A
	INNER JOIN QSPCanadaOrderManagement..CodeDetail B ON A.OrderTypeCode = B.Instance
	INNER JOIN QSPCanadaOrderManagement..CodeDetail C ON A.OrderQualifierId = C.Instance
WHERE
	A.StatusInstance = 40004 
	AND A.PickDate Is NULL
	--AND A.OrderQualifierID <> 39008 --Customer Service -- JLC - 2004/12/08 Temp filter

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 04', getdate())

---- Loop Through And Calculate Total Items and Determine whether the batch is entirely mag or not
DECLARE @OrderId int, @TotalQuantity int, @TotalCost money, @OnHandReturnValue int

DECLARE C1 CURSOR FOR
	SELECT
		OrderId
	FROM
		#TempUnpickedOrders

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 05', getdate())

OPEN C1

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 06', getdate())

	FETCH NEXT FROM C1 INTO
		@OrderId

WHILE @@Fetch_Status = 0
	BEGIN

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 07', getdate())
			
		SELECT @TotalQuantity = 0, @TotalCost = 0

		--- Get totals for NON mag items, if no items then update ispickable flag to 0
		SELECT
			@TotalQuantity = Sum(C.Quantity),
			@TotalCost = Sum(Price)    -- Removed Quantity Multiplication per Karen T.
		FROM
			Batch A
			INNER JOIN CustomerOrderHeader B ON A.[Date] = B.OrderBatchDate AND A.[Id] = B.OrderBatchId
			INNER JOIN CustomerOrderDetail C ON B.Instance = C.CustomerOrderHeaderInstance
		WHERE
			OrderId = @OrderId
			AND C.ProductType <> 46001
			AND C.PricingDetailsID <> 0
		GROUP BY
			A.OrderId

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 08', getdate())

		IF @TotalQuantity > 0 
			BEGIN
				UPDATE #TempUnpickedOrders SET ItemQuantity = @TotalQuantity, ItemTotalCost = @TotalCost WHERE OrderId = @OrderId
			END
		ELSE
			BEGIN
				UPDATE #TempUnpickedOrders SET IsPickable = 0 WHERE OrderId = @OrderId
			END
		
--		PRINT Convert(varchar,@TotalQuantity)
			
--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 09', getdate())

		if @DistributionCenterId is not null
			BEGIN
				--- Check OnHand Quantity
				exec pr_FSCheckOnhandQty @OrderId, @DistributionCenterId, @OnHandReturnValue output

				UPDATE #TempUnpickedOrders SET OnHandOK = @OnHandReturnValue WHERE OrderId = @OrderId
			END

--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 10', getdate())

		FETCH NEXT FROM C1 INTO
			@OrderId

	END

CLOSE C1
DEALLOCATE C1



--insert into jfp_trace (c1, c2) values ('pr_Get_UnpickedOrders 11', getdate())



SELECT * FROM #TempUnpickedOrders WHERE IsPickable = 1
GO
