USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Account_GetBalance]    Script Date: 06/07/2017 09:17:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Account_GetBalance]
(
	@CampaignID	INT,
	@AccountID	INT,
	@ToDate		DATETIME
)

RETURNS NUMERIC(16, 2)

AS

BEGIN
	
	RETURN
	(
		SELECT	ISNULL(SUM(TransactionAmount), 0)
		FROM	UDF_Statement_GetDetails_WithBusLogic(@ToDate)
		WHERE	AccountID = ISNULL(@AccountID, AccountID)
		AND		CampaignID = ISNULL(@CampaignID, CampaignID)
	)

END
GO
