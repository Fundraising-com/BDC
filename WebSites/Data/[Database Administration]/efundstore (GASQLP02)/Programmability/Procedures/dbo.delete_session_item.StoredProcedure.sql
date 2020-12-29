USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[delete_session_item]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure removes a session_item from 
		the session_items table by using the session_id and the 
		session_item_name.
*/
CREATE PROCEDURE [dbo].[delete_session_item]
	@intSessionID INT
	, @strItemName VARCHAR(25)
AS
DELETE
FROM 	session_items 
WHERE 
	session_id = @intSessionID
 AND	session_item_name = @strItemName
GO
