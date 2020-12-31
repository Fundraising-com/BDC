USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_RegisterTaxMag]    Script Date: 06/07/2017 09:17:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_RegisterTaxMag]

	@zUMC			varchar(20),
	@zGSTRegistrationNumber	varchar(20),
	@zHSTRegistrationNumber	varchar(20),
	@zPSTRegistrationNumber	varchar(20)

AS

	DECLARE @iTaxCount	int

	-- GST
	IF(@zGSTRegistrationNumber <> '')
	BEGIN
		SELECT	@iTaxCount = COUNT(*)
		FROM		QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 1

		IF(@iTaxCount = 0)
		BEGIN
			INSERT INTO	QSPCanadaCommon..TaxMagRegistration
					(TITLE_CODE,
					TAX_ID,
					TAX_REGISTRATION_NUMBER)
			VALUES	(@zUMC,
					1,
					@zGSTRegistrationNumber)
		END
		ELSE
		BEGIN
			UPDATE	QSPCanadaCommon..TaxMagRegistration
			SET		TAX_REGISTRATION_NUMBER = @zGSTRegistrationNumber
			WHERE	TITLE_CODE = @zUMC
			AND		TAX_ID = 1
		END
	END
	ELSE
	BEGIN
		DELETE FROM	QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 1
	END

	-- HST
	-- 3 different HST rows (2, 4, 5, 6, 7) but same value for each
	IF(@zHSTRegistrationNumber <> '')
	BEGIN
		SELECT	@iTaxCount = COUNT(*)
		FROM		QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 2

		IF(@iTaxCount = 0)
		BEGIN
			INSERT INTO	QSPCanadaCommon..TaxMagRegistration
					(TITLE_CODE,
					TAX_ID,
					TAX_REGISTRATION_NUMBER)
			VALUES	(@zUMC,
					2,
					@zHSTRegistrationNumber)
		END
		ELSE
		BEGIN
			UPDATE	QSPCanadaCommon..TaxMagRegistration
			SET		TAX_REGISTRATION_NUMBER = @zHSTRegistrationNumber
			WHERE	TITLE_CODE = @zUMC
			AND		TAX_ID = 2
		END


		SELECT	@iTaxCount = COUNT(*)
		FROM		QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 4

		IF(@iTaxCount = 0)
		BEGIN
			INSERT INTO	QSPCanadaCommon..TaxMagRegistration
					(TITLE_CODE,
					TAX_ID,
					TAX_REGISTRATION_NUMBER)
			VALUES	(@zUMC,
					4,
					@zHSTRegistrationNumber)
		END
		ELSE
		BEGIN
			UPDATE	QSPCanadaCommon..TaxMagRegistration
			SET		TAX_REGISTRATION_NUMBER = @zHSTRegistrationNumber
			WHERE	TITLE_CODE = @zUMC
			AND		TAX_ID = 4
		END


		SELECT	@iTaxCount = COUNT(*)
		FROM		QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 5

		IF(@iTaxCount = 0)
		BEGIN
			INSERT INTO	QSPCanadaCommon..TaxMagRegistration
					(TITLE_CODE,
					TAX_ID,
					TAX_REGISTRATION_NUMBER)
			VALUES	(@zUMC,
					5,
					@zHSTRegistrationNumber)
		END
		ELSE
		BEGIN
			UPDATE	QSPCanadaCommon..TaxMagRegistration
			SET		TAX_REGISTRATION_NUMBER = @zHSTRegistrationNumber
			WHERE	TITLE_CODE = @zUMC
			AND		TAX_ID = 5
		END

		SELECT	@iTaxCount = COUNT(*)
		FROM		QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 6

		IF(@iTaxCount = 0)
		BEGIN
			INSERT INTO	QSPCanadaCommon..TaxMagRegistration
					(TITLE_CODE,
					TAX_ID,
					TAX_REGISTRATION_NUMBER)
			VALUES	(@zUMC,
					6,
					@zHSTRegistrationNumber)
		END
		ELSE
		BEGIN
			UPDATE	QSPCanadaCommon..TaxMagRegistration
			SET		TAX_REGISTRATION_NUMBER = @zHSTRegistrationNumber
			WHERE	TITLE_CODE = @zUMC
			AND		TAX_ID = 6
		END

		SELECT	@iTaxCount = COUNT(*)
		FROM		QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 7

		IF(@iTaxCount = 0)
		BEGIN
			INSERT INTO	QSPCanadaCommon..TaxMagRegistration
					(TITLE_CODE,
					TAX_ID,
					TAX_REGISTRATION_NUMBER)
			VALUES	(@zUMC,
					7,
					@zHSTRegistrationNumber)
		END
		ELSE
		BEGIN
			UPDATE	QSPCanadaCommon..TaxMagRegistration
			SET		TAX_REGISTRATION_NUMBER = @zHSTRegistrationNumber
			WHERE	TITLE_CODE = @zUMC
			AND		TAX_ID = 7
		END
	END
	ELSE
	BEGIN
		DELETE FROM	QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID IN (2, 4, 5, 6, 7)
	END

	-- PST
	IF(@zPSTRegistrationNumber <> '')
	BEGIN
		SELECT	@iTaxCount = COUNT(*)
		FROM		QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 3

		IF(@iTaxCount = 0)
		BEGIN
			INSERT INTO	QSPCanadaCommon..TaxMagRegistration
					(TITLE_CODE,
					TAX_ID,
					TAX_REGISTRATION_NUMBER)
			VALUES	(@zUMC,
					3,
					@zPSTRegistrationNumber)
		END
		ELSE
		BEGIN
			UPDATE	QSPCanadaCommon..TaxMagRegistration
			SET		TAX_REGISTRATION_NUMBER = @zPSTRegistrationNumber
			WHERE	TITLE_CODE = @zUMC
			AND		TAX_ID = 3
		END
	END
	ELSE
	BEGIN
		DELETE FROM	QSPCanadaCommon..TaxMagRegistration
		WHERE	TITLE_CODE = @zUMC
		AND		TAX_ID = 3
	END
GO
