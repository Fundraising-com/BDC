USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_programs]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Program
CREATE PROCEDURE [dbo].[efrstore_get_programs] AS
begin

select Program_id, Name, Create_date from Program

end
GO
