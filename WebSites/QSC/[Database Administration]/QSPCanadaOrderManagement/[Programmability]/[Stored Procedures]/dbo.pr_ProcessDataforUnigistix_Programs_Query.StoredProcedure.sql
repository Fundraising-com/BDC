USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessDataforUnigistix_Programs_Query]    Script Date: 06/07/2017 09:20:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE        PROCEDURE [dbo].[pr_ProcessDataforUnigistix_Programs_Query] @orderid int    AS


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
		vw_unigistix as Campaign
	Where 	Batch.OrderID  = Campaign.orderid
		and Batch.orderid = @orderid
		and Dist.id=batch.Campaignid


FOR XML AUTO,ELEMENTS
GO
