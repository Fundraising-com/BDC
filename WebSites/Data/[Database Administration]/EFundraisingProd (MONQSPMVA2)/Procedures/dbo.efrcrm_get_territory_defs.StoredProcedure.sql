USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_territory_defs]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Territory_Def
CREATE PROCEDURE [dbo].[efrcrm_get_territory_defs] AS
begin

select Zip, Territory_ID from Territory_Def

end
GO
