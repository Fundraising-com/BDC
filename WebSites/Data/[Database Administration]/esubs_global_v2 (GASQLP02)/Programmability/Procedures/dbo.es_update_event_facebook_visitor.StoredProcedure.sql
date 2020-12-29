USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_event_facebook_visitor]    Script Date: 02/14/2014 13:07:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: July 26, 2010
-- Description:	Update personalization image
-- =============================================
CREATE PROCEDURE [dbo].[es_update_event_facebook_visitor]
	@event_facebook_visitor int
	,@personalization_id int
	,@facebook_id varchar(50)
	,@facebook_image_url varchar(500)
    ,@facebook_firstname varchar(50)
	,@facebook_lastname varchar(50)
    ,@deleted bit
AS
BEGIN
	UPDATE dbo.event_facebook_visitor
	SET personalization_id = @personalization_id
      ,facebook_id = @facebook_id
      ,facebook_image_url = @facebook_image_url 
	  ,facebook_firstname = @facebook_firstname 
	  ,facebook_lastname = @facebook_lastname 
      ,update_date=getdate()
      ,deleted = @deleted
	WHERE event_facebook_visitor= @event_facebook_visitor
END
GO
