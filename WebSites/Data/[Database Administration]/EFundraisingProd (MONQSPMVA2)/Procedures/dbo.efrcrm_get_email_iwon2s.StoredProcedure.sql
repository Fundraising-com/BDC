USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_email_iwon2s]    Script Date: 02/14/2014 13:04:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EMail_iwon2
CREATE PROCEDURE [dbo].[efrcrm_get_email_iwon2s] AS
begin

select ID, GoodEmail from EMail_iwon2

end
GO
