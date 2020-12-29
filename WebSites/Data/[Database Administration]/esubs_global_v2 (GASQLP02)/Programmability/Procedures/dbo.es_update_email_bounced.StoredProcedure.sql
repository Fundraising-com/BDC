USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_email_bounced]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	
*/
CREATE PROC [dbo].[es_update_email_bounced]
	@email_address varchar(255) , 
	@bounced bit = 1
AS
BEGIN
	UPDATE member
	SET bounced = @bounced
	WHERE email_address = @email_address
END
GO
