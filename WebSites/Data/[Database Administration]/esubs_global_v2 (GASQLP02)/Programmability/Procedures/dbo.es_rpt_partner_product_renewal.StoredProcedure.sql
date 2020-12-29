USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_product_renewal]    Script Date: 02/14/2014 13:06:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rpt_partner_product_renewal]
    @start_date datetime,
    @end_date datetime,
    @partner_id int
AS
BEGIN
      declare @_start_date datetime, @_end_date datetime, @_partner_id int;
      
      set @_start_date = @start_date;
      set @_end_date = @end_date;
      set @_partner_id = @partner_id;
      
    -- pre-generate the tps
    create table #tps (
        rownum int identity(1,1)
        , orderid int
        , quantity int
        , price money
        , suppID int   
        , charge numeric(18,2)
        , supporter varchar(50)
        , order_date datetime
            , product_type_name varchar(250) 
            , catalog_item_name varchar(250)
            , renewal bit
    )

    INSERT INTO #tps (
        orderid
        , quantity
        , price
        , suppID
        , charge
        , supporter
        , order_date
        , product_type_name
            , catalog_item_name
            , renewal
    )
    SELECT order_id, 
    	quantity,
    	price, 
    	supp_id AS suppid,
    	freight AS charge,
    	supp_name AS supporter,
    	create_date AS order_date,
    	product_type_desc AS product_type_name, 
    	product_desc AS catalog_item_name,
    	isrenewal AS renewal
    FROM [dbo].[es_get_valid_orders_items] ()
    WHERE create_date between @_start_date and @_end_date
        ORDER BY create_date 
           , price

    create index ix_suppid on #tps (suppid) 

    -- header du rapport
    select '-Campaign ID'
           , 'Group Name'
           , 'Member Name'
           , 'Supporter Name'
           , 'Product Type'
           , 'Product Name'
           , '$Product Price'
           , 'Renewal'
               , 'Order Date'

      select      
        ep.event_id
     ,  e.event_name
      ,    (case 
            -- sponsor order must be under his name
            when (mp.first_name + ' ' + mp.last_name) is null 
            then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
            -- participant orders must be under his name
            when cc.member_type_id = 2
            then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
            else dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
            end) as member_name
      ,  (case
            when (m.first_name + ' ' + m.last_name) is null
            then dbo.TitleCase(lower(tps.supporter)) COLLATE SQL_Latin1_General_CP1_CI_AS
            else dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
            end) as supporter_name  
    ,  (case when product_type_name = 'Pinevalley' then 'Cookie Dough' 
        else product_type_name end) as product_type
      ,  dbo.TitleCase(lower(ISNULL(tps.catalog_item_name, ''))) as product_name
      ,  COALESCE(tps.price, 0) as product_price
    ,  (case when tps.renewal = 1 then 'YES' else 'NO' end) as Renewal
    ,  tps.order_date
      from event_participation ep with (nolock)
      inner join event_group eg with (nolock) on eg.event_id = ep.event_id 
      inner join [group] g with (nolock) on g.group_id = eg.group_id 
      inner join event e with (nolock) on ep.event_id = e.event_id 
      -- get the partner profit percent
      --left outer join partner_payment_config ppc
      --on g.partner_id=ppc.partner_id
      --order
      inner join #tps tps
      on tps.suppid = ep.event_participation_id
      -- enfant
      inner join member_hierarchy mh with (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
      inner join member m with (nolock) on m.member_id = mh.member_id
      -- parent
      left outer join member_hierarchy mhp with (nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
      left outer join member mp with (nolock) on mp.member_id = mhp.member_id
      left join creation_channel cc with (nolock) on cc.creation_channel_id = mh.creation_channel_id
      where g.partner_id = @_partner_id
          and mh.active = 1 
      group by 
    ep.event_id
     , e.event_name
      , (case 
      -- sponsor order must be under his name
      when (mp.first_name + ' ' + mp.last_name) is null 
      then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
      -- participant orders must be under his name
      when cc.member_type_id = 2
      then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
      else dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
      end)
      ,(case
      when (m.first_name + ' ' + m.last_name) is null
      then dbo.TitleCase(lower(tps.supporter)) COLLATE SQL_Latin1_General_CP1_CI_AS
      else dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
      end)
      , tps.product_type_name    
    , tps.catalog_item_name
      , tps.price
      , tps.renewal
    , tps.order_date
      order by 1, 2

END
GO
