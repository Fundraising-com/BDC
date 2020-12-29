USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_combinaisons]    Script Date: 02/14/2014 13:08:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Combinaisons
CREATE PROCEDURE [dbo].[efrcrm_update_lead_combinaisons] @Lead_Combinaison_ID int, @Condition_ID int, @Lead_Qualification_Type_ID int AS
begin

update Lead_Combinaisons set Condition_ID=@Condition_ID, Lead_Qualification_Type_ID=@Lead_Qualification_Type_ID where Lead_Combinaison_ID=@Lead_Combinaison_ID

end
GO
