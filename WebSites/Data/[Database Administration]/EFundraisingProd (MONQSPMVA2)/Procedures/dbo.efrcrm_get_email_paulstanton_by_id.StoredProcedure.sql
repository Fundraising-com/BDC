USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_email_paulstanton_by_id]    Script Date: 02/14/2014 13:04:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EMail_PaulStanton
CREATE PROCEDURE [dbo].[efrcrm_get_email_paulstanton_by_id] @ID int AS
begin

select ID, GoodEmail from EMail_PaulStanton where ID=@ID

end
GO
