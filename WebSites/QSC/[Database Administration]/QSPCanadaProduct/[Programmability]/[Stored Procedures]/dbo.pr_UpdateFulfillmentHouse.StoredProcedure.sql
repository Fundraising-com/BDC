USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateFulfillmentHouse]    Script Date: 06/07/2017 09:18:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdateFulfillmentHouse]

	@iFulfillmentHouseID		int,
	@zStatus			varchar(20),
	@zName			varchar(80),
	@zAddress1			varchar(50),
	@zAddress2			varchar(50),
	@zCity				varchar(25),
	@zProvince			varchar(2),
	@zPostalCode			varchar(10),
	@zCountry			varchar(2),
	@iInterfaceMediaID		int,
	@iInterfaceLayoutID		int,
	@iTransmissionMethodID	int,
	@bHardCopy				bit,
	@zQSPAgencyCode		varchar(20),
	@zIsEffortKeyRequired		varchar(1)

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


	UPDATE	Fulfillment_House
	SET		Ful_Status = @zStatus,
			Ful_Name = @zName,
			Ful_Addr_1 = @zAddress1,
			Ful_Addr_2 = @zAddress2,
			Ful_City = @zCity,
			Ful_State = @zProvince,
			Ful_Zip = @zPostalCode,
			Ful_Zip_Four = @zZipFour,
			InterfaceMediaID = @iInterfaceMediaID,
			InterfaceLayoutID = @iInterfaceLayoutID,
			QSPAgencyCode = @zQSPAgencyCode,
			CountryCode = @zCountry,
			IsEffortKey = @zIsEffortKeyRequired,
			TransmissionMethodID = @iTransmissionMethodID,
			HardCopy = @bHardCopy
	WHERE	Ful_Nbr = @iFulfillmentHouseID
GO
