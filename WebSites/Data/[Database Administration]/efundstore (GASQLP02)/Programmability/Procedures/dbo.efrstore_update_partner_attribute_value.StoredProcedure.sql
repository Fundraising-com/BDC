USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_attribute_value]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_attribute_value
CREATE PROCEDURE [dbo].[efrstore_update_partner_attribute_value] @Partner_id int, @Partner_attribute_id int, @Culture_code nvarchar(10), @Value varchar(255), @Create_date datetime AS
begin

update Partner_attribute_value set Partner_attribute_id=@Partner_attribute_id, Culture_code=@Culture_code, Value=@Value, Create_date=@Create_date where Partner_id=@Partner_id

end
GO
