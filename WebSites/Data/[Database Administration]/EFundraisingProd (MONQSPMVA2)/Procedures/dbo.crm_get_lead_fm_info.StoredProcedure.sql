USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_lead_fm_info]    Script Date: 02/14/2014 13:03:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--
-- =========================================================
-- Author:		JF Lavigne
-- Create date: 
-- Description:	Agit comme get_unassigned_leads 
--              (donne les info des accounts qsp) 
--              mais inclu le lead associer a un 
--              consultant et recoit un lead en parametre
-- =========================================================
CREATE PROCEDURE [dbo].[crm_get_lead_fm_info]
    @lead_id int
AS
BEGIN
SELECT l.lead_id
       , left(l.country_code,2) as country_code
        ,l.fk_kit_type_id
       , l.lead_entry_date
       , l.lead_assignment_date AS Assignment_Date
       , l.organization
       , pt.Description AS Promo_Type
       , p.description AS Promotion
       , gt.description AS Group_Type
       , fy.lead_id as flagpole_lead
       , ls.Description AS Status,
         l.state_code,
         l.participant_count AS Part,
         l.day_phone,
         l.lead_status_id, 
         l.channel_code AS Channel
        ,ot.organization_type_desc
       , pgp.promo_group_id,
         fy.fm_nb_account,
         fy.fmid,
         fy.nb_fm,  
         fy.status,
         fy.email,
       c.ext_consultant_id,
       c.consultant_id,
       c.name, 
       c.is_active,
       c.email_address

FROM Lead l 
    INNER JOIN Promotion p ON l.promotion_id = p.promotion_id 
    LEFT JOIN promotion_group_promotion pgp on p.promotion_id = pgp.promotion_id
    INNER JOIN Promotion_Type pt ON p.promotion_type_code = pt.Promotion_Type_Code 
    LEFT JOIN Group_Type gt ON l.group_type_id = gt.group_type_id 
    INNER JOIN Lead_Status ls ON l.lead_status_id = ls.Lead_Status_ID 
    INNER JOIN Organization_Type ot ON l.organization_type_id = ot.organization_type_id 
    INNER JOIN (SELECT l.lead_id,
                       sum(qsp.total_sold) as total_sold,
                       count(qsp.account_no) as fm_nb_account,
                       max(qsp.fm_id) as fmid,
                       count(distinct qsp.fm_id) as nb_fm,
                       max(qsp.status) as status,
                       max(qsp.email) as email
               FROM lead l 
                INNER JOIN (SELECT account_no
                                 , matching_code
                                 , account_name
                                 , fm_id
                                 , total_sold
                                 , email
                                 , status
                                 , first_name
                                 , last_name
                                 , home_phone
                                 , work_phone
                                 , mobile_phone 
                            FROM (SELECT account_no
                                       , qsp_account_matching_code as matching_code
                                       , account_name
                                       , fm_id
                                       , total_sold
                                       , email
                                       , status,first_name
                                       , last_name
                                       , home_phone
                                       , work_phone
                                       , mobile_phone 
                                  FROM crm_static_Past3Seasons_new
                                  UNION ALL
                                  SELECT account_no
                                       , qsp_cust_billing_matching_code as matching_code
                                       , account_name
                                       , fm_id
                                       , total_sold
                                       , email
                                       , status
                                       , first_name
                                       , last_name
                                       , home_phone
                                       , work_phone
                                       , mobile_phone 
                                  FROM crm_static_Past3Seasons_new
                                  UNION ALL
                                  SELECT account_no
                                       , qsp_cust_shipping_matching_code as matching_code
                                       , account_name
                                       , fm_id
                                       , total_sold
                                       , email
                                       , status
                                       , first_name
                                       , last_name
                                       , home_phone
                                       , work_phone
                                       , mobile_phone 
                                  FROM crm_static_Past3Seasons_new
                              ) a
                             GROUP BY account_no
                                    , matching_code
                                    , account_name
                                    , fm_id
                                    , total_sold
                                    , email
                                    , status
                                    , first_name
                                    , last_name
                                    , home_phone
                                    , work_phone
                                    , mobile_phone
                         )qsp ON qsp.matching_code = l.matching_code
            WHERE l.lead_id = @lead_id 
              AND l.matching_code <> 'invalid'
            GROUP BY l.lead_id) fy ON l.Lead_id = fy.lead_id
    LEFT JOIN consultant c ON fy.fmid = c.ext_consultant_id
END
GO
