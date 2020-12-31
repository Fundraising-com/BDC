USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ChangeOfAddress]    Script Date: 06/07/2017 09:19:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ChangeOfAddress] 

@iCustomerOrderHeaderInstance		int 		= 0,
@sFirstName 				nvarchar(50)	= '',
@sLastName 				nvarchar(50)	= '',
@sAddress1				nvarchar(50)	= '',
@sAddress2				nvarchar(50)	= '',
@sCity					nvarchar(50)	= '',
@sStateCode				nvarchar(5)	= '',
@sZip					nvarchar(20)	= '',
@sZipPlusFour				nvarchar(5)	= '',
@sUserID 				varchar(15) ,
@dDate				datetime	=''

AS

DECLARE 	@iCustomerInstance 	int,
		@iCOHInstance		int, 
		@iTransID		int
		--@dDate datetime 
if @dDate = ''
	set @dDate = getdate()

-- Get the customer instance for this COH
SELECT @iCustomerInstance = CustomerBillToInstance
   FROM CustomerOrderHeader
WHERE Instance = @iCustomerOrderHeaderInstance

INSERT INTO CustomerAddressHistory
	SELECT getDate(), * 
	FROM    Customer
	WHERE Instance = @iCustomerInstance

-- Update the Address
UPDATE Customer 
        SET  Address1	= @sAddress1,
	   Address2	= @sAddress2,
	   City		= @sCity,
	   State		= @sStateCode,
	   Zip		= @sZip,
	   ZipPlusFour 	= @sZipPlusFour,
	   ChangeUserID	= @sUserID,
	   ChangeDate 	= @dDate
  WHERE Instance 	= @iCustomerInstance

-- Do a chadd for all COD of this Customer
DECLARE c1 CURSOR FOR SELECT CustomerOrderHeaderInstance, TransID FROM vw_GetCODByCustomerInstance WHERE CustomerInstance = @iCustomerInstance 

	OPEN c1
	FETCH NEXT FROM c1 INTO @iCOHInstance, @iTransID
		WHILE @@FETCH_STATUS = 0
		BEGIN
			EXEC pr_ChangeOfAddressByCOD @iCustomerInstance, @iCOHInstance, @iTransID,@sFirstName,@sLastName ,@sAddress1,@sAddress2,@sCity,@sStateCode,@sZip,@sZipPlusFour,@sUserID, @dDate
			FETCH NEXT FROM c1 INTO @iCOHInstance, @iTransID
		END
	CLOSE c1
DEALLOCATE c1
GO
