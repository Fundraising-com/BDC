USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdatePremium]    Script Date: 06/07/2017 09:18:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdatePremium]

	@iPremiumID		int,
	@zPremiumCode	varchar(20),
	@iYear			int,
	@zSeason		varchar(1),
	@iIsActive		int,
	@zEnglishDescription	varchar(100),
	@zFrenchDescription	varchar(100),
	@zUserID		varchar(10)

AS

	DECLARE	@iDescriptionCount	int

	UPDATE	Premium
	SET		Year = @iYear,
			Season = @zSeason,
			Code = @zPremiumCode,
			Valid = CASE @iIsActive WHEN 1 THEN 'Y' ELSE 'N' END,
			DateModified = getdate(),
			UserIDModified = @zUserID
	WHERE	ID = @iPremiumID

	SELECT	@iDescriptionCount = count(PremiumID)
	FROM		PremiumDescription
	WHERE	PremiumID = @iPremiumID
	AND		Lang = 'EN'

	if(@iDescriptionCount > 0)
	begin
		UPDATE	PremiumDescription
		SET		Description = @zEnglishDescription,
				DateChanged = getdate(),
				UserIDChanged = @zUserID
		WHERE	PremiumID = @iPremiumID
		AND		Lang = 'EN'
	end
	else
	begin
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
	end

	SELECT	@iDescriptionCount = count(PremiumID)
	FROM		PremiumDescription
	WHERE	PremiumID = @iPremiumID
	AND		Lang = 'FR'

	if(@iDescriptionCount > 0)
	begin
		UPDATE	PremiumDescription
		SET		Description = @zFrenchDescription,
				DateChanged = getdate(),
				UserIDChanged = @zUserID
		WHERE	PremiumID = @iPremiumID
		AND		Lang = 'FR'
	end
	else
	begin
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
	end
GO
