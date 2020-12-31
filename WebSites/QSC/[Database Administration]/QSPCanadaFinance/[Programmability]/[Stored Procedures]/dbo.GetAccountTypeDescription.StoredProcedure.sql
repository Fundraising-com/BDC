USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAccountTypeDescription]    Script Date: 06/07/2017 09:17:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAccountTypeDescription]
	@AccountID 	int,
	@AccountType   varchar(50) output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/2/2004 
--   Find out Account Type Description For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT TOP 1 @AccountType = CD.Description
FROM QSPCanadaCommon..CAccount A
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH 
		on COH.AccountID = A.ID
	INNER JOIN QSPCanadaOrderManagement..Customer C 
		on COH.CustomerBillToInstance = C.Instance
	INNER JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = C.Type
WHERE A.ID = @AccountID

SET NOCOUNT OFF
GO
