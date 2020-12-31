USE QSPCanadaOrderManagement
GO

SELECT		DISTINCT
			CASE WHEN act.Instance IN (21, 22) THEN (SELECT SUM(cod2.Price) FROM CustomerOrderDetail cod2 WHERE cod2.CustomerOrderHeaderInstance = coh.Instance)
				 ELSE cod.Price
			END AS Price,
			batch.Date AS BatchDate, 
			inc.DateCreated AS IncidentDate, 
			act.Description AS ActionDescription, 
			inc.IncidentInstance AS IncidentInstance, 
			inc.UserIDCreated AS LoggedByInstance, 
			inc.StatusInstance AS IncidentStatusInstance, 
			inc.ProblemCodeInstance AS ProblemCodeInstance, 
			incAct.Comments AS Comments, 
			fh.Ful_Nbr AS FulfillmentHouseInstance, 
			fh.Ful_Name AS FulfillmentHouseName, 
			fh.Ful_Addr_1 + '', '' + 
				CASE ISNULL(fh.Ful_Addr_2, '') 
					WHEN '' THEN	'' 
					ELSE			fh.Ful_Addr_2 + ', '
				END + 
				fh.Ful_City + '', '' + 
				fh.Ful_State + '', '' + 
				fh.CountryCode + '', '' + 
				fh.Ful_Zip AS FulfillmentHouseAddress, 
			fh.InterfaceLayoutID, 	
			pub.Pub_Nbr AS PublisherInstance, 
			pub.Pub_Name AS PublisherName, 
			prod.RemitCode AS TitleCodeInstance, 
			codrh.MagazineTitle AS MagazineTitle, 
			cod.CustomerOrderHeaderInstance, 
			cod.TransID, 
			batch.ID AS BatchID, 
			Currency.Description AS Currency, 
			acc.ID AS AccountID, 
			acc.Name AS AccountName, 
			batch.OrderID, 
			batch.CampaignID, 
			codrh.NumberOfIssues AS NbOfIssue,  
			codrh.ItemPriceTotal AS CustPrice, 
			codrh.BasePrice AS BasePrice, 
			inc.DateCreated AS DateSend, 
			incAct.DateCreated AS ActionDate, 	
			act.Instance AS ActionInstance, 
			pc.Description AS ProblemCodeDescription, 
			cdCodrh.Description AS SubStatus, 	
			cuser.LastName + ',' + cuser.FirstName AS LggedByName,
			crh.FirstName + ' ' + crh.LastName 	AS NameRevisedAddress,
			crh.Address1 AS Address1RevisedAddress,
			crh.Address2 AS Address2RevisedAddress,
			crh.city AS CityRevisedAddress, 
			'CA' AS CountryRevisedAddress,
			crh.Zip	AS PostalCodeRevisedAddress,
			crh.State AS ProvinceRevisedAddress,	
			crhOriginal.FirstName + ' ' + crhOriginal.LastName AS NameOnFile,
			crhOriginal.Address1 AS Address1OnFile,
			crhOriginal.Address2 AS Address2OnFile,
			crhOriginal.City AS CityOnFile,
			'CA' AS CountryOnFile,
			crhOriginal.zip AS PostalCodeOnFile,
			crhOriginal.State AS ProvinceOnFile,
			ISNULL(custBill.FirstName, '') AS PurchaserFirstName,
			ISNULL(custBill.LastName, '') AS PurchaserLastName,
			custBill.Phone AS PurchaserPhone,
			fm.Firstname FMFirstname,
			fm.Lastname FMLastname,
			fm.FMID
FROM		CustomerOrderHeader coh 	
JOIN		Incident inc
				ON	inc.CustomerOrderHeaderInstance = coh.Instance
JOIN 		IncidentAction incAct 
				ON	incAct.IncidentInstance = inc.IncidentInstance
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = inc.CustomerOrderHeaderInstance
				AND	cod.TransID = inc.TransID
LEFT JOIN	(CustomerOrderDetailRemitHistory codrh 
JOIN			CustomerRemitHistory crh 
					ON	crh.Instance = codrh.CustomerRemitHistoryInstance
JOIN			QSPCanadaCommon..CodeDetail cdCodrh
					ON	cdCodrh.Instance = codrh.Status)
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
				AND	codrh.Status IN (42000, 42001, 42004, 42010) --42000: Needs to be Sent, 42001: Sent, 42004: Canceled prior to remit, 42010: Magazine Inactive
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product prod
				ON	prod.Product_Instance = pd.Product_Instance
JOIN		QSPCanadaCommon..CodeDetail currency
				ON	currency.Instance = prod.Currency
JOIN		QSPCanadaProduct..Fulfillment_House fh
				ON	fh.Ful_Nbr = prod.Fulfill_House_Nbr
JOIN		QSPCanadaProduct..Publishers pub 
				ON	pub.Pub_Nbr = prod.Pub_Nbr 
JOIN		ProblemCode pc
				ON	pc.Instance = inc.ProblemCodeInstance
JOIN		IncidentStatus incSta
				ON	incSta.Instance = inc.StatusInstance 
JOIN		QSPCanadaCommon..CUserProfile cuser
				ON	cuser.Instance = inc.UserIDCreated
JOIN		QSPCanadaOrderManagement..Action act 
				ON	act.Instance = incAct.ActionInstance
JOIN		QSPCanadaOrderManagement..Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = batch.CampaignID 
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
LEFT JOIN	CustomerOrderDetailRemitHistory codrhOriginal
				ON 	codrhOriginal.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	codrhOriginal.TransID = codrh.TransID
				AND	codrhOriginal.Status IN (42001, 42006, 42007)
				AND	codrhOriginal.DateChanged =
					(SELECT		TOP 1
								codrhOriginal2.DateChanged
					FROM		CustomerOrderDetailRemitHistory codrhOriginal2
					WHERE		codrhOriginal2.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
					AND			codrhOriginal2.TransID = codrh.TransID
					AND			codrhOriginal2.Status IN (42001, 42006, 42007)
					AND			codrhOriginal2.DateChanged < codrh.DateChanged
					ORDER BY	codrhOriginal2.DateChanged DESC)
LEFT JOIN	CustomerRemitHistory crhOriginal
				ON	crhOriginal.Instance = codrhOriginal.CustomerRemitHistoryInstance
JOIN		Customer custBill
				ON	custBill.Instance = coh.CustomerBillToInstance
WHERE		batch.Date between '2013-01-01' and '2013-09-10'
AND			act.Instance IN (18, 21, 22)

--Remove 3rd call / remove from OEFU actions on subs that were reprocessed
AND	NOT		(act.Instance IN (21, 22) AND coh.Instance IN (SELECT	CustomerOrderHeaderInstance
															FROM	Incident inc2
															JOIN 	IncidentAction incAct2
																			ON	incAct2.IncidentInstance = inc2.IncidentInstance
															WHERE	inc2.CustomerOrderHeaderInstance = coh.Instance
															AND		incAct2.ActionInstance IN (18)
															))
															


--AND			inc.Comments <> 'Automated Update CC'
ORDER BY	act.Instance,
			inc.DateCreated

