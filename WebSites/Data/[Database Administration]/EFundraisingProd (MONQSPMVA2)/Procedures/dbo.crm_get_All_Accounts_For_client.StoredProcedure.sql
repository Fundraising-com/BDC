USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_All_Accounts_For_client]    Script Date: 02/14/2014 13:03:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Pour les flapgpoles, sort les clients qui match avec les accounts de qsp
--JF Lavigne

CREATE         procedure [dbo].[crm_get_All_Accounts_For_client] --39987,'UI'
                     @client_id int,
                     @client_sequence_code varchar(4)
as

            SELECT    l.lead_id,
                      qsp.first_name + ' ' + qsp.last_name as name,    
                      qsp.account_no,
                      qsp.account_name,
                      qsp.total_sold,
                      qsp.matching_code,
                      qsp.fm_ID,
                      qsp.status,
                      qsp.email,
                      qsp.home_Phone,
                      qsp.work_Phone,
                      qsp.mobile_Phone 
                      
            FROM      client_address ca inner join 
                      client c on ca.client_id = c.client_id and ca.client_sequence_code = c.client_sequence_code inner join
                      lead l on c.lead_id = l.lead_id INNER JOIN
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
                         on qsp.matching_code = ca.matching_code
                      

            WHERE     ca.client_id = @client_id and ca.client_sequence_code = @client_sequence_code and
                      ca.matching_code <> 'invalid'
GO
