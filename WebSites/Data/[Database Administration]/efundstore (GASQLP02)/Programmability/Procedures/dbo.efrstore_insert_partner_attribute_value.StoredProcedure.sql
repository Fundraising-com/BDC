USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_attribute_value]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_attribute_value
CREATE PROCEDURE [dbo].[efrstore_insert_partner_attribute_value] @Partner_id int OUTPUT, @Partner_attribute_id int, @Culture_code nvarchar(10), @Value varchar(255), @Create_date datetime AS
begin

insert into Partner_attribute_value(Partner_attribute_id, Culture_code, Value, Create_date) values(@Partner_attribute_id, @Culture_code, @Value, @Create_date)

select @Partner_id = SCOPE_IDENTITY()

end
GO
