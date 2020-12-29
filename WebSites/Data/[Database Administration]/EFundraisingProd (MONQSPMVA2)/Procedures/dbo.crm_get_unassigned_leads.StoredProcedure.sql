USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_unassigned_leads]    Script Date: 02/14/2014 13:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JF Lavigne
-- ALTER  date: <ALTER  Date,,>
-- Description:	<Description,,>
--
-- Modified by: JF Lavigne
-- Modified date: 2006/01/29
--
-- Modified by: JF Lavigne
-- Modified date: 2006/02/02
-- =============================================
CREATE       procedure [dbo].[crm_get_unassigned_leads] --0
	@promo_group_id int 
as
create table #temp (lead_id INT PRIMARY KEY,
                    country_code varchar(5),
                    fk_kit_type_id int,
                    lead_entry_date datetime,
                    lead_assignment_date datetime,
                    organization varchar(100),
                    promo_type varchar(100),
                    promotion_type_code varchar(100),
                    promotion varchar (100), 
                    group_type varchar(100),
                    ext_consultant varchar(100),
                    flagpole_lead int,
                    status varchar(50),
                    state_code varchar(50),
                    part int,
                    day_phone varchar (20),
                    evening_phone varchar (20),
                    lead_status_id int, 
                    Channel varchar(10),
                    organization_type_desc varchar(50),
                    promo_group_id int,
                    fm_nb_account int,
                    fmid int,
                    nb_fm int,
                    fm_status int,
                    fm_email varchar(50),
                    comment varchar(3),
                    decision_maker varchar(3))

insert into #temp
SELECT distinct l.lead_id
       , left(l.country_code,2) as country_code
        ,l.fk_kit_type_id
       , l.lead_entry_date
       , l.lead_assignment_date AS Assignment_Date
       , l.organization
       , pt.promotion_type_name  AS Promo_Type
       , p.promotion_type_code
       , p.promotion_name AS Promotion
       , gt.description AS Group_Type
       , c.name as external_consultant
       , fy.lead_id as flagpole_lead
       , ls.Description AS Status,
         l.state_code + ' (' + left(l.country_code,2) + ')' as country_code 
       , l.participant_count AS Part,
         l.day_phone,
         l.evening_phone, 
         l.lead_status_id, 
         l.channel_code AS Channel
        ,ot.organization_type_desc
       , pgp.promo_group_id,
         fy.nb_account,
         fy.fmid,
         fy.nb_fm,  
         fy.status,
         fy.email,
         case when len(substring(comments,0, charindex('///',comments))) > 5 or 
                 (len(ltrim(rtrim(comments))) > 5 and charindex('///',comments) = 0)
                 then 'yes' else 'no' end as comment,

         case when (decision_maker = 0) then 'no' else 'yes' end as comment


FROM Lead l LEFT JOIN
     consultant c on l.ext_consultant_id = c.consultant_id INNER JOIN
     efrcommon..Promotion p ON l.promotion_id = p.promotion_id LEFT JOIN 
     promotion_group_promotion pgp on p.promotion_id = pgp.promotion_id  INNER JOIN
     efrcommon..promotion_type pt ON p.promotion_type_code = pt.Promotion_Type_Code LEFT JOIN 
     Group_Type gt ON l.group_type_id = gt.group_type_id INNER JOIN 
     Lead_Status ls ON l.lead_status_id = ls.Lead_Status_ID INNER JOIN 
     Organization_Type ot ON l.organization_type_id = ot.organization_type_id left JOIN 
               ----un lead peux avoir plusieurs accounts
          (SELECT     l.lead_id,
                      sum(qsp.total_sold) as total_sold,
                      count(qsp.account_no) as nb_account,
                      max(qsp.fm_id) as fmid,
                      count(distinct qsp.fm_id) as nb_fm,
                      max(qsp.status) as status,
                      max(qsp.email) as email
                                           
            FROM      lead l INNER JOIN
                             --je prend mes matching code de 3 colonnes et les mets sous la meme
                            (select account_no, matching_code,account_name, fm_id, total_sold, email, status, first_name, last_name, home_phone, work_phone, mobile_phone from (  
                                     select account_no, qsp_account_matching_code as matching_code, account_name, fm_id, total_sold, email, status,first_name, last_name, home_phone, work_phone, mobile_phone from crm_static_Past3Seasons_new
                                     UNION ALL
                                     select account_no, qsp_cust_billing_matching_code as matching_code, account_name, fm_id, total_sold,email, status,first_name, last_name, home_phone, work_phone, mobile_phone from crm_static_Past3Seasons_new
                                     UNION ALL
                                     select account_no, qsp_cust_shipping_matching_code as matching_code,account_name, fm_id, total_sold, email, status, first_name, last_name, home_phone, work_phone, mobile_phone from crm_static_Past3Seasons_new
                              ) a
                             group by account_no, matching_code,account_name, fm_id, total_sold, email, status,first_name, last_name, home_phone, work_phone, mobile_phone
                         )qsp
                             ---------------------------------------------------------------------
                         on qsp.matching_code = l.matching_code
            WHERE     l.consultant_id = 0 aND l.matching_code <> 'invalid'
            GROUP BY l.lead_id) fy ON l.Lead_id = fy.lead_id  --12634
where l.consultant_id = 0 and lead_entry_date > '2008-01-01'


--select * from lead where lead_id = 525454

--///////////////////////////
--////////////////////////

select * from #temp where flagpole_lead is null ORDER BY lead_entry_date DESC,lead_id DESC

select t.flagpole_lead as lead_id,
       t.fm_nb_account,
       c.ext_consultant_id,
       c.consultant_id,
       t.fmid,
       t.nb_fm,
       c.name, 
       c.is_active,
       c.email_address
from #temp t left join
     consultant c on t.fmid = c.ext_consultant_id
where flagpole_lead is not null

drop table #temp
GO
