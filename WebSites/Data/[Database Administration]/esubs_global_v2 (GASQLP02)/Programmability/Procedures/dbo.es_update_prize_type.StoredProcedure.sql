USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_prize_type]    Script Date: 02/14/2014 13:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Prize_type
CREATE PROCEDURE [dbo].[es_update_prize_type] @Prize_type_id int, @Prize_type_name varchar(50), @Create_date datetime AS
begin

update Prize_type set Prize_type_name=@Prize_type_name, Create_date=@Create_date where Prize_type_id=@Prize_type_id

end
GO
