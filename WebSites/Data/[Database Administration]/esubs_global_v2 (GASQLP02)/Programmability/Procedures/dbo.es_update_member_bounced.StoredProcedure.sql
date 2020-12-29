USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_member_bounced]    Script Date: 02/14/2014 13:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	
*/
CREATE PROC [dbo].[es_update_member_bounced]
	@member_id int
AS
BEGIN
	UPDATE member
	SET bounced = 1
	WHERE member_id = @member_id
END
GO
