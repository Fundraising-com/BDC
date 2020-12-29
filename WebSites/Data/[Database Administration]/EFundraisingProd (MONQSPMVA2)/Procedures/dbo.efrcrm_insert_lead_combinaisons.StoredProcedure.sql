USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_combinaisons]    Script Date: 02/14/2014 13:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Combinaisons
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_combinaisons] @Lead_Combinaison_ID int OUTPUT, @Condition_ID int, @Lead_Qualification_Type_ID int AS
begin

insert into Lead_Combinaisons(Condition_ID, Lead_Qualification_Type_ID) values(@Condition_ID, @Lead_Qualification_Type_ID)

select @Lead_Combinaison_ID = SCOPE_IDENTITY()

end
GO
