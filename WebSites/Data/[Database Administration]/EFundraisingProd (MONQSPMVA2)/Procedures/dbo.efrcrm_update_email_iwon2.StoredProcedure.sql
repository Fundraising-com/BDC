USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_email_iwon2]    Script Date: 02/14/2014 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EMail_iwon2
CREATE PROCEDURE [dbo].[efrcrm_update_email_iwon2] @ID int, @GoodEmail varchar(50) AS
begin

update EMail_iwon2 set GoodEmail=@GoodEmail where ID=@ID

end
GO
