USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectRecipientAddressHistory]    Script Date: 06/07/2017 09:20:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectRecipientAddressHistory]
	@iCustomerOrderHeaderInstance int,
	@iTransID int
AS
/*

DECLARE 	@shipto int

SELECT @shipto = CustomerShipToInstance FROM CustomerOrderDetail WHERE CustomerOrderHeaderInstance =@iCustomerOrderHeaderInstance AND TransID=@iTransID

if coalesce(@shipto, 0) = 0
begin
	SELECT @shipto = CustomerBillToInstance FROM CustomerOrderHeader WHERE Instance =@iCustomerOrderHeaderInstance 
end


SELECT  FirstName,
	LastName,
	Address1,
	Address2,
	City,
	County as Country,
	State,
	Zip,
	MIN(Date) AS AddressCreationDate 
FROM CustomerAddressHistory
WHERE Instance = @shipto
GROUP BY
FirstName,
LastName,
Address1,
Address2,
City,
County,
State,
Zip

 ORDER BY AddressCreationDate Desc
*/

SELECT  cah.FirstName,
	cah.LastName,
	cah.Address1,
	cah.Address2,
	cah.City,
	cah.County as Country,
	cah.State,
	cah.Zip,
	MIN(Date) AS AddressCreationDate,
	Cust.Email
FROM CustomerAddressHistory cah
JOIN CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = cah.CustomerOrderHeaderInstance AND cod.TransID = cah.TransID
JOIN CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN Customer cust ON cust.Instance = CASE ISNULL(cod.CustomerShipToInstance, 0)
WHEN 0 THEN coh.CustomerBillToInstance ELSE cod.CustomerShipToInstance END
WHERE cah.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance and cah.TransID=@iTransID 
GROUP BY
cah.FirstName,
cah.LastName,
cah.Address1,
cah.Address2,
cah.City,
cah.County,
cah.State,
cah.Zip,
cust.Email
 ORDER BY AddressCreationDate Desc
GO
