USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_organization_type_desc]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Organization_type_desc
CREATE PROCEDURE [dbo].[efrstore_insert_organization_type_desc] @Organization_type_id int OUTPUT, @Culture_code nvarchar(10), @Description varchar(200) AS
begin

insert into Organization_type_desc(Culture_code, Description) values(@Culture_code, @Description)

select @Organization_type_id = SCOPE_IDENTITY()

end
GO
