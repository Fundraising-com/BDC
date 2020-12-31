USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Account_GetGroupOnlineID]    Script Date: 06/07/2017 09:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Account_GetGroupOnlineID]
(
	@AccountID INT
)

RETURNS INT

AS

BEGIN
	
	RETURN
	(
		SELECT		GroupOnlineID
		FROM		QSPCanadaCommon..CAccount
		WHERE		Id = @AccountID
						
		/*SELECT		CAST(c.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(c.ContractID) as varchar) AS GroupOnlineID
		FROM		QSPCanadaCommon..Campaign camp
        JOIN		GA.core.Contract c on c.SAPContractNo = camp.ID AND c.DivisionCode = 40 AND c.ContractTypeID = 3
		WHERE		camp.BillToAccountID = @AccountID
		AND			camp.IsStaffOrder = 0*/
		
		/*SELECT		TOP 1 CAST(camp.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(camp.ContractID) as varchar) AS GroupOnlineID
		FROM		QSPCanadaCommon..Campaign camp
		WHERE		camp.ContractID > 0
		AND			camp.BillToAccountID = @AccountID
		AND			camp.ID IN (SELECT cp.CampaignID FROM CampaignProgram cp WHERE cp.ProgramID IN (1, 2, 44, 50, 52, 54, 55) AND cp.DeletedTF = 0)
		AND			camp.IsTestCampaign = 0
		AND			camp.[Status] NOT IN (37005, 37008, 37009)
		AND			camp.IsStaffOrder = 0
		ORDER BY	camp.ContractID DESC*/
	)

END
GO
