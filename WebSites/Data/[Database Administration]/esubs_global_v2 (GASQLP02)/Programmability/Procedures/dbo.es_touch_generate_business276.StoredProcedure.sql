USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business276]    Script Date: 02/14/2014 13:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_touch_generate_business276]
	@start_date datetime,
	@end_date datetime,
	@launch_date datetime = ''
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @launch_date = ''
     SET @launch_date = GETDATE();
    
    declare @touch_info_id int
    declare @subject varchar(100)
    declare @body_txt varchar(8000)
    declare @body_html varchar(8000)

    begin transaction

    select @subject = subject, @body_txt = body_text, @body_html = body_html
    from business_rule br with(nolock) inner join email_template_culture etc with (nolock)
      on br.email_template_id = etc.email_template_id	
    where br.business_rule_id = 276

    exec [dbo].[es_create_touch_info]
		 @subject = @subject,
		 @body_txt = @body_txt,
		 @body_html = @body_html,
		 @launch_date = @launch_date,
		 @business_rule_id = 276,
		 @touch_info_id = @touch_info_id OUTPUT

    if @@error <> 0
    begin
	    rollback transaction
    end
    else
    begin
    	
	    insert into touch(event_participation_id,touch_info_id,processed,create_date)
    	
	    SELECT  ep.event_participation_id,
                @touch_info_id as touch_info_id,
				0 as processed,
				getdate() as create_date 
		FROM QSPFulfillment.dbo.[order] as o with(nolock)
				INNER JOIN QSPFulfillment.dbo.[order_detail] as od with(nolock)
					  ON od.order_id = o.order_id
				INNER JOIN QSPEcommerce.dbo.efundraisingtransaction as et with(nolock)
					  ON et.orderID = o.order_id
				INNER JOIN [QSPFulfillment].[dbo].[catalog_item_detail] as cid with(nolock)
					  ON od.catalog_item_detail_id = cid.catalog_item_detail_id
				INNER JOIN [QSPFulfillment].[dbo].[catalog_item] as ci with(nolock)
					  ON cid.catalog_item_id = ci.catalog_item_id
				INNER JOIN [QSPFulfillment].[dbo].[Product] p with(nolock)
					  ON ci.product_id = p.Product_id
				INNER JOIN event_participation ep with(nolock)
					  ON et.suppid  = ep.event_participation_id
				INNER JOIN member_hierarchy mh with(nolock)
					  ON ep.member_hierarchy_id = mh.member_hierarchy_id
				INNER JOIN creation_channel cc with (nolock)
					  ON mh.creation_channel_id = cc.creation_channel_id
				INNER JOIN [esubs_global_v2].[dbo].es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
				LEFT JOIN (
					select
						t.touch_id
						,t.event_participation_id
					from 
						touch t with(nolock)
					inner join touch_info ti with(nolock)
					on ti.touch_info_id = t.touch_info_id
					and business_rule_id = 276
					where datepart(year,t.create_date)=datepart(year,GETDATE())
				) t
				on t.event_participation_id = ep.event_participation_id
		WHERE cc.member_type_id = 2
		    AND o.deleted = 0
			AND od.deleted = 0
			AND o.create_date between @start_date and @end_date
			AND ci.catalog_item_name LIKE '%All You%'
			AND	mh.unsubscribe = 0
			AND	t.touch_id is null
		GROUP BY ep.event_participation_id

	    IF @@ROWCOUNT = 0 -- no rows inserted
	    begin
		    rollback transaction
	    end
	    else
	    begin
		    commit transaction
	    end
    end	

END
GO
