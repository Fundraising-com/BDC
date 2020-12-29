USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business154]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Pavel Tarassov
-- Create date: 09-11-2009
-- Updated: 10-23-2013 by Jiro Hidaka
-- Description: Runs once per day, sends email to the sponsor of the campaing
-- whenever first subs is generated in the event but sponsor did not provide 
-- the payment info yet (missing payment_info is null still)
-- =============================================

CREATE PROCEDURE [dbo].[es_touch_generate_business154]
AS
BEGIN
SET NOCOUNT ON;
		
declare @touch_info_id int

begin transaction
		
insert into touch_info(
	business_rule_id
	,launch_date
	,create_date
)values(
	154
	,getdate()
	,getdate()
)
		
set @touch_info_id = SCOPE_IDENTITY()
		
if @@error <> 0
begin
	rollback transaction
end
else
begin		
		
insert into touch(event_participation_id,touch_info_id,processed,create_date)
select
ep.event_participation_id
,@touch_info_id as touch_info_id
,0 as processed
, getdate() as create_date
		from
		event e with (nolock)
		inner join event_group eg with (nolock)
		on eg.event_id = e.event_id
		inner join [group] g with (nolock)
		on g.group_id = eg.group_id
		inner join event_participation ep with (nolock)
		on e.event_id = ep.event_id
		and ep.participation_channel_id = 3 --sponsor
		inner join payment_info [pi] with (nolock)
		on [pi].group_id = g.group_id
		and [pi].event_id = e.event_id and pi.active = 1
		left join postal_address pa with (nolock)
		on [pi].postal_address_id = pa.postal_address_id
		inner  join (
		                select distinct ep.event_id from
						esubs_global_v2.dbo.es_get_valid_orders_items() tps
						join esubs_global_v2.dbo.event_participation ep with (nolock) on tps.supp_id = ep.event_participation_id
						WHERE  tps.create_date > '02-01-2013'
						group by ep.event_id
						having count (tps.order_id) >= 1
		) tps on tps.event_id = e.event_id
		left outer join 
		(
			select  t.event_participation_id 	
			, max(t.create_date) as create_date 
			from
			touch t with(nolock)
			inner join touch_info ti with(nolock)
			on ti.touch_info_id = t.touch_info_id
			and business_rule_id = 154
			group by t.event_participation_id 
		) t
		on t.event_participation_id = ep.event_participation_id		
		
		where
		(t.event_participation_id is null)
		and ([pi].postal_address_id is null or ([pi].postal_address_id is not null and pa.address_1 is null and pa.zip_code is null and pa.city is null))
		and e.culture_code = 'en-US'
		and g.partner_id <> 143
		and e.create_date > '01-01-2012'
		and e.active = 1	

	IF @@ROWCOUNT = 0 -- no rows inserted
	begin
		rollback transaction
	end
	else
		begin
			commit transaction
		end
	end
END
GO
