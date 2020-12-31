USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CAccount_UpdateGroupOnlineID]    Script Date: 06/07/2017 09:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CAccount_UpdateGroupOnlineID]

AS

UPDATE	CAccount
SET		GroupOnlineID = CAST(c.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(c.ContractID) as varchar)
FROM	QSPCanadaCommon..CAccount acc
JOIN	QSPCanadaCommon..Campaign camp ON camp.BillToAccountID = acc.Id
JOIN	GA.core.Contract c on c.SAPContractNo = camp.ID AND c.DivisionCode = 40 AND c.ContractTypeID IN (3, 4)
GO
