USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_templates]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Template
CREATE PROCEDURE [dbo].[efrstore_get_templates] AS
begin

select Template_id, Name, Path, Create_date from Template

end
GO
