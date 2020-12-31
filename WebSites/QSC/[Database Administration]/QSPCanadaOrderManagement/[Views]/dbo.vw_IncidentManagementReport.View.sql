USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_IncidentManagementReport]    Script Date: 06/07/2017 09:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_IncidentManagementReport]
AS

SELECT	DISTINCT
		i.IncidentInstance AS IncidentInstance,
		i.UserIDCreated AS LoggedByInstance,
		i.DateCreated AS DateIncidentLoggedTo,
		i.StatusInstance AS IncidentStatusInstance,
		i.ProblemCodeInstance AS ProblemCodeInstance,
		ia.Comments AS Comments,
		fh.Ful_Nbr AS FulfillmentHouseInstance,
		fh.Ful_Name AS FulfillmentHouseName,
		fh.Ful_Addr_1 + ', ' +
		CASE COALESCE(fh.Ful_Addr_2, '')
			WHEN	'' THEN	''
			ELSE		fh.Ful_Addr_2 + ', '
		END +
		fh.Ful_City + ', ' + 
		fh.Ful_State + ', ' + 
		fh.CountryCode + ', ' +
		fh.Ful_Zip AS FulfillmentHouseAddress,
		fh.InterfaceLayoutID,
	
		pub.Pub_Nbr AS PublisherInstance,
	 	pub.Pub_Name AS PublisherName,
		p.Product_Code AS TitleCodeInstance,
		codrh.MagazineTitle AS MagazineTitle,
		codrh.CustomerOrderHeaderInstance,
		codrh.TransID,
		b.ID AS BatchID,
		b.Date AS BatchDate,
		Currency.Description AS Currency,
		ca.ID AS BillToID,
		ca.Name AS BillToName,
		b.OrderID,
		b.CampaignID,
		codrh.NumberOfIssues AS NbOfIssue, 
		codrh.ItemPriceTotal AS CustPrice,
		codrh.BasePrice AS BasePrice,
		i.DateCreated AS DateSend,
		ia.DateCreated AS ActionDate,
	
		a.Description AS ActionDescription,
		a.Instance AS ActionInstance,
		pc.Description AS ProblemCodeDescription,
		codrh.Status AS SubStatus,
	
		cuser.LastName + ',' + cuser.FirstName as LggedByName,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crh.FirstName + ' ' + crh.LastName ELSE '' END AS NameRevisedAddress,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crh.Address1  ELSE '' END AS Address1RevisedAddress,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crh.Address2  ELSE '' END AS Address2RevisedAddress,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crh.city  ELSE '' END AS CityRevisedAddress, 
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN 'CA' ELSE '' END AS CountryRevisedAddress,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crh.Zip ELSE '' END AS PostalCodeRevisedAddress,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crh.State ELSE '' END AS ProvinceRevisedAddress,
	
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crhOriginal.FirstName + ' ' + crhOriginal.LastName ELSE crh.FirstName + ' ' + crh.LastName END AS NameOnFile,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crhOriginal.Address1 ELSE crh.Address1 END AS Address1OnFile,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crhOriginal.Address2 ELSE crh.Address2 END AS Address2OnFile,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crhOriginal.City ELSE crh.City END AS CityOnFile,
		'CA' AS CountryOnFile,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crhOriginal.zip ELSE crh.zip END AS PostalCodeOnFile,
		CASE WHEN a.Instance = 4 OR i.ProblemCodeInstance = 196 THEN crhOriginal.State ELSE crh.State END AS ProvinceOnFile
	
FROM		QSPCanadaOrderManagement..Incident i
JOIN		QSPCanadaOrderManagement..IncidentAction ia
			ON	ia.IncidentInstance = i.IncidentInstance
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
			ON	coh.Instance = i.CustomerOrderHeaderInstance
JOIN		QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
			ON	codrh.CustomerOrderHeaderInstance = i.CustomerOrderHeaderInstance
			AND	codrh.TransID = i.TransID
JOIN		QSPCanadaOrderManagement..CustomerRemitHistory crh
			ON	crh.Instance = codrh.CustomerRemitHistoryInstance
			AND	crh.Instance = ia.CustomerRemitHistoryInstance
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod	
			ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
			AND	cod.TransID = codrh.TransID
JOIN		QSPCanadaProduct..Pricing_Details pd
			ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
			ON	p.Product_Instance = pd.Product_Instance
JOIN		QSPCanadaCommon..CodeDetail currency
			ON	currency.Instance = codrh.CurrencyID
JOIN		QSPCanadaProduct..Fulfillment_House fh
			ON	fh.Ful_Nbr = p.Fulfill_House_Nbr
JOIN		QSPCanadaProduct..Publishers pub
			ON	pub.Pub_Nbr = p.Pub_Nbr
JOIN		QSPCanadaOrderManagement..ProblemCode pc
			ON	pc.Instance = i.ProblemCodeInstance
JOIN		QSPCanadaOrderManagement..IncidentStatus incsta
			ON	incsta.Instance = i.StatusInstance
JOIN		QSPCanadaCommon..CUserProfile cuser
			ON	cuser.Instance = i.UserIDCreated
JOIN		QSPCanadaOrderManagement..Action a
			ON	a.Instance = ia.ActionInstance
JOIN		QSPCanadaOrderManagement..Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign c
			ON	c.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount ca
			ON	ca.ID = c.BillToAccountID
LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrhOriginal
			ON	codrhOriginal.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
			AND	codrhOriginal.TransID = codrh.TransID
			AND	codrhOriginal.Status IN (42001, 42006, 42007)
			AND	codrhOriginal.DateChanged =
				(SELECT	TOP 1
						codrhOriginal2.DateChanged
				FROM		CustomerOrderDetailRemitHistory codrhOriginal2
				WHERE	codrhOriginal2.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND		codrhOriginal2.TransID = codrh.TransID
				AND		codrhOriginal2.Status IN (42001, 42006, 42007)
				AND		codrhOriginal2.DateChanged < codrh.DateChanged
				ORDER BY	codrhOriginal2.DateChanged DESC)
LEFT JOIN	QSPCanadaOrderManagement..CustomerRemitHistory crhOriginal
			ON	crhOriginal.Instance = codrhOriginal.CustomerRemitHistoryInstance
GO
