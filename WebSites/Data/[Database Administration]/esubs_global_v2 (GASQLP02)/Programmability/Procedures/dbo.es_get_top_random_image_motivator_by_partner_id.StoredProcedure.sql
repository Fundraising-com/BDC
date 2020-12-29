USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_top_random_image_motivator_by_partner_id]    Script Date: 02/14/2014 13:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 9/13/2011
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_top_random_image_motivator_by_partner_id]
	@partner_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT top 300 lower(ISNULL(rtrim(ltrim(m.first_name)), '') + ' ' + ISNULL(rtrim(ltrim(m.last_name)), '')) as fullname
	 , et.createdate as create_date
	 , et.ImageMotivationComment
	 , sum(od.price*od.quantity) as AmountTotal
	 FROM event_participation ep with (nolock)
	INNER join qspecommerce.dbo.efundraisingtransaction et with (nolock) on et.suppid = ep.event_participation_id
	INNER join qspfulfillment.dbo.[order] o with (nolock) on o.order_id = et.orderid
	INNER join qspfulfillment.dbo.[order_detail] od with (nolock) ON od.order_id = o.order_id
	INNER join member_hierarchy mh  with (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
    INNER join member m  with (nolock) on mh.member_id = m.member_id
	--INNER join qspfulfillment.dbo.[customer] c with (nolock) on c.customer_id = o.customer_id
	INNER JOIN [esubs_global_v2].[dbo].es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
	WHERE partner_id = @partner_id AND et.IsImageMotivation = 1	AND ISNULL(rtrim(ltrim(m.first_name)), '') + ' ' + ISNULL(rtrim(ltrim(m.last_name)), '')<>' '
    GROUP BY 
	   lower(ISNULL(rtrim(ltrim(m.first_name)), '') + ' ' + ISNULL(rtrim(ltrim(m.last_name)), ''))
	 , et.AmountTotal
	 , et.createdate
	 , et.ImageMotivationComment
    ORDER BY newid()
END
GO
