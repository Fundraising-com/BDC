USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateCustomer]    Script Date: 06/07/2017 09:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateCustomer]

	@zFirstname				varchar(50),
	@zLastname				varchar(50),
	@zAddress1				varchar(50),
	@zAddress2				varchar(50),
	@zCity					varchar(50),
	@zCounty				varchar(31) = null,
	@zProvince				varchar(50),
	@zPostal				varchar(20),
	@zPostal2				varchar(4) = null,
	@zEmail					varchar(75),
	@zChangeUserId			varchar(4),
	@iCustomerInstance		int OUTPUT,
	@bOverrideAddress		bit = 0,
	@iType					int = 50601,
	@Phone					varchar(25) = null,
	@statusInstance			int = 300

AS

	set nocount on
	-- steal data from Account table
	declare @maxinstance int
	create table #temp
	(
		 NextInstance int
	)
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 3
		
	select @maxinstance = NextInstance from #temp
	drop table #temp

SELECT	@zPostal = REPLACE(@zPostal,' ','')

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
		@statusInstance, --modified to be sent in by the GA.ETL.TaskExecutor.OrderTransferProcessor
		@zLastName,
		@zFirstName,
		@zAddress1,
		@zAddress2,
		@zCity,
		@zCounty,
		@zProvince,
		@zPostal,
		@zPostal2,
		@bOverrideAddress,
		@zChangeUserId,
		GETDATE(),
		@zEmail,
		@Phone,
		@iType)

select	@iCustomerInstance = @maxinstance

-------- END -----------
GO
