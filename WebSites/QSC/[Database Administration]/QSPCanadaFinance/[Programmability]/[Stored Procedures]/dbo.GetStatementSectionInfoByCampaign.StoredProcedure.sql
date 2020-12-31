USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetStatementSectionInfoByCampaign]    Script Date: 06/07/2017 09:17:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[GetStatementSectionInfoByCampaign]

	@CampaignID		INT,
	@Realtime		BIT,
	@DateTo			DATETIME = NULL, 
	@StatementID	INT = NULL,
	@DateFrom		DATETIME = NULL

AS

SET NOCOUNT ON


IF @Realtime = CONVERT(BIT, 1)
	BEGIN
		SELECT		AccountID,
					CampaignID,
					StatementDetailTypeID,
					GroupingTransactionID,
					OrderID,
					TransactionDate,
					TransactionTypeName,
					Reference,
					TransactionAmount,
					CASE
						WHEN TransactionAmount > 0 THEN	TransactionAmount
						ELSE							0
					END AS DebitAmount,
					CASE
						WHEN TransactionAmount < 0 THEN	TransactionAmount
						ELSE							0
					END AS CreditAmount
		FROM		dbo.UDF_Statement_GetDetails_WithBusLogic_Aggregated(@DateFrom, @DateTo)
		WHERE		CampaignID = @CampaignID
		ORDER BY	StatementDetailTypeID,
					ISNULL(TransactionDate, '2099-12-31')
	END
	
ELSE

	BEGIN

		DECLARE	@StatementDate		DATETIME

		IF ISNULL(@StatementID, 0) > 0

			BEGIN

				SELECT	@StatementDate = CONVERT(VARCHAR(10), DATEADD(DAY, 1, stat.StatementDate), 120) --include transactions until midnight of the statement date
				FROM	[Statement] stat
				WHERE	stat.StatementID = @StatementID
				
				SELECT		stat.AccountID,
							stat.CampaignID,
							sd.StatementDetailTypeID,
							sd.GroupingTransactionID,
							sd.OrderID,
							sd.TransactionDate,
							sd.TransactionTypeName,
							sd.Reference,
							sd.TransactionAmount,
							CASE
								WHEN TransactionAmount > 0 THEN	TransactionAmount
								ELSE							0
							END AS DebitAmount,
							CASE
								WHEN TransactionAmount < 0 THEN	TransactionAmount
								ELSE							0
							END AS CreditAmount
				FROM		[Statement] stat
				JOIN		UDF_Statement_GetDetails_Aggregated(@StatementDate) sd
								ON	sd.StatementID = stat.StatementID
				WHERE		stat.StatementID = @StatementID
				ORDER BY	sd.StatementDetailTypeID,
							sd.TransactionDate		
							
			END
			
		ELSE
		
			BEGIN

				SELECT	@StatementID = MAX(StatementID)
				FROM	[Statement]
				WHERE	CampaignID = @CampaignID

				SELECT	@StatementDate = CONVERT(VARCHAR(10), DATEADD(DAY, 1, stat.StatementDate), 120) --include transactions until midnight of the statement date
				FROM	[Statement] stat
				WHERE	stat.StatementID = @StatementID

				SELECT		stat.AccountID,
							stat.CampaignID,
							sd.StatementDetailTypeID,
							sd.GroupingTransactionID,
							sd.OrderID,
							sd.TransactionDate,
							sd.TransactionTypeName,
							sd.Reference,
							sd.TransactionAmount,
							CASE
								WHEN TransactionAmount > 0 THEN	TransactionAmount
								ELSE							0
							END AS DebitAmount,
							CASE
								WHEN TransactionAmount < 0 THEN	TransactionAmount
								ELSE							0
							END AS CreditAmount
				FROM		[Statement] stat
				JOIN		UDF_Statement_GetDetails_Aggregated(@StatementDate) sd
								ON	sd.StatementID = stat.StatementID
				WHERE		stat.StatementID = @StatementID
				ORDER BY	sd.StatementDetailTypeID,
							sd.TransactionDate
							
			END
					
	END
	
SET NOCOUNT OFF
GO
