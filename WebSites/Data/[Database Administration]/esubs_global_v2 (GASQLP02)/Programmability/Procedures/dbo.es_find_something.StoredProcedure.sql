USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_find_something]    Script Date: 1/31/2018 2:49:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*  
  @searchType 'G' = group, 'P' = participant, NULL = ALL  
 exec [es_find_something] 'efund',NULL, NULL, 'CA'--'aim_is_good'  
 exec [es_find_something] 'efund','G', NULL, 'US'--'aim_is_good'  
 exec [es_find_something] 'drew','P', NULL, 'US', null, 8294, 1122894--'aim_is_good'  
*/  
ALTER  PROCEDURE [dbo].[es_find_something]  
 @keyword varchar(255)  
 , @searchType varchar(1) = NULL  
 , @city varchar(255) = NULL  
 , @country_code nvarchar(2) = NULL  
 , @subdivision_code nvarchar(7) = NULL  
 , @partner_id int = NULL
 , @group_id INT = NULL
AS  
BEGIN  
  
 IF @partner_id = 741 OR @partner_id = 0  
 BEGIN  
  SET @partner_id = NULL;  
 END  
  
 IF  @searchType = 'G'  
 BEGIN   
  --set @start = 0  
  select e.event_id  
  , NULL as event_participation_id   
  , e.event_name as [name]  
  , e.culture_code   
  , g.partner_id   
  , (case when pa.country_code is null then  right(e.culture_code, 2)  else pa.country_code end) as country_code   
  , pa.subdivision_code  
  , pa.address_1 as address  
  , pa.city   
  , CAST(COALESCE(eta.total_amount, 0) as decimal) as total_amount  
  , CAST(COALESCE(eta.total_supporters, 0) as integer) as total_supporters  
  , CAST(COALESCE(eta.total_donation_amount, 0) as decimal) as total_donation_amount  
  , CAST(COALESCE(eta.total_donars, 0) as integer) as total_donors  
  , CAST(COALESCE(eta.total_profit, 0) as decimal) as total_profit
  , CASE WHEN total_participants.event_id IS NOT NULL AND total_participants.total_participant > 1 THEN 1 ELSE 0 END as participant_count -- added by JasonF 2016-08-22
  , 'G' as [Type]
    
  from event e with(nolock)  
  inner join event_group eg with(nolock) on eg.event_id = e.event_id   
  inner join event_status es with(nolock) on es.event_status_id = e.event_status_id   
  inner join event_type et with(nolock) on e.event_type_id = et.event_type_id   
  inner join [group] g with(nolock) on g.group_id = eg.group_id   
  inner join payment_info pi with(nolock) on [pi].group_id = g.group_id and [pi].event_id = e.event_id and [pi].active = 1   
  left join postal_address as pa with(nolock) on pa.postal_address_id = [pi].postal_address_id   
  left join event_total_amount eta with (nolock) on e.event_id = eta.event_id 
  left join
	 (select count(*) as total_participant, event_id from event_participation ep with(nolock)
		join member_hierarchy mh on ep.member_hierarchy_id = mh.member_hierarchy_id
		join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
		join member m on mh.member_id = m.member_id

   where ep.participation_channel_id <> 3 and cc.member_type_id = 2 group by event_id) as total_participants on e.event_id = total_participants.event_id
  
  WHERE e.active = 1   
  AND e.displayable = 1   
  AND (pa.city = @city or @city is null)  
  AND (pa.country_code = @country_code or @country_code is null or (pa.country_code is null and right(e.culture_code, 2)=@country_code))  
  AND (pa.subdivision_code = @subdivision_code or @subdivision_code is null)  
  AND (g.partner_id = CAST(@partner_id as varchar(10)) or @partner_id is null)  
  AND (event_name like '%' + REPLACE(LTRIM(RTRIM(@keyword)), ' ', '% ') + '%' or @keyword is null)  
  AND len(e.event_name) > 0
  AND (g.group_id = @group_id OR @group_id IS NULL)

 END   
 if  @searchType = 'P'   
 BEGIN  
   
  select e.event_id  
  , ep.event_participation_id   
  , m.first_name + ' ' + m.last_name as [name]  
  , e.culture_code   
  , g.partner_id   
  , (case when pa.country_code is null then  right(e.culture_code, 2)  else pa.country_code end) as country_code   
  , pa.subdivision_code  
  , pa.address_1 as address  
  , pa.city   
  , CAST(COALESCE(pta.total_amount, 0) as decimal) as total_amount  
  , CAST(COALESCE(pta.total_supporters, 0) as integer) as total_supporters  
  , CAST(COALESCE(pta.total_donation_amount, 0) as decimal) as total_donation_amount  
  , CAST(COALESCE(pta.total_donors, 0) as integer) as total_donors  
  , CAST(COALESCE(pta.total_profit, 0) as decimal) as total_profit
  ,1 as has_participants  -- added by JasonF 2016-08-22
    , 'P' as [Type]
  from   
  event_participation ep with(nolock)  
  inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id   
  inner join member m with(nolock) on m.member_id = mh.member_id  
  inner join event e with(nolock) on ep.event_id = e.event_id   
  inner join event_group eg with(nolock) on eg.event_id = e.event_id   
  inner join event_status es with(nolock) on es.event_status_id = e.event_status_id   
  inner join event_type et with(nolock) on e.event_type_id = et.event_type_id   
  inner join [group] g with(nolock) on g.group_id = eg.group_id   
  inner join payment_info pi with(nolock) on [pi].group_id = g.group_id and [pi].event_id = e.event_id and [pi].active = 1   
  left join postal_address as pa with(nolock) on pa.postal_address_id = [pi].postal_address_id   
  left join participant_total_amount pta with (nolock) on ep.event_participation_id = pta.event_participation_id  
  WHERE e.active = 1   
  AND e.displayable = 1   
  AND (pa.city = @city or @city is null)  
  AND (pa.country_code = @country_code or @country_code is null or (pa.country_code is null and right(e.culture_code, 2)=@country_code))  
  AND (pa.subdivision_code = @subdivision_code or @subdivision_code is null)  
  AND (g.partner_id = @partner_id  or @partner_id is null)    
  AND (pta.participant_name like '%' + REPLACE(LTRIM(RTRIM(@keyword)), ' ', '% ') + '%' or @keyword is null)  
  AND len(pta.participant_name) > 0      
  AND m.password is not null   
  AND m.unsubscribe = 0   
  AND mh.unsubscribe = 0  
  AND mh.active = 1  
  and m.deleted = 0   
  AND (g.group_id = @group_id OR @group_id IS NULL)
  order by pta.participant_name  
 END   
 if  @searchType IS NULL   
 BEGIN  
 --set @start = 0  
  select e.event_id  
  , NULL as event_participation_id   
  , e.event_name as [name]  
  , e.culture_code   
  , g.partner_id   
  , (case when pa.country_code is null then  right(e.culture_code, 2)  else pa.country_code end) as country_code   
  , pa.subdivision_code  
  , pa.address_1 as address  
  , pa.city   
  , CAST(COALESCE(eta.total_amount, 0) as decimal) as total_amount  
  , CAST(COALESCE(eta.total_supporters, 0) as integer) as total_supporters  
  , CAST(COALESCE(eta.total_donation_amount, 0) as decimal) as total_donation_amount  
  , CAST(COALESCE(eta.total_donars, 0) as integer) as total_donors  
  , CAST(COALESCE(eta.total_profit, 0) as decimal) as total_profit  
  , CASE WHEN total_participants.event_id IS NOT NULL AND total_participants.total_participant > 1 THEN 1 ELSE 0 END as participant_count -- added by JasonF 2016-08-22
    , 'G' as [Type]
  from event e with(nolock)  
  inner join event_group eg with(nolock) on eg.event_id = e.event_id   
  inner join event_status es with(nolock) on es.event_status_id = e.event_status_id   
  inner join event_type et with(nolock) on e.event_type_id = et.event_type_id   
  inner join [group] g with(nolock) on g.group_id = eg.group_id   
  inner join payment_info pi with(nolock) on [pi].group_id = g.group_id and [pi].event_id = e.event_id and [pi].active = 1   
  left join postal_address as pa with(nolock) on pa.postal_address_id = [pi].postal_address_id   
  left join event_total_amount eta with (nolock) on e.event_id = eta.event_id
left join (select count(*) as total_participant, event_id from event_participation ep with(nolock) where ep.participation_channel_id <> 3 group by event_id) as total_participants on e.event_id = total_participants.event_id
  
  WHERE e.active = 1   
  AND e.displayable = 1   
  AND (pa.city = @city or @city is null)  
  AND (pa.country_code = @country_code or @country_code is null or (pa.country_code is null and right(e.culture_code, 2)=@country_code))  
  AND (pa.subdivision_code = @subdivision_code or @subdivision_code is null)  
  AND (g.partner_id = CAST(@partner_id as varchar(10)) or @partner_id is null)  
  AND (event_name like '%' + REPLACE(LTRIM(RTRIM(@keyword)), ' ', '% ') + '%' or @keyword is null)  
  AND len(e.event_name) > 0 
  AND (g.group_id = @group_id OR @group_id IS NULL)
  
       
  UNION   
  select e.event_id  
  , ep.event_participation_id   
  , pta.participant_name as [name]
  , e.culture_code   
  , g.partner_id   
  , (case when pa.country_code is null then  right(e.culture_code, 2)  else pa.country_code end) as country_code   
  , pa.subdivision_code  
  , pa.address_1 as address  
  , pa.city   
  , CAST(COALESCE(pta.total_amount, 0) as decimal) as total_amount  
  , CAST(COALESCE(pta.total_supporters, 0) as integer) as total_supporters  
  , CAST(COALESCE(pta.total_donation_amount, 0) as decimal) as total_donation_amount  
  , CAST(COALESCE(pta.total_donors, 0) as integer) as total_donors   
  , CAST(COALESCE(pta.total_profit, 0) as decimal) as total_profit 
  , CASE WHEN total_participants.event_id IS NOT NULL AND total_participants.total_participant > 1 THEN 1 ELSE 0 END as participant_count -- added by JasonF 2016-08-22
    , 'P' as [Type]
  from   
  event_participation ep with(nolock)  
  inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id   
  inner join member m with(nolock) on m.member_id = mh.member_id  
  inner join event e with(nolock) on ep.event_id = e.event_id   
  inner join event_group eg with(nolock) on eg.event_id = e.event_id   
  inner join event_status es with(nolock) on es.event_status_id = e.event_status_id   
  inner join event_type et with(nolock) on e.event_type_id = et.event_type_id   
  inner join [group] g with(nolock) on g.group_id = eg.group_id   
  inner join payment_info pi with(nolock) on [pi].group_id = g.group_id and [pi].event_id = e.event_id and [pi].active = 1   
  left join postal_address as pa with(nolock) on pa.postal_address_id = [pi].postal_address_id   
  left join participant_total_amount pta with (nolock) on ep.event_participation_id = pta.event_participation_id  
  left join (select count(*) as total_participant, event_id from event_participation ep with(nolock)
		join member_hierarchy mh on ep.member_hierarchy_id = mh.member_hierarchy_id
		join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
		join member m on mh.member_id = m.member_id

   where ep.participation_channel_id <> 3 and cc.member_type_id = 2 group by event_id) as total_participants on e.event_id = total_participants.event_id
  --left join (select count(*) as total_participant, event_id from event_participation ep with(nolock) where ep.participation_channel_id <> 3 group by event_id) as total_participants on e.event_id = total_participants.event_id

  WHERE e.active = 1   
  AND e.displayable = 1   
  AND (pa.city = @city or @city is null)  
  AND (pa.country_code = @country_code or @country_code is null or (pa.country_code is null and right(e.culture_code, 2)=@country_code))  
  AND (pa.subdivision_code = @subdivision_code or @subdivision_code is null)  
  AND (g.partner_id = CAST(@partner_id as varchar(10)) or @partner_id is null)  
  AND (pta.participant_name like '%' + REPLACE(LTRIM(RTRIM(@keyword)), ' ', '% ') + '%' or @keyword is null)  
  AND len(pta.participant_name) > 0       
  AND m.password is not null   
  AND m.unsubscribe = 0   
  AND mh.unsubscribe = 0  
  AND mh.active = 1  
  and m.deleted = 0 
  AND (g.group_id = @group_id OR @group_id IS NULL)
  
        
 END   
END  
