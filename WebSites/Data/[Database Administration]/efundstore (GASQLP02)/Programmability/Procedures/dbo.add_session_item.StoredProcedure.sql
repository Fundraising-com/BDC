USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[add_session_item]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 26, 2004
Description:	This stored procedure adds an item to the session_items 
		table associated with passed session_id.
*/
CREATE PROCEDURE [dbo].[add_session_item]
	@intSessionID INT
	, @strItemName VARCHAR(25)
	, @strItemValue VARCHAR(25)
AS
INSERT INTO session_items ( 
	session_id
	, session_item_name
	, session_item_value
) VALUES ( 
	@intSessionID 
	, @strItemName 
	, @strItemValue 
)
IF @@ERROR = 0
	RETURN( @@IDENTITY )
ELSE
	RETURN( -1 )
GO
