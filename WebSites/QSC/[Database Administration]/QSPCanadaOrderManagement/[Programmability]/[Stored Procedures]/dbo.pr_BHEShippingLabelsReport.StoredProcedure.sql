USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_BHEShippingLabelsReport]    Script Date: 06/07/2017 09:19:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_BHEShippingLabelsReport]
@ReportRequestID int, @OrderID int
AS
SET NOCOUNT ON

-- SS, Sep, 2004
-- used in .net BHE labels report

Declare @RecCount int, @RecType varchar(10), @ProductCode varchar(10), @OrderID1 int, @Recipient varchar(100), @Address1 varchar(100), @Address2 varchar(100), @City varchar(20), @State varchar(2) , @Zip varchar(10)

Create Table #LabelRecords
 (RecType varchar(10), ProductCode varchar(10), OrderID int, Recipient varchar(100), Address1 varchar(100), Address2 varchar(100), City varchar(20), State varchar(2) , Zip varchar(10) )


  Declare Cur_LabelsInfo Cursor  For
 Select cod.ProductCode,
	batch.OrderID,
	Upper(cod.Recipient) 	as Recipient,
	cus.Address1 		as Address1,
	cus.Address2 		as Address2,
	upper(cus.City) 		as City,
	upper(cus.State) 	as State ,
	cus.Zip 			as Zip
 From 	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh
	LEFT OUTER JOIN 	QSPCanadaOrderManagement..CustomerPaymentHeader	as ph
	ON    	coh.Instance	 	 =  ph.CustomerOrderHeaderInstance
	        and ph.StatusInstance		not in (601,602,603)-- payment errors
	,	
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod,
	QSPCanadaOrderManagement..Customer			as cus,
	QSPCanadaOrderManagement..Batch 			as batch
 Where
       coh.OrderBatchID    	  	  = Batch.id
   and coh.OrderBatchDate  	  = batch.Date
   and coh.Instance		  = cod.CustomerOrderHeaderInstance
   and coh.CustomerBilltoInstance  = cus.Instance
   and cod.StatusInstance 	 =  511  -- is picked 
   and cod.StatusInstance 	<> 508 -- not shipped
   and cod.productType 		in (46006,46007,46012) --book,music,video   
   and ( cod.Recipient 		is not null or batch.OrderTypeCode = 41008)-- or Group Order 
   and cus.Address1 		is not null
   and cus.City 			is not null
   and cus.State 			is not null
   and cus.Zip 			is not null
   and cus.Address1 		is not null
   and cus.StatusInstance  	<> 301 -- customer error
   and batch.OrderID 		= @OrderID -- order parameter must be provided by user
 Order By cod.ProductCode;

Set @RecCount = 0 --imp

	OPEN Cur_LabelsInfo

	    FETCH NEXT FROM Cur_LabelsInfo  INTO @ProductCode, @OrderID1, @Recipient, @Address1, @Address2, @City, @State, @Zip

	    WHILE @@FETCH_Status = 0

                BEGIN

		Set @RecCount  = @RecCount  +1

		IF @RecCount = 1 or @RecType = 'EVEN'
		  Begin
			Set @RecType = 'ODD'
		  End 				
		Else IF  @RecType = 'ODD'
		  Begin
			Set @RecType = 'EVEN'
		  End 				


		Insert Into #LabelRecords
		(RecType, ProductCode,OrderID,Recipient,Address1,Address2,City,State,Zip)
		Values (@RecType, @ProductCode, @OrderID1, @Recipient, @Address1, @Address2, @City, @State, @Zip) 

                    FETCH NEXT FROM Cur_LabelsInfo INTO @ProductCode, @OrderID1, @Recipient, @Address1, @Address2, @City, @State, @Zip   
  
                END

	CLOSE Cur_LabelsInfo
	DEALLOCATE Cur_LabelsInfo


 	Select RecType,ProductCode, OrderID , Recipient , Address1,  Address2 , City, State , Zip
	From #LabelRecords
	Order by OrderID, ProductCode,Recipient

	Drop Table #LabelRecords


--Following lines are written by saqib on 13-Apr-2005 to update data driven subscription support tables
IF @ReportRequestID <> 0  -- if the value is not zero it means the report is called from a data driven subscription
BEGIN
     
   UPDATE Qspcanadaordermanagement.dbo.ReportRequestBatch_BHEShippingLabelsReport
   set  RunDateStart = getdate()
   where [id]  = @ReportRequestID

END
GO
