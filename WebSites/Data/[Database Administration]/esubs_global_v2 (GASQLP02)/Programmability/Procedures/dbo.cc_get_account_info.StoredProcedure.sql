USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_account_info]    Script Date: 02/02/2015 14:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    Created by : Philippe Girard
    ALTER  on  : 2007-07-09
    Description: This stored procedure gets all the info of an account
*/

ALTER PROCEDURE [dbo].[cc_get_account_info] 
	@event_id int
AS
BEGIN
        SELECT e.event_id
              ,e.event_name
              ,e.active
              ,e.end_date
              ,e.create_date
              ,g.group_name
              ,g.lead_id
              ,g.external_group_id
              ,g.group_id
              ,p.partner_name
              ,(case when e.end_date is null then 'false' else 'true' end) as end_event
              ,pi.payment_name
              ,pi.active as active_payment_info
              ,pa.address_1 as address
              ,pa.city
              ,pa.zip_code
              ,pa.country_code
              ,right(sd.subdivision_code,2) as state_code
              ,a.account_number
              ,u.first_name + ' ' + u.last_name as sponsor_name
              ,pt.partner_type_name
              ,2 as address_type
              ,e.event_status_id
              ,e.event_type_id
         
        FROM event e
            join event_group eg
                on eg.event_id = e.event_id
            join [group] g
                on g.group_id = eg.group_id
            join member_hierarchy mh
                on mh.member_hierarchy_id = g.sponsor_id
            join member m
                on m.member_id = mh.member_id
            join users u
				on m.user_id = u.user_id
            join partner p
                on p.partner_id = g.partner_id
            join partner_type pt
                on pt.partner_type_id = p.partner_type_id
            join   (
                   select min(st.account_number) account_number, pst.partner_id from 
                   partner_store_template pst join
                   store_template st on st.store_template_id = pst.store_template_id
                   group by pst.partner_id)a on a.partner_id = p.partner_id
            join payment_info pi
                on pi.group_id = g.group_id
               and pi.event_id = e.event_id
            join postal_address pa
                on pa.postal_address_id = pi.postal_address_id
            join subdivision sd
                on sd.subdivision_code = pa.subdivision_code
            
        WHERE e.event_id = @event_id
          AND pi.active = 1

END



