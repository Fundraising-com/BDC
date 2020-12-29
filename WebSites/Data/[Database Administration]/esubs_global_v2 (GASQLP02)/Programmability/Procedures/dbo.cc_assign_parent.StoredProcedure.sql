USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_assign_parent]    Script Date: 02/14/2014 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jean-Francois Lavigne
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[cc_assign_parent]
    @member_hierarchy_id int
    ,@parent_member_hierarchy_id int
AS
BEGIN
    
    UPDATE  member_hierarchy 
    SET     parent_member_hierarchy_id = @parent_member_hierarchy_id
    where   member_hierarchy_id = @member_hierarchy_id
    
    if @@error = 0
	    return  0
    else
	    return -1 --une erreur
    
END
GO
