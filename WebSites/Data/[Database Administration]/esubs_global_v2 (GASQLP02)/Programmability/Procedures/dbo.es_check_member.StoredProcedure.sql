USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_check_member]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--
-- Added by phil
--

-- Stored procedure
CREATE PROCEDURE [dbo].[es_check_member]
	@first_name varchar(100)
	, @last_name varchar(100)
	, @email_address varchar(100)
	, @partner_id int = 0
	, @external_member_id varchar(20)
AS
BEGIN
	IF EXISTS(
		SELECT member_id 
		FROM member 
		WHERE email_address = @email_address 
		  AND first_name = @first_name 
		  AND last_name = @last_name
		  AND partner_id = @partner_id
	)
	BEGIN
		RETURN -1
	END

	IF EXISTS(
        SELECT m.member_id
          FROM member m
         WHERE m.external_member_id = @external_member_id
           AND m.partner_id = @partner_id
        )
    BEGIN
		RETURN -2
	END

	RETURN 0
END
GO
