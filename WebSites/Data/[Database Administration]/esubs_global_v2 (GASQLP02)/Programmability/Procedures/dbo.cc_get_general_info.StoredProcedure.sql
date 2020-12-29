USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_general_info]    Script Date: 02/14/2014 13:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--dbo.cc_get_general_info 'email_address','jwhelan@colisp.com'
--dbo.cc_get_general_info 'e.event_id',1008345

CREATE          PROCEDURE [dbo].[cc_get_general_info] 
	@searchField varchar(50),
        @value varchar(50)
AS
BEGIN

declare @strSQL varchar(2000)



if @searchField = 'email_address'
begin 

   declare @event_id int
 --  declare @searchField varchar(50)
 --  declare  @value varchar(50)
 --  print @searchField
 --  print @value


   select @event_id = ep.event_id 
   from event_participation ep
	                   inner join member_hierarchy mh
                           on mh.member_hierarchy_id = ep.member_hierarchy_id
                           inner join member m
                           on m.member_id = mh.member_id
   where m.email_address like '%' + @value + '%'    

   set @searchField = 'e.event_id'
   set @value = @event_id
  
 --  print @value

end



set @strSQL = 'select
         p.partner_name
	,g.group_name
        ,e.active
        ,e.event_name
	,e.event_id
	,pn.phone_number
	,st.account_number
	,m.email_address
	,m.[password]
	,m.first_name + '' '' + m.last_name as [name]
	,ep.event_participation_id
	, (case when m.opt_status_id = 1 then 0 else 1 end)
          as unsubscribed
        ,g.group_id
 	, max(case	when epr.prize_item_id > 0 then ''Yes'' else 
					''No'' 
				end ) as movie_ticket

From
	partner p
	inner join [group] g
	on p.partner_id = g.partner_id
	inner join partner_store_template pst
	on pst.partner_id = g.partner_id
	inner join store_template st
	on st.store_template_id = pst.store_template_id
	inner join event_group ge
	on g.group_id = ge.group_id
	inner join event e
	on e.event_id = ge.event_id
	inner join event_participation ep
	on ep.event_id = e.event_id
        and ep.participation_channel_id = 3
	inner join member_hierarchy mh
	on mh.member_hierarchy_id = ep.member_hierarchy_id
	inner join member m
	on m.member_id = mh.member_id
	left outer join member_postal_address mpa
	on mpa.member_id = m.member_id
	and mpa.active =1 
	and postal_address_type_id = 2
	left outer join postal_address pa
	on mpa.postal_address_id = pa.postal_address_id
        inner join payment_info pi
        on g.group_id = pi.group_id and pi.active = 1
	left outer join  phone_number pn
	on pn.phone_number_id = pi.phone_number_id
	--and pn.phone_number_type_id=1
	--and pn.active =1
        left outer join earned_prize epr
        on ep.event_participation_id = epr.event_participation_id WHERE'

if @searchField = 'e.event_id'
begin

--declare @strSQL varchar(100)
--set @strSQL = 'select ...'
    set @strSQL = @strSQL + ' ' + @searchField + ' = ' + @value
print @strSQL

end
else
begin
    set @strSQL = @strSQL + ' ' + @searchField + ' LIKE ''%' + @value + '%'''
end
	

set @strSQL = @strSQL + ' 

group by
         p.partner_name
	,g.group_name
        ,e.event_name
        ,e.active
	,e.event_id
	,pn.phone_number
	,st.account_number
	,m.email_address
	,m.[password]
	,m.first_name
        ,m.last_name 
	,ep.event_participation_id
	,m.opt_status_id 
        ,g.group_id
 	
'

exec (@strSQL)




/*
select 
	p.partner_name
	,e.event_name
	,e.event_id
	,pn.phone_number
	,st.account_number
	,m.email_address
	,m.[password]
	,m.first_name + ' ' + m.last_name as [name]
	,ep.event_participation_id
	,m.opt_status_id  --1 opt_in 2 opt_out
	--,case when min(mt.event_participation) is not null then 1 else 0 end as movie_ticket

From
	partner p
	inner join [group] g
	on p.partner_id = g.partner_id
	inner join partner_store_template pst
	on pst.partner_id = g.partner_id
	inner join store_template st
	on st.store_template_id = pst.store_template_id
	inner join event_group ge
	on g.group_id = ge.group_id
	inner join event e
	on e.event_id = ge.event_id
	inner join event_participation ep
	on ep.event_id = e.event_id
	and ep.participation_channel_id = 3
	inner join member_hierarchy mh
	on mh.member_hierarchy_id = ep.member_hierarchy_id
	inner join member m
	on m.member_id = mh.member_id
	left outer join member_postal_address mpa
	on mpa.member_id = m.member_id
	and mpa.active =1 
	and postal_address_type_id = 2
	left outer join postal_address pa
	on mpa.postal_address_id = pa.postal_address_id
	left outer join  phone_number pn
	on pn.member_id = m.member_id
	and phone_number_type_id=1
	and pn.active =1*/
	/*left outer join (
		select 
			event_participation_id
		from 
			earned_prize ep
			inner join prize_item [pi]
			on ep.prize_item_id = [pi].prize_item_id
			and [pi].prize_id = 2
	) mt
	on mt.event_participation_id = ep.event_participation_id*/
/*where
	e.event_id = 30569
*/

END
GO
