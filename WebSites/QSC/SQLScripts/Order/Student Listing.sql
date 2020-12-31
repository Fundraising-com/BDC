USE [QSPCanadaOrderManagement]
GO

SELECT 
NULL as [GaoParticipantListingID]
,getdate() as [GaoParticipantListingCreateDate]
, ACC.ID AS [OpportunityID]
, ACC.Name as [OpportunityName]
, BillAdd.StateProvince as [OpportunityBillingState]
, BillAdd.City as [OpportunityBillingCity]
, [CampaignID] = CAMP.[ID]
, ACC.Name as [CampaignName]
, NULL as [MarketingProgramID]
, [MarketingProgramName] = NULL
, [ProgramID] = NULL
,[ProgramName] = NULL
, COD.CustomerOrderHeaderInstance
, COD.TransID
, [ParticipantID] = STUD.instance
, [ParticipantFirstName] = STUD.FirstName
, [ParticipantLastName] = STUD.LastName
, [ParticipantFullName] = isnull(STUD.LastName,'') + ', ' + isnull(STUD.FirstName,'')
   + 
   case 
	when cod.StatusInstance = 500 then ' - Unpaid Orders'
	when cod.StatusInstance IN (502,507,508,509) then ' - Paid Orders'
	when cod.StatusInstance = 519 then ' - Partially Paid Orders'
	when cod.StatusInstance = 512 then ' - Unremittable'
	when cod.StatusInstance = 513 then ' - Unshippable'
	when cod.StatusInstance = 501 then ' - Order Errors'	
   end 
, COH.StudentInstance
, cod.StatusInstance as orderDetailStatusID
, [OrderDetailStatusDescription] = cd.[Description]
, [Payment_Status] =
  case
	when cod.StatusInstance = 500 then 'Unpaid Orders'
	when cod.StatusInstance IN (502,507,508,509) then 'Paid Orders'
	when cod.StatusInstance = 519 then 'Partially Paid Orders'
	when cod.StatusInstance = 512 then 'Unremittable'
	when cod.StatusInstance = 513 then 'Unshippable'
	when cod.StatusInstance = 501 then 'Order Errors'
  end
, BATCH.OrderID as BatchID
, COD.ProductType
, [ProductTypeName] = CASE COD.ProductType
	WHEN 46001 Then 'Magazine'
	WHEN 46002 Then 'Gift'
	WHEN 46003 Then 'WFC'
	WHEN 46005 Then 'Food'
	WHEN 46006 Then 'Book'
	WHEN 46007 Then 'Music'
	WHEN 46010 Then 'Magazine'
	WHEN 46011 Then 'National'
	WHEN 46012 Then 'Video'
	ELSE ''
  END
, COD.ProductCode
, COD.ProductName
, [RecipientName] = Recipient
, [ItemPriceTotal] = COD.Price
, [QTY] = CASE COD.ProductType
	WHEN 46001 Then 1		--Mag
	WHEN 46002 Then COD.Quantity --Gift
	WHEN 46003 Then COD.Quantity --WFC
	WHEN 46005 Then COD.Quantity --Food
	WHEN 46006 Then COD.Quantity --Book 
	WHEN 46007 Then COD.Quantity --Music
	WHEN 46010 Then COD.Quantity --MMB
	WHEN 46011 Then COD.Quantity --National
	WHEN 46012 Then COD.Quantity --Video
	WHEN 46016 Then COD.Quantity --Combo
	WHEN 46022 Then COD.Quantity --RestoCards
	ELSE COD.Quantity
  END
, [TeacherName] = ISNULL(TEACH.LastName,'Unknown')
, TEACH.[Classroom]
, [dbo].[UDF_GetPrizeLevel](coh.StudentInstance, BATCH.Orderid) as [Incentive]
, [dbo].[UDF_GetInternetOrderItems](BATCH.Orderid,coh.StudentInstance) as [TotalInternetItems]
, CUST.[LastName] as CustomerLastName
, CUST.[FirstName] as CustomerFirstName
, CUST.[Address1] as CustomerAddress1
, CUST.[Address2] as CustomerAddress2
, CUST.[City] as CustomerCity
, CUST.[County] as CustomerCounty
, CUST.[State] as CustomerState
, CUST.[Zip] as CustomerZip
, CUST.[ZipPlusFour] as CustomerZipPlusFour
, [CampaignProgramID] = NULL
, PROD.[Nbr_Of_Issues_Per_Year] as [NumIssuesPerYear]
, CAMP.fmid
, FM.FirstName as [SalesPersonFirstName]
, FM.LastName as [SalesPersonLastName]
,getdate()
FROM 

[dbo].[Batch] as BATCH with(NOLOCK)

INNER JOIN [QSPCanadaCommon].[dbo].[Campaign] as CAMP with(NOLOCK)
	ON BATCH.[CampaignID] = CAMP.[ID]
	
INNER JOIN [QSPCanadaCommon].[dbo].[CAccount] as ACC with (NOLOCK)
	ON ACC.ID = CAMP.BillToAccountID

LEFT JOIN [QSPCanadaCommon].[dbo].[Address] as BillAdd with (NOLOCK)
	ON BillAdd.AddressListID = ACC.AddressListID
	AND BillAdd.[Address_Type] = 54002 --Bill To

INNER JOIN [dbo].[CustomerOrderHeader] as COH with(NOLOCK)
	ON BATCH.[ID] = COH.[OrderBatchID]
	AND BATCH.[DATE] = COH.[OrderBatchDate]

INNER JOIN [dbo].[CustomerOrderDetail] as COD with(NOLOCK)
	ON COH.[Instance] = COD.[CustomerOrderHeaderInstance]

LEFT JOIN [QSPCanadaProduct].[dbo].[Pricing_Details] as PD with(nolock)
	ON COD.[PricingDetailsID] = PD.[MagPrice_Instance]

INNER JOIN [dbo].[Customer] as CUST with(NOLOCK)
	ON COH.CustomerBillToInstance=CUST.Instance
	
LEFT JOIN [QSPCanadaProduct].[dbo].[Product] as PROD with(NOLOCK)
	ON  pd.Product_Instance = PROD.Product_Instance
	
INNER JOIN [dbo].[Student] as STUD with(NOLOCK)
	ON COH.[StudentInstance] = STUD.[Instance]

LEFT JOIN [dbo].[Teacher] as TEACH with(NOLOCK)
	ON STUD.[TeacherInstance] = TEACH.[Instance]

Inner JOIN [QSPCanadaCommon].[dbo].CodeDetail CD 
     ON CD.Instance = cod.StatusInstance
          
LEFT JOIN [QSPCanadaCommon].[dbo].[FieldManager] FM
     ON FM.FMID = CAMP.FMID

WHERE 
	cod.StatusInstance in 
	(
		 500  --Good
		,501  --Error
		,502  --Paid
		,507  --sent to remit
		,508  --shipped
		,509  --pending to tpl
		,512  --unremittable
		,513  --Unshippable
		,519  --partrially paid
	)
	AND cod.ProductType 
	in (
	 46001 --Magazine
	,46002 --Gift
	,46003 --WFC
	,46005 --Food
	,46006 --Books
	,46007 --Music
	,46010 --MMB
	,46011 --National
	,46012 --Video
	)

and CAMP.StartDate BETWEEN '2011-07-01' AND '2011-12-31'

and CAMP.ID IN (SELECT	coh.CampaignID 
				FROM	CustomerOrderHeader coh
				JOIN	CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = coh.Instance
				WHERE	cod.StatusInstance in 
						(
							 500  --Good
							,501  --Error
							,502  --Paid
							,507  --sent to remit
							,508  --shipped
							,509  --pending to tpl
							,512  --unremittable
							,513  --Unshippable
							,519  --partrially paid
						)
				AND		cod.ProductType in 
						(
							 46001 --Magazine
							,46002 --Gift
							,46003 --WFC
							,46005 --Food
							,46006 --Books
							,46007 --Music
							,46010 --MMB
							,46011 --National
							,46012 --Video
						)
				GROUP BY coh.CampaignID
				HAVING	SUM(cod.Price) >= 10000)

ORDER BY Classroom , TeacherName, ParticipantFullName, RecipientName , ProductTypeName, ProductName




