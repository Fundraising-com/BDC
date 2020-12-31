USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetMagazineSubscriberInformationDetailedReport]    Script Date: 06/07/2017 09:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetMagazineSubscriberInformationDetailedReport]
	@publisher int,
	@fulfillment_nbr varchar(3),
	@fromid int,
	@toid int,
	@interfacemediaid int
as
	declare @wheredone int
	declare @sql varchar(2000)
	-- make sure we get the product as it falls between remit batch date
	select @sql = 'select p.pub_name, f.ful_name, rb.id, rb.date, titlecode, cod.productname,numberofissues,'
	select @sql = @sql+'codrh.defaultgrossvalue,TransType = case codrh.Renewal when ''N'' then ''New'' when ''R'' then ''Renewal'' end,'
	select @sql = @sql+' crh.lastname,crh.firstname,Address1,Address2,city,state,zip'
	select @sql = @sql+ ' from CustomerOrderDetailRemitHistory codrh, '
	select @sql = @sql+'CustomerRemitHistory crh,RemitBatch rb, '
	select @sql = @sql+'qspcanadaproduct..Fulfillment_house f,'
	select @sql = @sql+' qspcanadaproduct..Publishers p, '
	select @sql = @sql+' qspcanadaproduct..Product pr,'
	select @sql = @sql+' qspcanadaproduct..Pricing_Details pd,'
	select @sql = @sql+' qspcanadaordermanagement..customerorderdetail cod '
	select @sql = @sql+' where codrh.RemitBatchID = rb.id '
	select @sql = @sql+' and crh.instance = codrh.CustomerRemitHistoryInstance'
	select @sql = @sql+' and cod.customerorderheaderinstance = codrh.customerorderheaderinstance '
	select @sql = @sql+' and cod.transid = codrh.transid '
	select @sql = @sql+' and pd.magprice_instance=cod.PricingDetailsID '
	select @sql = @sql+' and f.ful_nbr = rb.FulfillmentHouseNbr '
	select @sql = @sql+' and p.pub_nbr = pr.pub_nbr  '
	select @sql = @sql+' and pr.product_season = pd.pricing_season '
	select @sql = @sql+' and pr.product_year = pd.pricing_year '

	if( @publisher <> 0 or @fulfillment_nbr<> '' or @fromid <> 0 or @toid <> 0 or @interfacemediaid<> 0)
	begin

		if( @publisher <> 0  )
		begin
			select @sql = @sql+' and p.pub_nbr = '+ cast(@publisher as varchar(10))
		end
		if( @fulfillment_nbr<> '' ) 
		begin
			select @sql = @sql+' and  f.ful_nbr = '''+@fulfillment_nbr + ''''
		end
		if( @fromid<> 0 ) 
		begin

			select @sql = @sql+' and rb.id >= '+ cast(@fromid as varchar(10))
		end
		if( @toid<> 0 ) 
		begin
			
			select @sql = @sql+' and  rb.id <= '+ @toid
		end
		if( @interfacemediaid<> 0 ) 
		begin
			select @sql = @sql+' and  f.interfacemediaid = '+ cast(@interfacemediaid as varchar(10))
		end
	end 
	select @sql = @sql+' order by p.pub_name, titlecode,rb.id, rb.date,crh.lastname'
print @sql
	EXEC (@sql)
--dump publisher,fulf house, title code, batch# and date , nbr of issues, sub value,
-- new or renewal, First, last addr1, addr2, city province postal
GO
