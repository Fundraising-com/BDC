USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_programs]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Program
CREATE PROCEDURE [dbo].[es_get_programs] AS
begin

select Program_id, Program_name, Create_date from Program

end
GO
