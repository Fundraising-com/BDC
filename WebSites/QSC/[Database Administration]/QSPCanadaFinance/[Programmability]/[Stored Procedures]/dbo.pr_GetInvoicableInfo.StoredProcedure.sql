USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetInvoicableInfo]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GetInvoicableInfo]

	@orderID int
 AS


SELECT 
	CCP.StatusInstance CreditCardStatus, CCP.ReasonCode, B.ID, B.Date, B.AccountID, B.CampaignID, OrderID, A.Name, 
	CD1.Description as BatchStatus, CD2.Description as OrderType,
 	COH.Instance, Transid,COD.ProductCode,COD.DelFlag,COD.ProductType,
 	CASE COD.ProductType
		WHEN 46001 Then (COD.ProductName) 	     --Mag
		WHEN 46002 Then (Product_Description_Alt) --Gift
		WHEN 46003 Then (Product_Description_Alt) --WFC
		WHEN 46005 Then (Product_Description_Alt) --Food
		WHEN 46006 Then (COD.ProductName) --Book
		WHEN 46007 Then (COD.ProductName) --Music
		WHEN 46010 Then (COD.ProductName) --MMB
		WHEN 46011 Then (COD.ProductName) --National
		WHEN 46012 Then (COD.ProductName) --Video
		ELSE (COD.ProductName)
	END as ProductName,
	COD.Quantity, 
	CASE COD.ProductType
		WHEN 46001 Then (COD.Price) 	                   --Mag
		WHEN 46002 Then (COD.Price/COD.Quantity) --Gift
		WHEN 46003 Then (COD.Price/COD.Quantity) --WFC
		WHEN 46005 Then (COD.Price/COD.Quantity) --Food
		WHEN 46006 Then (COD.Price/COD.Quantity) --Book
		WHEN 46007 Then (COD.Price/COD.Quantity) --Music
		WHEN 46010 Then (COD.Price) 		      --MMB
		WHEN 46011 Then (COD.Price/COD.Quantity) --National
		WHEN 46012 Then (COD.Price/COD.Quantity) --Video
		ELSE 0			
	END as Price, 
	CASE COD.ProductType
		WHEN 46001 Then (COD.Price) --Mag
		WHEN 46002 Then (COD.Price) --Gift
		WHEN 46003 Then (COD.Price) --WFC
		WHEN 46005 Then (COD.Price) --Food
		WHEN 46006 Then (COD.Price) --Book
		WHEN 46007 Then (COD.Price) --Music
		WHEN 46010 Then (COD.Price) --MMB
		WHEN 46011 Then (COD.Price) --National
		WHEN 46012 Then (COD.Price) --Video
		ELSE 0			
	END as TotalPrice,
	COD.Tax, COD.Tax2, --Tax = GST/HST, Tax2 = PST
	COD.Net, COD.Gross, -- Net is without taxes, Gross includes taxes
	CD3.Description as PaymentMethod, PS.Type as SectionType, PST.Description as SectionTypeDescription, 
	IsTaxIncluded, IsPriceWithTax, IsIncentive, DateBatchCompleted,OrderTypeCode, orderqualifierid
FROM
	QSPCanadaOrderManagement..Batch B
	 LEFT JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	 LEFT JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	 LEFT JOIN QSPCanadaOrderManagement..CustomerOrderDetail COD on COD.CustomerOrderHeaderInstance = COH.Instance 
			AND COD.DelFlag = 0 AND COD.ProgramSectionID <> 4 --NOT Incentive Item
	 LEFT JOIN QSPCanadaOrderManagement..Account A on A.ID = B.AccountID
	 LEFT JOIN QSPCanadaProduct..ProgramSection PS on PS.ID = COD.ProgramSectionID
	 LEFT JOIN QSPCanadaProduct..ProgramSectionType PST on PST.ID = PS.Type
	 LEFT JOIN QSPCanadaOrderManagement..CustomerPaymentHeader CPH on CPH.CustomerOrderHeaderInstance = COH.Instance 
	 LEFT JOIN QSPCanadaOrderManagement..CreditCardPayment CCP on CCP.CustomerPaymentHeaderInstance = CPH.Instance
	 LEFT JOIN QSPCanadaCommon..CodeDetail CD1 on CD1.Instance = B.StatusInstance
	 LEFT JOIN QSPCanadaCommon..CodeDetail CD2 on CD2.Instance = B.OrderTypeCode
	 LEFT JOIN QSPCanadaCommon..CodeDetail CD3 on CD3.Instance = COH.PaymentMethodInstance
	 LEFT JOIN QSPCanadaProduct..Pricing_Details Price on COD.PricingDetailsID = MagPrice_Instance --Price.Pricing_Year = @Year AND Pricing_Season = @Season
	 LEFT JOIN QSPCanadaProduct..ProductDescription Prod on  Prod.Product_Code = Price.OracleCode
					AND ISNULL(Prod.Language_Code,'EN') = ISNULL(C.Lang,'EN')
WHERE 
orderid=@orderID
GO
