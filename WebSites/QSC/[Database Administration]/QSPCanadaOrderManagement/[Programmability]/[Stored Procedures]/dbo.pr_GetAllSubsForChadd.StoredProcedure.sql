USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetAllSubsForChadd]    Script Date: 06/07/2017 09:20:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GetAllSubsForChadd]

@iCustomerOrderHeaderInstance	int = 0,
@iTransID			int,
@iShowCancelled		int = 0,
@iShowCurrentSub		int = 1

 AS
DECLARE	@iCustomerInstance 	int,
		@sqlStatement		nvarchar(4000)

--Get the customerinstance

SELECT	@iCustomerInstance = CustomerShipToInstance
FROM		CustomerOrderDetail
WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		TransID = @iTransID

IF(COALESCE(@iCustomerInstance, 0) = 0)
BEGIN
	SELECT	@iCustomerInstance = CustomerBillToInstance
	FROM		CustomerOrderHeader
	WHERE	Instance = @iCustomerOrderHeaderInstance
END

SET	@sqlStatement =
'SELECT	coh.Instance AS CustomerOrderHeaderInstance,
		cod.TransID,
		p.Product_Code AS ProductCode,
		p.Product_Sort_Name AS ProductName,
		cod.CreationDate AS SubscriptionDate,
		convert(numeric(10,2),  CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) AS Price, 
		--CONVERT(numeric(10, 2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) AS Price,
		c.FirstName AS RecipientFirstName,
		c.LastName AS RecipientLastName
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
			AND cod.DelFlag = 0
JOIN		Customer c
			ON	c.Instance = cod.CustomerShipToInstance
			AND	c.Instance = ' + CONVERT(nvarchar, @iCustomerInstance) + '
JOIN		QSPCanadaCommon..Campaign ca
			ON	ca.ID = coh.CampaignID
JOIN		QSPCanadaProduct..Pricing_Details pd
			ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p 
			ON	p.Product_Instance = pd.Product_Instance '

IF(@iShowCancelled = 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + '
LEFT JOIN	CustomerOrderDetailRemitHistory codrh
			ON	codrh.CustomerOrderHeaderInstance = coh.Instance
			AND	codrh.TransID = cod.TransID
			AND	codrh.CustomerRemitHistoryInstance =
			(SELECT	TOP 1
					codrh2.CustomerRemitHistoryInstance
			FROM		CustomerOrderDetailRemitHistory codrh2
			WHERE	codrh2.CustomerOrderHeaderInstance = coh.Instance
			AND		codrh2.TransID = cod.TransID)
WHERE	 codrh.Status NOT IN (42002, 42003, 42004) '
END

IF(@iShowCurrentSub = 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND (coh.Instance <> ' + CONVERT(nvarchar, @iCustomerOrderHeaderInstance) + ' OR cod.TransID <> ' + CONVERT(nvarchar, @iTransID) + ') '
END

SET	@sqlStatement = @sqlStatement + '
UNION
SELECT	coh.Instance AS CustomerOrderHeaderInstance,
		cod.TransID,
		p.Product_Code AS ProductCode,
		p.Product_Sort_Name AS ProductName,
		cod.CreationDate AS SubscriptionDate,
		convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) AS Price, 
		--CONVERT(numeric(10, 2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) AS Price,
		left(coalesce(cod.Recipient,''''), charindex('' '', coalesce(cod.Recipient,''''),charindex('' '', coalesce(cod.Recipient,''''),1))) as RecipientFirstName,
		ltrim(right(coalesce(cod.Recipient,''''), len(replace(coalesce(cod.Recipient,''''), '' '', ''_'')) - coalesce(charindex('' '', coalesce(cod.Recipient,''''),1), 0))) as RecipientLastName
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
			AND	coh.CustomerBillToInstance = ' + CONVERT(nvarchar, @iCustomerInstance) + '
			AND cod.CustomerShipToInstance = 0
			AND cod.DelFlag = 0
JOIN		QSPCanadaCommon..Campaign ca
			ON	ca.ID = coh.CampaignID
JOIN		QSPCanadaProduct..Pricing_Details pd
			ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
			ON	p.Product_Instance = pd.Product_Instance '

IF(@iShowCancelled = 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + '
LEFT JOIN	CustomerOrderDetailRemitHistory codrh
			ON	codrh.CustomerOrderHeaderInstance = coh.Instance
			AND	codrh.TransID = cod.TransID
			AND	codrh.CustomerRemitHistoryInstance =
			(SELECT	TOP 1
					codrh2.CustomerRemitHistoryInstance
			FROM		CustomerOrderDetailRemitHistory codrh2
			WHERE	codrh2.CustomerOrderHeaderInstance = coh.Instance
			AND		codrh2.TransID = cod.TransID)
WHERE	 codrh.Status NOT IN (42002, 42003, 42004) '
END

IF(@iShowCurrentSub = 0)
BEGIN
	SET	@sqlStatement = @sqlStatement + ' AND (coh.Instance <> ' + CONVERT(nvarchar, @iCustomerOrderHeaderInstance) + ' OR cod.TransID <> ' + CONVERT(nvarchar, @iTransID) + ') '
END

print @sqlStatement	
EXEC(@sqlStatement)
GO
