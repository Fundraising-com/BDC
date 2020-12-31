USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertPremium]    Script Date: 06/07/2017 09:17:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertPremium]

	@zPremiumCode	varchar(20),
	@iYear			int,
	@zSeason		varchar(1),
	@iIsActive		int,
	@zEnglishDescription	varchar(100),
	@zFrenchDescription	varchar(100),
	@zUserID		varchar(10)

AS

	DECLARE @iPremiumID	int

	create table #temp
	(
		 NextInstance int
	)
	
	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 24 -- PremiumNext
	select @iPremiumID = nextinstance from #temp
	truncate table #temp
				
	drop table #temp

	INSERT INTO	Premium
			(ID,
			Year,
			Season,
			Code,
			Valid,
			CountryCode,
			DateCreated,
			UserIDCreated,
			DateModified,
			UserIDModified)
	VALUES
			(@iPremiumID,
			@iYear,
			@zSeason,
			@zPremiumCode,
			CASE @iIsActive WHEN 1 THEN 'Y' ELSE 'N' END,
			'CA',
			getdate(),
			@zUserID,
			getdate(),
			@zUserID)

	INSERT INTO	PremiumDescription
			(PremiumID,
			Lang,
			Description,
			DateCreated,
			UserIDCreated,
			DateChanged,
			UserIDChanged)
	VALUES
			(@iPremiumID,
			'EN',
			@zEnglishDescription,
			getdate(),
			@zUserID,
			getdate(),
			@zUserID)

	INSERT INTO	PremiumDescription
			(PremiumID,
			Lang,
			Description,
			DateCreated,
			UserIDCreated,
			DateChanged,
			UserIDChanged)
	VALUES
			(@iPremiumID,
			'FR',
			@zFrenchDescription,
			getdate(),
			@zUserID,
			getdate(),
			@zUserID)

	SELECT	@iPremiumID
GO
