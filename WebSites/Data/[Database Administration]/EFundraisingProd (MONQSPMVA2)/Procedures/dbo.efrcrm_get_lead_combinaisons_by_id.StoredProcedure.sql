USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_combinaisons_by_id]    Script Date: 02/14/2014 13:04:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Combinaisons
CREATE PROCEDURE [dbo].[efrcrm_get_lead_combinaisons_by_id] @Lead_Combinaison_ID int AS
begin

select Lead_Combinaison_ID, Condition_ID, Lead_Qualification_Type_ID from Lead_Combinaisons where Lead_Combinaison_ID=@Lead_Combinaison_ID

end
GO
