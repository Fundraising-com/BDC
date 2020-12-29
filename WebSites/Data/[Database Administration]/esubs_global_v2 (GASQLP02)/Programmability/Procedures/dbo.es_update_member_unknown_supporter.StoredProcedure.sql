USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_member_unknown_supporter]    Script Date: 02/14/2014 13:07:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 13 Mars 2006
-- Update date: 24 October 2013
-- Description:	Updates the last 2 days unknown 
--              supporter based on QSP's 
--              customer data
-- =============================================
CREATE PROCEDURE [dbo].[es_update_member_unknown_supporter] (@numberOfDays INT = 2)
AS
BEGIN
	SET NOCOUNT ON;
    
    -- Update the member name only
    -- we *DO NOT* reassign the sale under a correct member if it exists
    update member
    set first_name = c.first_name
       ,last_name = c.last_name
       ,email_address = em.email_address
    from member_hierarchy mh with (nolock)
        join event_participation ep with (nolock)
            on ep.member_hierarchy_id = mh.member_hierarchy_id
        join qspecommerce..efundraisingtransaction et with (nolock)
            on et.suppID = ep.event_participation_id
        join qspfulfillment..[order] o with (nolock)
            on o.order_id = et.OrderID
        join qspfulfillment..customer c with (nolock)
            on c.customer_id = o.customer_id
        join QSPFulfillment.dbo.email em
	    on em.email_id = o.billing_email_id
        -- doit choisir le customer le plus recent
        -- quelque customer sont en double pour le meme member
        join (
            select m.member_id, max(c.create_date) as create_date
            from member m
                join member_hierarchy mh with (nolock)
                    on mh.member_id = m.member_id
                join event_participation ep with (nolock)
                    on ep.member_hierarchy_id = mh.member_hierarchy_id
                join qspecommerce..efundraisingtransaction et with (nolock)
                    on et.suppID = ep.event_participation_id
                join qspfulfillment..[order] o with (nolock)
                    on o.order_id = et.OrderID
                join qspfulfillment..customer c with (nolock)
                    on c.customer_id = o.customer_id
            where m.email_address like 'es%@efundraising.com'
              and m.first_name = ''
              and m.last_name = ''
              and DATEDIFF(day, et.createdate, getdate()) <= @numberOfDays
            group by m.member_id
        ) rm
            on rm.member_id = mh.member_id
            and c.create_date = rm.create_date
    where member.member_id = mh.member_id
      and member.email_address like 'es%@efundraising.com'
      and member.first_name = ''
      and member.last_name = ''
      and DATEDIFF(day, et.createdate, getdate()) <= @numberOfDays;
      
      
    -- UPDATE BY JIRO: OCT 2013
    --   Update the donars for our donation platform
    update member
    set first_name = pa.first_name
       ,last_name = pa.last_name
       ,email_address = e.email_address
    from member_hierarchy mh with (nolock)
        join event_participation ep with (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
        join creation_channel cc with (nolock) on mh.creation_channel_id = cc.creation_channel_id
        join EFRECommerce.dbo.[order] o WITH (NOLOCK) ON o.ext_participation_id = ep.event_participation_id
        join EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
        join dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
        join EFREcommerce.dbo.postal_address pa with (nolock) on o.billing_postal_address_id=pa.postal_address_id
        join EFREcommerce.dbo.email e with (nolock) on o.billing_email_id = e.email_id
    where member.member_id = mh.member_id
      and (member.email_address like 'es%@efundraising.com' OR cc.member_type_id = 3)
      and member.first_name = ''
      and member.last_name = ''
      and DATEDIFF(day, o.order_date, getdate()) <= @numberOfDays;

END
GO
