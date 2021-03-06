USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_IncidentManagementReport_Backup]    Script Date: 06/07/2017 09:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_IncidentManagementReport_Backup]
AS

select 	distinct
	i.incidentinstance as IncidentInstance,
	i.useridcreated as LoggedByInstance,
	i.datecreated as DateIncidentLoggedTo,
	i.StatusInstance as IncidentStatusInstance,
	i.ProblemCodeInstance as ProblemCodeInstance,
	ia.comments as comments,
	fh.ful_nbr as FulfillmentHouseInstance,
	fh.ful_name as Fulfillmenthousename,
	case
when fh.ful_addr_2 is null then
	fh.ful_addr_1 + ', ' +
	fh.ful_city + ', ' + 
	fh.ful_state + ', ' + 
	fh.CountryCode + ', ' +
	fh.ful_zip
else
	fh.ful_addr_1 + ', ' +
	fh.ful_addr_2 + ', ' + 
	fh.ful_city + ', ' + 
	fh.ful_state + ', ' + 
	fh.CountryCode + ', ' +
	fh.ful_zip
end
AS FulfillmentHouseAddress,
	fh.InterfaceLayoutID,
	
	pub.pub_nbr as PublisherInstance,
 	pub.pub_name as PublisherName,
	p.product_code as TitleCodeInstance,
	codrh.magazinetitle as MagazineTitle,
	codrh.CustomerOrderHeaderInstance,
	codrh.TransID,
	b.id as batchid,
	b.date as batchdate,
	currency.description as Currency,
	ca.id as billtoid,
	ca.name as billtoname,
	b.orderid,
	b.campaignid,
	codrh.numberofissues as NbOfIssue, 
	codrh.itempricetotal as custprice,
	codrh.baseprice as BasePrice,
	i.DateCreated as DateSend,   
	
	a.description as ActionDescription,
	a.Instance as ActionInstance,
	pc.description as ProblemCodeDescription,
	codrh.status as substatus,
	
	cuser.LastName + ',' + cuser.FirstName as LggedByName,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crh.FirstName + ' ' + crh.LastName else '' end as NameRevisedAddress,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crh.Address1  else '' end as Address1RevisedAddress,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crh.Address2  else '' end as Address2RevisedAddress,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crh.city  else '' end as CityRevisedAddress, 
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then 'CA'  else '' end  as CountryRevisedAddress,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crh.Zip  else '' end as PostalCodeRevisedAddress,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crh.State  else '' end as ProvinceRevisedAddress,
	
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crha.FirstName + ' ' + crha.LastName else crh.FirstName + ' ' + crh.LastName end as NameOnFile,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crha.address1 else  crh.address1 end as Address1OnFile,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crha.address2 else crh.address2 end as Address2OnFile,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crha.City else crh.City end as CityOnFile,
	'CA' as CountryOnFile,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crha.zip  else crh.zip end as PostalCodeOnFile,
	case when a.Instance = 4 or i.ProblemCodeInstance = 196 then crha.State else crh.State end as ProvinceOnFile
	
	
from
	QSPCanadaOrderManagement..Incident i, 
	QSPCanadaProduct..Fulfillment_house fh,
	QSPCanadaProduct..publishers pub,
	QSPCanadaProduct..product p,
	QSPCanadaOrderManagement..ProblemCode pc,
	qspcanadacommon..cuserprofile cuser,
	QSPCanadaOrderManagement..Action a,
	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh,
	QSPCanadaOrderManagement..CustomerOrderHeader coh,
	
	incidentstatus incsta,
	QSPCanadaCommon..CodeDetail currency,
	QSPCanadaCommon..Campaign c,
	QSPCanadaCommon..CAccount ca,
	QSPCanadaOrderManagement..Batch b,
	QSPCanadaOrderManagement..IncidentAction ia, 
QSPCanadaOrderManagement..CustomerRemitHistory crh left join 
	QSPCanadaOrderManagement..CustomerRemitHistoryAudit crha on  crh.Instance =  crha.instance

	

where 	
	i.incidentinstance = ia.incidentinstance
	and p.pub_nbr = pub.pub_nbr
	and p.Fulfill_House_Nbr = fh.ful_nbr
	and p.product_code = codrh.titlecode
	and pc.instance = i.problemcodeinstance
	and i.UserIDCreated = cuser.Instance
	and ia.actioninstance = a.instance
	--and codrh.customerorderheaderinstance = i.customerorderheaderinstance
	--and codrh.transid = i.transid
	and ia.CustomerRemitHistoryInstance = crh.instance
	and incsta.Instance = i.StatusInstance
	and currency.instance = codrh.currencyid
	and c.billtoaccountid = ca.id
	and coh.orderbatchdate = b.date
	and coh.orderbatchid = b.id
	and b.campaignid = c.id
	and coh.campaignid = c.id
	and codrh.CustomerRemitHistoryInstance = crh.instance
	and codrh.CustomerOrderHeaderInstance = coh.Instance
	-- Ben - 2006-01-31 : Changed so that it keeps the products for one month after a season change
	and p.Product_Season = (CASE
				WHEN MONTH(CONVERT(smalldatetime,StartDate)) > 7 OR MONTH(CONVERT(smalldatetime,StartDate)) = 1 THEN 'F'
				WHEN MONTH(CONVERT(smalldatetime,StartDate)) BETWEEN 2 AND 7 THEN 'S'
				ELSE ''
				END)
	and p.Product_Year = (CASE
				WHEN MONTH(CONVERT(smalldatetime,StartDate)) > 7 THEN YEAR(CONVERT(smalldatetime,StartDate)) + 1
				WHEN MONTH(CONVERT(smalldatetime,StartDate)) <= 7 THEN YEAR(CONVERT(smalldatetime,StartDate))
				ELSE
					0
				END)
GO
