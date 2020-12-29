USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[set_session_item]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 7, 2004
Description:	This stored procedure creates an online session if one does not already exist
		otherwise it will return the current session_id
*/
CREATE PROCEDURE [dbo].[set_session_item]
	@intSessionID INT
	, @strItemName VARCHAR(25)
	, @strItemValue VARCHAR(25)
AS
DECLARE @intResult INT
IF EXISTS(
	SELECT *
	FROM session_items
	WHERE 
		session_id = @intSessionID
	 AND	session_item_name = @strItemName
)
	EXEC @intResult = dbo.update_session_item
		@intSessionID 
		, @strItemName
		, @strItemValue
ELSE
	EXEC @intResult = dbo.add_session_item
		@intSessionID 
		, @strItemName
		, @strItemValue

IF @@ERROR = 0
	RETURN( @intResult )
ELSE
	RETURN( -1 )
GO
