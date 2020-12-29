USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_member_password]    Script Date: 02/14/2014 13:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	
*/
CREATE PROC [dbo].[es_update_member_password]
	@member_id int,
	@password varchar(100)
AS

if @password is not null and len(@password) > 0
begin
	UPDATE member
	SET password = @password
	WHERE member_id = @member_id
end
GO
