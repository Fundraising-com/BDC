USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sponsor_consultants]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sponsor_Consultant
CREATE PROCEDURE [dbo].[efrcrm_get_sponsor_consultants] AS
begin

select Sponsor_Consultant_ID, First_Name, [Middle Initial], Last_Name, Title, Day_Phone, Day_Time_Call, Evening_Phone, Evenig_Time_Call, Alternate_Phone, Fax, Email, Comment, Is_Active, Nt_Login, Commission_Rate from Sponsor_Consultant

end
GO
