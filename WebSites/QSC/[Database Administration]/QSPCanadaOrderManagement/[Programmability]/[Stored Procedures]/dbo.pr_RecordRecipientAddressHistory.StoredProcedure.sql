USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RecordRecipientAddressHistory]    Script Date: 06/07/2017 09:20:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_RecordRecipientAddressHistory]
	@iCustomerOrderHeaderInstance int,
	@iTransID int
AS

DECLARE 	@shipto		int,
		@billto		int,
		@firstname	varchar(50),
		@lastname	varchar(50),
		@index		int,
		@index2	int,
		@recipient	varchar(100)


SELECT @shipto = COALESCE(cod.CustomerShipToInstance, 0)
   FROM CustomerOrderDetail cod
WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	  AND TransID = @iTransID

SELECT @billto = COALESCE(coh.CustomerBillToInstance, 0)
   FROM CustomerOrderHeader coh
WHERE Instance = @iCustomerOrderHeaderInstance


IF @shipto = 0
BEGIN
	select top 1 @firstname = firstname, @lastname = lastname 
	  from 	customerorderdetailremithistory codrh,
		customerremithistory crh
	  where crh.instance = codrh.customerremithistoryinstance
		and codrh.customerorderheaderinstance = @iCustomerOrderHeaderInstance
		and transid = @iTransID
	order by crh.instance desc

	if(@firstname is null)
	BEGIN
		-- FIX to remove double spaces which crash the procs
		select 	  @recipient = replace(rtrim(recipient), '  ', ' ')
		from 	  customerorderdetail
		 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
			   AND TransID = @iTransID
	
		select @index = charindex(' ', @recipient,1)
			
		-- see if theres another blank
		select @index2 = charindex(' ', @recipient,@index+1)
		if(@index2 <> 0)
		begin
			select @firstname = left(@recipient, charindex(' ', @recipient,@index2)) 
			select @lastname = right(@recipient, len(@recipient) - @index2)	
		end
		else
		begin
			select @firstname = left(@recipient, charindex(' ', @recipient,@index)) 
			select @lastname = right(@recipient, len(@recipient) - @index)	
		end
	END

	INSERT INTO CustomerAddressHistory
		(Date, Instance, FirstName, LastName, Address1, Address2, City, State, Zip,changedate, customerorderheaderinstance, transid)
	SELECT getDate(),
		@billto,
		 @firstname as firstname,
	               @lastname as lastname,
	       	  c.Address1,
	      	  c.Address2,
	     	  c.City,
	     	  c.State,
	     	  c.Zip,
		  getDate(),
		  @iCustomerOrderHeaderInstance,
		  @iTransID
	  FROM   Customer c, CustomerOrderHeader coh 
	 WHERE coh.Instance = @iCustomerOrderHeaderInstance
	                AND coh.CustomerBillToInstance = c.Instance
END
ELSE
BEGIN
	INSERT INTO CustomerAddressHistory
		(Date, Instance, FirstName, LastName, Address1, Address2, City, State, Zip,changedate, customerorderheaderinstance, transid)
	SELECT getDate(),
		@shipto,
		 c.FirstName,
	               c.LastName,
	       	  c.Address1,
	      	  c.Address2,
	     	  c.City,
	     	  c.State,
	     	  c.Zip,
		  getDate(),
		  @iCustomerOrderHeaderInstance,
		  @iTransID
	  FROM   Customer c
	 WHERE c.Instance=@shipto
END
GO
