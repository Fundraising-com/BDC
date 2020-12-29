USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_prize_type]    Script Date: 02/14/2014 13:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Prize_type
CREATE PROCEDURE [dbo].[es_insert_prize_type] @Prize_type_id int OUTPUT, @Prize_type_name varchar(50), @Create_date datetime AS
begin

insert into Prize_type(Prize_type_name, Create_date) values(@Prize_type_name, @Create_date)

select @Prize_type_id = SCOPE_IDENTITY()

end
GO
