USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Update_CreditCardPayment_ForQSPCAOrders]    Script Date: 06/07/2017 09:17:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE Procedure [dbo].[Update_CreditCardPayment_ForQSPCAOrders]
AS
BEGIN

DECLARE @CurrentYear int
DECLARE @CurrentMonth int 
DECLARE @FromDate    DateTime

CREATE TABLE #InterOrderItem (BatchId Int,
				BatchDate DateTime,
				OrderId Int,
				CampaignId Int,
				AccountId Int,
				COHInstance Int,
				CustomerName Varchar(110),
				CPHInstance Int,
				Price  	Numeric(10,2),
				CPHTotal  Numeric(14,2),
				AuthorizationCode Varchar(15)
				)

CREATE TABLE ##InterOrderByCph (OrderId Int,
				COHInstance Int,
				CustomerName Varchar(110),
				CPHInstance Int,
				TotalPrice  Numeric(14,2),
				Authorizationcode Varchar(15)
				)

SELECT @CurrentYear=Year(GetDate()),@CurrentMonth=Month(GetDate())

IF  @CurrentMonth in (1,2,3)
BEGIN
	SET @FromDate = '04/01/'+ Convert(Varchar(4),@CurrentYear-1)

END
ELSE IF @CurrentMonth >=4
BEGIN
	SET @FromDate= '01/01/'+ Convert(Varchar(4),@CurrentYear)
END
SELECT @FromDate

--Insert All OrderItem from internet orders paid by CC (approved payments)and not invoiced yet
INSERT INTO #InterOrderItem
SELECT  Id,date,orderid,b.CampaignId,b.Accountid,
	h.instance ,
	c.firstname+' '+c.lastname ,
	cph.instance ,
	d.price,
	totalamount,
	Case IsNull(authorizationcode,'')
	When '' Then VerisignId
	Else authorizationcode
	End
FROM 	 QSPCanadaOrderManagement.dbo.batch b, 
	 QSPCanadaOrderManagement.dbo.customerorderheader h,
	 QSPCanadaOrderManagement.dbo.customerorderdetail d, 
	 QSPCanadaOrderManagement.dbo.Customer c, 
     	 QSPCanadaOrderManagement.dbo.customerpaymentheader cph, 
	 QSPCanadaOrderManagement.dbo.creditcardPayment ccp
WHERE b.id=orderbatchid
AND date=orderbatchdate
AND h.instance=cph.customerorderheaderinstance
AND h.instance=d.customerorderheaderinstance
AND cph.iscreditcard=1
AND ccp.customerpaymentheaderInstance= cph.instance
AND b.orderqualifierid=39009
and b.OrderTypeCode <> 41012 -- 12/22/09 Free Order type introduced for qsp.ca spree KT
AND b.statusinstance=40013
AND h.customerbilltoinstance *=c.instance
AND IsNull(isinvoiced,0) =0
AND ccp.statusInstance <> 19000
AND IsNull(VerisignId,'') <> ''
AND Cast(Convert(varchar(10),b.date,101) as datetime) >= @FromDate
ORDER BY 3,4,7 -- desc


--Total By order, CPH
INSERT INTO ##InterOrderByCph
SELECT Orderid, COHInstance, customerName,cphinstance, Sum(price) , Authorizationcode
FROM 	#InterOrderItem 
GROUP BY Orderid, COHInstance, cphinstance, Authorizationcode,customerName
ORDER BY 1,2,4,6 -- desc

SELECT * FROM ##InterOrderByCph

DECLARE 	@coh int
DECLARE	@cph int
DECLARE	@TotalAmount Numeric(10,2)
	
DECLARE AllOrders CURSOR FOR
	SELECT COHInstance,cphInstance,TotalPrice
	FROM ##InterOrderByCph 
	
	OPEN AllOrders
	FETCH NEXT FROM AllOrders INTO @coh,@cph, @TotalAmount
		
	WHILE @@Fetch_Status = 0
	BEGIN

	Select @coh,@cph, @TotalAmount

	UPDATE QSPCanadaOrderManagement.dbo.CustomerPaymentHeader
	SET TotalAmount=@TotalAmount,statusInstance=600, DateChanged =GetDate(),UserIdChanged='Proc'
	WHERE customerorderheaderinstance=@coh
	AND Instance=@cph
	
	--All internet orders have good payments
	UPDATE QSPCanadaOrderManagement.dbo.CreditCardPayment
	SET CreditCardNumber='4000000000000000',statusInstance=19000, DateChanged =GetDate(),UserIdChanged='Proc'
	WHERE CustomerPaymentHeaderInstance=@cph
	AND CreditCardNumber = '0000000000000000'

	FETCH NEXT FROM AllOrders INTO @coh,@cph, @TotalAmount
	END
	CLOSE AllOrders
	DEALLOCATE AllOrders


	--Create Log and Email
	DECLARE @Cnt Int
	DECLARE @Filename Varchar(50)
	DECLARE  @SQLcommand  Varchar(500)
	DECLARE  @path  Varchar(100)
	DECLARE  @RunDate DateTime
	DECLARE  @FileAttachment Varchar(100)
	DECLARE  @Body Varchar(500)
	DECLARE  @SendEmailTo varchar(100)
	
	SELECT @RunDate =  GetDate()

	SET @SendEmailTo =  'jmiles@gafundraising.com'

	SET @path = 'Q:\Projects\Paylater\QSPCAFinance\QSPCAOrderUpdateLogs\' 

	SET @Filename =  'OnlineCCOrderUpdateLog_' + 
			Cast(Datepart(YEAR,	@RunDate) 	AS Varchar) +
			Cast(Datepart(MONTH,	@RunDate) 	AS Varchar) +
			Cast(Datepart(DAY,	@RunDate) 	AS Varchar) + 
			Cast(Datepart(HOUR,	@RunDate) 	AS Varchar) + 
			Cast(Datepart(MINUTE,	@RunDate)	AS Varchar) + 
			Cast(Datepart(SECOND,	@RunDate) 	AS Varchar)+'.txt'

	SELECT @Cnt = Count(*)   From ##InterOrderByCph

	--If there are orders with error create a log file and email
	IF @Cnt > 0 
	BEGIN
			
		SET @SQLcommand = 'bcp "tempdb.##InterOrderByCph" out "Q:\Projects\Paylater\QSPCAFinance\QSPCAOrderUpdateLogs\' + @Filename + '" -c -q -T '
		EXEC master..xp_cmdshell @SQLcommand
			
		SET @Body= 	'List of Online Orders (CreditCard), Updated For invoice  '+
				Convert(Varchar(30),@RunDate,113)+Char(13)+Char(13)+
				'Please review attached file indicating OrderId,COH,Customer,CPH,Price andAuthorization respectively '+Char(13)+Char(13)	
				
		SET @FileAttachment = @path+@Filename
				
		EXEC  QSPCanadaCommon.dbo.Send_EMAIL_ATTACH 'OnlineCCOrderUpdate@QSP.com', @SendEmailTo,'Online CC Order Updated',@Body,@FileAttachment
		
		--Process All CC payments
		EXEC QSPCanadaOrderManagement.dbo.spProcessAllCCPayments

	END

DROP TABLE ##InterOrderByCph
DROP TABLE #InterOrderItem

END
GO
