USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_temp_dm_usa_hockey_inline_1s]    Script Date: 02/14/2014 13:06:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Temp_dm_usa_hockey_inline_1
CREATE PROCEDURE [dbo].[efrcrm_get_temp_dm_usa_hockey_inline_1s] AS
begin

select Id, Compagnie, Contact, Address1, Address2, City, State, Zip, Phone, Ext from Temp_dm_usa_hockey_inline_1

end
GO
