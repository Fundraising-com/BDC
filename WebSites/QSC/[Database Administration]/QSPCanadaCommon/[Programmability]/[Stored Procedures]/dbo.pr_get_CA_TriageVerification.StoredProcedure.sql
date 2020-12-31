USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_CA_TriageVerification]    Script Date: 06/07/2017 09:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE      PROCEDURE [dbo].[pr_get_CA_TriageVerification]
 @AccountID  int,
 @CampaignID int = 0
AS

DECLARE @reason varchar(255)
DECLARE @status bit
SELECT  @status = 1, @reason = ''

IF NOT
(
	--@status = 1 and 
	exists(select * from dbo.CAccount where [Id] = @AccountID)
)
begin
	select 
		  @status = 0
		, @reason = @reason + ' - Missing in CAccount'
end
/*
else
begin
	--select @reason = @reason + ',Common-CAccount'
end
*/

IF NOT
(
	--@status = 1 and 
	exists(select * from QSPCanadaOrderManagement.dbo.Account where [ID] = @AccountID)
)
begin
	select 
		  @status = 0
		, @reason = @reason + ' - Missing in OM-Account'
end


IF 
  @CampaignID <> 0 AND 
  NOT
(
	--@status = 1 and 
	exists(select * from dbo.Campaign where [ID] = @CampaignID 
		AND BillToAccountID = @AccountID and StartDate BETWEEN '07/01/2004' AND '06/30/2005')
)
begin
	select @status = 0

	if exists(select * from dbo.Campaign where [ID] = @CampaignID 
		 and StartDate NOT BETWEEN '07/01/2004' AND '06/30/2005')
	begin
		select @reason = @reason + ' - CA date not in FY'
	end

	if exists(select * from dbo.Campaign where [ID] = @CampaignID 
		 AND BillToAccountID <> @AccountID)
	begin
		select @reason = @reason + ' - CA has bad BillToAccountID'
	end

	if not exists(select * from dbo.Campaign where [ID] = @CampaignID)
	begin
		select @reason = @reason + ' - Missing in Campaign'
	end
end

declare @msg varchar(125)
declare @GR varchar(7)
declare @CA varchar(7)

if @CampaignID = 0
begin
	SELECT @GR = cast(@AccountID as varchar), @CA = '??'
end
else
begin
	SELECT @GR = cast(@AccountID as varchar), @CA = cast(@CampaignID as varchar)
end

while(len(@GR) < 6)
begin
	select @GR = ' ' + @GR
end
while(len(@CA) < 6)
begin
	select @CA = ' ' + @CA
end

select @msg = '--Group: ' + @GR + ' Campaign : ' + @CA

IF @status = 0
begin
	select @msg = @msg + ' - WILL NOT appear for triage' + @reason
end
else if @status = 1 and @CampaignID <> 0
begin
	select @msg = @msg + ' - will appear for triage.'
end
else if @status = 1 and @CampaignID = 0
begin
	select @msg = @msg + ' - will appear for triage - given a valid Campaign #.'
end

print @msg
GO
