USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sponsor_consultant]    Script Date: 02/14/2014 13:07:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sponsor_Consultant
CREATE PROCEDURE [dbo].[efrcrm_insert_sponsor_consultant] @Sponsor_Consultant_ID int OUTPUT, @First_Name varchar(50), @MiddleInitial varchar(50), @Last_Name varchar(50), @Title varchar(50), @Day_Phone varchar(20), @Day_Time_Call varchar(20), @Evening_Phone varchar(20), @Evenig_Time_Call varchar(20), @Alternate_Phone varchar(50), @Fax varchar(20), @Email varchar(50), @Comment text, @Is_Active bit, @Nt_Login varchar(50), @Commission_Rate float AS
begin

insert into Sponsor_Consultant(First_Name, [Middle Initial], Last_Name, Title, Day_Phone, Day_Time_Call, Evening_Phone, Evenig_Time_Call, Alternate_Phone, Fax, Email, Comment, Is_Active, Nt_Login, Commission_Rate) values(@First_Name, @MiddleInitial, @Last_Name, @Title, @Day_Phone, @Day_Time_Call, @Evening_Phone, @Evenig_Time_Call, @Alternate_Phone, @Fax, @Email, @Comment, @Is_Active, @Nt_Login, @Commission_Rate)

select @Sponsor_Consultant_ID = SCOPE_IDENTITY()

end
GO
