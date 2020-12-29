USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sponsor_found_stool_by_id]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Sponsor_Found_Stool
CREATE PROCEDURE [dbo].[efrcrm_get_sponsor_found_stool_by_id] @Stool_ID int AS
begin

select Stool_ID, Sales_ID, User_Name, Valeur, Modif_Date from Sponsor_Found_Stool where Stool_ID=@Stool_ID

end
GO
