USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_validate_member_hierarchy]    Script Date: 02/14/2014 13:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION 
[dbo].[es_validate_member_hierarchy] (
@member_hierarchy_id  int,
@member_id int,
@parent_member_hierarchy_id int)

RETURNS @T TABLE
   (
   	member_hierarchy_id int,
	validate_state	int
   )
AS

/*
	return validate_state are
	0 = OK
	1 = Exist member_hierachy with @member_id and @parent_member_hierarchy_id
*/

BEGIN

DECLARE @lcmember_hierarchy_id int
DECLARE @lcstatus int

SET @lcstatus = 0

	IF (@member_hierarchy_id = null) 
	begin
		set @lcmember_hierarchy_id = null
		-- INSERT
		select 
			@lcmember_hierarchy_id = member_hierarchy_id 
		from
			 member_hierarchy 
		where
			 @member_id is not null and member_id = @member_id 
			and 
			(@parent_member_hierarchy_id IS NOT NULL AND parent_member_hierarchy_id = @parent_member_hierarchy_id)

		IF @lcmember_hierarchy_id IS NOT NULL
		BEGIN			
			set @lcstatus = 10
			INSERT INTO @T (member_hierarchy_id, validate_state)
			VALUES (@lcmember_hierarchy_id, @lcstatus )
			RETURN
		END
	end
	ELSE
	begin
		set @lcmember_hierarchy_id = @member_hierarchy_id
		if(exists(select member_hierarchy_id 
		from
			 member_hierarchy 
		where
			 @member_id is not null and member_id = @member_id 
			and 
				(@parent_member_hierarchy_id IS NOT NULL AND parent_member_hierarchy_id = @parent_member_hierarchy_id)
			and 
				member_hierarchy_id != @member_hierarchy_id
		))
		BEGIN			
			set @lcstatus = 1
			INSERT INTO @T (member_hierarchy_id, validate_state)
			VALUES (@lcmember_hierarchy_id, @lcstatus )
			RETURN
		END
	end
	


INSERT INTO @T (member_hierarchy_id, validate_state)
VALUES (@lcmember_hierarchy_id, @lcstatus )

return
	
END
GO
