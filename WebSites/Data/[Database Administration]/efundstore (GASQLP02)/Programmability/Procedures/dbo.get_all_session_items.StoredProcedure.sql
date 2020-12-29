USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_all_session_items]    Script Date: 02/14/2014 13:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure returns all the session_items in the 
		session_items table associated with passed session_id.
*/
CREATE PROCEDURE [dbo].[get_all_session_items]
	@intSessionID INT
AS
SELECT
	session_item_id
	, session_item_name
	, session_item_value
FROM	session_items
WHERE 
	session_id = @intSessionID
GO
