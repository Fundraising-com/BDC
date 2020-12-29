USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_event_profit_range]    Script Date: 02/14/2014 13:07:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_update_event_profit_range]
	 @event_id int 
	, @nbsubs int 
AS
BEGIN

--drop table #profit 
create table #profit (
        rownum int identity(1,1)
        , event_id int
        , profit_id int
        , profit_range_id int
        , profit_calculated int
		, profit_percentage int
		, profit_range_percentage int
    )

	INSERT INTO #profit (
        event_id
        , profit_id 
        , profit_range_id 
        , profit_calculated 
		, p.profit_percentage 
		, pr.profit_range_percentage 
	)
		select e.event_id 
				, e.profit_group_id
				, pr.profit_range_id
				, e.profit_calculated
				, p.profit_percentage
				, pr.profit_range_percentage
				--, pr.min_sub
			from esubs_global_v2.dbo.event e with(nolock) 
			inner join EFRCommon.dbo.profit p with(nolock) on p.profit_group_id = e.profit_group_id and p.qsp_catalog_type_id is null
			left join  EFRCommon.dbo.profit_range pr with(nolock) on p.profit_id = pr.profit_id and @nbSubs >= min_sub
			where e.event_id = @event_id 
			group by e.event_id 
				, e.profit_group_id
				, pr.profit_range_id
				, e.profit_calculated
				, p.profit_percentage
				, pr.profit_range_percentage


update esubs_global_v2.dbo.[event] set profit_calculated = new_profit_calculated 
from esubs_global_v2.dbo.[event] e with(nolock) 
inner join  (
	select distinct event_id
		, profit_calculated 
		, (case when profit_range_percentage is null then profit_percentage
			else  profit_percentage+sum(profit_range_percentage) end ) as new_profit_calculated
	from   #profit
	group by  event_id
		, profit_calculated 
		, profit_percentage
		, profit_range_percentage
	having profit_calculated <> (case when profit_range_percentage is null then profit_percentage 
		else  profit_percentage+sum(profit_range_percentage) end ) 
)ep on ep.event_id = e.event_id 


update esubs_global_v2.dbo.event_profit_range set is_cancelled = 1, cancelled_date = getdate()
where event_profit_range_id in(
select event_profit_range_id
from esubs_global_v2.dbo.event_profit_range epr with (nolock) 
	left join  #profit p
		on  p.profit_id = epr.profit_id and  p.profit_range_id = epr.profit_range_id and is_cancelled = 0
	where p.profit_range_id is null 
)

insert into esubs_global_v2.dbo.event_profit_range  ( event_id, profit_id, profit_range_id) 
select  p.event_id, p.profit_id, p.profit_range_id from #profit p
	left join  esubs_global_v2.dbo.event_profit_range epr with (nolock) 
		on p.event_id = epr.event_id  and p.profit_id = epr.profit_id and  p.profit_range_id = epr.profit_range_id and is_cancelled = 0
	where epr.profit_range_id is null and p.profit_range_id is not null 



--select * from   #profit
--select * from EFRCommon.dbo.event_profit_range 
--select profit_calculated from esubs_global_v2.dbo.[event] e with(nolock)  where e.event_id = @event_id 

end
GO
