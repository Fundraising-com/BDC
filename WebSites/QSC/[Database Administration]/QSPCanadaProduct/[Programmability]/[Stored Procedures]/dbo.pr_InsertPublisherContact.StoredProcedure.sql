USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertPublisherContact]    Script Date: 06/07/2017 09:17:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertPublisherContact]

	@iPublisherID		int,
	@zContactFirstName	varchar(30),
	@zContactLastName	varchar(30),
	@zPosition		varchar(50),
	@zEmail		varchar(50)

AS

	DECLARE @iPhoneListID	int
	DECLARE @iPhoneID		int

	create table #temp
	(
		 NextInstance int
	)
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 28 -- PhoneListNext
	select @iPhoneListID = nextinstance from #temp
	truncate table #temp

	delete from #temp

	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 23 -- PhoneNext
	select @iPhoneID = nextinstance from #temp
	truncate table #temp

	drop table #temp


	INSERT INTO	PUBLISHER_CONTACTS
			(Pub_Nbr,
			Pub_Contact_Nbr,
			PContact_Prefix,
			PContact_FName,
			PContact_LName,
			PContact_Addr_1,
			PContact_Addr_2,
			PContact_City,
			PContact_State,
			PContact_Zip,
			PContact_Zip_Four,
			PContact_Tel,
			PContact_Tel_Extn,
			PContact_Fax,
			PContact_Title,
			PContact_Email,
			PContact_DateChanged,
			PContact_ChangedBy,
			PhoneListID)
	VALUES
			(@iPublisherID,
			1,
			null,
			@zContactFirstName,
			@zContactLastName,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			@zPosition,
			@zEmail,
			getdate(),
			null,
			@iPhoneListID)

	SELECT	IDENT_CURRENT('PUBLISHER_CONTACTS') AS PContact_Instance,
			@iPublisherID AS Pub_Nbr,
			@zContactFirstName AS PContact_FName,
			@zContactLastName AS PContact_LName,
			@zPosition AS PContact_Title,
			@zEmail AS PContact_Email,
			@iPhoneListID AS PhoneListID
GO
