USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_crm_static_past3seasons_forfall_tbds]    Script Date: 02/14/2014 13:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Crm_static_past3seasons_forfall_tbd
CREATE PROCEDURE [dbo].[efrcrm_get_crm_static_past3seasons_forfall_tbds] AS
begin

select AccountInstance, Total_Sold, Zzzzz, Aa99 from Crm_static_past3seasons_forfall_tbd

end
GO
