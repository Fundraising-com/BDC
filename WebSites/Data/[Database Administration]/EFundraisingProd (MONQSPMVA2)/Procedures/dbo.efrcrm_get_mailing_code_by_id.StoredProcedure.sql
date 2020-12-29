USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_mailing_code_by_id]    Script Date: 02/14/2014 13:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Mailing_Code
CREATE PROCEDURE [dbo].[efrcrm_get_mailing_code_by_id] @Mailing_Code_ID int AS
begin

select Mailing_Code_ID, List_Name, List_ID, Flyer_Code, Launch_Date, Mailing_Code_Label, Mailing_Name_ID from Mailing_Code where Mailing_Code_ID=@Mailing_Code_ID

end
GO
