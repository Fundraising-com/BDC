USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CancelSubPriorToRemit]    Script Date: 06/07/2017 09:19:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CancelSubPriorToRemit]

	@iCustomerOrderHeaderInstance int,
	@iTransID			int,
	@sUserID nvarchar(15),
	@dDate datetime = ''

 AS

DECLARE @iRemitBatchID int

if @dDate = ''
	set @dDate = getdate()

SELECT @iRemitBatchID = coalesce(MAX(RemitBatchID),0)
   FROM CustomerOrderDetailRemitHistory
WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
 	 TransID = @iTransID AND 
	 Status = '42000'

if @iRemitBatchID = 0
SELECT @iRemitBatchID = MAX(RemitBatchID)
   FROM CustomerOrderDetailRemitHistory
WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
 	 TransID = @iTransID AND 
	 Status = '42010'

UPDATE CustomerOrderDetailRemitHistory
        SET Status='42004' ,
	UserIDChanged =@sUserID,
	DateChanged = @dDate 
WHERE  CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
 	  TransID = @iTransID AND
	  RemitBatchID = @iRemitBatchID
GO
