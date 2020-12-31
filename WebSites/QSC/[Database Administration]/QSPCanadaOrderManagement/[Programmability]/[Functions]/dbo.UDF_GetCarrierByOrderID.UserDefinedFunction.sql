USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetCarrierByOrderID]    Script Date: 06/07/2017 09:21:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_GetCarrierByOrderID]

(@OrderID INT)

RETURNS INT

AS  

BEGIN 

	DECLARE @LamontagneUnits	INT,
			@UTIUnits			INT,
			@CDUnits			INT,
			@CarrierID			INT,
			@Province			VARCHAR(10)

	SELECT	@LamontagneUnits = SUM(cod.Quantity)
	FROM	Batch b
	JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchDate = b.Date
				AND	coh.OrderBatchID = b.ID
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
	WHERE	b.OrderID = @OrderID
	AND		cod.DelFlag = 0
	AND		cod.DistributionCenterID IN (3)

	SELECT	@UTIUnits = SUM(cod.Quantity)
	FROM	Batch b
	JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchDate = b.Date
				AND	coh.OrderBatchID = b.ID
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
	WHERE	b.OrderID = @OrderID
	AND		cod.DelFlag = 0
	AND		cod.DistributionCenterID IN (2)

	SELECT	@CDUnits = SUM(cod.Quantity)
	FROM	Batch b
	JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchDate = b.Date
				AND	coh.OrderBatchID = b.ID
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
	WHERE	b.OrderID = @OrderID
	AND		cod.DelFlag = 0
	AND		cod.ProductType = 46018
	
	SELECT		@Province = prov.PROVINCE_CODE
	FROM		Batch b
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		Customer custCoh
					ON	custCoh.Instance = coh.CustomerBillToInstance
	JOIN		Customer custCod
					ON	custCod.Instance = cod.CustomerShipToInstance
	JOIN		QSPCanadaCommon..Province prov
					ON	prov.Province_Code =	CASE coh.CustomerBillToInstance
													WHEN 0 THEN custCod.State
													ELSE custCoh.State
												END
	WHERE		b.OrderID = @OrderID
	AND			cod.Delflag = 1
	AND			cod.DistributionCenterID = 2		

	IF (@LamontagneUnits > 0)
		SET @CarrierID = 53010 --Lamontagne
	ELSE IF (@UTIUnits >= 12)
		SET @CarrierID = 53009 --20/20
	ELSE IF (@CDUnits > 0)
		SET @CarrierID = 53010 --Purolator
	ELSE IF (@UTIUnits > 0 AND @Province IN ('MB','SK'))
		SET @CarrierID = 53010 --Purolator
	ELSE IF (@UTIUnits > 0 AND @Province NOT IN ('MB','SK'))
		SET @CarrierID = 53001 --DHL	
	ELSE
		SET @CarrierID = 53007 --Other Carrier
	
	RETURN @CarrierID
END
GO
