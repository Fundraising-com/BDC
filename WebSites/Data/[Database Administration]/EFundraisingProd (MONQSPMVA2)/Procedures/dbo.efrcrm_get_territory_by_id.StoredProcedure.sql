USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_territory_by_id]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Territory
CREATE PROCEDURE [dbo].[efrcrm_get_territory_by_id] @Territory_id int AS
begin

select Territory_id, Territory_name from Territory where Territory_id=@Territory_id

end
GO
