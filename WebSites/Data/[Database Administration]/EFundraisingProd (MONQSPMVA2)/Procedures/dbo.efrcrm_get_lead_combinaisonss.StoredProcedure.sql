USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_combinaisonss]    Script Date: 02/14/2014 13:04:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Combinaisons
CREATE PROCEDURE [dbo].[efrcrm_get_lead_combinaisonss] AS
begin

select Lead_Combinaison_ID, Condition_ID, Lead_Qualification_Type_ID from Lead_Combinaisons

end
GO
