USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_qualification_types]    Script Date: 02/14/2014 13:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Qualification_Type
CREATE PROCEDURE [dbo].[efrcrm_get_lead_qualification_types] AS
begin

select Lead_Qualification_Type_ID, Description, Weight from Lead_Qualification_Type

end
GO
