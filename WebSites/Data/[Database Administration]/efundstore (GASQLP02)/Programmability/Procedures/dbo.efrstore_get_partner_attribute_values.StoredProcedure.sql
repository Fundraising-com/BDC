USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_attribute_values]    Script Date: 02/14/2014 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_attribute_value
CREATE PROCEDURE [dbo].[efrstore_get_partner_attribute_values] AS
begin

select Partner_id, Partner_attribute_id, Culture_code, Value, Create_date from Partner_attribute_value

end
GO
