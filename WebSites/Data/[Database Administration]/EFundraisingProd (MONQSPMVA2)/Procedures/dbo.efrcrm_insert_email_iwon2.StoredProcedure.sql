USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_email_iwon2]    Script Date: 02/14/2014 13:06:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EMail_iwon2
CREATE PROCEDURE [dbo].[efrcrm_insert_email_iwon2] @ID int OUTPUT, @GoodEmail varchar(50) AS
begin

insert into EMail_iwon2(GoodEmail) values(@GoodEmail)

select @ID = SCOPE_IDENTITY()

end
GO
