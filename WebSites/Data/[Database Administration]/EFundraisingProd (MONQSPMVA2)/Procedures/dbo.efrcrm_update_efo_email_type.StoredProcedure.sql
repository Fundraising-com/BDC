USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_email_type]    Script Date: 02/14/2014 13:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Email_Type
CREATE PROCEDURE [dbo].[efrcrm_update_efo_email_type] @Email_Type_ID int, @Body text, @Description varchar(150) AS
begin

update EFO_Email_Type set Body=@Body, Description=@Description where Email_Type_ID=@Email_Type_ID

end
GO
