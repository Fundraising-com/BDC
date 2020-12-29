USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_user_id_by_member_id]    Script Date: 02/14/2014 13:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_get_user_id_by_member_id] (@member_id int)  
RETURNS int AS  
BEGIN 
	declare @userId int

	select @userId = [user_id] from member where member_id = @member_id

	if(@userId is null)
	begin
		return -1
	end

	return @userId
END
GO
