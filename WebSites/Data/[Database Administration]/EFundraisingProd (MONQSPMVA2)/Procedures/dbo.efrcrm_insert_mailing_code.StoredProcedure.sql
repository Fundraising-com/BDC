USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_mailing_code]    Script Date: 02/14/2014 13:07:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Mailing_Code
CREATE PROCEDURE [dbo].[efrcrm_insert_mailing_code] @Mailing_Code_ID int OUTPUT, @List_Name varchar(25), @List_ID int, @Flyer_Code varchar(25), @Launch_Date datetime, @Mailing_Code_Label varchar(25), @Mailing_Name_ID int AS
begin

insert into Mailing_Code(List_Name, List_ID, Flyer_Code, Launch_Date, Mailing_Code_Label, Mailing_Name_ID) values(@List_Name, @List_ID, @Flyer_Code, @Launch_Date, @Mailing_Code_Label, @Mailing_Name_ID)

select @Mailing_Code_ID = SCOPE_IDENTITY()

end
GO
