USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_session_item]    Script Date: 02/14/2014 13:06:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Session_item
CREATE PROCEDURE [dbo].[efrstore_update_session_item] @Session_item_id int, @Session_id int, @Name varchar(25), @Value varchar(25) AS
begin

update Session_item set Session_id=@Session_id, Name=@Name, Value=@Value where Session_item_id=@Session_item_id

end
GO
