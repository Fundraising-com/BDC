USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_OnlineProgramProfitStatementReport]    Script Date: 06/07/2017 09:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_OnlineProgramProfitStatementReport]

	@iCampaignID	int,
	@iOver100		int,
	@DateFrom		datetime,
	@DateTo			datetime,
	@zFMID			varchar(4)

AS

SET NOCOUNT ON

IF ISNULL(@zFMID, '') = ''
BEGIN
	SET @zFMID = NULL
END

IF ISNULL(@iCampaignID, 0) = 0
BEGIN
	SET @iCampaignID = NULL
END

SELECT		b.CampaignID, 	
			acc.ID AS AcctID, 
			QSPCanadaFinance.dbo.UDF_CleanPhoneNumber(ph.phoneNumber,'-') phoneNumber,
			acc.Name  AS AcctName,	
			adShip.Street1 AS ShippingAddress,
			adShip.Street2 AS ShippingAddress2,	
			adShip.City AS ShippingCity, 
			AdShip.StateProvince AS ShippingState,
			AdShip.Postal_Code AS ShippingZip,
			CASE AdShip.StateProvince
				WHEN 'NB' THEN 'HST' 
				WHEN 'NS' THEN 'HST'	
				WHEN 'NL' THEN 'HST' 
				WHEN 'QC' THEN 'GST/QST' 
				ELSE 'GST' 
			END TaxRegion,
			camp.FMID, 
			' EN' AS Lang, 
			cont.FirstName + ' ' + cont.LastName AS ContactName,
			fm.FirstName + ' ' + fm.LastName AS FMName, 
			cod.ProductCode, 
			cod.ProductName,
			CASE
				WHEN codCurrent.CustomerOrderHeaderInstance IS NULL THEN 'N'
				ELSE 'Y'
			END AS CurrItem,
			CASE
				WHEN codCurrent.CustomerOrderHeaderInstance IS NULL THEN 0
				ELSE	CASE
							WHEN cod.ProductType = 46001 THEN 1
							ELSE cod.Quantity
						END
				END	AS Quantity,
			CASE
				WHEN cod.ProductType = 46001 THEN 1
				ELSE cod.Quantity 
			END	AS QuantityToDate,
			ISNULL(codCurrent.Price, 0) AS Price,
			cod.Price AS PriceTodate,
			ISNULL(codCurrent.Tax, 0) AS TotalGST,
			cod.Tax AS TotalGSTTodate,
			gst.Tax_Rate AS GST_Rate,
			ISNULL(codCurrent.Tax2, 0) AS TotalPST,	
			cod.Tax2 TotalPSTTodate,
			ISNULL(pst.Tax_Rate, 0) PST_Rate,
			CASE
				WHEN codCurrent.CustomerOrderHeaderInstance IS NULL THEN 0
				ELSE	CASE adShip.StateProvince 
							WHEN 'NB' THEN (cod.Tax + cod.Tax2) 
							WHEN 'NS' THEN (cod.Tax + cod.Tax2) 
							WHEN 'NL' THEN (cod.Tax + cod.Tax2) 
							ELSE 0
						END
			END AS TotalHST,	
			CASE adShip.StateProvince 
				WHEN 'NB' THEN (cod.Tax + cod.Tax2) 
				WHEN 'NS' THEN (cod.Tax + cod.Tax2) 
				WHEN 'NL' THEN (cod.Tax + cod.Tax2) 
				ELSE 0
			END AS TotalHSTTodate,
			ISNULL(hst.Tax_Rate, 0) AS HST_Rate,
			CASE p.IsQSPExclusive  
				WHEN 1 THEN 1 
				ELSE 0 
			END AS ExclusiveItemCount,
			CASE p.IsQSPExclusive 
				WHEN 1 THEN cod.Price 
				ELSE 0
			END AS ExclusiveItemAmount,
			st.FirstName,
			st.LastName,
			CASE
				WHEN codCurrent.CustomerOrderHeaderInstance IS NULL THEN 0
				ELSE (codCurrent.Price - (codCurrent.Tax + codCurrent.Tax2)) * cp.GroupProfit * .01
			END AS ProfitEarned,
			(cod.Price - (cod.Tax + cod.Tax2)) * cp.GroupProfit * .01 AS ProfitEarnedTodate,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN '6600, route Transcanadienne - bureau 750' 
				ELSE '695 Riddell Road' 	
			END AS QSPAddress1Label,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN 'Pointe-Claire, QC   H9R 4S2' 
				ELSE 'Orangeville, ON   L9W 4Z5 ' 
			END AS QSPAddress2Label,
			CASE ISNULL(camp.Lang, 'EN') 
				WHEN 'FR' THEN '1-866-342-3863' 
				ELSE '1-866-342-3863' 
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
				ON 	cont.ID = camp.BillToCampaignContactID
JOIN		QSPCanadaCommon..FieldManager fm (NOLOCK)
				ON	fm.FMID = camp.FMID
JOIN		CustomerOrderHeader coh (NOLOCK)
				ON	coh.OrderBatchID = b.[ID]
				AND	coh.OrderBatchDate = b.[DATE]
JOIN		CustomerOrderDetail cod (NOLOCK)
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
				AND cod.DelFlag = 0
JOIN		QSPCanadaCommon..CampaignProgram cp (NOLOCK)
				ON	cp.CampaignId = camp.ID
				AND	(cp.ProgramId IN (1,2,49,52) OR (isnull(cod.IsVoucherRedemption,0) = 1 AND cp.ProgramID = 50))
				AND	cp.DeletedTF = 0
LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
LEFT JOIN	CustomerOrderDetail codCurrent (NOLOCK)
				ON	codCurrent.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codCurrent.TransID = cod.TransID
				AND	codCurrent.CreationDate BETWEEN @DateFrom AND @DateTo
JOIN		QSPCanadaProduct..Pricing_Details pd (NOLOCK)
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p (NOLOCK)
				ON	p.Product_Instance = pd.Product_Instance
JOIN		Student st (NOLOCK)
				ON	st.Instance = coh.StudentInstance
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
WHERE		b.OrderQualifierID = 39009
AND			((codrh.Status IN (42000, 42001, 42004, 42010)
OR			codrh.Status IS NULL)
OR			cod.StatusInstance = 508)
AND			fm.FMID = ISNULL(@zFMID, fm.FMID)
ORDER BY	camp.ID
GO
