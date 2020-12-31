USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_GroupOnlinePasswordReport]    Script Date: 06/07/2017 09:33:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GroupOnlinePasswordReport]

 @FMID varchar(4)
 
AS

SELECT		DISTINCT
			acc.Name AS GroupName,
			acc.Id AS GroupID,
			dbo.UDF_Account_GetGroupOnlineID(camp.BillToAccountID) GroupOnlineID,
			acc.Id AS SponsorPassword
FROM		Campaign camp
JOIN		CAccount acc ON acc.Id = camp.BillToAccountID
JOIN		FieldManager fm ON fm.FMID = camp.FMID
WHERE		camp.IsTestCampaign = 0
AND			camp.[Status] NOT IN (37005, 37007, 37008, 37009)
AND			dbo.UDF_Account_GetGroupOnlineID(camp.BillToAccountID) > 0
AND			dbo.UDF_Account_GetFMID(camp.BillToAccountID, DATEADD(mm, 6, GETDATE())) = @FMID
ORDER BY	acc.Name
GO
