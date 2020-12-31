USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateCustomerAccount]    Script Date: 06/07/2017 09:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateCustomerAccount]

	@accountID int,
	@ChangeUserId varchar(4),
	@customerinstance int OUTPUT

AS

	SET nocount ON
	-- steal data from Account table
	declare @maxinstance int
	create table #temp
	(
		 NextInstance int
	)
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 3
		
	select @maxinstance = NextInstance from #temp
	drop table #temp
	
	declare @zName			varchar(40)
	declare @zAddress1		varchar(50)
	declare @zAddress2		varchar(50)
	declare @zCity			varchar(50)
	declare @zProvince		varchar(50)
	declare @zPostal		varchar(10)
	declare @zPostal2		varchar(4)
	declare @zPhone			varchar(20)
	declare @zEmail			varchar(75)


SELECT		@zName = acc.Name,
			@zAddress1 = a.Street1,
			@zAddress2 = a.Street2,
			@zCity = a.City,
			@zProvince = a.StateProvince,
			@zPostal = a.Postal_Code,
			@zPostal2 = a.Zip4,
			@zEmail = acc.Email
FROM		QSPCanadaCommon..CAccount acc
JOIN		QSPCanadaCommon..Address a
				ON	a.AddressListID = acc.AddressListID
WHERE		a.address_type = 54001 --shipping address
AND			acc.ID = @accountID


SELECT	@zPostal = REPLACE(@zPostal,' ','')


SELECT		@zPhone = A.PhoneNumber
FROM		QSPCanadaCommon..Phone A
INNER JOIN	QSPCanadaCommon..CAccount B
				ON	A.Id = B.PhoneListId
WHERE		B.Id = @accountID
AND			A.Type = 30500


INSERT	Customer
		(Instance,
		StatusInstance,
		FirstName,
		LastName,
		Address1,
		Address2,
		City,
		County,
		State,
		Zip,
		ZipPlusFour,
		OverrideAddress,
		ChangeUserID,
		ChangeDate,
		Email,
		Phone,
		Type)
VALUES	(@maxinstance,
		300, -- make it good
		@zName,
		'',
		@zAddress1,
		@zAddress2,
		@zCity,
		'',
		@zProvince,
		@zPostal,
		@zPostal2,
		0,
		@ChangeUserId,
		GetDate(),
		@zEmail,
		@zPhone,
		50603)

SELECT	@customerInstance = @maxinstance

INSERT	CustomerAccount
		(AccountID,
		CustomerInstance)
VALUES	(@accountID,
		@customerInstance)
GO
