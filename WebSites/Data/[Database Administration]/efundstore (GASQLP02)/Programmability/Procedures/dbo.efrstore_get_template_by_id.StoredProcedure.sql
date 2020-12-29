USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_template_by_id]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Template
CREATE PROCEDURE [dbo].[efrstore_get_template_by_id] @Template_id int AS
begin

select Template_id, Name, Path, Create_date from Template where Template_id=@Template_id

end
GO
