USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_by_order_date]    Script Date: 08/21/2014 15:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	EXEC [dbo].[es_get_event_by_order_date] '07/01/2014','08/01/2014'
*/
	ALTER PROCEDURE [dbo].[es_get_event_by_order_date]
		  @startdate datetime
	    , @enddate datetime
	AS
	BEGIN
		DECLARE @startdate2 DATETIME
		DECLARE @enddate2 DATETIME

		SET @startdate2 = @startdate
		SET @enddate2 = @enddate

		create table #payment_cancelled (
			qsp_order_detail_id int
		)

		create table #event_order (
			event_id int
		)

		insert into #payment_cancelled
		select distinct qsp_order_detail_id 
		from payment_item
		where payment_id in 
			(select pps.payment_id
			from payment_payment_status pps
				join (
					select payment_id, max(create_date) as create_date
					from payment_payment_status 
					group by payment_id
				) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date      
			where pps.payment_status_id not in (9) -- Payment Cancelled
			  and datepart(YEAR,pps.create_date)=DATEPART(year,getdate())
			)

		insert into #event_order
		select ep.event_id
		from event_participation ep
			join [dbo].[es_get_valid_orders_items]() tps on tps.supp_id = ep.event_participation_id
			left join #payment_cancelled pc 
				on tps.order_item_id = pc.qsp_order_detail_id
		where (tps.create_date between @startdate2 and @enddate2)
		  and pc.qsp_order_detail_id is null
		group by ep.event_id

		select e.event_id
		    , e.event_status_id
		    , e.culture_code
		    , e.event_name
		    , e.start_date
		    , e.end_date	
		    , e.active
		    , e.comments
		    , e.create_date
		    , es.event_status_name
		    , eg.group_id
		    , g.partner_id
		    , ep.event_participation_id  as sponsor_event_participation_id
		    , isnull(p.redirect,'') as redirect
			, e.group_type_id
			, e.group_type_id 
			, e.[profit_group_id]
			, e.[profit_calculated]
			, e.[processing_fee]
			, e.event_type_id
			, et.event_type_name
			, e.donation
			, e.discount_site
	    from event e (nolock)
		join event_type et (nolock)
		    on et.event_type_id = e.event_type_id
		join event_status es (nolock)
				ON es.event_status_id = e.event_status_id 
		join event_group eg (nolock)
		    on eg.event_id = e.event_id
		join [group] g (nolock)
		    on g.group_id = eg.group_id
		join event_participation ep (nolock)
		    on ep.event_id = e.event_id
		join #event_order eo
		    on eo.event_id = e.event_id
		left join dbo.personalization p (nolock)
		    on p.event_participation_id = ep.event_participation_id
	    where ep.participation_channel_id = 3 and g.partner_id  NOT in (719, 816)

	END
