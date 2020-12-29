USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_crm_static_past3seasons_tbd_by_id]    Script Date: 02/14/2014 13:04:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Crm_static_past3seasons_tbd
CREATE PROCEDURE [dbo].[efrcrm_get_crm_static_past3seasons_tbd_by_id] @Crm_static_past3seasons_id int AS
begin

select Crm_static_past3seasons_id, AccountInstance, AccountName, Total_Sold, Zzzzzaa99, Zzzzz, Aa99, FmID, Status, Email, FirstName, LastName, HomePhone, WorkPhone, MobilePhone from Crm_static_past3seasons_tbd where Crm_static_past3seasons_id=@Crm_static_past3seasons_id

end
GO
