USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_email_sent]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from event_group where group_id = 836706

/*  retourne group emails
    mcote 2009-03-27

      exec [es_get_event_email_sent] 1478602    

      update by Melissa Cote 
      update date 2010.09.30
      add new business rule id
      
      update by Jiro Hidaka
	  update date 2013.12.04
      cleaned up business rule id
*/

CREATE  procedure [dbo].[es_get_event_email_sent]
      @event_id int
as
BEGIN
      select 
            Convert(datetime, Convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date))) as launch_date
            , (case 
                        -- Maximum 30 caracteres for description                    then 'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX'
                        when br.business_rule_id in (96, 115, 164)                  then 'Participant First Email'
                        
                        when br.business_rule_id in (97, 116, 165)                  then 'Participant First Reminder'

                        when br.business_rule_id in (98, 117, 166)                  then 'Participant Second Reminder'

                        WHEN br.business_rule_id IN (156, 167)                      then 'Participant Final Reminder'

                        when br.business_rule_id IN (99, 173)                       then 'Participant Reminder'

                        when br.business_rule_id IN (100)                           then 'Participant Personal Note'

                        when br.business_rule_id IN(101, 168)                       then 'Supporter First Email'

                        when br.business_rule_id IN (102, 169)                      then 'Supporter First Reminder'

                        when br.business_rule_id IN (103, 170)                      then 'Supporter Second Reminder'

                        WHEN br.business_rule_id IN (171, 172)                      then 'Supporter Final Reminder'

                        --WHEN br.business_rule_id IN (174)                           then 'Supporter Reminder'

                  else 'OTHER' end ) as email_desc

            , count(*) as email_sent

      from touch t with(nolock)

      inner join touch_info ti with(nolock) on ti.touch_info_id = t.touch_info_id

      inner join business_rule br with(nolock) on ti.business_rule_id = br.business_rule_id

      inner join event_participation ep with(nolock) on ep.event_participation_id = t.event_participation_id

      where ep.event_id = @event_id

            and br.business_rule_id in (96,97,98,99,100,101,102,103,115,116,117,156,164,165,166,167,168,169,170,171,172,173)

			and t.processed not in (12,14)

      group by Convert(datetime,convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date)))

            ,br.business_rule_id , business_rule_name

      order by Convert(datetime,convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date))) desc

            ,br.business_rule_id , business_rule_name

END
GO
