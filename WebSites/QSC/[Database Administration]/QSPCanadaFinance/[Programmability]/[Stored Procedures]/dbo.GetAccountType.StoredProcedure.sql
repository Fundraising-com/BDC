USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAccountType]    Script Date: 06/07/2017 09:17:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAccountType]
	@AccountID 	int,
	@AccountType   int output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/6/2004 
--   Find out Account Type For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT TOP 1 @AccountType = C.Type
FROM QSPCanadaCommon..CAccount A
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH 
		on COH.AccountID = A.ID
	INNER JOIN QSPCanadaOrderManagement..Customer C 
		on COH.CustomerBillToInstance = C.Instance
WHERE A.ID = @AccountID

SET NOCOUNT OFF
GO
