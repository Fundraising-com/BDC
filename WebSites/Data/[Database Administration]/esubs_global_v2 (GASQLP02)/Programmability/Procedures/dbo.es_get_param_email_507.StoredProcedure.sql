USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_507]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_507]
		@identification int
		,@source_id bigint
as
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select 
		 @source_id as source_id
		, case when rtrim(isnull(ep.salutation,''))='' 
		  then m.first_name + ' ' + m.last_name 
		  else ep.salutation COLLATE SQL_Latin1_General_CP1_CI_AI end as supporter 
		, 507 as email_template_id
		, @identification as identification
		, e.event_name as campaign
	from
		member m
		inner join member_hierarchy mh
		on mh.member_id = m.member_id
		inner join event_participation ep
		on ep.member_hierarchy_id = mh.member_hierarchy_id		
		inner join event e
		on ep.event_id = e.event_id
		where 
			ep.event_participation_id = @identification
END
GO
