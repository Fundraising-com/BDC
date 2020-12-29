USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_session_item_by_id]    Script Date: 02/14/2014 13:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Session_item
CREATE PROCEDURE [dbo].[efrstore_get_session_item_by_id] @Session_item_id int AS
begin

select Session_item_id, Session_id, Name, Value from Session_item where Session_item_id=@Session_item_id

end
GO
