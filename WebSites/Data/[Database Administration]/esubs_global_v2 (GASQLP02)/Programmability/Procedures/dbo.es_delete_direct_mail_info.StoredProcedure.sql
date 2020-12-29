USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_delete_direct_mail_info]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_delete_direct_mail_info] 
	@direct_mail_info_id int
AS
BEGIN

begin transaction

delete from direct_mail 
where direct_mail_info_id = @direct_mail_info_id;

delete from direct_mail_info
where direct_mail_info_id = @direct_mail_info_id

commit transaction

 
END
GO
