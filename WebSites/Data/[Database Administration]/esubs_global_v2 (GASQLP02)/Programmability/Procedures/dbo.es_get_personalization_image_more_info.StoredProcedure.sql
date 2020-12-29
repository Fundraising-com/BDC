USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization_image_more_info]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jason Farrell
-- Create date: Sept 1, 2010
-- Description:	Retrieve images and member information 
-- =============================================
Create PROCEDURE [dbo].[es_get_personalization_image_more_info]
	-- Add the parameters for the stored procedure here
	@image_id int = null
AS
BEGIN
	SET NOCOUNT ON;

     SELECT 
		m.first_name
		, m.last_name
		, m.email_address
		, m.password
		, m.lead_id
		, e.event_name
		, ep.event_id
		, ep.event_participation_id
		, p.redirect as member_redirect
		, ps.redirect as group_redirect
		, pimg.create_date
		, pimg.approver_name
		, pimg.approved_date
		, pimg.image_approval_status_id
		, ias.image_approval_status_description
	FROM dbo.personalization_image pimg with(nolock)
	INNER JOIN personalization p on pimg.personalization_id = p.personalization_id
	INNER JOIN event_participation ep on p.event_participation_id = ep.event_participation_id
	INNER JOIN [event] e on ep.event_id = e.event_id
	INNER JOIN member_hierarchy mh on ep.member_hierarchy_id = mh.member_hierarchy_id
	INNER JOIN member m on mh.member_id = m.member_id 
	INNER JOIN Image_approval_status ias on pimg.image_approval_status_id = ias.image_approval_status_id
	INNER JOIN event_group eg on e.event_id = eg.event_id 
	INNER JOIN [group] g on eg.group_id = g.group_id 
	INNER JOIN member_hierarchy mhs on g.sponsor_id = mhs.member_hierarchy_id 
	INNER JOIN event_participation eps on eps.member_hierarchy_id = mhs.member_hierarchy_id and eps.event_id = e.event_id 
	INNER JOIN personalization ps on ps.event_participation_id = eps.event_participation_id
	WHERE pimg.image_id = @image_id

END
GO
