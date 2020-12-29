USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_address_zone]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Address_zone
CREATE PROCEDURE [dbo].[efrcrm_insert_address_zone] @Address_zone_id int OUTPUT, @Description varchar(255) AS
begin

insert into Address_zone(Description) values(@Description)

select @Address_zone_id = SCOPE_IDENTITY()

end
GO
