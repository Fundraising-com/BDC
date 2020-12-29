USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_member_password]    Script Date: 02/14/2014 13:05:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[cc_update_member_password]
	@member_id int,
    @password nvarchar(50)
as

DECLARE @user_id int
SELECT @user_id = user_id FROM member WHERE member_id = @member_id

UPDATE member  
SET password = @password        
WHERE member_ID = @member_id

IF @user_id IS NOT NULL
	UPDATE users  
	SET password = @password        
	WHERE user_id = @user_id
GO
