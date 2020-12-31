USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessReserveQuantities_ByOrderId_V2]    Script Date: 06/07/2017 09:20:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[pr_ProcessReserveQuantities_ByOrderId_V2]

@OrderId int

AS

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempRQ]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempRQ]

CREATE TABLE [dbo].[#TempRQ] (
	[OrderID] [int] not null,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransId] [int] NOT NULL,
	[PricingDetailsId] [int] NOT NULL ,
	[ProductCode] [varchar](50) NOT NULL,
	[ItemQuantity] [int] NULL,
	[DistributionCenterId] [int] NULL,
	[ProductLine] [int] NULL
) ON [PRIMARY]



DECLARE @BatchDate datetime, @BatchId int

Declare @IsCumulative int , @IsPrizeCalc int, @Students int, @Prizes int 
DECLARE  @Error int , @ErrorMsg varchar(1000), @HasError int , @EmailSubject varchar(300) 
 Set @IsCumulative = 0
 Set @IsPrizeCalc = 0
 Set @Students = 0
 Set @Prizes = 0
 Set @Error = 0 


---verify if prizes and some key data is fine

 Exec  QSPCanadaOrderManagement.dbo.pr_VerifyOrder  @OrderId, @ErrorMsg  output , @HasError output 
 Set @ErrorMsg = @ErrorMsg

 Select top 1 @Error = 1
 from QspCanadaCommon.dbo.SystemErrorLog   
 where Orderid = @OrderId
            and isFixed = 0 


 IF  @Error = 1
   begin
	 Set @EmailSubject = 'Order#'+str(@OrderId)+'- pr_ProcessReserveQuantities_ByOrderId_V2 -  Did Not Run'
              Select 'Error - Order# '+str(@OrderId)+ ' - pr_ProcessReserveQuantities_ByOrderId_V2 -  Did not run due to an un-fixed issue in SystemErrorLog table' 
   end 
ELSE

   Begin



SELECT
	@BatchDate = [Date]
	, @BatchId = [Id]
FROM
	QSPCanadaOrderManagement..Batch
WHERE
	OrderId = @OrderId



INSERT INTO
	#TempRQ
SELECT @OrderId,
	C.CustomerOrderHeaderInstance
	, C.TransId
	, C.PricingDetailsId
	, C.ProductCode
	, Quantity
	, NULL
	, E.ProductLine
FROM
	QSPCanadaOrderManagement..Batch A
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader B ON B.OrderBatchDate = A.[Date] AND B.OrderBatchId = A.[Id]
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderDetail C ON C.CustomerOrderHeaderInstance = B.Instance
	INNER JOIN QSPCanadaProduct..Pricing_Details D ON D.MagPrice_Instance = C.PricingDetailsId
	INNER JOIN QSPCanadaProduct..Product E ON E.Product_Instance = D.Product_Instance-- AND E.Product_Season = D.Pricing_Season AND E.Product_Code = D.Product_Code
	INNER JOIN QSPCanadaOrderManagement..Customer F on B.CustomerBillToInstance=F.Instance
WHERE
	A.OrderId = @OrderId
	AND C.ProductType not in ( 46001, 46017, 46012, 46021)
	AND C.PricingDetailsID <> 0
	AND F.StatusInstance=300
	and C.StatusInstance not in (508,509)
	AND C.DelFlag = 0

--- UPDATE THE DistributionCenterId IF THE ProductLine has an absolute DistributionCenterId
UPDATE
	#TempRQ
SET
	DistributionCenterId = B.DistributionCenterId
FROM
	#TempRQ A
	INNER JOIN QSPCanadaCommon..QSPProductLine B ON B.Id = (A.ProductLine + 46000)
WHERE
	B.DistributionCenterId is not null
--AND (A.ProductLine <> 24 OR dbo.UDF_Entertainment_IsShippedToAccount(A.OrderID) = 1)

--INSERT INTO
	--QSPCanadaOrderManagement..BatchDistributionCenter
/*
SELECT
	DistributionCenterId,
	ProductCode
FROM
	#TempRQ
GROUP BY
	ProductCode
	, DistributionCenterId

*/
--SELECT * FROM #TempRQ

---- LOOP THROUGH AND Reserve Qty For Each Product Code/DistributionCenter

DECLARE @DistributionCenterIdTemp int, @ProductCode varchar(50)
Declare @retVal int
Declare @COH int
Declare @Transid int

DECLARE C1 CURSOR FOR
	SELECT
		CustomerOrderHeaderInstance,
		Transid,
		DistributionCenterId,
		ProductCode
	FROM
		#TempRQ
		order by ProductCode
	

OPEN C1
	FETCH NEXT FROM C1 INTO	
		@COH, @Transid, @DistributionCenterIdTemp,@ProductCode

WHILE @@Fetch_Status = 0
	BEGIN
		
		--PRINT 'ProductCode: ' + @ProductCode + ' DC: ' + Convert(varchar, @DistributionCenterIdTemp)
		
		if( @DistributionCenterIdTemp = 1)
		begin
			exec pr_FSCheckProductOnhandQty_IndividualItem @OrderID , @COH, @Transid, @DistributionCenterIdTemp,@ProductCode, @retVal  output
--	print @retVal
	
			if(@retVal = 1 )
			begin
				exec pr_FSReserveProductQtyIndividualItem @COH, @Transid, @DistributionCenterIdTemp, @ProductCode
	
	
				UPDATE
					QSPCanadaOrderManagement..CustomerOrderDetail
				SET
					StatusInstance = 509  -- Pending to TPL
					, DistributionCenterId = B.DistributionCenterId
				FROM
					QSPCanadaOrderManagement..CustomerOrderDetail A
					INNER JOIN #TEMPRQ B ON A.CustomerOrderHeaderInstance 
						= B.CustomerOrderHeaderInstance AND A.TransId = B.TransId
					where B.DistributionCenterID=2
						and A.CustomerOrderHeaderInstance = @COH and A.Transid=@Transid
	--					And A.StatusInstance=502
	
				
				UPDATE
					QSPCanadaOrderManagement..CustomerOrderDetail
				SET
					StatusInstance = 511 -- Picked 
					, DistributionCenterId = B.DistributionCenterId
				FROM
					QSPCanadaOrderManagement..CustomerOrderDetail A
					INNER JOIN #TEMPRQ B ON A.CustomerOrderHeaderInstance = B.CustomerOrderHeaderInstance AND A.TransId = B.TransId
					where B.DistributionCenterID=1
						and A.CustomerOrderHeaderInstance = @COH and A.Transid=@Transid
	--					And A.StatusInstance=502
				
			end
			else
			begin
				-- Left in pickable state
				
				UPDATE
					QSPCanadaOrderManagement..CustomerOrderDetail
				SET
					 DistributionCenterId = B.DistributionCenterId
				FROM
					QSPCanadaOrderManagement..CustomerOrderDetail A
					INNER JOIN #TEMPRQ B ON A.CustomerOrderHeaderInstance = B.CustomerOrderHeaderInstance AND A.TransId = B.TransId
					where
						 A.CustomerOrderHeaderInstance = @COH and A.Transid=@Transid
	
	
			end
		END
		ELSE
		BEGIN

			UPDATE
					QSPCanadaOrderManagement..CustomerOrderDetail
				SET
					StatusInstance = 509  -- Pending to TPL
					, DistributionCenterId = B.DistributionCenterId
				FROM
					QSPCanadaOrderManagement..CustomerOrderDetail A
					INNER JOIN #TEMPRQ B ON A.CustomerOrderHeaderInstance 
						= B.CustomerOrderHeaderInstance AND A.TransId = B.TransId
					where B.DistributionCenterID IS NOT NULL
						and A.CustomerOrderHeaderInstance = @COH and A.Transid=@Transid
						And A.StatusInstance <> 508
	--					And A.StatusInstance=502
	
		END
	
		FETCH NEXT FROM C1 INTO	
				@COH, @Transid, @DistributionCenterIdTemp,@ProductCode
		

	
	END

CLOSE C1
DEALLOCATE C1

END




---- UPDATE THE OVERALL BATCH STATUS HERE

 IF  @HasError = 1 

   Begin
        Exec QSPCanadaCommon.dbo.Send_EMail  'pr_ProcessReserveQuantities_ByOrderId_V2@qsp.com','qsp-qspfulfillment-dev@qsp.com', @EmailSubject, @ErrorMsg,'qsp-qspfulfillment-dev@qsp.com' 
       Select 'Order not picked due to problem in prizes, Order# '  +str(@orderid)
   End 

 Else

 Begin

 UPDATE 
	QSPCanadaOrderManagement..Batch
 SET
	StatusInstance = 40010
 WHERE
	OrderId = @OrderId

 End
GO
