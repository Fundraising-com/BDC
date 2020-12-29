USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_partner_type_culture]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_type_culture
CREATE PROCEDURE [dbo].[es_insert_partner_type_culture] @Partner_type_id int OUTPUT, @Culture_code nvarchar(10), @Partner_type_name varchar(255), @Create_date datetime AS
begin

insert into Partner_type_culture(Culture_code, Partner_type_name, Create_date) values(@Culture_code, @Partner_type_name, @Create_date)

select @Partner_type_id = SCOPE_IDENTITY()

end
GO
