USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_validate_event_participation]    Script Date: 02/14/2014 13:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_validate_event_participation] (@eventParticipantID int, @eventID int, @memberHierarchyID int)  
RETURNS @T TABLE(
	event_participant_id INT,
	validate_state INT
)
AS  
BEGIN 

-- status of the validation
declare @status int
declare @lc_event_participantid int

-- set status to ok for now
set @status = 0
set @lc_event_participantid = -1


if(@eventParticipantID is null)
begin
	-- Insert
	--if(exists(select event_participant_id from [event_participant] where event_id =  @eventID and member_hierarchy_id = @memberHierarchyID))
	set @lc_event_participantid = null
	select @lc_event_participantid= event_participation_id from dbo.event_participation 
	where 
		@eventID is not null and event_id =  @eventID
		and
		@memberHierarchyID is not null and member_hierarchy_id = @memberHierarchyID

	if (@lc_event_participantid is not null )
	begin
		set @status = 1
		INSERT INTO @T (event_participant_id, validate_state)
		VALUES (@lc_event_participantid, @status)
		RETURN
	end
end
else
begin
	-- Update
	-- Check if the member_hierachy_id already exists in the database
	--if(exists(select event_participant_id from [event_participant] where event_participant_id !=  @eventParticipantID and member_hierarchy_id = @memberHierarchyID and event_id = @eventID))
	set @lc_event_participantid = @eventParticipantID
	if(exists(select event_participation_id from dbo.event_participation 
	where 
		event_participation_id !=  @eventParticipantID
		and
		(@eventID is not null and event_id =  @eventID)
		and 
		(@memberHierarchyID is not null and member_hierarchy_id = @memberHierarchyID)
	))
	begin
		set @status = 1
		INSERT INTO @T (event_participant_id, validate_state)
		VALUES (@lc_event_participantid, @status)
		RETURN
	end
end

INSERT INTO @T (event_participant_id, validate_state)
VALUES (@lc_event_participantid, @status)
RETURN

END
GO
