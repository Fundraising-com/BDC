USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_root_group]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[es_get_root_group]
    @group_id int
AS
BEGIN
    DECLARE @parent_group_id int
    
    SELECT @parent_group_id = parent_group_id from [group] where group_id = @group_id
    
    IF @parent_group_id IS NULL
    BEGIN
        SELECT @parent_group_id = @group_id
    END
    ELSE
    BEGIN
        WHILE @parent_group_id IS NOT NULL
        BEGIN
            SELECT @parent_group_id = parent_group_id from [group] where group_id = @parent_group_id
        END
    END
    
    SELECT 
	g.group_id
	, g.parent_group_id
	, g.sponsor_id
	, g.partner_id
	, g.lead_id
	, g.external_group_id
	, g.group_name
	, g.test_group
	, g.expected_membership
	, g.redirect
	, g.group_url
	, g.comments
	, g.create_date
    FROM [group] g 
    WHERE (g.group_id = @group_id)
END
GO
