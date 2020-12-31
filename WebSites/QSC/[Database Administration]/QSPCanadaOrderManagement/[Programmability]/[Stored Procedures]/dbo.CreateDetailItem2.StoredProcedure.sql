USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateDetailItem2]    Script Date: 06/07/2017 09:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create     procedure [dbo].[CreateDetailItem2]
	@orderdate smalldatetime,
	@coh int,
	@productcode varchar(10),
	@firstname varchar(50),
	@lastname varchar(50),
	@quantity int,
	@price numeric(10,2),
	@programsectionid int,
	@catalogprice numeric(10,2),
	@quantityreserved int,
	@producttype int,
	@pricingdetailsid int,
	@status int
as
	declare @nexttransid int
	declare @tax1 numeric(10,2)
	declare @tax2 numeric(10,2)		
	declare @net numeric(10,2)
	declare @gross numeric(10,2)		
	declare @campaign int
	declare @orderdatestr varchar(20)
	select @orderdatestr = convert(varchar(20),@orderdate,101)	

	---- Build the temp table
	CREATE TABLE #temp2
		(
			tax1 money,
			tax2 money,
			gross money,
			net money
		)


--select cast(convert(datetime,getdate(),1) as varchar(10)	)
	PRINT '@orderdate: ' +  Convert(varchar, @orderdate)
	PRINT '@price: ' + Convert(varchar, @price)
	PRINT '@programsectionid: ' + Convert(varchar, @programsectionid)
	PRINT '@productcode: ' + Convert(varchar, @productcode)
	PRINT '@campaign: ' + Convert(varchar, @campaign)
--cast(convert(datetime,getdate(),1) as varchar(10)	)
print @orderdatestr
	insert into #temp2 exec qspcanadacommon..PR_CALC_ORDER_ITEM_AMOUNTS
	@orderdatestr, @price, @programsectionid, 
			@productcode, 'N', @campaign, @pricingdetailsid

	select @tax1=tax1, @tax2=tax2, @net=net, @gross=gross from #temp2

	DROP TABLE #temp2
GO
