USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_group_status]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Group_status
CREATE PROCEDURE [dbo].[es_insert_group_status] @Group_status_id int OUTPUT, @Description varchar(50) AS
begin

insert into Group_status(Description) values(@Description)

select @Group_status_id = SCOPE_IDENTITY()

end
GO
