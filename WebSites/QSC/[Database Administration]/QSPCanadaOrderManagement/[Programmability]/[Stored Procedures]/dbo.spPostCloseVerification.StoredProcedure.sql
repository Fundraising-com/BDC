USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spPostCloseVerification]    Script Date: 06/07/2017 09:20:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[spPostCloseVerification] @OrderId  Int, @RetVal Int Output
 As
Set Nocount On
Declare @DetailCount 		Int,
	@RemittedCount	Int,
	@DetailItemCnt		Int,
	@MaxId		Int,
	@Cnt			Int,
	@Dcoh			Int,
	@DTrans		Int,
	@RemitMaxId		Int,
	@productCode		Varchar(20),
	@ProductName		Varchar(50),
	@Dummy		Int


	Declare
	@OrderDetailItem TABLE (Id				Int	Identity,
				OrderId				Int	,
				CustomerOrderHeaderInstance	Int,	
				TransId				Int,	
				ProductCode			Varchar(20),
				ProductName			Varchar(50)
				)


	Declare
	@RemitItem TABLE (
			Id				Int	Identity,
			OrderId				Int,
			DetailCoh			Int,
			DetailTransId			Int,	
			DetailProductCode		Varchar(20),
			DetailProductName 		Varchar(50),
			DetailQuantity			Int, 
	                      	CCPAuthorizationCode		Varchar(50), 
			CreditCardNumber		Varchar(20), 
			CCPayStatus			Int,
			PaymentMethod			Int,
			RemitCoh			Int,
			RemitTransId			Int,
			RemitTitleCode			Varchar(20),
			RemitMagazineTitle		Varchar(55),
			RemitNumberofIssues		Int,
			RemitBatchID			Int, 
			RemitStatus			Int, 
			TotalCCAmount			Numeric(10,2), 
			CustomerLastName		Varchar(50), 
			CustomerFirstName		Varchar(50), 
			CustomerAddress1		Varchar(50), 
			CustomerCity			Varchar(50), 
			CustomerState			Varchar(30), 
			CustomerZip			Varchar(10)
			
			)

		Declare
		@AllException TABLE (
					Id				Int	Identity,
					CustomerOrderHeaderInstance	Int,
					TransId				Int,
					ProductCode			Varchar(20),
					ProductName			Varchar(50),
					Description			Varchar(250)
				         )	


	Insert Into @OrderDetailItem
	Select    b.orderid,d.customerorderheaderinstance,d.transid,d.productcode,d.productName 
	From 	QSPCanadaOrderManagement..CustomerOrderDetail d ,
		QSPCanadaOrderManagement..CustomerOrderHeader h,
		QSPCanadaOrderManagement..Batch b
	Where  	d.CustomerOrderHeaderInstance=h.Instance
	And h.OrderBatchDate=b.Date
	And h.OrderBatchID=b.id
	And b.OrderID = @orderid
	And producttype In ( 46001)
	And d.StatusInstance In  (507)
	And d.delflag <> 1

	Select @DetailCount = Count(*)  From @OrderDetailItem


	Insert Into @RemitItem
	Select   b.OrderId ,
		d.CustomerOrderHeaderInstance, 
		d.TransID, 
		d.ProductCode, 
		d.ProductName ,
		d.Quantity, 
                      	ccp.AuthorizationCode, 
		ccp.CreditCardNumber, 
		ccp.StatusInstance,
		h.PaymentMethodInstance, 
		codRh.CustomerOrderHeaderInstance,
		codRh.TransId,
		TitleCode,
		MagazineTitle,
		NumberofIssues,
		codRH.RemitBatchID, 
		codRH.Status, 
		cph.TotalAmount, 
		c.LastName, 
		c.FirstName, 
		c.Address1, 
		c.City, 
		c.State, 
		c.Zip
	From     QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory codRH RIGHT OUTER JOIN
                     	QSPCanadaOrderManagement.dbo.Customer c RIGHT OUTER JOIN
                    	QSPCanadaOrderManagement.dbo.Batch b INNER JOIN
                      	QSPCanadaOrderManagement.dbo.CustomerOrderHeader h ON b.ID = h.OrderBatchID AND b.[Date] = h.OrderBatchDate INNER JOIN
                      	QSPCanadaOrderManagement.dbo.CustomerOrderDetail d ON h.Instance = d.CustomerOrderHeaderInstance ON 
                      	c.Instance = h.CustomerBillToInstance ON codRH.CustomerOrderHeaderInstance = h.Instance AND codRH.TransID = d.TransID LEFT OUTER JOIN
                      	QSPCanadaOrderManagement.dbo.CreditCardPayment CCP INNER JOIN
                     	 QSPCanadaOrderManagement.dbo.CustomerPaymentHeader cph ON CCP.CustomerPaymentHeaderInstance = cph.Instance ON 
                     	 h.Instance = cph.CustomerOrderHeaderInstance
	Where    b.OrderID IN (@orderid)
	And d.ProductType IN (46001)		--Magazine 
	And d.StatusInstance IN (507)		-- Remit 
	And codRH.Status in (42001,42000,42010,42004) --Remit status Need to be Sent, Sent or Magazine Inactive,Cancel before Remit
	And d.delflag <> 1


	Select  @RemittedCount = Count(*) From @RemitItem

	--Only Check the item detail if there is mismatch between COD and CODRH count
	If  IsNull(@RemittedCount,0) <> IsNull(@DetailCount,0)
	Begin
		Select @DetailItemCnt= Count(*) , @MaxId = Max(Id)  From @OrderDetailItem  
		Set  @Cnt = 0
		While  @DetailItemCnt > 0
		Begin
			Select @DCoh = CustomerOrderHeaderInstance , @DTrans = TransId ,@productCode = ProductCode, @productName = ProductName
			From @OrderDetailItem Where  id = (@MaxId - @Cnt)

			Select @Dummy = RemitCoh  from @RemitItem  Where  RemitCoh = @DCoh And @DTrans = RemitTransId
			If IsNull(@@RowCount,0) = 0
			Begin
				--Item Not found in remit history insert into exception
				Insert into @AllException Values (@DCoh,@DTrans,@productCode,@productName,' Remit history item status not in ''Need to be Sent,Sent or Magazine Inactive''')
			End

		Set @Cnt = @Cnt+1
		Set @DetailItemCnt = @DetailItemCnt -1
		End
	End

	--Check other exceptions 
	Insert Into @AllException
	Select 	DetailCoh,
		DetailTransId,
		DetailProductCode,
		DetailProductName,
		(Case	When DetailProductCode = '9999'				Then 'Invalid Product code - Illegible Item'
			--MS Oct 03, 2006 As per KT
			--When DetailProductCode <> RemitTitleCode			Then 'Detail product code mismatch with remitted item'
			--When Upper(DetailProductName) <> Upper(RemitMagazineTitle)	Then 'Magazine title in COD mismatch with remitted item'
			When Soundex(Substring(Upper(Replace(DetailProductName, '&', 'and')),1,30)) <> Soundex(Substring(Upper(Replace(RemitMagazineTitle,'&','and')),1,30))  Then 'Magazine title in COD mismatch with remitted item'
			When (IsNull(DetailQuantity,0) <> IsNull(RemitNumberofIssues,-1) AND RemitTitleCode <> '5003')	Then 'Number of Issues mismatch for remitted item'
			When RemitStatus not in (42000,42001,42010,42004)  		Then 'Item status not among ''Need to be Sent,Sent, Magazine Inactive, Cancel Before Remit'' in Remit History'
			When (Substring( CreditCardNumber,1,1) ='4'  And (IsNull(PaymentMethod,0) <> 50003)) Then 'Incorrect credit card type'
			When (Substring( CreditCardNumber,1,1) ='5'  And (IsNull(PaymentMethod,0) <> 50004)) Then 'Incorrect credit card type'
			
			When (PaymentMethod in (50004,50003) And (IsNull(CCPayStatus,19001) <>19000)) Then 'Credit card status un-approved'
			When PaymentMethod not  in (50004,50003,50002,50005) 			  Then 'Invalid Payment Method' 

			When (PaymentMethod in (50004,50003) And 
			           (IsNull(CustomerFirstName,'')='' Or  IsNull(CustomerLastName,'')='' ))	Then 'Credit card owner name missing'
			--When (PaymentMethod in (50002) And 
			         --  (IsNull(CustomerFirstName,'')='' Or  IsNull(CustomerLastName,'')='' ))	Then 'Customer last/first name missing'

			When IsNull(CustomerAddress1,'') = ''  		Then 'Missing Street Address for customer'
	      		When Isnull(CustomerCity,'') = ''      		Then 'Missing City for Customer'
	      		When Isnull(CustomerState,'')= ''      		Then 'Missing province for customer'
              		When Isnull(CustomerZip,'')= ''        		Then 'Missing Postal Code for Customer'

		End) Error
	From @RemitItem
	Where  (DetailProductCode = '9999' OR RemitMagazineTitle = '9999')		
	 OR (PaymentMethod in (50004,50003) And  (IsNull(CustomerFirstName,'')='' Or  IsNull(CustomerLastName,'')='' ))	
	 OR IsNull(CustomerAddress1,'') = ''  		
	 OR Isnull(CustomerCity,'') = ''      		
	 OR Isnull(CustomerState,'')= ''      		
              OR Isnull(CustomerZip,'')= ''  
	OR (PaymentMethod in (50004,50003) And (IsNull(CCPayStatus,19001) <>19000))	
	OR  RemitStatus not in (42000,42001,42010,42004)     		--Remit status Need to be Sent, Sent or Magazine Inactive,Cancel before Remit
	OR (Substring( CreditCardNumber,1,1) ='4'  And (IsNull(PaymentMethod,0) <> 50003))
	OR (Substring( CreditCardNumber,1,1) ='5'  And (IsNull(PaymentMethod,0) <> 50004))
	OR (IsNull(DetailQuantity,0) <> IsNull(RemitNumberofIssues,-1) AND RemitTitleCode <> '5003')
	OR DetailProductCode <> RemitTitleCode	
	OR  Soundex(Substring(Upper(Replace(DetailProductName, '&', 'and')),1,30) )<> Soundex(Substring(Upper(Replace(RemitMagazineTitle,'&','and')),1,30))	 
	--OR Rtrim(Ltrim(Upper(DetailProductName))) <>  Rtrim(Ltrim(Upper(RemitMagazineTitle)))	

	Select @Cnt = Count(*)  from @AllException
	If  @Cnt > 0
	Begin
		Set @RetVal= 0	--Fail
		--Log all item with error
		Insert into QSPCanadaOrderManagement.dbo.OrderClosingLog
		Select @OrderId,  CustomerOrderHeaderInstance,	TransId	, ProductCode,ProductName, Description,'POST',GetDate(),0,Null
		From @AllException
	End
	Else
		Set @RetVal =1 --Success
Set Nocount Off
GO
