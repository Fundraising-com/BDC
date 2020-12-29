USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_best_time_call_desc]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Best_time_call_desc
CREATE PROCEDURE [dbo].[efrstore_insert_best_time_call_desc] @Best_time_call_id int OUTPUT, @Culture_code nvarchar(10), @Description varchar(25) AS
begin

insert into Best_time_call_desc(Culture_code, Description) values(@Culture_code, @Description)

select @Best_time_call_id = SCOPE_IDENTITY()

end
GO
