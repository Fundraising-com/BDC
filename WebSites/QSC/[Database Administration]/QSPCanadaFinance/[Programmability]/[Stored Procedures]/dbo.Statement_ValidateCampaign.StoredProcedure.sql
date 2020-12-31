USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Statement_ValidateCampaign]    Script Date: 06/07/2017 09:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Statement_ValidateCampaign]

	@CampaignID			INT,
	@DateTo				DATETIME,
	@IsCampaignValid	BIT OUTPUT

AS

SET @IsCampaignValid = CONVERT(BIT, 1)

DECLARE	@AccountID				INT,
		@Error					BIT,
		@StatementErrorTypeID	INT

SELECT	@AccountID = acc.ID
FROM	QSPCanadaCommon..Campaign camp
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = camp.BillToAccountID
WHERE	camp.ID = @CampaignID

SELECT		TOP 1
			@Error = CONVERT(BIT, 1),
			@StatementErrorTypeID = 1 --1: Missing Campaign Contact
FROM		QSPCanadaCommon..Campaign camp
LEFT JOIN	QSPCanadaCommon..Contact cont
				ON	cont.ID = camp.BillToCampaignContactID
WHERE		(ISNULL(cont.FirstName, '') = ''
OR			ISNULL(cont.LastName, '') = '')
AND			camp.ID = @CampaignID

IF ISNULL(@Error, 0) = 1
BEGIN
	INSERT INTO StatementError
	(
		CreationDate,
		CampaignID,
		StatementErrorTypeID
	)
	VALUES
	(
		GETDATE(),
		@CampaignID,
		@StatementErrorTypeID
	)
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsCampaignValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1),
			@StatementErrorTypeID = 2 --2: Missing Account Name or Address
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
LEFT JOIN	QSPCanadaCommon..Address addBill
				ON	addBill.AddressListID = acc.AddressListID
				AND	addBill.Address_Type = 54002 --54002: BillTo
WHERE		(ISNULL(acc.Name, '') = ''
OR			ISNULL(addBill.Street1, '') = ''
OR			ISNULL(addBill.City, '') = ''
OR			ISNULL(addBill.StateProvince, '') = ''
OR			ISNULL(addBill.Postal_Code, '') = '')
AND			camp.ID = @CampaignID

IF ISNULL(@Error, 0) = 1
BEGIN
	INSERT INTO StatementError
	(
		CreationDate,
		CampaignID,
		StatementErrorTypeID
	)
	VALUES
	(
		GETDATE(),
		@CampaignID,
		@StatementErrorTypeID
	)
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsCampaignValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1),
			@StatementErrorTypeID = 3 --3: Missing FM Name or FMID
FROM		QSPCanadaCommon..Campaign camp
LEFT JOIN	QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
WHERE		(ISNULL(fm.FMID, '') = ''
OR			ISNULL(fm.FirstName, '') = ''
OR			ISNULL(fm.LastName, '') = '')
AND			camp.ID = @CampaignID

IF ISNULL(@Error, 0) = 1
BEGIN
	INSERT INTO StatementError
	(
		CreationDate,
		CampaignID,
		StatementErrorTypeID
	)
	VALUES
	(
		GETDATE(),
		@CampaignID,
		@StatementErrorTypeID
	)
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsCampaignValid = CONVERT(BIT, 0)
END

IF @IsCampaignValid = 1
BEGIN
	UPDATE	StatementError
	SET		IsFixed = 1
	WHERE	CampaignID = @CampaignID
END
GO
