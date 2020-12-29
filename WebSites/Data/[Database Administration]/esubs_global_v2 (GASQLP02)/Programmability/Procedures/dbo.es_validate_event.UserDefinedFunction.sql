USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_validate_event]    Script Date: 02/14/2014 13:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*  Validation of the group. (Please log any changes with author, date and validation rules)
 *  
 *  
 *  Version 1.0
 *  	Created by JF Buist on April 5, 2006
 *
 *  	Every stored procedures that creates/update the event should call this function to apply validation rules.
 *
 *  	Integrity rules:
 *		RETURN 0: Validation Successfull
 * 		RETURN 1: Redirect already exists
 *
 */

CREATE FUNCTION [dbo].[es_validate_event] (@eventID int, @redirect varchar(256))  
RETURNS @T TABLE
   (
   	event_id	int,
	validate_state	int
   )
AS  
BEGIN 

-- UPDATE BY JIRO: NOV 2013
--   Redirect no longer applies to event table. I didnt know about this function so it was a bug!

-- status of the validation
declare @status int
declare @lc_event_id int
-- set status to ok for now
set @status = 0
set @lc_event_id = -1
-- If the event_id is null, it means the validation is an create, else it is an update
/*
if(@eventID is null)
begin
	-- Check if the redirect already exists in the database	
	--if(exists(select event_id from [event] where redirect = @redirect))
	set @lc_event_id = null
	select @lc_event_id = event_id from [event] 
	where 
		@redirect is not null and redirect = @redirect 
		and 
		active =1
	if (@lc_event_id IS NOT NULL)
	begin
		set @status = 1
		INSERT INTO @T (event_id, validate_state)
		VALUES (@lc_event_id, @status)
		return
	end
end
else
begin
	-- Check if the redirect already exists in the database 
	--if(exists(select event_id from [event] where redirect = @redirect and event_id != @eventID))
	set @lc_event_id = @eventID
	if(exists(select event_id from [event] 
	where
		(@redirect is not null AND redirect = @redirect) 
		and 
		event_id != @eventID
		and 
		active =1
	))
	begin
		set @status = 1
		INSERT INTO @T (event_id, validate_state)
		VALUES	(@lc_event_id, @status)
		return
	end
end
*/

INSERT INTO @T (event_id, validate_state)
VALUES	(@lc_event_id, @status)

RETURN

END
GO
