USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_CA_SchoolLookup]    Script Date: 06/07/2017 09:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[pr_get_CA_SchoolLookup]
 @AccountID  int,
 @CampaignID int = 0,
 @Verbose    bit = 1
AS
SET NOCOUNT ON

print 'Groups with this AccountID: '
select * from dbo.CCanadaAccount where [Id] = @AccountID

if @CampaignID <> 0
begin
	--one campaign
	if(exists(
		select * 
		  from dbo.Campaign 
		 where [ID] = @CampaignID 
			AND [Status] <> 37005 
			AND IsStaffOrder = 0 
			AND IsTestCampaign = 0
		))
	begin
		print 'Valid Campaigns with this CampaignID: ';
		select * from dbo.Campaign where [ID] = @CampaignID 
			AND [Status] <> 37005 AND IsStaffOrder = 0 AND IsTestCampaign = 0 ;
	end
	
	
	if(exists(
		select * 
		  from dbo.Campaign 
		 where [ID] = @CampaignID 
			AND ([Status] = 37005 OR IsStaffOrder = 1 OR IsTestCampaign = 1 )
		))
	begin
		print 'InValid Campaigns with this CampaignID: ';
		select * from dbo.Campaign where [ID] = @CampaignID 
			AND ([Status] = 37005 OR IsStaffOrder = 1 OR IsTestCampaign = 1 );
	end
	
	
	if @Verbose = 1
	begin
		print 'Programs for this Campaign: '
		select * from dbo.CampaignProgram where CampaignID = @CampaignID and DeletedTF <> 1;
	end
end
else
begin
	--all campaigns for this Account
	DECLARE CampaignCursor CURSOR FOR
	 SELECT DISTINCT [ID] 
	   FROM dbo.Campaign
	  WHERE BillToAccountID = @AccountID OR ShipToAccountID = @AccountID

	open CampaignCursor
	fetch next from CampaignCursor into @CampaignID
	
	while(@@fetch_status <> -1)
	begin
	
		if(exists(
			select * 
			  from dbo.Campaign 
			 where [ID] = @CampaignID 
				AND [Status] <> 37005 
				AND IsStaffOrder = 0 
				AND IsTestCampaign = 0
			))
		begin
			print 'Valid Campaigns with CampaignID ' + cast(@CampaignID as varchar) + ': ';
			select * from dbo.Campaign where [ID] = @CampaignID 
				AND [Status] <> 37005 AND IsStaffOrder = 0 AND IsTestCampaign = 0;
		end
		
		
		if(exists(
			select * 
			  from dbo.Campaign 
			 where [ID] = @CampaignID 
				AND ([Status] = 37005 OR IsStaffOrder = 1 OR IsTestCampaign = 1)
			))
		begin
			print 'InValid Campaigns with CampaignID ' + cast(@CampaignID as varchar) + ': ';
			select * from dbo.Campaign where [ID] = @CampaignID 
				AND ([Status] = 37005 OR IsStaffOrder = 1 OR IsTestCampaign = 1);
		end
		
		
		if @Verbose = 1
		begin
			print 'Programs for CampaignID ' + cast(@CampaignID as varchar) + ': ';
			select * from dbo.CampaignProgram where CampaignID = @CampaignID;
		end
		
		fetch next from CampaignCursor into @CampaignID

	end
	close CampaignCursor
	deallocate CampaignCursor
end
SET NOCOUNT OFF
GO
