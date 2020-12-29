USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_session_item]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Session_item
CREATE PROCEDURE [dbo].[efrstore_insert_session_item] @Session_item_id int OUTPUT, @Session_id int, @Name varchar(25), @Value varchar(25) AS
begin

insert into Session_item(Session_id, Name, Value) values(@Session_id, @Name, @Value)

select @Session_item_id = SCOPE_IDENTITY()

end
GO
