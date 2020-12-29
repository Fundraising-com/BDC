USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_choose_member]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: August 9, 2010
-- Description:	Fundraising - Choose a Member
-- exec [es_get_choose_member] NULL, 1333303, NULL
--    exec es_get_choose_member 4125829,1350913
-- =============================================
CREATE PROCEDURE [dbo].[es_get_choose_member]
	@member_hierarchy_id int = NULL, 
    @eventID INT, 
    @keyword varchar(255) = NULL
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @profit_calculated FLOAT

    -- pre-generate the tps
    CREATE TABLE #tps (
		  rownum INT IDENTITY(1,1)
		, orderid INT
		, quantity INT
		, price MONEY
		, suppID INT
        	, item_type_id int
        	, product_type int
		, charge NUMERIC(18,2)
		, updatedate DATETIME
		, catalog_type INT
		, store_id INT
	)    

    -- The top member (@member_hierarchy_id) must be a sponsor
    /* 1) Build participants */
    CREATE TABLE #part (
		part_event_participation_id INT,
        first_name VARCHAR(100), 
        last_name VARCHAR(100),
	    create_date DATETIME
	)
    INSERT INTO #part (
        part_event_participation_id, first_name, last_name, create_date
    )
    SELECT   ep.event_participation_id AS part_event_participation_id, 
            CASE 
				WHEN u.first_name IS NULL AND  LEN(m.first_name) < 20 THEN m.first_name COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.first_name IS NULL AND  LEN(m.first_name) > 20 THEN LEFT(m.first_name, 20) + '...' COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN LEN(u.first_name) > 20 THEN  LEFT(u.first_name, 20) + '...' COLLATE SQL_Latin1_General_CP1_CI_AS 
				ELSE u.first_name COLLATE SQL_Latin1_General_CP1_CI_AS END AS first_name, 

             CASE 
				WHEN u.last_name IS NULL AND m.first_name is not null AND LEN(m.last_name) < 15 THEN  m.last_name
				WHEN u.last_name IS NULL AND m.first_name is not null AND LEN(m.last_name) > 15 THEN  LEFT(m.last_name, 15) + '...'
				WHEN u.last_name IS NULL AND u.first_name is not null AND LEN(m.last_name) < 15 THEN  m.last_name
				WHEN u.last_name IS NULL AND u.first_name is not null AND LEN(m.last_name) > 15 THEN  LEFT(m.last_name, 15) + '...'
				WHEN u.last_name IS NOT NULL AND m.first_name is not null AND LEN(u.last_name) < 15 THEN  u.last_name
				WHEN u.last_name IS NOT NULL AND m.first_name is not null AND LEN(u.last_name) > 15 THEN  LEFT(u.last_name, 15) + '...'
				WHEN u.last_name IS NOT NULL AND u.first_name is not null AND LEN(u.last_name) < 15 THEN  u.last_name
				WHEN u.last_name IS NOT NULL AND u.first_name is not null AND LEN(u.last_name) > 15 THEN  LEFT(u.last_name, 15) + '...'
				
				WHEN u.last_name IS NULL AND LEN(m.last_name) + LEN(m.first_name) < 20 AND u.first_name IS NULL THEN m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NULL AND LEN(m.last_name) + LEN(u.first_name) < 20 THEN m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NULL AND LEN(m.first_name) > 20 AND u.first_name IS NULL THEN '' COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NULL AND LEN(u.first_name) > 20 THEN '' COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NULL AND LEN(m.last_name) + LEN(m.first_name) > 20 AND u.first_name IS NULL THEN LEFT(m.last_name, 20-LEN(m.first_name)-1) + '...' COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NULL AND LEN(m.last_name) + LEN(u.first_name) > 20 THEN LEFT(m.last_name, 20 - LEN(u.first_name)-1) + '...' COLLATE SQL_Latin1_General_CP1_CI_AS 

				WHEN u.last_name IS NOT NULL AND LEN(m.last_name) + LEN(m.first_name) < 20 AND u.first_name IS NULL THEN u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NOT NULL AND LEN(m.last_name) + LEN(u.first_name) < 20 THEN u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NOT NULL AND LEN(m.first_name) > 20 AND u.first_name IS NULL THEN '' COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NOT NULL AND LEN(u.first_name) > 20 THEN '' COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NOT NULL AND LEN(m.last_name) + LEN(m.first_name) > 20 AND u.first_name IS NULL THEN LEFT(u.last_name, 20-LEN(m.first_name)-1) + '...' COLLATE SQL_Latin1_General_CP1_CI_AS 
				WHEN u.last_name IS NOT NULL AND LEN(m.last_name) + LEN(u.first_name) > 20 THEN LEFT(u.last_name, 20 - LEN(u.first_name)-1) + '...' COLLATE SQL_Latin1_General_CP1_CI_AS 

				ELSE u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS END AS last_name, m.create_date
    FROM     event_participation ep with (nolock)
             INNER JOIN member_hierarchy mh with (nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
             INNER JOIN member m with (nolock) ON m.member_id = mh.member_id
             LEFT JOIN [users] u with (nolock) ON m.[user_id] = u.[user_id]
    WHERE    (@member_hierarchy_id IS NULL OR mh.parent_member_hierarchy_id = @member_hierarchy_id)
      AND    (@keyword IS NULL OR 
             LTRIM(RTRIM(CASE WHEN u.first_name IS NULL THEN m.first_name COLLATE SQL_Latin1_General_CP1_CI_AS ELSE u.first_name COLLATE SQL_Latin1_General_CP1_CI_AS END))+' '+LTRIM(RTRIM(CASE WHEN u.last_name IS NULL THEN m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS ELSE u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS END)) LIKE '%' + REPLACE(LTRIM(RTRIM(@keyword)), ' ', '% ') + '%')
      AND     ep.event_id = @eventID AND mh.creation_channel_id in (7, 20, 22, 23, 35, 40) AND mh.active = 1 AND m.deleted = 0  
    ORDER BY 2, 3

    CREATE INDEX ix_event_participation_id1 ON #part (part_event_participation_id)

    /* 2) Build  supporters */
    CREATE TABLE #supp (
        part_event_participation_id INT,
		supp_event_participation_id INT
	)
    INSERT INTO #supp (
        part_event_participation_id, supp_event_participation_id
    )
	SELECT part.part_event_participation_id, part.part_event_participation_id as supp_event_participation_id
    FROM   #part part
    UNION
    SELECT part.part_event_participation_id, ep.event_participation_id as supp_event_participation_id
    FROM   event_participation ep with (nolock)            
            INNER JOIN member_hierarchy mh with (nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member_hierarchy mhp with (nolock) ON mhp.member_hierarchy_id = mh.parent_member_hierarchy_id            
            INNER JOIN event_participation epp with (nolock) ON epp.member_hierarchy_id = mhp.member_hierarchy_id
            INNER JOIN #part part ON part.part_event_participation_id = epp.event_participation_id
    WHERE  mh.active = 1 AND mh.creation_channel_id in (12, 14, 15, 37, 41, 29, 45, 46)

    CREATE INDEX ix_event_participation_id2 ON #supp (part_event_participation_id, supp_event_participation_id)

    /* 3) Build  all supporter sales */
	INSERT INTO #tps (
	   quantity
	 , price
	 , suppID
         , product_type
         , item_type_id
         , charge
         , updatedate
         , catalog_type
         , store_id
	)
	SELECT 
	  quantity
	, sub_total
	, supp_id
	, product_type_id
	, item_type_id
	, (ISNULL(freight,0) + ISNULL(other_fees,0)) AS charge
	, create_date AS updatedate
	, product_type_id as catalog_type
	, store_id
	FROM es_get_valid_orders_items_by_event_id (@eventID)
	ORDER BY create_date

    CREATE INDEX ix_suppid ON #tps (suppID)

    -- Get current profit calculated from event table
    SELECT @profit_calculated = profit_calculated FROM event with (nolock) WHERE event_id = @eventID

	SELECT 
		  part.part_event_participation_id as event_participation_id
		, part.first_name
		, part.last_name
        , part.create_date
        , p.personalization_id
        , COALESCE(p.fundraising_goal,0) as fundraising_goal        
        --, CASE WHEN pim.image_id is not null THEN pim.image_url ELSE '/Resources/Images/sponsor/participant_default.gif' END as image_url        
	    --, ISNULL(pim.image_approval_status_id, 3) as image_approval_status_id
		--, ISNULL(pim.image_id, -1) as image_id
		, '' as image_url
		, -1 as image_approval_status_id
		, -1 as image_id
    	, COALESCE(SUM(case 
			when tps.catalog_type = 18 and tps.store_id = 1 then 0 -- DONATION
			else tps.quantity end),0) as nb_subs
	, COALESCE(SUM(case 
			when tps.catalog_type = 18 and tps.store_id = 1 then 0 -- DONATION
			else tps.price end),0) AS amount
	, COALESCE(SUM(case 
			-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
			when tps.catalog_type = 18 and tps.store_id = 1 then
				(case when tps.updatedate < '2011-04-01' then 
					tps.quantity * tps.price * 93.5/100.0
				else
					tps.quantity * tps.price * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
			when tps.item_type_id = 6 and tps.store_id = 10 then -- Personalized product coming from GA storefront, which is only 25% profit and there is no way to do profit by product line at the moment in esubs
				tps.price * 25.0/100.0 
			else
			-- For all other product percent profit use event profit calculated field (January 6, 2011)
				(tps.price - charge) * Isnull(@profit_calculated, 40.0)/100.0 end),0) AS profit
    	, COALESCE(SUM(efrcstats.quantity * efrcstats.price), 0) AS total_donation_amount
	FROM  #part part
	      INNER JOIN  #supp supp ON part.part_event_participation_id = supp.part_event_participation_id
          INNER JOIN dbo.personalization p with (nolock) ON part.part_event_participation_id = p.event_participation_id
          --LEFT JOIN dbo.personalization_image pim with (nolock) ON p.personalization_id = pim.personalization_id and deleted = 0          
		  LEFT JOIN  #tps tps ON tps.suppid = supp.supp_event_participation_id
		  
		  -- get donation profit from efrcommon.dbo.profit
	      INNER JOIN event_participation ep with (nolock) ON part.part_event_participation_id = ep.event_participation_id
		  INNER JOIN event e with (nolock) ON ep.event_id = e.event_id
		  LEFT OUTER JOIN efrcommon.dbo.profit donation_profit with (nolock) 
			ON e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11

		  -- EFR Donation orders from EFRECommerce db:
		  LEFT JOIN (
				select efreo.ext_participation_id, efreod.quantity, efreod.price
				from EFRECommerce.dbo.[order] efreo WITH (NOLOCK)
				join EFRECommerce.dbo.order_detail efreod WITH (NOLOCK) ON efreo.order_id = efreod.order_id AND efreod.deleted = 0 AND efreod.product_id = 1
				join dbo.es_get_valid_efrecommerce_order_status() efreos ON efreo.status_id = efreos.status_id
				where efreo.deleted = 0 AND efreo.source_id = 1
		  ) efrcstats on efrcstats.ext_participation_id = supp.supp_event_participation_id
	GROUP BY 
		  part.part_event_participation_id,
		  part.first_name, 
          part.last_name,
          part.create_date,
          p.personalization_id,
          p.fundraising_goal
       -- pim.deleted,
       -- pim.image_url,
	   -- pim.image_approval_status_id,
	   -- pim.image_id
	ORDER BY 2, 3
END
GO
