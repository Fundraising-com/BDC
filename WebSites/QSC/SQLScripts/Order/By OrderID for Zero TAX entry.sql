USE QSPCanadaOrderManagement
GO



DECLARE
	@OrderID						INT,
	@OrderDateStr					VARCHAR(10),		-- SPCanadaOrderManagement..CustomerOrderDetail.CreationDate  
	@CatalogPrice					NUMERIC(10,2),		-- SPCanadaOrderManagement..CustomerOrderDetail.Price  
	@ProgramSectionID				INT,				-- SPCanadaOrderManagement..CustomerOrderDetail.ProgramSectionID
	@ProductCode					VARCHAR(10)  ,		-- QSPCanadaProduct..Product.Product_Code			 
	@CampaignID						INT,				-- QSPCanadaOrderManagement..batch.CampaignID			 
	@PricingDetailsID				INT,				-- QSPCanadaProduct..Pricing_Details.MagPrice_Instance  
	@Province						VARCHAR(2),			-- QSPCanadaCommon..TaxRegionProvince.Province	
			 
	@CustomerOrderHeaderInstance	INT,				-- QSPCanadaOrderManagement..customerorderheader.Instance
	@TransID						INT,				-- QSPCanadaOrderManagement..customerorderdetail.TransID
	
	@Tax 							NUMERIC(14,6),  
	@TaxA 							NUMERIC(14,6),  
	@Gross							NUMERIC(14,6),
	@Net							NUMERIC(14,6)
	
SET @OrderID = 9939095

DECLARE c cursor for 
	-- Retreive the values that will be used for the QSPCanadaCommon..pr_Calc_Order_Item_Amounts stored procedure
	SELECT		LEFT(CONVERT(VARCHAR, GetDate(), 120), 10) AS OrderDate,
				cod.Price,
				cod.ProgramSectionID,
				p.Product_Code,
				b.CampaignID,
				pd.MagPrice_Instance,
				trp.Province,
				coh.Instance, 
				cod.TransID
				-- ,cod.Tax 		
	FROM		QSPCanadaOrderManagement..batch b
	JOIN		QSPCanadaOrderManagement..customerorderheader	coh		ON coh.orderbatchid = b.id and coh.orderbatchdate = b.date
	JOIN		QSPCanadaOrderManagement..CustomerOrderDetail	cod		ON cod.customerorderheaderinstance = coh.instance
	JOIN		QSPCanadaOrderManagement..Customer				cust	ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
																							WHEN 0 THEN coh.CustomerBillToInstance
																							ELSE		cod.CustomerShipToInstance
																							END
	JOIN		QSPCanadaCommon..TaxRegionProvince				trp		ON cust.State = trp.Province  
	JOIN		QSPCanadaProduct..Pricing_Details				pd		ON cod.pricingdetailsid = pd.magprice_instance
	JOIN		QSPCanadaProduct..Product						p 		ON pd.product_instance = p.product_instance
	WHERE		b.orderID = @OrderID AND isnull(cod.Tax,0.00) = 0.00	

Open c
fetch NEXT from c into	@OrderDateStr, @CatalogPrice, @ProgramSectionID, @ProductCode, @CampaignID, 
						@PricingDetailsID, @Province, @CustomerOrderHeaderInstance, @TransID					

CREATE TABLE #Taxes
(
	Tax1	numeric(14,6),  
	Tax2 	numeric(14,6),  
	Net		numeric(14,6),
	Gross	numeric(14,6)
)

while @@fetch_status = 0
BEGIN

	INSERT INTO #Taxes
	EXEC QSPCanadaCommon..pr_Calc_Order_Item_Amounts
			@OrderDateStr, @CatalogPrice, @ProgramSectionID, 
			@ProductCode, 'N', @CampaignID, @PricingDetailsID, @Province

	SELECT	@Tax = Tax1,
			@TaxA = Tax2,
			@Net = Net,
			@Gross = Gross
	FROM	#Taxes
		
	UPDATE	QSPCanadaOrderManagement..CustomerOrderDetail
	SET		Tax = @Tax,
			TaxA = @Tax,
			Tax2 = @TaxA,
			Tax2A = @TaxA,
			Net = @Net,
			Gross = @Gross
	WHERE	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		TransID = @TransID


	--PRINT ' ' 
	--PRINT '@OrderID: ' + cast(@OrderID as varchar)
	--PRINT '@CustomerOrderHeaderInstance: ' + cast(@CustomerOrderHeaderInstance as varchar)
	--PRINT '@TransID: ' + cast(@TransID as varchar)
	--PRINT '@Tax: ' + cast(@Tax as varchar)
	--PRINT '@TaxA: ' + cast(@TaxA as varchar)
	--PRINT '@Net: ' + cast(@Net as varchar)
	--PRINT '@Gross: ' + cast(@Gross as varchar)
	--PRINT ' ' 
	

	delete #Taxes
	fetch NEXT from c into	@OrderDateStr, @CatalogPrice, @ProgramSectionID, @ProductCode, @CampaignID, 
							@PricingDetailsID, @Province, @CustomerOrderHeaderInstance, @TransID					
END

-- Free the resources
drop table #Taxes
CLOSE c
DEALLOCATE c


