USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_Contact]    Script Date: 06/07/2017 09:33:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[pr_ins_Contact]
	@ContactListID int = null,
	@CAccountID int,
	@Title varchar(10),
	@FirstName varchar(20),
	@LastName varchar(30),
	@MiddleInitial varchar(10),
	--@TypeId int,
	@Function varchar(50),
	@Email varchar(60),
	@AddressID int OUT,
	@PhoneListID int OUT,
	@ContactID int OUT
AS

-----------------------------------------------
--  Get a new PhoneListID   ---
-----------------------------------------------
insert into PhoneList(CreateDate) values(GetDate())	
SELECT @PhoneListID = @@Identity

-------------------------------------------------
--  Get a new AddressID   ---
-------------------------------------------------
--insert an empty address
EXEC QSPCanadaCommon.dbo.pr_ins_Address
 @street1 = '', 
 @street2 = '',
 @city    = '', 
 @stateProvince = '', 
 @postal_code = '', 
 @zip4 = '', 
 @country = '', 
 @address_type = 54001,
 @AddressListID = null, 
 @Address_ID = @AddressID OUTPUT 


INSERT INTO Contact (
	ContactListID,
	CAccountID,
	Title,
	FirstName,
	LastName,
	MiddleInitial,
	TypeId,
	[Function],
	Email,
	AddressID,
	PhoneListID
)VALUES(
	@ContactListID,
	@CAccountID,
	@Title,
	@FirstName,
	@LastName,
	@MiddleInitial,
	-10,--this field isn't used right now
	@Function,
	@Email,
	@AddressID,
	@PhoneListID
);

SELECT @ContactID = @@Identity;
GO
