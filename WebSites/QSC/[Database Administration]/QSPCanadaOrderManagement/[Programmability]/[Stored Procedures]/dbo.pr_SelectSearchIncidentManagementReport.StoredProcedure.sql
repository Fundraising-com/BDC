USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSearchIncidentManagementReport]    Script Date: 06/07/2017 09:20:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectSearchIncidentManagementReport]

	@IncidentStatusInstance		INT,
	@IncidentIDfrom				INT,
	@IncidentIDto				INT,
	@DateIncidentLoggedFrom		DATETIME,
	@DateIncidentLoggedTo		DATETIME,
	@OrderIDFrom				INT,
	@OrderIDTo					INT,
	@LoggedByInstance			INT,
	@ProblemCodeInstance		INT,
	@FulfillmentHouseInstance	INT,
	@PublisherInstance			INT,
	@TitleCodeInstance			VARCHAR(10),
	@PrintAll					BIT,
	@JustOne					BIT,
	@ByIndividual				BIT,
	@ActionInstance				INT,
	@RemoveAutomated			BIT,
	@CampaignID					INT,
	@AccountID					INT

AS

IF @IncidentStatusInstance = 0 SET @IncidentStatusInstance = NULL
IF @IncidentIDFrom = 0 SET @IncidentIDFrom = NULL
IF @IncidentIDTo = 0 SET @IncidentIDTo = NULL
IF @DateIncidentLoggedFrom = '1955-01-01' SET @DateIncidentLoggedFrom = NULL
IF @DateIncidentLoggedTo = '1955-01-01' SET @DateIncidentLoggedTo = NULL
IF @OrderIDFrom = 0 SET @OrderIDFrom = NULL
IF @OrderIDTo = 0 SET @OrderIDTo = NULL
IF @LoggedByInstance = 0 SET @LoggedByInstance = NULL
IF @ProblemCodeInstance = 0 SET @ProblemCodeInstance = NULL
IF @FulfillmentHouseInstance = 0 SET @FulfillmentHouseInstance = NULL
IF @PublisherInstance = 0 SET @PublisherInstance = NULL
IF @TitleCodeInstance = '0' SET @TitleCodeInstance = NULL
IF @ActionInstance = 0 SET @ActionInstance = NULL
IF @CampaignID = 0 SET @CampaignID = NULL
IF @AccountID = 0 SET @AccountID = NULL

SELECT		DISTINCT
			inc.IncidentInstance AS IncidentInstance, 
			inc.UserIDCreated AS LoggedByInstance, 
			inc.DateCreated AS DateIncidentLoggedTo, 
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
			codrh.CustomerOrderHeaderInstance, 
			codrh.TransID, 
			batch.ID AS BatchID, 
			batch.Date AS BatchDate, 
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
			act.Description AS ActionDescription, 
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
			custBill.Phone AS PurchaserPhone
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
				--AND	act.Instance = 4
JOIN		QSPCanadaOrderManagement..Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = batch.CampaignID 
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
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
WHERE		inc.StatusInstance = ISNULL(@IncidentStatusInstance, inc.StatusInstance)
AND			inc.IncidentInstance BETWEEN ISNULL(@IncidentIDFrom, inc.IncidentInstance) AND ISNULL(@IncidentIDTo, inc.IncidentInstance)
AND			inc.DateCreated BETWEEN ISNULL(@DateIncidentLoggedFrom, inc.DateCreated) AND ISNULL(@DateIncidentLoggedTo, inc.DateCreated)
AND			inc.UserIDCreated = ISNULL(@LoggedByInstance, inc.UserIDCreated)
AND			inc.ProblemCodeInstance = ISNULL(@ProblemCodeInstance, inc.ProblemCodeInstance)
AND			(ISNULL(@RemoveAutomated, 0) = 0 OR incAct.Comments NOT LIKE 'Automated%')
AND			prod.RemitCode = ISNULL(@TitleCodeInstance, prod.RemitCode)
AND			fh.Ful_Nbr = ISNULL(@FulfillmentHouseInstance, fh.Ful_Nbr)
AND			(ISNULL(@PrintAll, 0) = 0 OR act.Instance NOT IN (6, 1, 4))
AND			(ISNULL(@ByIndividual, 0) = 0 OR act.Instance = ISNULL(@ActionInstance, act.Instance))
AND			batch.OrderID BETWEEN ISNULL(@OrderIDFrom, batch.OrderID) AND ISNULL(@OrderIDTo, batch.OrderID)
AND			(ISNULL(@JustOne, 0) = 0 OR (((fh.InterfaceLayoutID NOT IN (33004, 33009)
										AND incAct.ActionInstance IN (6, 1, 4))
										OR incAct.ActionInstance = 6)
										AND ((incAct.ActionInstance = 4
										AND codrh.Status IN (42006, 42007))
										OR incAct.ActionInstance <> 4
										AND codrh.Status <> 42004)))
AND	        camp.ID = ISNULL(@CampaignID, camp.ID)
AND			acc.ID = ISNULL(@AccountID, acc.ID) 
ORDER BY	incAct.ActionDate
GO
