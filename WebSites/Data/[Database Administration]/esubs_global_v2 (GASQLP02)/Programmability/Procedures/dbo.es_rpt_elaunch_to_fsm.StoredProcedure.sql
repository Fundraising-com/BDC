USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_elaunch_to_fsm]    Script Date: 02/14/2014 13:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* 

created by: Mel		2006/03/22
*/
CREATE   PROCEDURE [dbo].[es_rpt_elaunch_to_fsm]
AS


select 'FM Name'
	, '+Email Sent to FM'
	, '+Confirmation Page Open'
	, '+FM Clicked Yes'
	, '+FM Clicked No'
  	, '+Email sent'
	, '+Email Open'
	, '+Email Bounced'

select 	 fm_name
		, count(distinct external_account_id)   as email_sent
		--, count(distinct external_account_id)   as accounts
		, sum(case when action_id = 2001 then 1 else 0 end) as pageopen
		, sum(case when action_id = 2002 then 1 else 0 end) as aYes
		, sum(case when action_id = 2003 then 1 else 0 end) as aNo
		--, ea.food_account_id
		--, sum(case when taction_id IS NULL then 1 
		--		else 0 end) as no_action
		, sum(case when action_id = 1 or action_id = 103 then 1 
				--when ta.action_id = 103 then 1 
				else 0 end) as Sent
		, sum(case when action_id = 2 then 1 
				else 0 end) as [open]
		, sum(case when action_id = 3 then 1 
				else 0 end) as bounc
from (
select	distinct 
	ea.external_account_id
	, fsm.first_name + ' ' + fsm.last_name as fm_name
	--, ea.external_account_id
	--, eaa.action_id as eaction_id
	, ta.action_id as action_id
	
	FROM external_account ea
		INNER JOIN QSPFulfillment..account a
			ON a.account_id = ea.food_account_id
		INNER JOIN QSPFulfillment..field_sales_manager fsm
			ON fsm.fm_id = a.fm_id
           		AND fsm.[deleted] = 0
		Left outer join external_account_action eaa
			on ea.external_account_id = eaa.external_account_id
		left outer join touch t 
			on ea.event_participation_id=t.event_participation_id 
		left outer join esubs_global_v2.dbo.touch_action ta
			on t.touch_id = ta.touch_id 
	--select * from Action		
union 
select	distinct 
	ea.external_account_id
	, fsm.first_name + ' ' + fsm.last_name as fm_name
	--, ea.external_account_id
	, eaa.action_id 
	--, ta.action_id as taction_id
	
	FROM external_account ea
		INNER JOIN QSPFulfillment..account a
			ON a.account_id = ea.food_account_id
		INNER JOIN QSPFulfillment..field_sales_manager fsm
			ON fsm.fm_id = a.fm_id
           		AND fsm.[deleted] = 0
		Left outer join external_account_action eaa
			on ea.external_account_id = eaa.external_account_id
		left outer join touch t 
			on ea.event_participation_id=t.event_participation_id 
		left outer join esubs_global_v2.dbo.touch_action ta
			on t.touch_id = ta.touch_id 
	--select * from Action		

) r
group by 
	fm_name
		
order by 1
GO
