USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_program_by_id]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Program
CREATE PROCEDURE [dbo].[efrstore_get_program_by_id] @Program_id int AS
begin

select Program_id, Name, Create_date from Program where Program_id=@Program_id

end
GO
