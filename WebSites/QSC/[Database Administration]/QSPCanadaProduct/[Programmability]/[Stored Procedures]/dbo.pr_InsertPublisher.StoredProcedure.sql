USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertPublisher]    Script Date: 06/07/2017 09:17:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertPublisher]

	@zStatus		varchar(10),
	@zPublisherName	varchar(80),
	@zAddress1		varchar(50),
	@zAddress2		varchar(50),
	@zCity			varchar(50),
	@zProvince		varchar(2),
	@zPostalCode		varchar(10),
	@zCountry		varchar(10)

AS

	DECLARE @iPublisherID	int
	DECLARE @zZipFour			varchar(4)

	if(len(@zPostalCode) = 10 and charindex('-', @zPostalCode) <> 0)
	begin
		set @zZipFour = substring(@zPostalCode, 7, 4)
		set @zPostalCode = substring(@zPostalCode, 1, 5)
	end
	else
	begin
		set @zZipFour = ''
	end

	create table #temp
	(
		 NextInstance int
	)
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 25 -- PublisherNext
	select @iPublisherID = nextinstance from #temp
	truncate table #temp
				
	drop table #temp


	INSERT INTO	Publishers
			(Pub_Nbr,
			Pub_Status,
			Pub_Name,
			Pub_Addr_1,
			Pub_Addr_2,
			Pub_City,
			Pub_State,
			Pub_Zip,
			Pub_Zip_Four,
			Pub_Tel,
			Pub_Fax,
			Pub_Change_Dt,
			Pub_Change_By,
			Pub_CountryCode,
			Pub_Contact_Name,
			Pub_Contact_Title,
			Pub_Contact_Email,
			Pub_Contact_Phone,
			Pub_Contact_Fax,
			Pub_UserName,
			Pub_Password)
	VALUES
			(@iPublisherID,
			@zStatus,
			@zPublisherName,
			@zAddress1,
			@zAddress2,
			@zCity,
			@zProvince,
			@zPostalCode,
			@zZipFour,
			'',
			'',
			getdate(),
			null,
			@zCountry,
			null,
			null,
			null,
			null,
			null,
			substring(replace(@zPublisherName, ' ', ''), 1, 8) + convert(varchar, @iPublisherID + 500),
			null)

	SELECT @iPublisherID
GO
