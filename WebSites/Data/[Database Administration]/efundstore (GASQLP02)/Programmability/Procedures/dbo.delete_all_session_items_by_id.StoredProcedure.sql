USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[delete_all_session_items_by_id]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure removes all the session_items  
		in the session_items table associated with passed session_id.
*/
CREATE PROCEDURE [dbo].[delete_all_session_items_by_id]
	@intSessionID INT
AS
DELETE
FROM	session_items
WHERE 
	session_id = @intSessionID
GO
