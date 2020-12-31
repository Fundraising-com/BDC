USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateCustomerFM]    Script Date: 06/07/2017 09:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateCustomerFM]
	@FMID Varchar(4),
	@ChangeUserId varchar(4),
	@AddressTypeId int,
	@customerInstance int OUTPUT

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
	
	declare @zFirstName varchar(40)
	declare @zLastName varchar(40)
	declare @zAddress1 varchar(50)
	declare @zAddress2 varchar(50)
	declare @zCity varchar(50)
	declare @zProvince varchar(50)
	declare @zPostal varchar(10)
	declare @zPostal2 varchar(4)
	declare @zPhone varchar(20)
	declare @zEmail varchar(75)

SELECT		@zFirstName = firstname,
			@zLastName = lastname,
			@zAddress1 = street1,
			@zAddress2 = isnull(street2,''),
			@zCity = a.city,
			@zProvince = a.stateProvince,
			@zPostal = a.postal_code,
			@zPostal2 = a.zip4,
			@zEmail = IsNull(Email, '')
FROM		QSPCanadaCommon..FieldManager fm
JOIN		QSPCanadaCommon..Address a 
				ON	a.AddressListID = fm.AddressListID
WHERE		a.address_type = @addresstypeid
AND			fm.FMID = @FMID


SELECT	@zPostal = REPLACE(@zPostal,' ','')


SELECT		@zPhone = p.PhoneNumber
FROM		QSPCanadaCommon..Phone p
INNER JOIN	QSPCanadaCommon..FieldManager fm
				ON	p.Id = fm.PhoneListId
WHERE		fm.FMID = @FMID
AND			p.Type = 30500


INSERT	Customer
		(Instance,
		StatusInstance,
		LastName,
		FirstName,
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
		@zLastName,
		@zFirstName,
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
		50602)

SELECT	@customerInstance = @maxinstance

INSERT	CustomerFM
		(FMID,
		CustomerInstance)
VALUES	(@FMID,
		@customerInstance)

SELECT	@customerInstance
GO
