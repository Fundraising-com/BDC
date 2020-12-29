USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[update_session_item]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure updates the session_item_value in the 
		session_items table associated with passed session_id and
		session_item_name.  The number of rows affected by the 
		update is returned, if no error occurs.
*/
CREATE PROCEDURE [dbo].[update_session_item]
	@intSessionID INT
	, @strItemName VARCHAR(25)
	, @strItemValue VARCHAR(25)
AS
UPDATE
	session_items 
SET 	session_item_value = @strItemValue
WHERE 
	session_id = @intSessionID
 AND	session_item_name = @strItemName
IF @@ERROR = 0
	RETURN( @@ROWCOUNT )
ELSE
	RETURN( -1 )
GO
