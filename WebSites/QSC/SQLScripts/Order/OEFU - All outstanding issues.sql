--OEFU - Mag.xlsx
Select    
	fm.Firstname + ' ' + fm.Lastname FM,
	batch.orderid,
	batch.Date,
	acc.ID AccountID,
	acc.Name AccountName,
	ca.ID CampaignID,
	tch.Classroom ,
	case ca.Lang when 'FR' then case tch.LastName when 'UNKNOWN' then 'INCONNU' else  tch.LastName end 
	else  tch.LastName end as TeacherName,
	isnull(stu.FirstName,'')+' '+stu.LastName as ParticipantName,
    coh.CustomerBillToInstance,
	CustBill.StatusInstance,
  	cod.Recipient 		as SubscriberName,
    isnull(CustBill.FirstName,'')+' '+isnull(CustBill.LastName,'') as PurchaserName,
	CustBill.Phone 		as CustomerPhone,
	CustBill.Address1       as CustomerAddress1,
	CustBill.Address2 	as CustomerAddress2,
	CustBill.City 		as CustomerCity,
	CustBill.State 		As CustomerState,
	CustBill.Zip 		as CustomerZip,
	cod.ProductCode as TitleCode,
	cod.ProductName as MagazineTitle,
	cod.Quantity 	as Numberofissues,
	Case when ca.isstafforder = 0 then Convert(numeric(10,2),cod.Price) else Convert(numeric(10,2),cod.Price*(100-ca.stafforderDiscount)*.01) End as CatalogPrice, --MS Nov 21, 06 2 decimal places
	ca.Lang,
	cod.CustomerOrderHeaderInstance,
	cod.TransID,
	cod.InvoiceNumber,
	
	Case when cod.StatusInstance = 501 and cod.ProductCode LIKE 'D%' AND ISNULL(CustBill.Email, '') = '' then 'DIGITAL SUBSCRIPTION ERROR[S] - MISSING EMAIL ADDRESS'
		when Isnull(cod.Recipient,'')= ''       	OR
			 IsNull(CustBill.Address1,'') = ''  		OR
	      	 Isnull(CustBill.City,'') = ''      		OR
	      	 Isnull(CustBill.State,'')= ''      		OR
	      	 Isnull(CustBill.StatusInstance,0) = 301      		OR
			 Isnull(CustBill.Zip,'')= ''	Then   'MAGAZINE SUBSCRIPTION ERROR[S] - MISSING or INCOMPLETE ADDRESS[ES]'	
	    when  CustBill.State NOT IN 
			(SELECT	distinct 	Province
			 FROM	QspCanadaCommon..TaxRegionProvince) OR
	      		 (LEN(RTRIM(CustBill.Zip)) <> 6  or not  CustBill.Zip LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]') then 'MAGAZINE SUBSCRIPTION ERROR[S] - WRONG ADDRESS[ES]'
		when cod.ProductCode = 'NNNN' then 'ILLEGIBLE ITEM'
		when cod.StatusInstance = 501 then 'MISSING OR INCOMPLETE PRODUCT INFORMATION'
		when (cod.StatusInstance = 515 and ca.IsStaffOrder=1 and PaymentMethodInstance in (50002,50003,50004)) Then 'MAGAZINE SUBSCRIPTION ERROR[S] - UNAUTHORIZED MAGAZINE**'
		when (cod.StatusInstance = 514) Then 'MAGAZINE SUBSCRIPTION ERROR[S] - LIBRARY ORDER'
		when (cod.Recipient like '%LIBRARY%') Then 'MAGAZINE SUBSCRIPTION ERROR[S] - LIBRARY ORDER'	--MS Nov 24, 06
		Else 'CUSTOMER STATUS ERROR'
	END  as ErrorCategory,

	Case when cod.StatusInstance = 501 and cod.ProductCode LIKE 'D%' AND ISNULL(CustBill.Email, '') = '' then 'ERREUR[S] DES ABONNEMENTS NUMÉRIQUES – ADRESSE COURRIEL MANQUANTE'
		when Isnull(cod.Recipient,'')= ''       	OR
			 IsNull(CustBill.Address1,'') = ''  		OR
	      	 Isnull(CustBill.City,'') = ''      		OR
	      	 Isnull(CustBill.State,'')= ''      		OR
	      	 Isnull(CustBill.StatusInstance,0) = 301      		OR
			 Isnull(CustBill.Zip,'')= ''	Then 'ERREUR D''ABONNEMENT A UN MAGAZINE - ADRESSE INCOMPLETE OU MANQUANTE'
	    when  CustBill.State NOT IN 
			(SELECT	distinct 	Province
			FROM		QspCanadaCommon..TaxRegionProvince) OR
	       		(LEN(RTRIM(CustBill.Zip)) <> 6  or not  CustBill.Zip LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]') then 'MAGAZINE SUBSCRIPTION ERROR[S] - WRONG ADDRESS[ES]'
		when cod.ProductCode = 'NNNN' then 'ILLEGIBLE ITEM'
		when cod.StatusInstance = 501 then 'INFORMATION MANQUANTE OU INCOMPLÈTE DU PRODUIT'
	 	when (cod.StatusInstance = 515 and ca.IsStaffOrder=1 and PaymentMethodInstance in (50002,50003,50004)) Then 'ERREUR D''ABONNEMENT A UN MAGAZINE - MAGAZINE NON AUTHORISE**'
		when (cod.StatusInstance = 514) Then 'ERREUR D''ABONNEMENT A UN MAGAZINE - COMMANDE DE BIBLIOTHÈQUE'	
		when (cod.Recipient like '%LIBRARY%') Then 'ERREUR D''ABONNEMENT A UN MAGAZINE - COMMANDE DE BIBLIOTHÈQUE'	
	    Else 'CUSTOMER STATUS ERROR'
	END  as ErrorCategory_FR,

	Case when cod.StatusInstance = 501 and cod.ProductCode LIKE 'D%' AND ISNULL(CustBill.Email, '') = '' then 'TOTAL DIGITAL SUBSCRIPTION ERRORS:'	
		when Isnull(cod.Recipient,'')= ''       	OR
			 IsNull(CustBill.Address1,'') = ''  		OR
	      	 Isnull(CustBill.City,'') = ''      		OR
	      	 Isnull(CustBill.State,'')= ''      		OR
	      	 Isnull(CustBill.StatusInstance,0) = 301      		OR
			 Isnull(CustBill.Zip,'')= ''			OR
	       	 CustBill.State NOT IN (SELECT	distinct 	Province
			 FROM		QspCanadaCommon..TaxRegionProvince) OR
	         (LEN(RTRIM(CustBill.Zip)) <> 6  or not  CustBill.Zip LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]') then 'TOTAL ADDRESS ERRORS:'
		when cod.ProductCode = 'NNNN' then 'TOTAL PRODUCT CODE ERRORS:'
		when cod.StatusInstance = 501 then 'TOTAL PRODUCT CODE ERRORS:'
		when (cod.StatusInstance = 515 and ca.IsStaffOrder=1 and PaymentMethodInstance in(50002,50003,50004)) Then 'TOTAL OTHER ERRORS:'
		when (cod.StatusInstance = 514 ) Then 'TOTAL OTHER ERRORS:'
		when (cod.Recipient like '%LIBRARY%') Then 'TOTAL OTHER ERRORS:'
	    Else 'CUSTOMER STATUS ERROR'
	END  as ErrorType,

	Case when cod.StatusInstance = 501 and cod.ProductCode LIKE 'D%' AND ISNULL(CustBill.Email, '') = '' then 'TOTAL D’ERREURS DES ABONNEMENTS NUMÉRIQUES:'
		when Isnull(cod.Recipient,'')= ''       	OR
			 IsNull(CustBill.Address1,'') = ''  		OR
	      	 Isnull(CustBill.City,'') = ''      		OR
	      	 Isnull(CustBill.State,'')= ''      		OR
	      	 Isnull(CustBill.StatusInstance,0) = 301      		OR
			 Isnull(CustBill.Zip,'')= ''			OR
	       	 CustBill.State NOT IN (SELECT	distinct 	Province
			 FROM		QspCanadaCommon..TaxRegionProvince) OR
	         (LEN(RTRIM(CustBill.Zip)) <> 6  or not  CustBill.Zip LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]') then 'ERREURS D''ADRESSE TOTALES :'
		when cod.ProductCode = 'NNNN' then 'TOTAL DES ERREURS DE CODE DU PRODUIT:'
		when cod.StatusInstance = 501 then 'TOTAL DES ERREURS DE CODE DU PRODUIT:'
		when (cod.StatusInstance = 515 and ca.IsStaffOrder=1 and PaymentMethodInstance in (50002,50003,50004)) Then 'TOTAL DES AUTRES ERREURS:'
		when (cod.StatusInstance = 514 ) Then  'TOTAL DES AUTRES ERREURS:'
		when (cod.Recipient like '%LIBRARY%') Then   'TOTAL DES AUTRES ERREURS:'
	      Else 'ERREURS D''ADRESSE TOTALES :'
	      END  as ErrorType_FR,
	      cod.StatusInstance

 From 	QSPCanadaOrderManagement.dbo.CustomerOrderHeader as coh,
	QSPCanadaCommon.dbo.Campaign 			 as ca,
	QSPCanadaOrderManagement.dbo.CustomerOrderDetail as cod ,
	QSPCanadaOrderManagement.dbo.Student 		as stu,
	QSPCanadaOrderManagement.dbo.Teacher 		as tch,
	QSPCanadaOrderManagement.dbo.Customer 		as CustBill,
	QSPCanadaOrderManagement.dbo.Batch 		as batch,
	QSPCanadaCommon.dbo.FieldManager as fm,
	QSPCanadaCommon.dbo.CAccount as acc
 Where coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.StudentInstance 	= stu.instance
   and stu.teacherInstance 	= tch.Instance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.CampaignID 		= ca.ID
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= batch.Date
   and fm.FMID = ca.FMID
   and acc.ID = ca.BillToAccountID
   and ISNULL(cod.ProductType, 0) in  (0, 46001, 46006, 46007, 46023)
   and 
   ( cod.Recipient is null 		or cod.Recipient = '' or
	 CustBill.Address1 is null 	or CustBill.Address1 = '' or
	 CustBill.City is null 		or CustBill.City = '' or
	 CustBill.State is null 	or CustBill.State   = '' or
	 CustBill.State NOT IN (SELECT	Distinct 	Province FROM		QspCanadaCommon.dbo.TaxRegionProvince) OR 
	 CustBill.Zip is null 		or CustBill.Zip = '' or
	 (LEN(RTRIM(CustBill.Zip)) <> 6  or not  CustBill.Zip LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]') or 
	 CustBill.StatusInstance  = 301
	 OR cod.ProductCode = 'NNNN' --illegible items
	 OR cod.StatusInstance = 501
	 OR (cod.statusInstance = 515 and PaymentMethodInstance in (50002,50003,50004)) 
	 OR  cod.statusInstance = 514 
	 OR cod.Recipient Like '%LIBRARY%'
    ) 
    and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
    and cod.delflag <>1
    
    AND NOT EXISTS	(SELECT	1
					FROM	incident inc
					JOIN	incidentAction incA
								ON	inc.IncidentInstance = incA.IncidentInstance
					WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
					AND		inc.TransID = cod.TransID
					AND		incA.ActionInstance IN (18, 22, 150, 151) --CC Update, Remove From OEFU, New Sub to Invoice, New Item to Invoice
					)

    --and batch.OrderID 		= @OrderID
    and cod.IsVoucherRedemption = 0
    and batch.Date >= '2017-07-01'
	and cod.StatusInstance NOT IN (508)
 ORDER BY   batch.Date, batch.orderid, tch.LastName, tch.Classroom,ParticipantName
 
--OEFU - Gift.xlsx 
  Select 	
   	fm.Firstname + ' ' + fm.Lastname FM,
	batch.OrderID,
	batch.Date,
   	acc.ID AccountID,
	acc.Name AccountName,
	ca.ID CampaignID,
	tch.Classroom ,
	case ca.Lang when 'FR' then case tch.LastName when 'UNKNOWN' then 'INCONNU' else  tch.LastName end 
	else  tch.LastName end as TeacherName,
	isnull(stu.FirstName,'')+' '+stu.LastName as ParticipantName,
        	coh.CustomerBillToInstance,
  	cod.Recipient 			as PurchaserName,
	CustBill.Phone 			as CustomerPhone,
	cod.Catalogprice,cod.Quantity,cod.Price,
	ca.Lang

 From 	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh,
	QSPCanadaCommon..Campaign 			as ca,
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement..Student 		as stu,
	QSPCanadaOrderManagement..Teacher 		as tch,
	QSPCanadaOrderManagement..Batch 		as batch,
	QSPCanadaOrderManagement..Customer		as CustBill,
	QSPCanadaCommon.dbo.FieldManager as fm,
	QSPCanadaCommon.dbo.CAccount as acc
 Where coh.Instance  		= cod.CustomerOrderHeaderInstance
   and coh.StudentInstance 	= stu.instance
   and stu.teacherInstance 	= tch.Instance
   and coh.CampaignID 		= ca.ID
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= Batch.Date
   and fm.FMID = ca.FMID
   and acc.ID = ca.BillToAccountID
   and coh.CustomerBillToInstance = CustBill.Instance
   and (cod.ProductCode = 'NNNN' OR cod.StatusInstance = 501) -- Illegible Item Code
   and ISNULL(cod.ProductType, 0) IN (0, 46002, 46018, 46020, 46022)
   and cod.delflag <>1
   and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
   --and batch.OrderID 		= @OrderID
   and batch.Date >= '2017-07-01'

    AND NOT EXISTS	(SELECT	1
				FROM	incident inc
				JOIN	incidentAction incA
							ON	inc.IncidentInstance = incA.IncidentInstance
				WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND		inc.TransID = cod.TransID
				AND		incA.ActionInstance IN (18, 22, 150, 151) --CC Update, Remove From OEFU, New Sub to Invoice, New Item to Invoice
				)

  Order by  batch.Date, batch.orderid, tch.LastName,tch.Classroom, ParticipantName

--OEFU - CC.xlsx
  SELECT		batch.campaignid AS CA,
			batch.OrderID,
			batch.Date OrderDate,
			fm.FMID,
			fm.FirstName AS FMFirstName,
			fm.LastName AS FMLastName,
			coh.Instance AS COH,
			cod.TransID,
			teach.Classroom,
			teach.LastName,
			ISNULL(stu.FirstName, '') + ' ' + ISNULL(stu.LastName, '') AS ParticipantName,
			cod.Recipient AS SubscriberName,
			ISNULL(CustBill.FirstName, '') + ' ' + ISNULL(CustBill.LastName, '') AS PurchaserName,
			CustBill.Phone AS CustomerPhone,
			CustBill.Address1 AS CustomerAddress1,
			CustBill.Address2 AS CustomerAddress2,
			CustBill.City AS CustomerCity,
			CustBill.State AS CustomerState,
			CustBill.Zip AS CustomerZip,
			cod.ProductCode AS TitleCode,
			cod.productName AS MagazineTitle,
			cod.Quantity AS Numberofissues,
			cod.Price,
			cp.CreditCardNumber,
			cp.ExpirationDate,
			ISNULL(coh.ToteID,0) AS ToteID,
			CASE WHEN batch.OrderQualifierID in (39001,39002) THEN lo.LandedOrderID
				ELSE 0 	END AS CustomerOrderID	
FROM		CreditCardPayment cp
JOIN		CustomerPaymentHeader ph
				ON	ph.Instance = cp.CustomerPaymentHeaderInstance
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = ph.CustomerOrderHeaderInstance
JOIN		QSPCanadaCommon..Campaign ca
				ON	ca.ID = coh.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = ca.FMID
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		Student stu
				ON	stu.instance = coh.StudentInstance
JOIN		Teacher teach
				ON	teach.Instance = stu.teacherInstance
JOIN		Customer custBill
				ON	custBill.Instance = coh.CustomerBillToInstance
JOIN		Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.Date = coh.OrderBatchDate
/*JOIN		QSPCanadaCommon.dbo.Season s  
				ON	GetDate() BETWEEN s.StartDate AND s.EndDate  
				AND	s.Season = 'Y'  
				AND	batch.Date BETWEEN s.StartDate AND s.EndDate  */
LEFT JOIN	LandedOrder lo
				ON batch.OrderQualifierID IN (39001,39002) AND lo.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
WHERE		coh.PaymentMethodInstance IN (50003, 50004, 50005)
AND			cp.StatusInstance IN (19001, 19002, 19005)
AND			batch.OrderQualifierID NOT IN (39009, 39011, 39014) --Internet, Internet Fix, CC Reprocess Courtesy
AND			cod.DelFlag <> 1
AND			cod.ProductType NOT IN (46017, 46021)
AND			batch.StatusInstance NOT IN (40001, 40002, 40003, 40004, 40005, 40006)
AND			NOT EXISTS		(SELECT	1
							FROM	incident inc
							JOIN	incidentAction incA
										ON	inc.IncidentInstance = incA.IncidentInstance
							WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
							AND		incA.ActionInstance IN (18) --Update Credit Card Info
							)
AND			NOT EXISTS		(SELECT	1
							FROM	incident inc
							JOIN	incidentAction incA
										ON	inc.IncidentInstance = incA.IncidentInstance
							WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
							AND		inc.TransID = cod.TransID
							AND		incA.ActionInstance IN (150, 151) --New Sub to Invoice, New Item to Invoice
							)
AND			batch.Date >= '2017-07-01'
ORDER BY	batch.OrderID,
			coh.Instance,
			cod.TransID,
			teach.LastName,
			teach.Classroom,
			ParticipantName,
			PurchaserName
