USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_sponsor_login]    Script Date: 02/14/2014 13:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[cc_update_sponsor_login]
		@event_participation_id int,
        @email_address nvarchar(50),
        @password nvarchar(50),
        @first_name nvarchar(50),
        @last_name nvarchar(50)
as

DECLARE @member_id int, @user_id int

SELECT @member_id = mh.member_id, @user_id = m.user_id
  FROM event_participation ep with (nolock) 
  JOIN member_hierarchy mh with (nolock) ON ep.member_hierarchy_id = mh.member_hierarchy_id
  JOIN member m with (nolock) ON mh.member_id = m.member_id
 WHERE event_participation_id = @event_participation_id

UPDATE dbo.member
SET   [password] = @password
	, first_name = @first_name
	, last_name = @last_name
	, email_address = @email_address
WHERE member_id = @member_id
	
IF @user_id IS NOT NULL
BEGIN
	UPDATE dbo.[users]
	SET   [password] = @password
		, first_name = @first_name
		, last_name = @last_name
		, email_address = @email_address
	WHERE user_id = @user_id
END

IF @@error = 0
	RETURN 0
ELSE
	RETURN -1 --une erreur
GO
