USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_mailing_code]    Script Date: 02/14/2014 13:08:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Mailing_Code
CREATE PROCEDURE [dbo].[efrcrm_update_mailing_code] @Mailing_Code_ID int, @List_Name varchar(25), @List_ID int, @Flyer_Code varchar(25), @Launch_Date datetime, @Mailing_Code_Label varchar(25), @Mailing_Name_ID int AS
begin

update Mailing_Code set List_Name=@List_Name, List_ID=@List_ID, Flyer_Code=@Flyer_Code, Launch_Date=@Launch_Date, Mailing_Code_Label=@Mailing_Code_Label, Mailing_Name_ID=@Mailing_Name_ID where Mailing_Code_ID=@Mailing_Code_ID

end
GO
