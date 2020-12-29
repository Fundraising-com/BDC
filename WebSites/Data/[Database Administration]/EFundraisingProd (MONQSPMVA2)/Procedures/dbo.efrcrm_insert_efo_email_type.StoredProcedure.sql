USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_email_type]    Script Date: 02/14/2014 13:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Email_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_email_type] @Email_Type_ID int OUTPUT, @Body text, @Description varchar(150) AS
begin

insert into EFO_Email_Type(Body, Description) values(@Body, @Description)

select @Email_Type_ID = SCOPE_IDENTITY()

end
GO
