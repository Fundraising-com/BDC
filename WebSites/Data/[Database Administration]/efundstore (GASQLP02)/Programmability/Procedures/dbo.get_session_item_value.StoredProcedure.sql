USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_session_item_value]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure returns the session_item_value in the 
		session_items table associated with passed session_id and
		session_item_name.
*/
CREATE PROCEDURE [dbo].[get_session_item_value]
	@intSessionID INT
	, @strItemName VARCHAR(25)
AS
SELECT 
	session_item_value 
FROM	session_items
WHERE 
	session_id = @intSessionID
 AND	session_item_name = @strItemName
GO
