USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateCustomerForCoupon]    Script Date: 06/07/2017 09:19:49 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CreateCustomerForCoupon] 

	@sFirstName	nvarchar (50)	='',
	@sLastName	nvarchar (50)	='',
	@sAddress1	nvarchar (128)	='',
	@sAddress2	nvarchar (128)	='',
	@sCity		nvarchar (50)	='',
	@sState		nvarchar (5)	='',
	@sZip		nvarchar (15)	='',
	@sEmail		nvarchar (100)	='',
	@sPhone		nvarchar (25)	='',
	@iType 		int 			= 0,
	@iUserID	int 			= 0

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
		0, 
		@iUserID,
		GetDate(),
		@sEmail,
		@sPhone,
		@iType)

SELECT	@maxinstance
GO
