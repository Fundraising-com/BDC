USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_types]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_type
CREATE PROCEDURE [dbo].[efrstore_get_partner_types] AS
begin

select Partner_type_id, Name, Create_date from Partner_type

end
GO
