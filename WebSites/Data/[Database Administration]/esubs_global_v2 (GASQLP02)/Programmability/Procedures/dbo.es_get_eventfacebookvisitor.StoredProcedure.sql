USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_eventfacebookvisitor]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_eventfacebookvisitor]
	@personalization_id int
AS
BEGIN

  SELECT event_facebook_visitor, personalization_id, facebook_id, facebook_image_url, facebook_firstname, facebook_lastname, update_date, create_date, deleted
  FROM   dbo.event_facebook_visitor
  WHERE  personalization_id = @personalization_id AND deleted = 0
  ORDER BY update_date desc;
END
GO
