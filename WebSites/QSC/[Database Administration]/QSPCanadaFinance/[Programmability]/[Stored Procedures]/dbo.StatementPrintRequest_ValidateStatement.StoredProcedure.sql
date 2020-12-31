USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequest_ValidateStatement]    Script Date: 06/07/2017 09:17:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequest_ValidateStatement]

	@StatementID				INT,
	@ForceStatementPrintRequest	BIT,
	@IsStatementValid			BIT OUTPUT

AS

SET @IsStatementValid = CONVERT(BIT, 1)

DECLARE	@AccountID							INT,
		@StatementRunDate					DATETIME,
		@Error								BIT,
		@StatementPrintRequestErrorTypeID	INT

SELECT	@AccountID = AccountID
FROM	[Statement]
WHERE	StatementID = @StatementID

SELECT	@StatementRunDate = statRun.StatementRunDate
FROM	[Statement] stat
JOIN	StatementRun statRun
			ON	statRun.StatementRunID = stat.StatementRunID
WHERE	stat.StatementID = @StatementID

IF @ForceStatementPrintRequest = CONVERT(BIT, 0) --If statement print request is forced then skip this check
BEGIN

	DECLARE @PriorCampaignOwing	BIT,
			@CampaignBalance	NUMERIC(12, 2)

	SELECT		@CampaignBalance = stat.Balance
	FROM		[Statement] stat
	WHERE		stat.StatementID = @StatementID

	IF @CampaignBalance < 0.00
	BEGIN
		/*SELECT		@AccountBalance = SUM(TransactionAmount)
		FROM		dbo.UDF_Statement_GetDetails_WithBusLogic(@StatementRunDate) sdAcc
		JOIN		QSPCanadaCommon..Campaign camp
						ON	camp.ID = sdAcc.CampaignID
		WHERE		camp.StartDate >= '2009-07-01' --Start of account balance calculation
		AND			sdAcc.AccountID = @AccountID
		GROUP BY	sdAcc.AccountID
		HAVING		ISNULL(SUM(sdAcc.TransactionAmount), 0.00) > 0.00*/
		
		DECLARE	@CurrentSeasonStartDate DATETIME

		SELECT	@CurrentSeasonStartDate = s.StartDate
		FROM	QSPCanadaCommon..Season s
		WHERE	GETDATE() BETWEEN s.StartDate AND s.EndDate
		AND		s.Season IN ('F','S')

		SELECT		@PriorCampaignOwing = CONVERT(BIT, 1)
		FROM		dbo.UDF_Statement_GetDetails_WithBusLogic(@StatementRunDate) sdAcc
		JOIN		QSPCanadaCommon..Campaign camp
						ON	camp.ID = sdAcc.CampaignID
		WHERE		camp.StartDate >= '2012-01-01' --Start of account balance calculation
		AND			sdAcc.AccountID = @AccountID
		AND			camp.StartDate < @CurrentSeasonStartDate
		GROUP BY	camp.ID
		HAVING		SUM(TransactionAmount) > 5.00
		
	END
	
	print @PriorCampaignOwing
	print ISNULL(@PriorCampaignOwing, 0)

	IF @CampaignBalance < 0.00 AND ISNULL(@PriorCampaignOwing, 0) = 1
	BEGIN
		SET @StatementPrintRequestErrorTypeID = 1 --1: Account in a state of debit, Campaign in a state of credit

		INSERT INTO StatementPrintRequestError
		(
			CreationDate,
			StatementID,
			StatementPrintRequestErrorTypeID
		)
		VALUES
		(
			GETDATE(),
			@StatementID,
			@StatementPrintRequestErrorTypeID
		)
		
		SET @Error = CONVERT(BIT, 0)
		SET @IsStatementValid = CONVERT(BIT, 0)
	END
END
GO
