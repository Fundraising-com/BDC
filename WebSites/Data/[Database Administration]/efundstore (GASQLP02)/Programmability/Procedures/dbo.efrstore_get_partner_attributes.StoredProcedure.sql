USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_attributes]    Script Date: 02/14/2014 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_attribute
CREATE PROCEDURE [dbo].[efrstore_get_partner_attributes] AS
begin

select Partner_attribute_id, Name, Create_date from Partner_attribute

end
GO
