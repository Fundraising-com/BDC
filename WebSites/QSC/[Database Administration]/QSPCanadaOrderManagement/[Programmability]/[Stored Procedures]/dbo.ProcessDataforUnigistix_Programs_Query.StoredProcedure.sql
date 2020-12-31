USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ProcessDataforUnigistix_Programs_Query]    Script Date: 06/07/2017 09:20:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[ProcessDataforUnigistix_Programs_Query] @orderid int    AS

	
 /*

 Select 	Batch.ID as BatchID,
	Convert(varchar(10),Batch.Date,101) as BatchDate,
	Batch.OrderID as OrderID,
	Campaign.CampaignID as CampaignID,
        	Campaign.ProgramID   as ProgramID,
        	Campaign.GroupProfit,
	Campaign.IsPreCollect,
	Program.Name as programName,
	ProgramType.Description as ProgramTypeDesc
 From 	QSPCanadaOrderManagement..Batch as Batch,
	QSPCanadaCommon..CampaignProgram as Campaign,
	QSPCanadaCommon..Program as Program,
	QSPCanadaCommon..CodeDetail as ProgramType
Where 	Batch.Campaignid  = Campaign.CampaignID
	and Campaign.programID = Program.id 
	and Program.ProgramTypeID = ProgramType.Instance
	and batch.id 	= @BatchID
	and batch.date 	= @BatchDate
Order by Program.ID
FOR XML AUTO,ELEMENTS
*/


	Select 	Batch.ID as BatchID,
		Convert(varchar(10),Batch.Date,101) as BatchDate,
		Batch.OrderID as OrderID,
		Batch.CampaignID as CampaignID,
		Distribution = case Dist.IncentivesDistributionID
				when 62001 then 'GROUP'
				when 62003 then 'PARTICIPANT'
				when 62002 then 'CLASSROOM'
				end,
	        	Campaign.ProgramID   as ProgramID,
	        	isnull(Campaign.GroupProfit,0.00) as GroupProfit,
		Campaign.IsPreCollect,
		programName,
		ProgramTypeDesc
	 From 	QSPCanadaOrderManagement..Batch as Batch,
		qspcanadacommon..campaign as Dist,
		unigistixtemp as Campaign
	Where 	Batch.OrderID  = Campaign.orderid
		and Batch.orderid = @orderid
and Dist.id=batch.Campaignid

FOR XML AUTO,ELEMENTS
GO
