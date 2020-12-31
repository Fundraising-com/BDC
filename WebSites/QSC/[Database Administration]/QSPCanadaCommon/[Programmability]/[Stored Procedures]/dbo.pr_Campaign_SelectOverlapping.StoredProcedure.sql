USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectOverlapping]    Script Date: 06/07/2017 09:33:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SelectOverlapping]

AS

select fm.FirstName + ' ' + fm.LastName FM, camp1.ID CampaignID1, camp2.ID CampaignID2, camp1.billtoaccountid AccountID, camp1.StartDate Campaign1StartDate, camp1.EndDate Campaign1EndDate, camp2.StartDate Campaign2StartDate, camp2.EndDate Campaign2EndDate
from QSPCanadaCommon..Campaign camp1
join QSPCanadaCommon..Campaign camp2 on camp2.BillToAccountID = camp1.BillToAccountID and camp1.ID < camp2.ID
join QSPCanadaCommon..FieldManager fm on fm.FMID = camp2.FMID
where (camp1.StartDate BETWEEN camp2.StartDate AND camp2.EndDate OR camp2.StartDate BETWEEN camp1.StartDate AND camp1.EndDate)
and camp1.StartDate >= '2016-07-01'
and camp1.IsStaffOrder = 0
and camp2.IsStaffOrder = 0
and camp1.Status = 37002
and camp2.Status = 37002
and camp1.ID in (select CampaignID from QSPCanadaCommon..CampaignProgram cp where cp.ProgramID IN (1, 2, 44, 50, 52, 54, 55, 56, 57, 58, 59) and cp.DeletedTF = 0)
and camp2.ID in (select CampaignID from QSPCanadaCommon..CampaignProgram cp where cp.ProgramID IN (1, 2, 44, 50, 52, 54, 55, 56, 57, 58, 59) and cp.DeletedTF = 0)
order by camp1.BillToAccountID
GO
