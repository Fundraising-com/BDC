USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_validate_group]    Script Date: 02/14/2014 13:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*  Validation of the group. (Please log any changes with author, date and validation rules)
 *  
 *  
 *  Version 1.0
 *  	Created by JF Buist on April 5, 2006
*	Adding sponsorID to verify if groupID and sponsorID is unique Jul 11, 2006
 *
 *  	Every stored procedures that creates/update the group should call this function to apply validation rules.
 *
 *  	Integrity rules:
 *		RETURN 0: Validation Successfull
 * 		RETURN 1: Partner ID and ExternalGroupID already exists.
 * 		RETURN 2: SponsorID already exist..
 *
 */
CREATE FUNCTION [dbo].[es_validate_group] (@groupID int, @partnerID int, @externalGroupID varchar(128), @sponsorID int)  
RETURNS @T TABLE (group_id int, validate_state int)

AS  
BEGIN 

-- status of the validation
declare @status int
declare @lc_group_id int
-- set status to ok for now
set @status = 0
set @lc_group_id = -1



-- If the group_id is null, it means the validation is an create, else it is an update
if(@groupID is null)
begin
	-- INSERT BLOCK

	-- Check if the sponsor id and group id is unique
	set @lc_group_id = null
	select @lc_group_id = group_id from [group] 
	where 
		@sponsorID is not null and sponsor_id = @sponsorID
	
	if (@lc_group_id is not null)
	begin
		set @status = 2
		INSERT INTO @T (group_id, validate_state)
		VALUES (@lc_group_id, @status)
		return
	end
	-- Check if the partner id and external group already exists in the database
	IF (@externalGroupID IS NOT NULL)
	BEGIN
		set @lc_group_id = null
		select @lc_group_id = group_id from [group] 
		where 
			@partnerID is not null and partner_id = @partnerID
			and 
			external_group_id = @externalGroupID

		if (@lc_group_id is not null)
		begin
			set @status = 1
			INSERT INTO @T (group_id, validate_state)
			VALUES (@lc_group_id, @status)
			return
		end
	END
end
else
begin
	-- UPDATE BLOCK

	-- Check if the sponsor id and group id is unique
	--if(exists(select group_id from [group] where sponsor_id = @sponsorID and group_id != @groupID))
	set @lc_group_id = @groupID
	if(exists(select  group_id from [group] 
	where 
		@sponsorID is not null and sponsor_id = @sponsorID
		and 
		group_id != @groupID
	))
	begin
		set @status = 2
		INSERT INTO @T (group_id, validate_state)
		VALUES (@lc_group_id, @status)
		return
	end
	-- Check if the partner id and external group already exists in the database for another group id
	IF (@externalGroupID IS NOT NULL)
	BEGIN
		if(exists(select group_id from [group] 
		where 
			@partnerID is not null and partner_id = @partnerID
			and 
			external_group_id = @externalGroupID
			and 
			group_id != @groupID
		))
		begin
			set @status = 1
			INSERT INTO @T (group_id, validate_state)
			VALUES (@lc_group_id, @status)
			return
		end
	END

	
end


INSERT INTO @T (group_id, validate_state)
VALUES (@lc_group_id, @status)

return

END
GO
