USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sponsor_consultant]    Script Date: 02/14/2014 13:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sponsor_Consultant
CREATE PROCEDURE [dbo].[efrcrm_update_sponsor_consultant] @Sponsor_Consultant_ID int, @First_Name varchar(50), @MiddleInitial varchar(50), @Last_Name varchar(50), @Title varchar(50), @Day_Phone varchar(20), @Day_Time_Call varchar(20), @Evening_Phone varchar(20), @Evenig_Time_Call varchar(20), @Alternate_Phone varchar(50), @Fax varchar(20), @Email varchar(50), @Comment text, @Is_Active bit, @Nt_Login varchar(50), @Commission_Rate float AS
begin

update Sponsor_Consultant set First_Name=@First_Name, [Middle Initial]=@MiddleInitial, Last_Name=@Last_Name, Title=@Title, Day_Phone=@Day_Phone, Day_Time_Call=@Day_Time_Call, Evening_Phone=@Evening_Phone, Evenig_Time_Call=@Evenig_Time_Call, Alternate_Phone=@Alternate_Phone, Fax=@Fax, Email=@Email, Comment=@Comment, Is_Active=@Is_Active, Nt_Login=@Nt_Login, Commission_Rate=@Commission_Rate where Sponsor_Consultant_ID=@Sponsor_Consultant_ID

end
GO
