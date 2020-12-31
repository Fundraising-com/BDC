SELECT 
	B.Date,
	B.AccountID, 
	B.CampaignID, 
	OrderID,
	A.Name,  
	CD1.Description as BatchStatus, 
 	COH.Instance, 
	COD.TransId,
	COD.InvoiceNumber,
	s.Firstname ParticipantFirstname,
	s.Lastname ParticipantLastname,
	COD.ProductCode, 
	COD.ProductType,
 	COD.ProductName,
	COD.Quantity, 
	CASE COD.ProductType
		WHEN 46001 Then (COD.Price) 	                 	 --Mag
		WHEN 46002 Then (COD.Price/COD.Quantity) 	--Gift
		WHEN 46003 Then (COD.Price/COD.Quantity) 	--WFC
		WHEN 46004 Then (COD.Price/COD.Quantity)	--Kanata (Bulk) Items may Include incentive MS Oct 11, 2005
		WHEN 46005 Then (COD.Price/COD.Quantity)	--Food
		WHEN 46006 Then (COD.Price/COD.Quantity) 	--Book
		WHEN 46007 Then (COD.Price/COD.Quantity) 	--Music
		WHEN 46008 Then (COD.Price/COD.Quantity) 	--Kanata Prizes	
		WHEN 46010 Then (COD.Price) 		      	--MMB
		WHEN 46011 Then (COD.Price/COD.Quantity) 	--National
		WHEN 46012 Then (COD.Price/COD.Quantity) 	--Video
		WHEN 46013 Then (COD.Price/COD.Quantity) 	--Incentive for Kanata 	MS Feb28,2007
		WHEN 46014 Then (COD.Price/COD.Quantity) 	--Incentive for Kanata
		WHEN 46015 Then (COD.Price/COD.Quantity) 	--Incentive for Kanata
		WHEN 46017 Then (COD.Price)					--Processing fees, independant of quantity
		WHEN 46018 Then (COD.Price/COD.Quantity)	--Cookie Dough
		WHEN 46019 Then (COD.Price/COD.Quantity)	--Chocolate
		WHEN 46020 Then (COD.Price/COD.Quantity)	--Jewelry
		WHEN 46021 Then (COD.Price/COD.Quantity)	--Shipping
		WHEN 46022 Then (COD.Price/COD.Quantity) 	--Candle
		WHEN 46023 Then (COD.Price/COD.Quantity) 	--TRT
		WHEN 46024 Then (COD.Price/COD.Quantity) 	--Entertainment
		ELSE 0			
	END as Price, 
	CASE COD.ProductType
		WHEN 46001 Then (COD.Price) --Mag
		WHEN 46002 Then (COD.Price) --Gift
		WHEN 46003 Then (COD.Price) --WFC
		WHEN 46004 Then (COD.Price) -- Kanata Bulk Items MS Oct 11, 2005
		WHEN 46005 Then (COD.Price) --Food
		WHEN 46006 Then (COD.Price) --Book
		WHEN 46007 Then (COD.Price) --Music
		WHEN 46008 Then (COD.Price) 	--Kanata Prizes	
		WHEN 46010 Then (COD.Price) --MMB
		WHEN 46011 Then (COD.Price) --National
		WHEN 46012 Then (COD.Price) --Video
		WHEN 46013 Then (COD.Price) 	--Incentive for Kanata  MS Feb28,2007
		WHEN 46014 Then (COD.Price) 	--Incentive for Kanata
		WHEN 46015 Then (COD.Price) 	--Incentive for Kanata
		WHEN 46017 Then (COD.Price)		--Processing fees
		WHEN 46018 Then (COD.Price)		--Cookie Dough
		WHEN 46019 Then (COD.Price)		--Chocolate
		WHEN 46020 Then (COD.Price)		--Jewelry
		WHEN 46021 Then (COD.Price)		--Shipping
		WHEN 46022 Then (COD.Price)		--Candle
		WHEN 46023 Then (COD.Price)		--TRT
		WHEN 46024 Then (COD.Price)		--Entertainment
		ELSE 0			
	END as TotalPrice,
	COD.Tax, COD.Tax2, 	--Tax = GST/HST, Tax2 = PST
	CASE COD.ProductType
		WHEN 46017 THEN 8 --Proc Fee switch from 2 to 8 so GP is not included
		ELSE			PS.Type
	END AS SectionType,
	CASE COD.ProductType
		WHEN 46017 THEN 'Processing fees' --Proc Fee switch from 2 to 8 so GP is not included
		ELSE			PST.Description
	END AS SectionTypeDescription,
	isnull(Price.PostageAmount,0) *  isnull(Price.PostageRemitRate,0) * isnull(Price.ConversionRate,0)  as PostageAmount,
	b.OrderQualifierID,
	PM.SubType ProgramType,
	(CASE WHEN PS.Type = 1 THEN CASE WHEN A.CAccountCodeClass = 'FM' THEN 0.00
														WHEN OrderQualifierID = 39022 THEN 0.00
														WHEN PM.SubType IN (30327) THEN 0.75
														WHEN PM.SubType IN (30323, 30329) THEN 0.45
														ELSE 0.40
													END
					  WHEN PS.Type = 2 THEN CASE c.IsStaffOrder WHEN 1 THEN 0.00 ELSE 0.37 END
					  WHEN PS.Type = 9 THEN 0.40 --Todo
					  WHEN PS.Type = 11 THEN CASE WHEN OrderQualifierID = 39022 THEN 0.00 
														WHEN A.CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.40
													END
					  WHEN PS.Type = 14 THEN CASE WHEN OrderQualifierID = 39022 THEN 0.00 
														WHEN A.CAccountCodeClass = 'FM' THEN 0.00
														WHEN TRTGenerationCode IN ('2') THEN 0.20
														WHEN TRTGenerationCode IN ('0', 'N') THEN 0.00
														ELSE 0.37
													END
					  WHEN PS.Type = 15 THEN CASE WHEN OrderQualifierID = 39022 THEN 0.00 
														WHEN A.CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.37
													END
					  ELSE 0
			END) GPRate,
			(CASE WHEN PS.Type = 1 THEN CASE WHEN A.CAccountCodeClass = 'FM' THEN 0.00
														WHEN OrderQualifierID = 39022 THEN 0.00
														WHEN PM.SubType IN (30327) THEN 0.75
														WHEN PM.SubType IN (30323, 30329) THEN 0.45
														ELSE 0.40
													END
					  WHEN PS.Type = 2 THEN CASE c.IsStaffOrder WHEN 1 THEN 0.00 ELSE 0.37 END
					  WHEN PS.Type = 9 THEN 0.40 --Todo
					  WHEN PS.Type = 11 THEN CASE WHEN OrderQualifierID = 39022 THEN 0.00 
														WHEN A.CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.40
													END
					  WHEN PS.Type = 14 THEN CASE WHEN OrderQualifierID = 39022 THEN 0.00 
														WHEN A.CAccountCodeClass = 'FM' THEN 0.00
														WHEN TRTGenerationCode IN ('2') THEN 0.20
														WHEN TRTGenerationCode IN ('0', 'N') THEN 0.00
														ELSE 0.37
													END
					  WHEN PS.Type = 15 THEN CASE WHEN OrderQualifierID = 39022 THEN 0.00 
														WHEN A.CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.37
													END
					  ELSE 0
			END) * (COD.Price - (isnull(Price.PostageAmount,0) *  isnull(Price.PostageRemitRate,0) * isnull(Price.ConversionRate,0)) - CASE WHEN PS.Type IN (1, 3, 7, 10, 11) THEN 0.00 ELSE (tax+Tax2) END) GroupProfitAmount

FROM	QSPCanadaOrderManagement.dbo.Batch B LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.Campaign C ON C.ID = B.CampaignID LEFT OUTER JOIN
      	QSPCanadaOrderManagement.dbo.CustomerOrderHeader COH ON COH.OrderBatchDate = B.[Date] AND COH.OrderBatchID = B.ID LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.CustomerOrderDetail COD ON COD.CustomerOrderHeaderInstance = COH.Instance AND COD.ProgramSectionID <> 4 LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CAccount A ON A.Id = B.AccountID LEFT OUTER JOIN
      	QSPCanadaProduct.dbo.ProgramSection PS ON PS.ID = COD.ProgramSectionID LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.ProgramSectionType PST ON PST.ID = PS.Type LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.PROGRAM_MASTER PM ON PM.Program_ID = PS.Program_ID LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.CustomerPaymentHeader CPH ON CPH.CustomerOrderHeaderInstance = COH.Instance LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.CreditCardPayment CCP ON CCP.CustomerPaymentHeaderInstance = CPH.Instance LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CodeDetail CD1 ON CD1.Instance = B.StatusInstance LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CodeDetail CD2 ON CD2.Instance = B.OrderTypeCode LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.PRICING_DETAILS Price ON COD.PricingDetailsID = Price.MagPrice_Instance LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
join	QSPCanadaOrderManagement..Student s on s.instance = coh.StudentInstance
WHERE		c.id = 101803
AND			PS.Type IN (1,2,6,9,10,11,14,15)
AND			cod.ProductType NOT IN (46017)
AND			cod.IsVoucherRedemption = 0
order by	s.FirstName, s.LastName, cod.ProductType, ProgramType