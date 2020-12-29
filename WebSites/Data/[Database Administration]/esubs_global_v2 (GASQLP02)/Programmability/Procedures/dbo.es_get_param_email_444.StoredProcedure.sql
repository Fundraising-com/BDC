USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_444]    Script Date: 02/14/2014 13:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_444]

      @identification int

      ,@source_id bigint

AS

BEGIN

      select 

             @source_id as source_id

            , 444 as email_template_id

            , m.first_name + ' ' + m.last_name as sponsor_name

            , m.email_address as sponsor_email

            , @identification as identification

      

 

      from

            member m with(nolock)

            inner join member_hierarchy mh with(nolock)

            on mh.member_id = m.member_id

            inner join event_participation ep with(nolock)

            on ep.member_hierarchy_id = mh.member_hierarchy_id

            inner join event e with(nolock)

            on ep.event_id = e.event_id

            where 

                  ep.event_participation_id = @identification

END
GO
