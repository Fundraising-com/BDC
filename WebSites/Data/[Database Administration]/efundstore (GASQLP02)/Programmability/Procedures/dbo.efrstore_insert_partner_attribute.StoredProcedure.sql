USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_attribute]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_attribute
CREATE PROCEDURE [dbo].[efrstore_insert_partner_attribute] @Partner_attribute_id int OUTPUT, @Name varchar(50), @Create_date datetime AS
begin

insert into Partner_attribute(Name, Create_date) values(@Name, @Create_date)

select @Partner_attribute_id = SCOPE_IDENTITY()

end
GO
