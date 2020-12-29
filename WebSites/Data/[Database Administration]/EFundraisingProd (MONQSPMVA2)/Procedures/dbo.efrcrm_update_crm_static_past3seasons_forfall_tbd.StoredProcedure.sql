USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_crm_static_past3seasons_forfall_tbd]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Crm_static_past3seasons_forfall_tbd
CREATE PROCEDURE [dbo].[efrcrm_update_crm_static_past3seasons_forfall_tbd] @AccountInstance int, @Total_Sold decimal, @Zzzzz varchar(5), @Aa99 varchar(4) AS
begin

update Crm_static_past3seasons_forfall_tbd set Total_Sold=@Total_Sold, Zzzzz=@Zzzzz, Aa99=@Aa99 where AccountInstance=@AccountInstance

end
GO
