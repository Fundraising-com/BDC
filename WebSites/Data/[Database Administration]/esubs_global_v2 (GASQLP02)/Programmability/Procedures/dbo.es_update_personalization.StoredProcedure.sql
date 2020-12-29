USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_personalization]    Script Date: 02/14/2014 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_update_personalization]
	@personalization_id int
	, @header_title1 varchar(100)
	, @header_title2 varchar(100)
	, @body varchar(2000)
	, @fundraising_goal money
	, @site_bgcolor varchar(7)
	, @header_bgcolor varchar(7)
	, @header_color varchar(7)
	, @group_url varchar(255)
	, @image_url varchar(255)
    , @image_motivator tinyint = 0
	, @displayGroupMessage bit
    , @redirect varchar(255) = NULL
	, @remind_later bit = NULL
AS
BEGIN
UPDATE personalization
SET   	  header_title1 = @header_title1
    	, header_title2 = @header_title2
    	, body = @body
		, fundraising_goal = @fundraising_goal
		, site_bgcolor = @site_bgcolor
		, header_bgcolor = @header_bgcolor
		, header_color = @header_color
		, group_url = @group_url
		, image_url = @image_url
		, image_motivator = @image_motivator
		, displayGroupMessage = @displayGroupMessage
		, redirect = @redirect
	    , remind_later = @remind_later
WHERE personalization_id = @personalization_id
END
GO
