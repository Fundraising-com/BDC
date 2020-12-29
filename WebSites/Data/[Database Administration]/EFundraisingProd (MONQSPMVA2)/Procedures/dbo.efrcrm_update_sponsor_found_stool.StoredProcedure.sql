USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sponsor_found_stool]    Script Date: 02/14/2014 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sponsor_Found_Stool
CREATE PROCEDURE [dbo].[efrcrm_update_sponsor_found_stool] @Stool_ID int, @Sales_ID int, @User_Name varchar(25), @Valeur bit, @Modif_Date varchar(50) AS
begin

update Sponsor_Found_Stool set Sales_ID=@Sales_ID, User_Name=@User_Name, Valeur=@Valeur, Modif_Date=@Modif_Date where Stool_ID=@Stool_ID

end
GO
