USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateCustomerForCHADD]    Script Date: 06/07/2017 09:19:49 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CreateCustomerForCHADD] 

@sLastName		nvarchar (40)	='',
@sFirstName		nvarchar (40)	='',
@sAddress1		nvarchar (50)	='',
@sAddress2		nvarchar (50)	='',
@sCity			nvarchar (50)	='',
@sState			nvarchar (10)	='',
@sZip			nvarchar (10)	='',
@iUserID 		int,
@dChangeDate	datetime		=getdate,
@sEmail			nvarchar (50)	='',
@sPhone			nvarchar (25)	='',
@iType 			int				= 0

AS

	declare @maxinstance int
	create table #temp
	(
		 NextInstance int
	)

	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 3
	select @maxinstance=nextinstance from #temp
	truncate table #temp
	
	drop table #temp

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
		@sLastName,
		@sFirstName,
		@sAddress1,
		@sAddress2,
		@sCity,
		'',
		@sState,
		@sZip,
		'',
		'', 
		@iUserID,
		@dChangeDate,
		@sEmail,
		@sPhone,
		@iType)

select	@maxinstance
GO
