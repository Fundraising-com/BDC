USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_group_type]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_group_type
CREATE PROCEDURE [dbo].[efrstore_insert_partner_group_type] @Partner_group_type_id int OUTPUT, @Description varchar(20) AS
begin

insert into Partner_group_type(Description) values(@Description)

select @Partner_group_type_id = SCOPE_IDENTITY()

end
GO
