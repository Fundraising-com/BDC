USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_496]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_496]
		@identification int
		,@source_id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN
select 
		  @source_id as [source_id]
		, o.order_id as [order_id]
		, CAST(ROUND ((select sum(EFRECommerce..[order_detail].quantity*EFRECommerce..[order_detail].price) from EFRECommerce..[order_detail] where EFRECommerce..[order_detail].order_id = @identification),2,1) AS VARCHAR) as [amount]
		, dbo.[es_insert_space_before_CapCase]( em.recipient_name) as [supporter]
		, em.email_address as [email_address]
		, pa.first_name + ' ' + pa.last_name as [billing_name]
		, pa.address1 + ' ' + pa.address2   as [billing_address]
		, pa.city + ', '+ RIGHT(pa.[subdivision_code], 2) + ' ' + pa.zip  COLLATE DATABASE_DEFAULT as [billing_city_state_zip]
		, UPPER(cct.credit_card_type_name) + ' XXXXXXXXXXXX' + ca.last_cc_digits COLLATE DATABASE_DEFAULT as [credit_card_info]
		, 496 as [email_template_id]
		, case 
			when mh.parent_member_hierarchy_id is null then e.event_name
			else m.first_name + ' '+ m.last_name COLLATE DATABASE_DEFAULT
		  end as [supporter_name]
		, co.country_name as [country]
		, p.partner_name as [partner_name]
		, @identification as [identification]
		
		from
		EFRECommerce..[order] o with (nolock)
		inner join EFRECommerce..[email] em with (nolock)
			on em.email_id =  o.billing_email_id
		inner join EFRECommerce..[postal_address] pa
			on pa.postal_address_id = o.billing_postal_address_id
		inner join EFRECommerce..[subdivision] su
			on su.subdivision_code = pa.subdivision_code
		inner join EFRECommerce..[country] co
			on co.country_code = su.country_code
		inner join EFRECommerce..[credit_card] ca
			on ca.credit_card_id = o.credit_card_id
		inner join EFRECommerce..[credit_card_type] cct with (nolock)
			on ca.credit_card_type_id = cct.credit_card_type_id
	    inner join event_participation ep with(nolock) 
	    	on o.ext_participation_id = ep.event_participation_id
	    inner join member_hierarchy mh with(nolock)
			on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join [event] e with(nolock)
			on e.event_id = ep.event_id
		inner join event_group eg with(nolock)
			on eg.event_id = e.event_id
		inner join [group] g with(nolock)
			on g.group_id = eg.group_id
		inner join partner p with(nolock)
			on g.partner_id = p.partner_id
	    left join member_hierarchy mh1 with (nolock)
			on mh.parent_member_hierarchy_id = mh1.member_hierarchy_id
		inner join member m with(nolock)
			on m.member_id = mh1.member_id
		inner join [users] u with (nolock)
			on u.[user_id] = m.[user_id]
	
		where 
			o.order_id = @identification 
			
	END		
END
GO
