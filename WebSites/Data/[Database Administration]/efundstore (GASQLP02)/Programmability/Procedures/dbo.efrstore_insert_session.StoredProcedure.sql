USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_session]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Session
CREATE PROCEDURE [dbo].[efrstore_insert_session] @Session_id int OUTPUT, @Visitors_log_id int, @Date_created datetime AS
begin

insert into Session(Visitors_log_id, Date_created) values(@Visitors_log_id, @Date_created)

select @Session_id = SCOPE_IDENTITY()

end
GO
