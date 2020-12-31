USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCAccountContactInfo]    Script Date: 06/07/2017 09:33:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateCAccountContactInfo]
	@Mode bit = null,
	@ContactId int,
	@AccountId int,
	@Title varchar(10) = null,
	@FirstName varchar(20) = null,
	@LastName varchar(30) = null,
	@MiddleInitial varchar(10) = null,
	@TypeId int = 1,
	@Email varchar(60) = null,
	@Address1 varchar(50) = null,
	@Address2 varchar(50) = null,
	@City varchar(30) = null,
	@State varchar(2) = null,
	@Zip varchar(10) = null,
	@HomePhone varchar(20) = null,
	@WorkPhone varchar(20) = null,
	@FaxPhone varchar(20) = null,
	@MobilePhone varchar(20) = null,
	@Delete_TF bit = 0

AS

if @Delete_TF <> 0
begin
	UPDATE QSPCanadaCommon.dbo.CAccountContact
	SET Deleted_TF = 1
	WHERE Id = @ContactId
end
else
begin
	--Create a new record for a new ContactId
	if @ContactId = -1
	begin
		INSERT INTO QSPCanadaCommon.dbo.CAccountContact (
			AccountId,
			Title,
			FirstName,
			LastName,
			MiddleInitial,
			TypeId,
			Email
		)
		VALUES (
			@AccountId,'Mr', '', '', '',1,''
		)
	end
	else
	begin
		if @Mode = 0 --Update basic information
		begin
			UPDATE
				QSPCanadaCommon.dbo.CAccountContact
			SET
				Title = @Title,
				FirstName = @FirstName,
				LastName = @LastName,
				MiddleInitial = @MiddleInitial,
				TypeId = @TypeId,
				Email = @Email
			WHERE
				Id = @ContactId
		end
		else --Update detailed contact information
		begin
			UPDATE
				QSPCanadaCommon.dbo.CAccountContact
			SET
				Address1 = @Address1,
				Address2 = @Address2,
				City = @City,
				State = @State,
				Zip = @Zip,
				HomePhone = @HomePhone,
				WorkPhone = @WorkPhone,
				FaxPhone = @FaxPhone,
				MobilePhone = @MobilePhone
			WHERE
				Id = @ContactId
		end
	end
end
GO
