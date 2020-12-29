USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_program_by_id]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Program
CREATE PROCEDURE [dbo].[es_get_program_by_id] @Program_id int AS
begin

select Program_id, Program_name, Create_date from Program where Program_id=@Program_id

end
GO
