USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerAddress]    Script Date: 06/07/2017 09:20:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerAddress]
	@iCustomerOrderHeaderInstance int,
	@iTransID int
AS

declare 	@index int,
	@index2 int,
	@recipient nvarchar(50),
	@fn nvarchar(50),
	@ln nvarchar(50)


select 	  @recipient = ltrim(rtrim(recipient))
	from 	  customerorderdetail
	 WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		   AND TransID = @iTransID

	select @index = charindex(' ', @recipient,1)
		
	-- see if theres another blank
	select @index2 = charindex( ' ', @recipient,@index+1)
	if(@index2 <> 0 and @index2 <= len(@recipient))
	begin
		select @fn = left(@recipient, charindex(' ', @recipient,@index2)) 
		select @ln = right(@recipient, len(@recipient) - @index2)	
	end
	else
	begin
		select @fn = left(@recipient, charindex(' ', @recipient,@index)) 
		select @ln = right(@recipient, len(@recipient) - @index)	

	end


SELECT coalesce(c.firstname,@fn) as firstname,
               coalesce(c.lastname,@ln) as lastname,
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
GO
