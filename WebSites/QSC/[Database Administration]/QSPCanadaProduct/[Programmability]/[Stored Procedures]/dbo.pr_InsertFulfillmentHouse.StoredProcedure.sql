USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertFulfillmentHouse]    Script Date: 06/07/2017 09:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertFulfillmentHouse]

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
	@bHardCopy	bit,
	@zQSPAgencyCode		varchar(20),
	@zIsEffortKeyRequired		varchar(1)

AS

	DECLARE @iFulfillmentHouseID		int
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
	
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 29 -- FULFILLMENT_HOUSENext
	select @iFulfillmentHouseID = nextinstance from #temp
	truncate table #temp
				
	INSERT INTO	Fulfillment_House
			(Ful_Nbr,
			Ful_Status,
			Ful_Name,
			Ful_Addr_1,
			Ful_Addr_2,
			Ful_City,
			Ful_State,
			Ful_Zip,
			Ful_Zip_Four,
			Ful_Tel,
			Ful_Fax,
			Ful_Change_Dt,
			Ful_Change_By,
			InterfaceMediaID,
			InterfaceLayoutID,
			QSPAgencyCode,
			CountryCode,
			IsEffortKey,
			IsCancelFileReqd,
			TransmissionMethodID,
			HardCopy)
	VALUES
			(@iFulfillmentHouseID,
			@zStatus,
			@zName,
			@zAddress1,
			@zAddress2,
			@zCity,
			@zProvince,
			@zPostalCode,
			@zZipFour,
			null,
			null,
			null,
			0,
			@iInterfaceMediaID,
			@iInterfaceLayoutID,
			@zQSPAgencyCode,
			@zCountry,
			@zIsEffortKeyRequired,
			'N',
			@iTransmissionMethodID,
			@bHardCopy)

	SELECT @iFulfillmentHouseID
GO
