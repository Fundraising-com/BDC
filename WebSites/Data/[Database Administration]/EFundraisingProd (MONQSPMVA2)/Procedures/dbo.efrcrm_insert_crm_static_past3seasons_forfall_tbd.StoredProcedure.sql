USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_crm_static_past3seasons_forfall_tbd]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Crm_static_past3seasons_forfall_tbd
CREATE PROCEDURE [dbo].[efrcrm_insert_crm_static_past3seasons_forfall_tbd] @AccountInstance int OUTPUT, @Total_Sold decimal, @Zzzzz varchar(5), @Aa99 varchar(4) AS
begin

insert into Crm_static_past3seasons_forfall_tbd(Total_Sold, Zzzzz, Aa99) values(@Total_Sold, @Zzzzz, @Aa99)

select @AccountInstance = SCOPE_IDENTITY()

end
GO
