USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerServiceOrdersStatementReport]    Script Date: 06/07/2017 09:19:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerServiceOrdersStatementReport] 
  
	@iCampaignID	INT,
	@iOver100		INT,  
	@dFrom			DATETIME,  
	@dTo			DATETIME,  
	@zFMID			VARCHAR(4)  
  
AS  

IF ISNULL(@zFMID, '') = ''
BEGIN
	SET @zFMID = NULL
END

IF ISNULL(@iCampaignID, 0) = 0
BEGIN
	SET @iCampaignID = NULL
END  

SELECT		cph.Instance,
			cph.TotalAmount * -1 AS TotalAmount,
			acc.ID AS AcctID,
			ph.phoneNumber,
			b.CampaignID,
			camp.IsStaffOrder,
			CASE OrderQualifierID
				WHEN 39015 THEN CASE camp.Lang
									WHEN 'FR' THEN 'Commandes payées par cartes de crédit'
									ELSE 'Credit Card Orders'
								END  
				WHEN 39013 THEN	CASE camp.Lang
									WHEN 'FR' THEN 'Commandes payées par cartes de crédit'  
									ELSE 'Credit Card Orders'
								END
				ELSE	CASE camp.Lang
							WHEN 'FR' THEN 'Commandes payées par ďautres moyeus'
							ELSE 'Non Credit Card Orders (Charged to Account)'
						END
				END AS IsCCOrder,
			camp.FMID,
			fm.FirstName + ' ' + fm.LastName AS FMName,
			ISNULL(b.ContactFirstName + ' ' + b.ContactLastName, cont.FirstName + ' ' + cont.LastName) AS ContactName,
			acc.Name AS AcctName,  
			adShip.Street1 AS ShippingAddress,  
			adShip.Street2 AS ShippingAddress2,  
			adShip.City AS ShippingCity,  
			adShip.StateProvince AS ShippingState,  
			adShip.Postal_Code AS ShippingZip,  
			CASE adShip.StateProvince  
				WHEN 'NB' THEN CASE ISNULL(camp.Lang, 'EN')
									WHEN 'FR' THEN 'TVH'
									ELSE 'HST'
								END
				WHEN 'NS' THEN CASE ISNULL(camp.Lang,'EN')
									WHEN 'FR' THEN  'TVH'
									ELSE 'HST'
								END
				WHEN 'NL' THEN CASE ISNULL(camp.Lang,'EN')
									WHEN 'FR' THEN  'TVH'
									ELSE 'HST'
								END
				WHEN 'QC' THEN CASE ISNULL(camp.Lang,'EN')
									WHEN 'FR' THEN  'TPS/TVQ'
									ELSE 'GST/QST'
								END
				ELSE	CASE ISNULL(camp.Lang,'EN')
							WHEN 'FR' THEN  'TPS'
							ELSE 'GST'
						END
				END AS TAXRegion,
			camp.Lang,
			cod.Tax AS TotalGST,
			ISNULL(gst.Tax_Rate, 0) AS GST_Rate,
			cod.Tax2 AS TotalPST,
			ISNULL(pst.Tax_Rate, 0) AS PST_Rate,
			CASE adShip.StateProvince
				WHEN 'NB' THEN (cod.Tax + cod.Tax2)
				WHEN 'NS' THEN (cod.Tax + cod.Tax2)
				WHEN 'NL' THEN (cod.Tax + cod.Tax2)
				ELSE 0
			END AS TotalHST,
			ISNULL(hst.Tax_Rate, 0) HST_Rate,
			cp.GroupProfit,
			st.FirstName,
			st.LastName,
			cod.ProductCode,
			cod.ProductName,
			CASE cod.ProductType
				WHEN 46001 THEN 1
				ELSE cod.Quantity
			END AS Quantity,
			cod.Price,
			CASE camp.IsStaffOrder
				WHEN 1 THEN 0
				WHEN 0 THEN (cod.Price - (cod.Tax + cod.Tax2)) * cp.GroupProfit *.01
			END AS ProfitEarned,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN '6600, route Transcanadienne - bureau 750'
				ELSE '695 Riddell Road'
			END AS QSPAddress1Label,
			CASE ISNULL(camp.Lang, 'EN')
				WHEN 'FR' THEN 'Pointe-Claire, QC   H9R 4S2'
				ELSE 'Orangeville, ON   L9W 4Z5 ' 
			END AS QSPAddress2Label,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN '1-800-667-2536' 
				ELSE '1-800-667-2536' 
			END AS QSPPhoneLabel
FROM    	Batch b
LEFT JOIN	QSPCanadaCommon..CAccount acc (NOLOCK) 
				ON	acc.Id = b.AccountID
LEFT JOIN	QSPCanadaCommon..Phone ph (NOLOCK)
				ON	ph.PhoneListID = acc.PhoneListID
				AND	ph.Type = 30505  --Main
LEFT JOIN	QSPCanadaCommon..AddressList al (NOLOCK)
				ON	al.ID = acc.AddressListID
LEFT JOIN	QSPCanadaCommon..Address adShip (NOLOCK)
				ON	adShip.AddressListID = al.ID
				AND	adShip.Address_Type = 54002
LEFT JOIN	QSPCanadaCommon..TaxApplicableTax tat (NOLOCK)
				ON	tat.Province_Code = adShip.StateProvince
				AND	tat.Tax_ID = 1
				AND	tat.Section_Type_ID = 2
LEFT JOIN	QSPCanadaCommon..Tax gst (NOLOCK)
				ON	gst.Tax_ID = tat.Tax_ID
LEFT JOIN	QSPCanadaCommon..TaxApplicableTax tatpst (NOLOCK)
				ON	tatpst.Province_Code = adShip.StateProvince 
				AND	tatpst.Tax_Id NOT IN (1, 2, 4, 5)
				AND	tatpst.SECTION_TYPE_ID = 2
LEFT JOIN	QSPCanadaCommon..Tax pst (NOLOCK)
				ON	pst.Tax_Id = tatpst.Tax_Id
LEFT JOIN	QSPCanadaCommon..TaxApplicableTax tathst (NOLOCK)
				ON	adShip.StateProvince = tathst.Province_Code
				AND	tathst.Tax_Id IN (2, 4, 5) 
				AND	tathst.Section_Type_ID = 2
LEFT JOIN	QSPCanadaCommon..Tax hst (NOLOCK)
				ON	hst.Tax_Id = tathst.Tax_Id
JOIN		QSPCanadaCommon..Campaign camp (NOLOCK)
				ON	camp.[Id] = b.Campaignid
				AND	camp.IsStaffOrder = 0
				AND	camp.ID = ISNULL(@iCampaignID, camp.ID)
LEFT JOIN	QSPCanadaCommon..Contact cont (NOLOCK)
				ON 	cont.ID = camp.ShipToCampaignContactID
JOIN		QSPCanadaCommon..CampaignProgram cp (NOLOCK)
				ON	cp.CampaignId = camp.ID
				AND	cp.ProgramId IN (1,2)
				AND	cp.DeletedTF = 0
JOIN		QSPCanadaCommon..FieldManager fm (NOLOCK)
				ON	fm.FMID = camp.FMID
JOIN		CustomerOrderHeader coh (NOLOCK)
				ON	coh.OrderBatchID = b.[ID]
				AND	coh.OrderBatchDate = b.[DATE]
JOIN		CustomerOrderDetail cod (NOLOCK)
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
				AND cod.DelFlag = 0
LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh (NOLOCK)
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
JOIN		Student st (NOLOCK)
				ON	st.Instance = coh.StudentInstance
LEFT JOIN	QSPCanadaOrderManagement..CustomerPaymentHeader cph (NOLOCK)
				ON	cph.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	QSPCanadaOrderManagement..CreditCardPayment ccp (NOLOCK)
				ON	ccp.CustomerPaymentHeaderInstance = cph.Instance
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
WHERE		b.OrderQualifierID IN (39013, 39015)
AND			(coh.PaymentMethodInstance = 50002 OR ccp.StatusInstance = 19000) -- Cash/Cheque or CC Payment Good
AND			cod.CreationDate BETWEEN CONVERT(NVARCHAR, @dFrom, 101) AND CONVERT(NVARCHAR, @dTo, 101)
AND			(codrh.Status IN (42000, 42001, 42004, 42010)
OR			cod.StatusInstance = 508)
AND			fm.FMID = ISNULL(@zFMID, fm.FMID)
ORDER BY	b.OrderQualifierID DESC,
			st.LastName,
			st.FirstName
GO
