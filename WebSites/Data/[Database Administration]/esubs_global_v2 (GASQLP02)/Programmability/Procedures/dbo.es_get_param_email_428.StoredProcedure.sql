USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_428]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_428]
            @identification int
            ,@source_id bigint
AS
BEGIN
      -- SET NOCOUNT ON added to prevent extra result sets from
      -- interfering with SELECT statements.
      SET NOCOUNT ON;
select 
             @source_id as source_id
            , m.first_name + ' ' + m.last_name as [participant]
            , 428 as email_template_id
            , m.email_address as participant_email
            , m.password as participant_password
            , @identification as identification
            , e.redirect as redirect
            , p.partner_name as partner_name
            from
            member m with(nolock)
            inner join member_hierarchy mh with(nolock)
            on mh.member_id = m.member_id
            inner join event_participation ep with(nolock)
            on ep.member_hierarchy_id = mh.member_hierarchy_id
            inner join event e with(nolock)
            on ep.event_id = e.event_id
            inner join event_group eg with(nolock)
            on eg.event_id = e.event_id
            inner join [group] g with(nolock)
            on g.group_id = eg.group_id
            inner join [partner] p with (nolock)
            on p.partner_id = g.partner_id
            where 
                  ep.event_participation_id = @identification
END
GO
