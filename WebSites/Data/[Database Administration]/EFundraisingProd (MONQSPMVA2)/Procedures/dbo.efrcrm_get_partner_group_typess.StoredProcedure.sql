USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partner_group_typess]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_group_types
CREATE PROCEDURE [dbo].[efrcrm_get_partner_group_typess] AS
begin

select Partner_group_type_id, Partner_group_type_desc from Partner_group_types

end
GO
