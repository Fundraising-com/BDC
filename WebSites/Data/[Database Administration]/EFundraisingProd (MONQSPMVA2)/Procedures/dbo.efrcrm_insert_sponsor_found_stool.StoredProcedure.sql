USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sponsor_found_stool]    Script Date: 02/14/2014 13:07:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sponsor_Found_Stool
CREATE PROCEDURE [dbo].[efrcrm_insert_sponsor_found_stool] @Stool_ID int OUTPUT, @Sales_ID int, @User_Name varchar(25), @Valeur bit, @Modif_Date varchar(50) AS
begin

insert into Sponsor_Found_Stool(Sales_ID, User_Name, Valeur, Modif_Date) values(@Sales_ID, @User_Name, @Valeur, @Modif_Date)

select @Stool_ID = SCOPE_IDENTITY()

end
GO
