USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_participation_by_touch_id]    Script Date: 08/29/2014 00:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	EXEC [dbo].[es_get_event_participation_by_touch_id] 14219707
*/
ALTER PROCEDURE [dbo].[es_get_event_participation_by_touch_id]
	@touch_id int
AS
BEGIN
	IF EXISTS (SELECT * FROM touch WHERE touch_id = @touch_id)
	BEGIN
		SELECT     
			ep.event_participation_id
			, ep.event_id
			, ep.member_hierarchy_id
			, ep.participation_channel_id
			, ep.salutation
			, pc.participation_channel_name	
			, ep.coppa_month
			, ep.coppa_year
			, ep.agree_term_services
		FROM         
			event_participation ep (NOLOCK)
		LEFT JOIN participation_channel pc (NOLOCK)
			ON ep.participation_channel_id = pc.participation_channel_id 
		JOIN touch t (NOLOCK)
			ON ep.event_participation_id = t.event_participation_id
		JOIN member_hierarchy mh (NOLOCK)
			on mh.member_hierarchy_id = ep.member_hierarchy_id
		WHERE (t.touch_id = @touch_id)
	END
	ELSE
	BEGIN
		IF EXISTS (SELECT * FROM touch_archive WHERE touch_id = @touch_id)
		BEGIN
			SET IDENTITY_INSERT [esubs_global_v2].[dbo].[touch] ON
			INSERT INTO [esubs_global_v2].[dbo].[touch]
			   ([touch_id]
			   ,[event_participation_id]
			   ,[member_hierarchy_id]
			   ,[touch_info_id]
			   ,[processed]
			   ,[create_date])
			SELECT  [touch_id]
				  ,[event_participation_id]
				  ,[member_hierarchy_id]
				  ,[touch_info_id]
				  ,[processed]
				  ,[create_date]
			FROM [esubs_global_v2].[dbo].[touch_archive]
			WHERE touch_id = @touch_id
			SET IDENTITY_INSERT [esubs_global_v2].[dbo].[touch] OFF
			SELECT     
				ep.event_participation_id
				, ep.event_id
				, ep.member_hierarchy_id
				, ep.participation_channel_id
				, ep.salutation
				, pc.participation_channel_name	
				, ep.coppa_month
				, ep.coppa_year
				, ep.agree_term_services
			FROM         
				event_participation ep (NOLOCK)
			LEFT JOIN participation_channel pc (NOLOCK)
				ON ep.participation_channel_id = pc.participation_channel_id 
			JOIN touch t (NOLOCK)
				ON ep.event_participation_id = t.event_participation_id
			JOIN member_hierarchy mh (NOLOCK)
				on mh.member_hierarchy_id = ep.member_hierarchy_id
			WHERE (t.touch_id = @touch_id)
		END
		ELSE
		BEGIN
			-- RETURN EMPTY RESULTS
			SELECT     
				ep.event_participation_id
				, ep.event_id
				, ep.member_hierarchy_id
				, ep.participation_channel_id
				, ep.salutation
				, pc.participation_channel_name	
				, ep.coppa_month
				, ep.coppa_year
				, ep.agree_term_services
			FROM         
				event_participation ep (NOLOCK)
			JOIN participation_channel pc (NOLOCK)
				ON ep.participation_channel_id = pc.participation_channel_id
			WHERE 1=2
		END
	END
END