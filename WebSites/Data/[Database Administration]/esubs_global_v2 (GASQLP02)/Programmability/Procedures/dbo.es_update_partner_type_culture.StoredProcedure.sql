USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_partner_type_culture]    Script Date: 02/14/2014 13:07:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_type_culture
CREATE PROCEDURE [dbo].[es_update_partner_type_culture] @Partner_type_id int, @Culture_code nvarchar(10), @Partner_type_name varchar(255), @Create_date datetime AS
begin

update Partner_type_culture set Culture_code=@Culture_code, Partner_type_name=@Partner_type_name, Create_date=@Create_date where Partner_type_id=@Partner_type_id

end
GO
