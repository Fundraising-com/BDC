USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_group_types]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_group_type
CREATE PROCEDURE [dbo].[efrstore_get_partner_group_types] AS
begin

select Partner_group_type_id, Description from Partner_group_type

end
GO
