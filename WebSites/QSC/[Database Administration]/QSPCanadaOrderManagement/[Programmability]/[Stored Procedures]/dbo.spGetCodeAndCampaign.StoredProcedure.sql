USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGetCodeAndCampaign]    Script Date: 06/07/2017 09:20:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetCodeAndCampaign]
	@code varchar(20),
	@campaign int
as
	declare @state varchar(2)
	declare @shiptoid int

	declare  @fiscal  int
	select @fiscal = long1value from qspcanadacommon..systemoptions where keyvalue='FiscalYear'

	-- get the shipto state for this
	select @shiptoid=shiptoaccountid from qspcanadacommon..campaign where id=@campaign
	select @state=State from qspcanadacommon..caccount where id=@shiptoid	


	select pd.product_code as Code,
		p.product_sort_name, 
		pd.magprice_instance as MagPriceInstance,
		pd.nbr_of_issues as Term,
		pd.qsp_price as Price,
		pd.programsectionid as ProgramSection,
		pd.pricing_season,
		ps.CatalogCode
		from qspcanadaproduct..programsection ps,
			qspcanadaproduct..pricing_details pd,
			qspcanadacommon..taxregionprovince pr,
			qspcanadaproduct..product p,
			qspcanadaproduct..program_master pm
	--	where ps.catalogcode in 
	--	(select distinct contentprogrammastercode from qspcanadacommon..brochure b, 
	--		qspcanadacommon..program p ,
	--		qspcanadaproduct..FieldSupplySection fs
	--				where campaignid=@campaign and 
					--where campaignid=2465 and 
	--			 b.programid=p.id 
	--	and majorproductlineid=1 	
--		and fs.programmastercode=b.programmastercode)
		where
		 p.product_code=pd.product_code
		and programsectionid=id
		and province=@state
		and pr.taxregionid=pd.taxregionid
		and pricing_year=@fiscal
		and product_year=@fiscal
		and pd.product_code=@code
		and product_year=@fiscal
		and pm.code=ps.catalogcode
		and p.countrycode='CA'
		and pd.pricing_season = p.product_season
		and pm.status in (30403,30404 )   -- approved or in use
GO
