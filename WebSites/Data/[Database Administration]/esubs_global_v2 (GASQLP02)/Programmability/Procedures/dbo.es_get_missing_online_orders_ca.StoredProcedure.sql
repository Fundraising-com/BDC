USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_missing_online_orders_ca]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: January 18, 2010
-- Description:	
/*   
    Canadian ESubs orders goes through the following steps:
	1. Order placed on magfundraising.ca goes into proddb1.QSPFulfillment
	2. Order extracted from proddb1.QSPFulfillment (periodically)
	3. Order inserted into proddb2.QSPCanadaOrderManagement (periodically)
	4. Order closed in proddb2.QSPCanadaOrderManagement (at midnight)
	5. Both above DB’s synced to EFR DB’s -> caefr3k01 & qspproddb3 (instant)

	Problems may occur in step 3 so this proc will pull out a list of all orders 
    that did not make it into proddb2.QSPCanadaOrderManagement. Hence it will also
	be missing from the replicated DB's in EFR.
*/
-- =============================================
CREATE PROCEDURE [dbo].[es_get_missing_online_orders_ca]
AS
BEGIN
	SET NOCOUNT ON;

    select distinct o.order_date as [Order Date], ep.event_id as [ESubs Campaign ID], c.eds_order_id as [EDS Order ID], 
      (case 
	      when (ioid.InternetOrderID is null)
	         then 'Missing in ''qspproddb3\qspCanadaOrderManagement.dbo.InternetOrderID'' table'
	      when (ioid.InternetOrderID is not null and cod.customerorderheaderinstance is null)
	         then 'Missing in ''qspproddb3\qspCanadaOrderManagement.dbo.CustomerOrderDetail'' table'
          when (ioid.InternetOrderID is not null and cod.customerorderheaderinstance is not null and hist.CustomerOrderHeaderInstance is null)
	         then 'Missing in ''qspproddb3\qspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory'' table'
	   end) as [ERROR DESCRIPTION]
	from 
	qspecommerce.dbo.efundraisingtransaction et with (nolock)
	inner join event_participation ep with (nolock) on ep.event_participation_id = et.suppid
	inner join event e with (nolock) on ep.event_id = e.event_id
	inner join qspfulfillment.dbo.[order] o with (nolock) on o.order_id = et.orderid
	inner join qspfulfillment.dbo.campaign cmp with (nolock) on cmp.campaign_id = o.campaign_id
	inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
	inner join qspEcommerce.dbo.cart c with (nolock) on c.x_order_id = o.order_id
	left join qspCanadaOrderManagement.dbo.InternetOrderID ioid with (nolock) ON ioid.internetorderid = c.eds_order_id 
    left join qspCanadaOrderManagement.dbo.CustomerOrderDetail cod with (nolock) 
		on cod.customerorderheaderinstance = ioid.customerorderheaderinstance
	left join qspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory hist with (nolock) 
		on  hist.customerorderheaderinstance = cod.customerorderheaderinstance and hist.transid = cod.transid 
	where e.culture_code = 'en-CA' and cod.delflag = 0 and
	  (
		 ioid.InternetOrderID is null or 
		 cod.customerorderheaderinstance is null or 
		 (cod.producttype in (46001) and cod.StatusInstance not in (517) and hist.CustomerOrderHeaderInstance is null OR 
		  (cod.producttype in (46006, 46007, 460012) and cod.StatusInstance not in (508, 509, 510, 511, 517))
		 )
	  ) 
	and DATEDIFF(day, et.createdate, getdate()) >= 3 -- Goes back 72 hours	
	and cmp.fiscal_year = qspfulfillment.dbo.fnc_getDateFiscalYR(getdate())
    order by 3  
END
GO
