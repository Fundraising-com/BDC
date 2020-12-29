USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_qualification_type_by_id]    Script Date: 02/14/2014 13:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Qualification_Type
CREATE PROCEDURE [dbo].[efrcrm_get_lead_qualification_type_by_id] @Lead_Qualification_Type_ID int AS
begin

select Lead_Qualification_Type_ID, Description, Weight from Lead_Qualification_Type where Lead_Qualification_Type_ID=@Lead_Qualification_Type_ID

end
GO
