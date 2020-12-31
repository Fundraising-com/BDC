USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_OrderEntryFollowupReport_Mag]    Script Date: 06/07/2017 09:20:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_OrderEntryFollowupReport_Mag] 
 
@OrderID int

AS

SET NOCOUNT ON

Select    tch.Classroom ,
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
	      END  as ErrorType_FR

 From 	QSPCanadaOrderManagement.dbo.CustomerOrderHeader as coh,
	QSPCanadaCommon.dbo.Campaign 			 as ca,
	QSPCanadaOrderManagement.dbo.CustomerOrderDetail as cod ,
	QSPCanadaOrderManagement.dbo.Student 		as stu,
	QSPCanadaOrderManagement.dbo.Teacher 		as tch,
	QSPCanadaOrderManagement.dbo.Customer 		as CustBill,
	QSPCanadaOrderManagement.dbo.Batch 		as batch
 Where coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.StudentInstance 	= stu.instance
   and stu.teacherInstance 	= tch.Instance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.CampaignID 		= ca.ID
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= batch.Date
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

    and batch.OrderID 		= @OrderID
 ORDER BY   tch.LastName, tch.Classroom,ParticipantName
GO
