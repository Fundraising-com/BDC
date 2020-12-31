USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_Unigistix]    Script Date: 06/07/2017 09:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Unigistix] AS

	SELECT 	Batch.OrderID as OrderID,
		Batch.CampaignID,
	        Campaign.ProgramID   as ProgramID,
	        Campaign.GroupProfit,
		Campaign.IsPreCollect,
		Program.Name as programName,
		ProgramType.Description as ProgramTypeDesc   

	FROM	QSPCanadaOrderManagement..Batch as Batch,
		QSPCanadaCommon..CampaignProgram as Campaign,
		QSPCanadaCommon..Program as Program,
		QSPCanadaCommon..CodeDetail as ProgramType
	WHERE 	Batch.Campaignid  = Campaign.CampaignID
		and Campaign.programID = Program.id 
		and Program.ProgramTypeID = ProgramType.Instance
		and Campaign.DeletedTF = 0
GO
