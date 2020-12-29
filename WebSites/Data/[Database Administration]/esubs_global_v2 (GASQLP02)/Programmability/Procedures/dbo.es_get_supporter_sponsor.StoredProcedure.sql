USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_supporter_sponsor]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	Created by: Philippe Girard
	Date: 4 august 2005
	
	Description: return sponsor for a supporter
*/
CREATE PROC [dbo].[es_get_supporter_sponsor]
	@member_hierarchy_id int
	, @sponsor_id int OUTPUT
AS
BEGIN
	DECLARE @parent_hierarchy_id int
	DECLARE @current_hierarchy_id int

	SELECT @parent_hierarchy_id = parent_member_hierarchy_id
	FROM member_hierarchy
	WHERE member_hierarchy_id = @member_hierarchy_id

	WHILE @parent_hierarchy_id IS NOT NULL
	BEGIN
		SET @current_hierarchy_id = @parent_hierarchy_id

		SELECT @parent_hierarchy_id = parent_member_hierarchy_id
		FROM member_hierarchy
		WHERE member_hierarchy_id = @current_hierarchy_id
	END

	SET @sponsor_id = @current_hierarchy_id
	
	RETURN 0
END
GO
