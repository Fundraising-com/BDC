USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sponsor_found_stools]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sponsor_Found_Stool
CREATE PROCEDURE [dbo].[efrcrm_get_sponsor_found_stools] AS
begin

select Stool_ID, Sales_ID, User_Name, Valeur, Modif_Date from Sponsor_Found_Stool

end
GO
