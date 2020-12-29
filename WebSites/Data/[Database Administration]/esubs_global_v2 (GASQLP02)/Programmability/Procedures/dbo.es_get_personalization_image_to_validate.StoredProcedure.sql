USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization_image_to_validate]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jason Farrell
-- Create date: Sept 1, 2010
-- Description:	Retrieve images to approve
-- =============================================
Create PROCEDURE [dbo].[es_get_personalization_image_to_validate]
	-- Add the parameters for the stored procedure here
	@image_status int = null
	,@start_date Datetime
	,@end_date Datetime
	,@event_id int = null

AS
BEGIN
	SET NOCOUNT ON;

 declare @end_date2 varchar(30)
 set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
 set @end_date = convert(datetime, @end_date2)


     SELECT ep.event_id
		, ep.event_participation_id
		, pimg.image_id
		, pimg.personalization_id
		, pimg.image_url
		, pimg.deleted
        , pimg.isCoverAlbum
        , pimg.create_date
		, pimg.approver_name
		, pimg.approved_date
		, pimg.image_approval_status_id
	FROM dbo.personalization_image pimg with (nolock)
	INNER JOIN personalization p on pimg.personalization_id = p.personalization_id
	INNER JOIN event_participation ep on p.event_participation_id = ep.event_participation_id
	WHERE pimg.create_date Between @start_date And @end_date
		AND	(pimg.image_approval_status_id = CAST(@image_status as varchar(10)) or  @image_status is null)
		AND	(ep.event_id = CAST(@event_id as varchar(10)) or  @event_id is null)
END
GO
