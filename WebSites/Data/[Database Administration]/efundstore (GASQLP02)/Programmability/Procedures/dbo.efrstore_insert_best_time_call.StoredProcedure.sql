USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_best_time_call]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Best_time_call
CREATE PROCEDURE [dbo].[efrstore_insert_best_time_call] @Best_time_call_id int OUTPUT, @Description varchar(20) AS
begin

insert into Best_time_call(Description) values(@Description)

select @Best_time_call_id = SCOPE_IDENTITY()

end
GO
