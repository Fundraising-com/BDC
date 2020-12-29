USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_all_flagpoles]    Script Date: 02/14/2014 13:03:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE           procedure [dbo].[crm_get_all_flagpoles]
           
as



SELECT     
          c.name, 
          l.lead_id,
          l.day_phone,
          l.evening_phone,
          c2.name, 
          min(qsp.account_name) as account_name, 
          l.lead_entry_date,
          p.description, 
          sum(qsp.total_sold) as qsp_total_sold,
          sum(s.total_amount) as efund_total_sold,
                 
                     count(qsp.account_no) as nb_account,
                     max(qsp.fm_id) as fmid,
                     count(distinct qsp.fm_id) as nb_fm,
                     max(qsp.status) as status,
                     max(qsp.email) as email

                                           
            FROM      lead l inner JOIN  --je prend mes matching code de 3 colonnes et les mets sous la meme
                            (select account_no, matching_code,account_name, fm_id, total_sold, email, status from (  
                                     select account_no, qsp_account_matching_code as matching_code, account_name, fm_id, total_sold, email, status from crm_static_Past3Seasons_new
                                     UNION ALL
                                     select account_no, qsp_cust_billing_matching_code as matching_code, account_name, fm_id, total_sold,email, status from crm_static_Past3Seasons_new
                                     UNION ALL
                                     select account_no, qsp_cust_shipping_matching_code as matching_code,account_name, fm_id, total_sold, email, status from crm_static_Past3Seasons_new
                              ) a
                             group by account_no, matching_code,account_name, fm_id, total_sold, email, status)qsp
                             ---------------------------------------------------------------------
                      on qsp.matching_code = l.matching_code
                      inner join consultant c on l.consultant_id = c.consultant_id
                      inner join promotion p on l.promotion_id = p.promotion_id
                      left join consultant c2 on qsp.fm_id = c2.ext_consultant_id
                      left join client cl
                        on l.lead_id = cl.lead_id
                      left join sale s
                         on cl.client_id = s.client_id and cl.client_sequence_code = s.client_sequence_code
            WHERE     l.consultant_id <> 0 and l.consultant_id <> 9000 and c.is_active != 0 and c.is_fm = 0 and
                      p.promotion_id <> 2834 --product specialists
                       aND l.matching_code <> 'invalid'
                       and qsp.account_no is not null
            GROUP BY l.lead_id,c.name,l.lead_entry_date, p.description, l.day_phone,
                          l.evening_phone,c2.name
            order by l.lead_entry_date desc
GO
