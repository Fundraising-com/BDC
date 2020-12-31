USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetShipmentInfo]    Script Date: 06/07/2017 09:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_GetShipmentInfo] AS
select    b.orderid,
	b.date as orderdate,
	os.description as orderstatus,
	ot.description as ordertype,
	oq.description as orderqualifier,
	a.id as accountid,
	b.campaignid,
	a.name as groupname,
	s.id as shipmentid,
	s.shipmentdate,
	s.expecteddeliverydate,
	fm.fmid,
	fm.firstname as fmfirstname,
	fm.lastname as fmlastname
  from  batch b,
	qspcanadacommon..campaign c,
	qspcanadacommon..fieldmanager fm,	
	customerorderheader coh,
	customerorderdetail cod,
	shipment s,
	codedetail cd,
	codedetail os,
	codedetail ot,
	codedetail oq,
	qspcanadacommon..caccount a
 where  	s.carrierid = cd.instance and
	b.statusinstance = os.instance and
	b.ordertypecode = ot.instance and 
	b.orderqualifierid = oq.instance and
	--b.ShipToAccountID = a.id and
	b.AccountID = a.id and
	b.campaignid = c.id and
	c.fmid = fm.fmid and
	b.date = coh.orderbatchdate and
	b.id = coh.orderbatchid and
	cod.customerorderheaderinstance = coh.instance and
	cod.shipmentid = s.id
group by
 	fm.fmid,
	fm.firstname,
	fm.lastname,
	a.id,
	b.campaignid,
	a.name,
	b.orderid,
	os.description,
	ot.description,
	oq.description,
	b.date,
	s.id,
	s.shipmentdate,
	s.expecteddeliverydate
GO
