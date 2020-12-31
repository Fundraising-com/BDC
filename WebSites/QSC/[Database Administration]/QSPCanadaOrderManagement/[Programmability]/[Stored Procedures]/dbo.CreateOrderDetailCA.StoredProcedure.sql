USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateOrderDetailCA]    Script Date: 06/07/2017 09:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[CreateOrderDetailCA]
as

declare	@order_item_status_id int
declare	@offer_id int
declare	@catalog_section_id int
declare	@New_or_renewal varchar(1)
declare	@price_override_id int
declare	@qty_ordered int
declare	@catalog_price numeric(10,2)
declare	@item_price_total numeric(10,2)
declare	@title_code varchar(4)
declare	@number_issues_entered int
declare	@invoice_id int
declare	@gross_amount numeric(10,2)
declare	@net_amount numeric(10,2)
declare	@tax1_amount numeric(10,2)
declare	@tax2_amount numeric(10,2)
declare	@shiptoinstance int

declare @pricingdetail int
declare @accountid int
declare @state varchar(2)
declare @taxregion int
declare @last varchar(40)
declare @first varchar(40)
declare @supportername varchar(80)
declare @orderid int


set nocount on

select distinct order_ship_id,
	0 as currtrans,
	order_item_id, 
	order_item_status_id = case  order_item_status_id when 1 then 501  -- under review
							  when 2 then 500  -- approved
							  when 3 then 500  -- donr have concept of a "new"  in PayLater
							  when 4 then 500  -- in process wouldn't apply either
                                                          when 5 then 506  -- cancelled
                                                          when 6 then 508  -- shipped
                                                          when 7 then 507  -- sent to remit
                                                          when 8 then 504  -- paid pending
			       end,   
	offer_id, 
	catalog_section_id,
	New_or_renewal,
	price_override_id = case isnull(price_override_id,0) when 0 then 45004
                                                   when 1 then 45001
                                                   when 2 then 45002
                                                   when 3 then 45003
                                                   when 5 then 45002
                                                   when 6 then 45001
                            end,
	Isnull(number_issues_entered,0) as qty_ordered,
	isnull(catalog_price,0.00) as catalog_price,
	isnull(item_price_total,0.00) as item_price_total,
	 replicate( '0', 4-len(title_code)) + convert(varchar,title_code) as title_code,
	isnull(number_issues_entered,0) as number_issues_entered,
	invoice_id,
	gross_amount,
	net_amount,
	tax1_amount,
	isnull(tax2_amount,0.00) as tax2_amount,
	shiptoinstance,
	recipient_first_name,
	recipient_last_name,
	qspcanadaordermanagement..batch.accountid,
	qspcanadacommon..taxregionprovince.taxregionid,
	supporter_name,
	MagPrice_instance into #orderdetail
	from    om_tbl_order_item left join qspcanadaproduct..pricing_details 
			on qspcanadaproduct..pricing_details.product_code=replicate( '0', 4-len(title_code)) + convert(varchar,title_code) 
				and ProgramSectionID=catalog_section_id
				and Nbr_of_Issues=number_issues_entered
			and QSP_Price=catalog_price
--			and TaxRegionID = @taxregion
			and offer_code = offer_id,
		qspcanadaordermanagement..customerorderheader,
		qspcanadaordermanagement..batch ,##temp,
		qspcanadacommon..caccount account,
		qspcanadacommon..taxregionprovince ,
		om_tbl_order_slip
	where 
	order_form_type_code='MAGAZINE'	
	and 	
	order_ship_id =instance  
	and  orderbatchdate=date and orderbatchid=qspcanadaordermanagement..batch.id 
	and qspcanadaordermanagement..batch.orderid=##temp.orderid 
	and account.id = qspcanadaordermanagement..batch.accountid
	and province=account.state
	and order_slip_id=order_ship_id

	order by order_ship_id

declare	@order_ship_id int
declare	@order_item_id int
declare @currtrans int
declare @currslip int
declare @count int
declare a cursor for select order_ship_id, order_item_id from #orderdetail
open a

fetch next from a into
		@order_ship_id ,
		@order_item_id 



select @currtrans = 1
select @currslip = @order_ship_id

while(@@fetch_status <> -1)
begin

	update #orderdetail set  currtrans = @currtrans where order_item_id = @order_item_id

	fetch next from a into
		@order_ship_id ,
		@order_item_id 

	if(@@fetch_status <> -1)
	begin
		select @count = count(*) from #orderdetail  where  order_ship_id=@order_ship_id
--print @count
		if(@count = 1)
		begin
			select @currtrans = 1
		end
		else
		begin
			if( @currslip <> @order_ship_id)
			begin
				select @currtrans = 1
--print 'yuch'
			end
			else
			begin
				set @currtrans = @currtrans+1

			end
		end

		select @currslip = @order_ship_id
	end
end
close a
deallocate a

	insert qspcanadaordermanagement..customerorderdetail
--		(CustomerOrderHeaderInstance,
--			TransID,
--			CustomerShipToInstance,
--			ProductCode,
--			ProductName,
--			Quantity,
--			Price,
--			PriceA,
--			Tax,
--			TaxA,
--			StatusInstance,
--			DelFlag,
--			Renewal,
--			Recipient,
--			OverrideProduct,
--			CreationDate,
--			CrossedBridgeDate,
--			ChangeUserID,
--			ChangeDate,
--			InvoiceNumber,
--			AlphaProductCode,
--			CouponPage,
--			FDIndicator,
--			MktgIndicator,
--			ToteInstance,
--			GiftCD,
--			IsGift,
--			IsGiftCardSent,
--			SendGiftCardBeforeDate,
--			ProgramSectionID,
--			CatalogPrice,
--			QuantityReserved,
--			PriceOverrideID,
--			ProductType,
--			PricingDetailsID,
--			Tax2,
--			Tax2A,
--			Net,
--			Gross,
--			SupporterName,
--			SendGiftCard,
--			QuantityShipped,
--			ShipId,
				
--			)
		select 
			order_ship_id,--CustomerOrderHeaderInstance,
			currtrans,--TransID,
			0,--@shiptoinstance,--CustomerShipToInstance,
			title_code,--ProductCode,
			'',--ProductName   FILLED IN AFTER
			number_issues_entered,--Quantity,
			item_price_total,--Price,
			item_price_total,--PriceA,
			tax1_amount,--Tax,
			tax1_amount,--TaxA,
			order_item_status_id,--StatusInstance,
			0, --DelFlag,
			New_or_renewal,--Renewal,			
			recipient_first_name + ' '+recipient_last_name,--Recipient,
			0,--OverrideProduct,
			GetDate(),--CreationDate,
			'1/1/95',--CrossedBridgeDate,
			'KT',--ChangeUserID,
			GetDate(),--ChangeDate,
			invoice_id,--InvoiceNumber,
			title_code,--AlphaProductCode,
			0,--CouponPage,
			'',--FDIndicator,
			'',--MktgIndicator,
			0,--ToteInstance,
			'',--GiftCD,
			0,--IsGift,
			0,--IsGiftCardSent,
			'1/1/95',--SendGiftCardBeforeDate,
			catalog_section_id,--ProgramSectionID,
			catalog_price,--CatalogPrice,
			0.00,--QuantityReserved,
			price_override_id,--PriceOverrideID,
			0,--ProductType,
			magprice_instance,--PricingDetailsID,
			tax2_amount,--Tax2,
			tax2_amount,--Tax2A,
			net_amount,--Net,
			gross_amount,
			supporter_name,
			0,
			0,
			0,
			''
		from #orderdetail


update qspcanadaordermanagement..customerorderdetail set productname = product_sort_name, ProductType=46000+productline from
	qspcanadaordermanagement..customerorderdetail,qspcanadaproduct..product,#orderdetail where productcode=product_code
	and qspcanadaordermanagement..customerorderdetail.customerorderheaderinstance=#orderdetail.order_ship_id
	and qspcanadaordermanagement..customerorderdetail.transid=#orderdetail.currtrans


--46006
-- set book and music to shipped\
update qspcanadaordermanagement..customerorderdetail set StatusInstance = 508 from
	qspcanadaordermanagement..customerorderdetail,#orderdetail  where producttype in (46006, 46007)
	and qspcanadaordermanagement..customerorderdetail.customerorderheaderinstance=#orderdetail.order_ship_id
	and qspcanadaordermanagement..customerorderdetail.transid=#orderdetail.currtrans



--update qspcanadaordermanagement..customerorderdetail set supportername = supporter_name
--	qspcanadaordermanagement..customerorderdetail,
--		qspcanadaordermanagement..customer where instance=CustomerShipToInstance


update om_tlb_order_item set TransID = CurrTrans from om_tlb_order_item, #orderdetail
	where om_tlb_order_item.order_item_id = #orderdetail.order_item_id

drop table #orderdetail
GO
