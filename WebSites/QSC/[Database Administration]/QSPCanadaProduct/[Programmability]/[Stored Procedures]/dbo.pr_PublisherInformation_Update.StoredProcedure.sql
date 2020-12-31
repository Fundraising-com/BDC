USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_PublisherInformation_Update]    Script Date: 06/07/2017 09:17:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_PublisherInformation_Update]

	@iPublisherID		int,
	@zUMC		varchar(4),
	@zPublisherName	varchar(80),
	@zAddress1		varchar(50),
	@zAddress2		varchar(50),
	@zCity			varchar(50),
	@zProvince		varchar(2),
	@zPostalCode		varchar(10),
	@zCountry		varchar(10),
	@zContactFirstName	varchar(30),
	@zContactLastName	varchar(30),
	@zPosition		varchar(50),
	@zEmail		varchar(50),
	@zWorkPhone		varchar(20),
	@zFaxNumber		varchar(20)

AS
	DECLARE	@count				int
	DECLARE	@countphone			int
	DECLARE	@iPhoneListID			int
	DECLARE	@iPhoneID			int
	DECLARE	@iExistingPContact_Instance	int

	UPDATE	PUBLISHERS
	SET		Pub_Name = @zPublisherName,
			Pub_Addr_1 = @zAddress1,
			Pub_Addr_2 = @zAddress2,
			Pub_City = @zCity,
			Pub_State = @zProvince,
			Pub_Zip = @zPostalCode,
			Pub_CountryCode = @zCountry
	WHERE	Pub_Nbr = @iPublisherID


	SELECT	@count = count(PContact_Instance)
	FROM		PUBLISHER_CONTACTS
	WHERE	Pub_Nbr = @iPublisherID

	if(@count = 0)
	BEGIN
		create table #temp
		(
			 NextInstance int
		)
		insert into #temp exec qspcanadaordermanagement..InsertNextInstance 28 -- PhoneListNext
		select @iPhoneListID = nextinstance from #temp
		truncate table #temp
	
		drop table #temp
	
	
		create table #temp2
		(
			 NextInstance int
		)
		insert into #temp2 exec qspcanadaordermanagement..InsertNextInstance 23 -- PhoneNext
		select @iPhoneID = nextinstance from #temp2
		truncate table #temp2
	
		drop table #temp2

		INSERT INTO PUBLISHER_CONTACTS
		(Product_Code,
		Pub_Nbr,
		PContact_FName,
		PContact_LName,
		PContact_Title,
		PContact_Email,
		PhoneListID)
		VALUES
		(@zUMC,
		@iPublisherID,
		@zContactFirstName,
		@zContactLastName,
		@zPosition,
		@zEmail,
		@iPhoneListID)

		if(ltrim(rtrim(@zWorkPhone)) <> '')
		BEGIN
			INSERT INTO Phone
			(ID,
			Type,
			PhoneListID,
			PhoneNumber)
			VALUES
			(@iPhoneID,
			1,
			@iPhoneListID,
			@zWorkPhone)
		END

		if(ltrim(rtrim(@zFaxNumber)) <> '')
		BEGIN
			INSERT INTO Phone
			(ID,
			Type,
			PhoneListID,
			PhoneNumber)
			VALUES
			(@iPhoneID + 1,
			3,
			@iPhoneListID,
			@zFaxNumber)
		END
	END
	else
	BEGIN
		SELECT	top 1
				@iExistingPContact_Instance = PContact_Instance
		FROM		PUBLISHER_CONTACTS
		WHERE	Pub_Nbr = @iPublisherID
		AND		PContact_FName = @zContactFirstName
		AND		PContact_LName = @zContactLastName

		if(coalesce(@iExistingPContact_Instance, 0) <> 0)
		BEGIN
			SELECT	@iPhoneListID = coalesce(PhoneListID, 0) FROM PUBLISHER_CONTACTS WHERE PContact_Instance = @iExistingPContact_Instance

			if(@iPhoneListID <> 0)
			BEGIN
				UPDATE	PUBLISHER_CONTACTS
				SET		PContact_Title = @zPosition,
						PContact_Email = @zEmail
				WHERE	PContact_Instance = @iExistingPContact_Instance

				SELECT	@countphone = count(ID)
				FROM		Phone
				WHERE	PhoneListID = @iPhoneListID
				AND		Type = 1

				if(@countphone = 0)
				BEGIN
					if(ltrim(rtrim(@zWorkPhone)) <> '')
					BEGIN
						create table #temp3
						(
							 NextInstance int
						)
						insert into #temp3 exec qspcanadaordermanagement..InsertNextInstance 23 -- PhoneNext
						select @iPhoneID = nextinstance from #temp3
						truncate table #temp3
					
						drop table #temp3
	
						INSERT INTO Phone
						(ID,
						Type,
						PhoneListID,
						PhoneNumber)
						VALUES
						(@iPhoneID,
						1,
						@iPhoneListID,
						@zWorkPhone)
					END
				END
				else
				BEGIN
					UPDATE	Phone
					SET		PhoneNumber = @zWorkPhone
					WHERE	PhoneListID = @iPhoneListID
					AND		Type = 1
				END
	
				SELECT	@countphone = count(ID)
				FROM		Phone
				WHERE	PhoneListID = @iPhoneListID
				AND		Type = 3

				if(@countphone = 0)
				BEGIN
					if(ltrim(rtrim(@zFaxNumber)) <> '')
					BEGIN
						create table #temp4
						(
							 NextInstance int
						)
						insert into #temp4 exec qspcanadaordermanagement..InsertNextInstance 23 -- PhoneNext
						select @iPhoneID = nextinstance from #temp4
						truncate table #temp4
					
						drop table #temp4
	
						INSERT INTO Phone
						(ID,
						Type,
						PhoneListID,
						PhoneNumber)
						VALUES
						(@iPhoneID,
						3,
						@iPhoneListID,
						@zFaxNumber)
					END
				END
				else
				BEGIN
					UPDATE	Phone
					SET		PhoneNumber = @zFaxNumber
					WHERE	PhoneListID = @iPhoneListID
					AND		Type = 3
				END
			END
			else
			BEGIN
				create table #temp5
				(
					 NextInstance int
				)
				insert into #temp5 exec qspcanadaordermanagement..InsertNextInstance 28 -- PhoneListNext
				select @iPhoneListID = nextinstance from #temp5
				truncate table #temp5
			
				drop table #temp5
			
			
				create table #temp6
				(
					 NextInstance int
				)
				insert into #temp6 exec qspcanadaordermanagement..InsertNextInstance 23 -- PhoneNext
				select @iPhoneID = nextinstance from #temp6
				truncate table #temp6
			
				drop table #temp6

				UPDATE	PUBLISHER_CONTACTS
				SET		PContact_Title = @zPosition,
						PContact_Email = @zEmail,
						PhoneListID = @iPhoneListID
				WHERE	PContact_Instance = @iExistingPContact_Instance

				if(ltrim(rtrim(@zWorkPhone)) <> '')
				BEGIN
					INSERT INTO Phone
					(ID,
					Type,
					PhoneListID,
					PhoneNumber)
					VALUES
					(@iPhoneID,
					1,
					@iPhoneListID,
					@zWorkPhone)
				END

				if(ltrim(rtrim(@zFaxNumber)) <> '')
				BEGIN
					INSERT INTO Phone
					(ID,
					Type,
					PhoneListID,
					PhoneNumber)
					VALUES
					(@iPhoneID + 1,
					3,
					@iPhoneListID,
					@zFaxNumber)
				END
			END
		END
		else
		BEGIN
			create table #temp7
			(
				 NextInstance int
			)
			insert into #temp7 exec qspcanadaordermanagement..InsertNextInstance 28 -- PhoneListNext
			select @iPhoneListID = nextinstance from #temp7
			truncate table #temp7
		
			drop table #temp7
		
		
			create table #temp8
			(
				 NextInstance int
			)
			insert into #temp8 exec qspcanadaordermanagement..InsertNextInstance 23 -- PhoneNext
			select @iPhoneID = nextinstance from #temp8
			truncate table #temp8
		
			drop table #temp8

			INSERT INTO PUBLISHER_CONTACTS
			(Product_Code,
			Pub_Nbr,
			PContact_FName,
			PContact_LName,
			PContact_Title,
			PContact_Email,
			PhoneListID)
			VALUES
			(@zUMC,
			@iPublisherID,
			@zContactFirstName,
			@zContactLastName,
			@zPosition,
			@zEmail,
			@iPhoneListID)
			
			if(ltrim(rtrim(@zWorkPhone)) <> '')
			BEGIN
				INSERT INTO Phone
				(ID,
				Type,
				PhoneListID,
				PhoneNumber)
				VALUES
				(@iPhoneID,
				1,
				@iPhoneListID,
				@zWorkPhone)
			END

			if(ltrim(rtrim(@zFaxNumber)) <> '')
			BEGIN
				INSERT INTO Phone
				(ID,
				Type,
				PhoneListID,
				PhoneNumber)
				VALUES
				(@iPhoneID + 1,
				3,
				@iPhoneListID,
				@zFaxNumber)
			END
		END
	END
GO
