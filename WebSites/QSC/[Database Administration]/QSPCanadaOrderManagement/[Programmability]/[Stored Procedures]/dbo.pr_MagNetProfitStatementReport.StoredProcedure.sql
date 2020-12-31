USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_MagNetProfitStatementReport]    Script Date: 06/07/2017 09:20:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_MagNetProfitStatementReport]

@iCampaignID	int		= 0,
@iOver100	int		= 0,
@dFrom	datetime	= '01/01/1955',
@dTo		datetime	= '01/01/1955',
@zFMID	varchar(4)	= ''

AS

DECLARE	@sqlStatement	nvarchar(4000),
		@DateFrom	datetime,
		@DateTo	datetime


if(@dFrom <> '01/01/1955' and @dTo <> '01/01/1955')
BEGIN
	set @DateFrom = @dFrom
	set @DateTo = @dTo
END
ELSE
BEGIN
--TEMPORARY---------------------------------------------------------------------------------------------
	SET @DateFrom = '2006-02-15'
SET @DateTo    = '2006-04-16'
----------------------------------------------------------------------------------------------------------------------
END


set @sqlStatement = 'SELECT
	fm.FirstName + '' '' + fm.LastName as FMName,
	fm.FMID as FMID,
	c.ID as CampaignID,
	getDate() as StatementDate,
	''2006-02-14'' as LastStatementDate,
	c.ShipToAccountID as ShipToGroupID,
	sc.Name as ShipToName,
	sa.street1 as ShipToAddress1,
	sa.street2 as ShipToAddress2,
	sa.city as ShipToCity,
	sa.stateProvince as ShipToProvinceCode,
	sa.postal_code as ShipToPostalCode,
	sp.PhoneNumber as ShipToTel,
	sco.FirstName + '' '' + sco.LastName as ShipToContactName,
	c.BillToAccountID as BillToGroupID,
	bc.Name as BillToName,
	ba.street1 as BillToAddress1,
	ba.street2 as BillToAddress2,
	ba.city as BillToCity,
	ba.stateProvince as BillToProvinceCode,
	ba.postal_code as BillToPostalCode,
	bp.PhoneNumber as BillToTel,
	bco.FirstName + '' '' + bco.LastName as BillToContactName,
	osc.totalunits as CurrentUnits,
	osc.totalamount as CurrentAmountWithTaxes,
	osc.totalnet as CurrentAmountWithoutTaxes,
	cast(osc.totalnet *.37 as numeric (10,2)) as CurrentGroupProfitEarned,
	0 as CurrentPlusDonations,
	osc.PostageCosts as CurrentPostageCosts,
	CASE WHEN cast(osc.totalnet *.37 as numeric (10,2)) < osc.PostageCosts THEN 0 ELSE cast(osc.totalnet *.37 as numeric (10,2)) - osc.PostageCosts END as CurrentNetProfitAmount,
	osc_t.totalunits as TotalUnits,
	osc_t.totalamount as TotalAmountWithTaxes,
	osc_t.totalnet as TotalAmountWithoutTaxes,
	cast(osc_t.totalnet *.37 as numeric (10,2)) as TotalGroupProfitEarned,
	0 as TotalPlusDonations,
	osc_t.PostageCosts as TotalPostageCosts,
	CASE WHEN cast(osc_t.totalnet *.37 as numeric (10,2)) < osc_t.PostageCosts THEN 0 ELSE cast(osc_t.totalnet *.37 as numeric (10,2)) - osc_t.PostageCosts END as TotalNetProfitAmount,
	v.ParticipantName,
	v.TitleCode,
	v.MagazineTitleName,
	v.TotalNumberOfSubs,
	v.TotalSalesAmount,
	v.TotalProfitEarned


FROM
	QSPCanadaCommon..FieldManager fm,
	QSPCanadaCommon..Campaign c left join QSPCanadaCommon..Contact sco on sco.Id = c.ShipToCampaignContactID left join QSPCanadaCommon..Contact bco on bco.Id = c.BillToCampaignContactID,
	QSPCanadaCommon..Address sa,
	QSPCanadaCommon..CAccount bc left join (select * from QSPCanadaCommon..Phone where type=30505) bp on bp.PhoneListID = bc.PhoneListID,
	QSPCanadaCommon..Address ba,
	UDF_MagNetSalesByCampaign(''' + convert(nvarchar, '2005-07-01',101) + ''', ''' + convert(nvarchar, @DateTo,101) + ''', ' + cast(@iOver100 as nvarchar(1)) + ') osc_t,
	UDF_MagNetSalesByCampaign(''' + convert(nvarchar, @DateFrom,101) + ''', ''' + convert(nvarchar, @DateTo,101) + ''', ' + cast(@iOver100 as nvarchar(1)) + ') osc,
	UDF_DetailedMagNetSales(''' + convert(nvarchar, @DateFrom,101) + ''', ''' + convert(nvarchar, @DateTo,101) + ''') v,
	QSPCanadaCommon..CAccount sc left join (select * from QSPCanadaCommon..Phone where type=30505) sp on sp.PhoneListID = sc.PhoneListID  


WHERE
	fm.FMID = c.FMID
AND	sc.Id = c.ShipToAccountID
AND	sa.AddressListID = sc.AddressListID
AND	sa.address_type = 54001
AND	bc.Id = c.BillToAccountID
AND	ba.AddressListID = bc.AddressListID
AND	ba.address_type = 54002
AND 	osc.CampaignID = c.ID
AND 	osc_t.CampaignID = c.ID
AND	c.ID=v.CampaignID '
--AND	c.StartDate >= ''' + convert(nvarchar, @DateFrom,101) + '''
--AND	c.EndDate <= ''' + convert(nvarchar, @DateTo,101) + ''''

if(@iCampaignID <> 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' AND 	c.ID = ' + cast(@iCampaignID as varchar(7)) + ' '
END

if(@zFMID <> '')
BEGIN
	set @sqlStatement = @sqlStatement + ' AND	fm.FMID = ''' + @zFMID + ''' '
END

set @sqlStatement = @sqlStatement + ' ORDER BY	c.ID, v.ParticipantName, v.MagazineTitleName '



exec(@sqlStatement)
GO
