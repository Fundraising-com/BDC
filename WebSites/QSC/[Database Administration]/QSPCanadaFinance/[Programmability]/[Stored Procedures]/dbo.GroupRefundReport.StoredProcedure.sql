USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GroupRefundReport]    Script Date: 06/07/2017 09:17:23 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GroupRefundReport]

	@RefundAmountFrom	NUMERIC(10,2),
	@RefundAmountTo		NUMERIC(10,2),
	@DateCreatedFrom	DATETIME,
	@DateCreatedTo		DATETIME,
	@CampaignID			INT,
	@AccountID			INT,
	@SortBy				VARCHAR(10)

AS

IF @RefundAmountFrom = 0.00 SET @RefundAmountFrom = NULL
IF @RefundAmountTo = 0.00 SET @RefundAmountTo = NULL
IF @DateCreatedFrom = '1995-01-01' SET @DateCreatedFrom = NULL
IF @DateCreatedTo = '1995-01-01' SET @DateCreatedTo = NULL
IF @CampaignID = 0 SET @CampaignID = NULL
IF @AccountID = 0 SET @AccountID = NULL

SELECT		apcb.CreationDate AS DateSent,
			apc.ChequeNumber,
			ref.Campaign_ID,
			acc.ID AS Account_ID,
			acc.Name AS Account_Name,
			ref.CreateDate,
			ref.Amount
FROM		Refund ref
LEFT JOIN	(AP_Cheque apc
				JOIN	AP_Cheque_Batch apcb
							ON	apcb.AP_Cheque_Batch_ID = apc.AP_Cheque_Batch_ID)
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = ref.Campaign_ID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
WHERE		ref.Refund_Type_ID = 2 --2: Group Refund
AND			ref.Amount BETWEEN ISNULL(@RefundAmountFrom, ref.Amount) AND ISNULL(@RefundAmountTo, ref.Amount)
AND			ref.Campaign_ID = ISNULL(@CampaignID, ref.Campaign_ID)
AND			acc.ID = ISNULL(@AccountID, acc.ID)
AND			ref.CreateDate BETWEEN ISNULL(@DateCreatedFrom, ref.CreateDate) AND ISNULL(@DateCreatedTo, ref.CreateDate)
ORDER BY	CASE @SortBy
				WHEN 'AMOUNT' THEN	ref.Amount
			END,
			CASE @SortBy
				WHEN 'DATE' THEN	ref.CreateDate
			END,
			CASE @SortBy
				WHEN 'NAME' THEN	acc.Name
			END
GO
