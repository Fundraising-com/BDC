USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_participants_by_partner_id]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: September 5, 2011
-- Description:	
--    exec es_get_participants_by_partner_id 143
-- =============================================
CREATE PROCEDURE [dbo].[es_get_participants_by_partner_id]
    @partner_id int
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
        , product_type int
		, charge NUMERIC(18,2)
		, updatedate DATETIME
		, catalog_type INT
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
             CASE WHEN u.first_name IS NULL THEN m.first_name COLLATE SQL_Latin1_General_CP1_CI_AS ELSE u.first_name COLLATE SQL_Latin1_General_CP1_CI_AS END AS first_name, 
             CASE WHEN u.last_name IS NULL THEN m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS ELSE u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS END AS last_name, m.create_date
    FROM     event_participation ep with (nolock)
             INNER JOIN member_hierarchy mh with (nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
             INNER JOIN member m with (nolock) ON m.member_id = mh.member_id
             LEFT JOIN [users] u with (nolock) ON m.[user_id] = u.[user_id]
    WHERE    m.partner_id = @partner_id AND mh.creation_channel_id in (7, 22, 23, 35, 40) AND mh.active = 1 AND m.deleted = 0  
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
    WHERE  mh.active = 1 AND mh.creation_channel_id in (12, 14, 15, 37, 41, 29, 45)

    CREATE INDEX ix_event_participation_id2 ON #supp (part_event_participation_id, supp_event_participation_id)


	SELECT 
		  part.part_event_participation_id as event_participation_id
		, part.first_name
		, part.last_name
        , part.create_date
        , p.personalization_id
        , COALESCE(p.fundraising_goal,0) as fundraising_goal
        -- UPDATED LOGIC FOR IMAGE TO SHOW
        , pim.image_url
	    , pim.image_approval_status_id
		, pim.image_id
		, SUM(COALESCE(pta.items, 0)) as nb_subs
		, SUM(COALESCE(pta.total_amount, 0)) AS amount
		, SUM(COALESCE(pta.total_supporters, 0)) AS total_supporters
		, 0 AS profit -- NOT SUPPORTED YET
		, SUM(COALESCE(pta.total_donation_amount, 0)) AS total_donation_amount
		, SUM(COALESCE(pta.total_donors, 0)) AS total_donors
	FROM  #part part
          INNER JOIN dbo.personalization p with (nolock) ON part.part_event_participation_id = p.event_participation_id
          LEFT JOIN dbo.personalization_image pim with (nolock) ON p.personalization_id = pim.personalization_id
          LEFT JOIN  #supp supp ON part.part_event_participation_id = supp.part_event_participation_id
		  LEFT JOIN  participant_total_amount pta ON supp.supp_event_participation_id = pta.event_participation_id
    WHERE ISNULL(pim.deleted, 0) = 0
	GROUP BY 
		  part.part_event_participation_id,
		  part.first_name, 
          part.last_name,
          part.create_date,
          p.personalization_id,
          p.fundraising_goal,
    	  pim.image_url,
		  pim.image_approval_status_id,
	      pim.image_id
	ORDER BY 2, 3
END
GO
