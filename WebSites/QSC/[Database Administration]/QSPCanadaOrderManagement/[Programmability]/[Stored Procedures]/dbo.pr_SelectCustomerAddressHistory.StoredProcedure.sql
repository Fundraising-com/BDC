USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerAddressHistory]    Script Date: 06/07/2017 09:20:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerAddressHistory]
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
	select @index2 = charindex(' ', @recipient,@index+1)
	if(@index2 <> 0 and @index2 <= len(@recipient))
	begin
		select @fn = rtrim(left(@recipient, charindex(' ', @recipient,@index2)))
		select @ln = ltrim(right(@recipient, len(@recipient) - @index2))
	end
	else
	begin
		select @fn = rtrim(left(@recipient, charindex(' ', @recipient,@index)))
		select @ln = ltrim(right(@recipient, len(@recipient) - @index))

	end

SELECT	DISTINCT
	coalesce(c.FirstName, @fn) as firstname,
	coalesce(c.LastName, @ln) as lastname,
	cah.Address1,
	cah.Address2,
	cah.City,
	'CA' as Country,
	cah.State,
	cah.Zip,
	MIN(cah.Date) AS AddressCreationDate,
	c.Email
FROM	CustomerOrderHeader coh,
	Customer c,
	CustomerAddressHistory cah
WHERE	coh.Instance = @iCustomerOrderHeaderInstance
AND	c.Instance = coh.CustomerBillToInstance
AND	cah.Instance = c.Instance
GROUP BY
c.FirstName,
c.LastName,
cah.LastName,
cah.Address1,
cah.Address2,
cah.City,
cah.County,
cah.State,
cah.Zip,
c.Email
ORDER BY AddressCreationDate Desc
GO
