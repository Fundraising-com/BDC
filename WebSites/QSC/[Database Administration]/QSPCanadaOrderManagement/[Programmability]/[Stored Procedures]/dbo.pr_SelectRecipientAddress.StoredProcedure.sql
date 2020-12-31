USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectRecipientAddress]    Script Date: 06/07/2017 09:20:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectRecipientAddress]
	@iCustomerOrderHeaderInstance int,
	@iTransID int
AS

DECLARE 	@shipto int,
		@firstname varchar(50),
		@lastname varchar(50),
		@recipient varchar(50),
		@index int,
		@index2 int,
		@count int
		
select @count = count(*) from customerorderdetailremithistory codrh, customerremithistory crh where crh.instance = codrh.customerremithistoryinstance and codrh.customerorderheaderinstance=@iCustomerOrderHeaderInstance and transid = @iTransID


If @count = 0 
begin
SELECT @shipto = COALESCE(cod.CustomerShipToInstance, 0)
   FROM CustomerOrderDetail cod
WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	  AND TransID = @iTransID

IF @shipto = 0
BEGIN

select @recipient =recipient
	  from 	customerorderdetail cod
	  where cod.customerorderheaderinstance = @iCustomerOrderHeaderInstance
		and cod.transid = @iTransID


select @index = charindex(' ', @recipient,1)
		
			-- see if theres another blank
			select @index2 = charindex( ' ', @recipient,@index+1)
			if(@index2 <> 0)
			begin
				select @firstname = left(@recipient, charindex(' ', @recipient,@index2)) 
				select @lastname = right(@recipient, datalength(@recipient) - @index2)	
			end
			else
			begin
				select @firstname = left(@recipient, charindex(' ', @recipient,@index)) 
				select @lastname = right(@recipient, datalength(@recipient) - @index)	
			end

	

	SELECT @firstname as firstname,
	               @lastname as lastname,
	       	  c.Address1,
	      	  c.Address2,
		  'CA' as Country,
	     	  c.City,
	     	  c.State,
	     	  c.Zip,
		  coalesce(c.Phone, '') as phone,
		  c.Email
	  FROM   Customer c, CustomerOrderHeader coh 
	 WHERE coh.Instance = @iCustomerOrderHeaderInstance
	                AND coh.CustomerBillToInstance = c.Instance
END
ELSE
BEGIN
	SELECT c.firstname,
	               c.lastname,
	       	  c.Address1,
	      	  c.Address2,
		  'CA' as Country,
	     	  c.City,
	     	  c.State,
	     	  c.Zip,
		  coalesce(c.Phone, '') as phone,
		  c.Email
	  FROM   Customer c
	 WHERE c.Instance=@shipto
END

END
ELSE
BEGIN

	SELECT		top 1 crh.firstname,
	            crh.lastname,
	       		crh.Address1,
	      		crh.Address2,
				'CA' as Country,
	     		crh.City,
	     		crh.State,
	     		crh.Zip,
				'' as phone,
				cust.Email
	FROM		customerorderdetailremithistory codrh
	JOIN		customerremithistory crh ON crh.instance = codrh.customerremithistoryinstance
	JOIN		CustomerOrderHeader coh ON coh.Instance = codrh.CustomerOrderHeaderInstance
	JOIN		CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance AND cod.TransID = codrh.TransID
	JOIN		Customer cust
					ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
											WHEN 0 THEN coh.CustomerBillToInstance
											ELSE		cod.CustomerShipToInstance
										END	
	where		codrh.customerorderheaderinstance = @iCustomerOrderHeaderInstance 
	and			codrh.TransID = @iTransID
	order by	crh.instance desc

END
GO
