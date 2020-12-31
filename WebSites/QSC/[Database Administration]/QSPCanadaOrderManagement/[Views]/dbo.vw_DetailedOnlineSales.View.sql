USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_DetailedOnlineSales]    Script Date: 06/07/2017 09:18:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_DetailedOnlineSales] as 
SELECT b.campaignid,
	s.LastName + ', ' + s.FirstName as ParticipantName,
	cod.ProductCode as TitleCode,
	cod.ProductName as MagazineTitleName,
	count(cod.Price) as TotalNumberOfSubs,
	coalesce(sum(cod.Price),0) as TotalSalesAmount,
	cast((coalesce(sum(cod.Price),0) - coalesce(sum(cod.Tax),0)) * 0.37 as numeric(10, 2)) as TotalProfitEarned

FROM
	QSPCanadaOrderManagement..Student s,
	QSPCanadaOrderManagement..Teacher t,
	QSPCanadaCommon..CAccount ca,
	QSPCanadaCommon..Campaign c,
	QSPCanadaOrderManagement..Batch b,
	QSPCanadaOrderManagement..CustomerOrderHeader coh,
	QSPCanadaOrderManagement..CustomerOrderDetail cod,
	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh

WHERE
	s.TeacherInstance = t.Instance
	AND	ca.ID = t.AccountID
	AND	c.BillToAccountID = ca.ID
	AND	b.CampaignID = c.ID
	AND	b.Date = coh.OrderBatchDate
	AND 	b.id = coh.OrderBatchID
	AND	coh.StudentInstance = s.Instance
	AND	cod.CustomerOrderHeaderInstance = coh.Instance
	AND	b.OrderQualifierID = 39009
	--AND	c.ID = @iCampaignID
	AND 	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
	AND 	cod.TransID = codrh.TransID
	AND 	codrh.DateChanged between convert(nvarchar, '2004-08-01',101)   and convert(nvarchar, '2004-11-19',101)  
	AND 	codrh.status in('42000','42001')

GROUP BY 
	b.campaignid,
	s.LastName, 
	s.FirstName, 
	cod.ProductCode, 
	cod.ProductName
GO
