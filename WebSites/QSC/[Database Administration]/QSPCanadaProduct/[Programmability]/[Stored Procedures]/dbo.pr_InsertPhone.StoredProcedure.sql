USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertPhone]    Script Date: 06/07/2017 09:17:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertPhone]

	@iType			int,
	@iPhoneListID		int,
	@zPhoneNumber	varchar(50),
	@zBestTimeToCall	varchar(2000)

AS

	DECLARE @iPhoneID	int

	create table #temp
	(
		 NextInstance int
	)
	
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 23 -- PhoneNext
	select @iPhoneID = nextinstance from #temp
	truncate table #temp
				
	drop table #temp

	INSERT INTO	Phone
			(ID,
			Type,
			PhoneListID,
			PhoneNumber,
			BestTimeToCall)
	VALUES
			(@iPhoneID,
			@iType - 30500,
			@iPhoneListID,
			@zPhoneNumber,
			@zBestTimeToCall)

	SELECT	@iPhoneID
GO
