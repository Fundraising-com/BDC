USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_session_items]    Script Date: 02/14/2014 13:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Session_item
CREATE PROCEDURE [dbo].[efrstore_get_session_items] AS
begin

select Session_item_id, Session_id, Name, Value from Session_item

end
GO
