USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_exception_type]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Exception_type
CREATE PROCEDURE [dbo].[es_insert_exception_type] @Exception_type_id int OUTPUT, @Description varchar(50) AS
begin

insert into Exception_type(Description) values(@Description)

select @Exception_type_id = SCOPE_IDENTITY()

end
GO
