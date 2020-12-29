USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[xf_get_member]    Script Date: 02/14/2014 13:08:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	
	Created by: Dat Nghiem
	Projects: Esubs v2
	Date: 12 July 2006
	
    Description: Retrieve members from the temporary table after partner creating participants then update those members to deleted
	
	
*/

CREATE PROC [dbo].[xf_get_member]
    	@external_group_id varchar(128) ,
    	@partner_id int = 0,
	@deleted bit
AS
BEGIN
    SELECT * 
    FROM xfactor_member pm
    WHERE 
    pm.external_group_id = @external_group_id
    and partner_id = @partner_id
    and Isnull(@deleted, 0) = Isnull(deleted, 0)


    UPDATE [xfactor_member]
	    SET
	    deleted = 1
	    WHERE 
	    external_group_id = @external_group_id
	    AND partner_id = @partner_id
END
GO
