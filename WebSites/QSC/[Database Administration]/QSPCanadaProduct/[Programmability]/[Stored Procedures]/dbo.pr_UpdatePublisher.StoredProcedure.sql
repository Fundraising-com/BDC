USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdatePublisher]    Script Date: 06/07/2017 09:18:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdatePublisher]

	@iPublisherID		int,
	@zStatus		varchar(10),
	@zPublisherName	varchar(80),
	@zAddress1		varchar(50),
	@zAddress2		varchar(50),
	@zCity			varchar(50),
	@zProvince		varchar(2),
	@zPostalCode		varchar(10),
	@zCountry		varchar(10)

AS

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

	UPDATE	Publishers
	SET		Pub_Status = @zStatus,
			Pub_Name = @zPublisherName,
			Pub_Addr_1 = @zAddress1,
			Pub_Addr_2 = @zAddress2,
			Pub_City = @zCity,
			Pub_State = @zProvince,
			Pub_Zip = @zPostalCode,
			Pub_Zip_Four = @zZipFour,
			Pub_CountryCode = @zCountry,
			Pub_UserName = substring(replace(@zPublisherName, ' ', ''), 1, 8) + convert(varchar, @iPublisherID + 500)
	WHERE	Pub_Nbr = @iPublisherID
GO
