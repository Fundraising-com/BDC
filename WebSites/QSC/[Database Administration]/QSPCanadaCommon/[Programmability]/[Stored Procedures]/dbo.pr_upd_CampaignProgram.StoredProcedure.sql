USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_CampaignProgram]    Script Date: 06/07/2017 09:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_upd_CampaignProgram]
  @CampaignID int,
  @ProgramID int,
  @IsPreCollect bit,
  @GroupProfit numeric(10, 2),
  @Choice bit,
  @UserIDModified UserID_UDDT
AS

--- 
--- Conditionally insert or remove an instance of a running program
--- JLC MAY-2004 Initial Version
---

DECLARE @IsPreCollectVar varchar(1)
SELECT @IsPreCollectVar = case @IsPreCollect
		when 1 then 'Y'
		when 0 then 'N'
	end 

DECLARE @ProgramCount int
SELECT @ProgramCount = Count(CampaignID) 
FROM CampaignProgram 
WHERE CampaignID = @CampaignID AND ProgramId = @ProgramID AND DeletedTF = 0

IF (@ProgramCount = 0 AND @Choice = 1 )
begin
	--No program yet, insert it
	--print 'IF (@ProgramCount = 0 AND @Choice = 1 )-No program yet, insert it'


	--but was there a logical delete previously ??
	DECLARE @ProgramCountLD int --Logical Delete	
	SELECT 
		@ProgramCountLD = Count(CampaignID) 
	FROM 
		CampaignProgram 
	WHERE 
		CampaignID = @CampaignID 
		AND ProgramId = @ProgramID
		AND DeletedTF = 1
		
	IF @ProgramCountLD > 0 
	BEGIN
		--logically UN-DELETE this item
		UPDATE 
			CampaignProgram
		SET
			DeletedTF 	= 0,
			IsPreCollect 	= @IsPreCollectVar,
			GroupProfit 	= @GroupProfit
			--ModifyDate = getdate(),
			--ModifiedBy = @UserIDModified
		WHERE
			CampaignID = @CampaignID 
			AND ProgramId = @ProgramID ;
	END
	ELSE
	BEGIN
		--do a true physical insert
		INSERT INTO CampaignProgram(
 			CampaignID, 
 			ProgramID,
			IsPreCollect,
			GroupProfit
			--ModifyDate
			--ModifiedBy
		)VALUES(
 			@CampaignID,
			@ProgramID,
			@IsPreCollectVar,
			@GroupProfit
			--getdate()
			--@UserIDModified
		);
	END	
	return(0);

end
ELSE IF (@ProgramCount = 0 AND @Choice = 0)
begin
	--DO NOTHING (no program, none wanted)
	--print 'ELSE IF (@ProgramCount = 0 AND @Choice = 0)-DO NOTHING (no program, none wanted)'
	return(0);
end
ELSE  IF (@ProgramCount = 1 AND @Choice = 1)
begin
	 --DO NOTHING (user is happy with the choice of running program)
	--print 'ELSE  IF (@ProgramCount = 1 AND @Choice = 1)-DO NOTHING (user is happy with the choice of running program)'
	return(0);
end
ELSE  IF (@ProgramCount = 1 AND @Choice = 0)
begin
	--user wants to delete an existing program
	--print 'ELSE  IF (@ProgramCount = 1 AND @Choice = 0)-user wants to delete an existing program'

	--check to see if it can be deleted
	DECLARE @Activity int
	--SELECT @Activity = count(ordersplaced) etc find out this rule	

	SELECT @Activity = 0
	if (@Activity > 0)
	begin
		--this deletion is not allowed
		return (-5);
	end
	else
	begin
		--yeah, do the deletion
		UPDATE 
			CampaignProgram
		SET
			DeletedTF = 1
			--ModifyDate = getdate(),
			--ModifiedBy = @UserIDModified
		WHERE
			CampaignID = @CampaignID 
			AND ProgramId = @ProgramID ;

		return(0);
	end
end
GO
