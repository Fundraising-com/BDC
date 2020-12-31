USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetParticipantListingReport]    Script Date: 06/07/2017 09:19:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetParticipantListingReport] --  0, 803094
	@ReportRequestID int, @OrderID	int
AS

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 9/8/2004 
--   modified by Saqib - April - 2006 
--   Throws data to Participant Listing Report
--   Modified for unknown product code/Type, Removed Insert into  MS April 28 07.
--   Modified for Incetive Level Sept MS 26, 2007
--   Increased RecipientName Column width to fix error (order-9598445) MS Aug 1,08 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--DECLARE @ReportRequestID int
--DECLARE	@OrderID	int

--SET @ReportRequestID = 0
--SET @OrderID		 = 803094


CREATE TABLE  #Data(
		COHInstance 	Int,
		TransId		Int,
		StudentInstance	Int,
		ParticipentName Varchar(110),
		ProductType	Int,
		ProductCode	Varchar(10),
		ProductName	Varchar(50),
		ProductName_FR	Varchar(50),
		CustomerBilltoInstance Int,
		RecipientName	Varchar(100),
		CCHolderName	Varchar(50),
		CCHolderPhone	Varchar(20),
		RecipientAddress1 Varchar(50),
		RecipientAddress2 Varchar(50),
		RecipientCity 	  Varchar(50),
		RecipientState    Varchar(30),
		RecipientZip	  Varchar(15),
		RecipientPhone    Varchar(20),
		ItemPriceTotal	  Numeric(10,2),
		QTY		  Int,
		TeacherInstance   Int,
		TeacherName	  Varchar(50),
		ClassRoom	  Varchar(50),
		Lang		  Varchar(10),
		ProductTypeName	  Varchar(50),
		Incentive	  Varchar(5),
		TotalInternetItems Int,
		CustomerOEFlag	   Varchar(15),
		QSPPremiumID	   Int,
		Product_Instance   Int,
		Sale_Type	   Varchar(30),
		People_12Issues   Int,
		People_22Issues   Int,
		People_53Issues   Int,
		IsShippedToAccount Bit,
		OrderQualifierID Int
		)

CREATE TABLE #Premiums (
		StudentInstance Int,
		Pub_Nbr Int,
		PremiumCount Int)


INSERT  #Data
SELECT coh.Instance COHInstance,cod.TransId,
	coh.StudentInstance,isnull(S.LastName,'') + ', ' + isnull(S.FirstName,'') as ParticipentName, 
	COD.ProductType,
	ISNULL(ProductCode,' ') ProductCode, 	
	case c.lang when 'EN' then ProductName else isNull(pdesc.product_description_Alt,ProductName) END as ProductName,
	isNull(pdesc.product_description_Alt,ProductName) as ProductName_FR, 	
	coh.CustomerBillToInstance,
--	Recipient as RecipientName, 
	CASE WHEN cod.CustomerShipToInstance = 0 OR cod.CustomerShipToInstance = coh.CustomerBillToInstance THEN cod.recipient ELSE isnull(customer.FirstName,'')+' '+isnull(customer.LastName,'') END as RecipientName,
	isnull(customerCC.FirstName,'')+' '+isnull(customerCC.LastName,'')  as CCHolderName,
	customerCC.Phone as ccholderphone,
	customer.Address1 as RecipientAddress1,
	customer.Address2 as RecipientAddress2,
	customer.City as RecipientCity,
	customer.State as RecipientState,
	customer.Zip as RecipientZip,
	customer.Phone as RecipientPhone,
	COD.Price as ItemPriceTotal,
	CASE COD.ProductType
		WHEN 46001 Then (1) 	   --Mag
		Else COD.Quantity
	END AS 'QTY',
	T.Instance TeacherInstance,
	IsNull(Upper(RTrim(LTrim(dbo.[UDF_RemoveTitle](t.LastName)))), 'UNKNOWN') as TeacherName,
	Classroom,
	c.Lang, 	cast(null as varchar) as ProductTypeName,
--	QSPCanadaOrderManagement.dbo.UDF_GetPrizeLevel(coh.StudentInstance, b.Orderid) as Incentive,
	cast(null as varchar) as Incentive,
	0 as TotalInternetItems, --QSPCanadaOrderManagement.dbo.UDF_GetInternetOrderItems(b.Orderid,coh.StudentInstance) as TotalInternetItems,
	cast(null as varchar) as CustomerOEFlag, --QSPCanadaOrderManagement.dbo.UDF_GetOEFlags(b.Orderid,coh.StudentInstance,coh.CustomerBilltoInstance) as CustomerOEFlag,
	pd.QspPremiumID,
	pd.Product_Instance,
    'LANDED' AS Sale_Type,
	dbo.UDF_GetPeopleNbrOfIssuesByStudentID(@OrderID, 12, coh.StudentInstance) AS People_12Issues,	
	dbo.UDF_GetPeopleNbrOfIssuesByStudentID(@OrderID, 22, coh.StudentInstance) AS People_22Issues,	
	dbo.UDF_GetPeopleNbrOfIssuesByStudentID(@OrderID, 53, coh.StudentInstance) AS People_53Issues,
	cod.IsShippedToAccount,
	b.OrderQualifierID
    
FROM		QSPCanadaOrderManagement..CustomerOrderDetail	COD  (NOLOCK)
INNER JOIN	QSPCanadaOrderManagement..CustomerOrderHeader	COH  ON COH.Instance = COD.CustomerOrderHeaderInstance
INNER JOIN	QSPCanadaOrderManagement..Student				S (NOLOCK) ON S.Instance = COH.StudentInstance
INNER JOIN	QSPCanadaOrderManagement..Batch					B (NOLOCK) ON COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
INNER JOIN	QSPCanadaCommon..Campaign						C (NOLOCK) ON C.ID = B.CampaignID
--'NNNN' Issue MS Sept 12
LEFT JOIN QSPCanadaProduct.dbo.Pricing_Details pd (NOLOCK) ON pd.MagPrice_Instance = cod.PricingDetailsID 
LEFT JOIN QSPCanadaProduct.dbo.Product product (NOLOCK) ON 	 Product.Product_Instance  = pd.Product_Instance 
	--INNER JOIN QSPCanadaProduct.dbo.Pricing_Details pd (NOLOCK) ON pd.MagPrice_Instance = cod.PricingDetailsID 
	--INNER JOIN QSPCanadaProduct.dbo.Product product (NOLOCK) ON 	 Product.Product_Instance  = pd.Product_Instance 
INNER JOIN QSPCanadaOrderManagement.dbo.Customer customer (NOLOCK) ON   customer.instance = CASE cod.CustomerShipToInstance WHEN 0 THEN coh.customerbilltoinstance ELSE cod.CustomerShipToInstance END
INNER JOIN QSPCanadaOrderManagement.dbo.Customer customerCC (NOLOCK) ON   customerCC.instance =  coh.customerbilltoinstance 
LEFT JOIN QspCanadaproduct.dbo.ProductDescription pdesc (NOLOCK) ON pdesc.Product_Code  = product.OracleCode
							  and pdesc.language_code = 'FR'
LEFT JOIN QSPCanadaOrderManagement..Teacher T (NOLOCK) ON T.Instance =  S.TeacherInstance

WHERE  B.ORDERID = @OrderID
	 and COD.StatusInstance <>  506 -- Voided Due To Error 
	and cod.delflag <> 1
	and  ( cod.ProductType in (46001,  46002, 46003, 46005, 46006, 46007,46010, 46011,  46012, 46018, 46019, 46020, 46023, 46024)  or cod.ProductType =0)

UNION ALL 

-- now add in qsp.ca items 

SELECT coh.Instance COHInstance,cod.TransId,
	coh.StudentInstance,isnull(S.LastName,'') + ', ' + isnull(S.FirstName,'') as ParticipentName, 
	COD.ProductType,
	ProductCode, 	
	ProductName,
	isNull(pdesc.product_description_Alt,ProductName) as ProductName_FR, 	
	coh.CustomerBillToInstance,
--	Recipient as RecipientName, 
	CASE cod.CustomerShipToInstance WHEN 0 THEN cod.recipient ELSE isnull(customer.FirstName,'')+' '+isnull(customer.LastName,'') END as RecipientName,
	isnull(customerCC.FirstName,'')+' '+isnull(customerCC.LastName,'')  as CCHolderName,
	customerCC.Phone as ccholderphone,
	customer.Address1 as RecipientAddress1,
	customer.Address2 as RecipientAddress2,
	customer.City as RecipientCity,
	customer.State as RecipientState,
	customer.Zip as RecipientZip,
	customer.Phone as RecipientPhone,
	COD.Price as ItemPriceTotal,
	CASE COD.ProductType
		WHEN 46001 Then (1) 	   --Mag
		Else COD.Quantity
	END AS 'QTY',
	T.Instance TeacherInstance,
	IsNull(dbo.[UDF_RemoveTitle](T.LastName),'Unknown') as TeacherName,
	Classroom,
	c.Lang, 	cast(null as varchar) as ProductTypeName,
--	QSPCanadaOrderManagement.dbo.UDF_GetPrizeLevel(coh.StudentInstance,@OrderID) as Incentive,
	cast(null as varchar) as Incentive,
	--QSPCanadaOrderManagement.dbo.UDF_GetInternetOrderItems(@OrderID,coh.StudentInstance) as TotalInternetItems,
	0 as TotalInternetItems,
	null  as CustomerOEFlag,
	pd.QspPremiumID  as QspPremiumID,		--> added Aug 31
	pd.Product_instance as Product_Instance,	--> aug 31
    'ONLINE' AS Sale_Type,
	dbo.UDF_GetPeopleNbrOfIssuesByStudentID(@OrderID, 12, coh.StudentInstance) AS People_12Issues,	
	dbo.UDF_GetPeopleNbrOfIssuesByStudentID(@OrderID, 22, coh.StudentInstance) AS People_22Issues,	
	dbo.UDF_GetPeopleNbrOfIssuesByStudentID(@OrderID, 53, coh.StudentInstance) AS People_53Issues,
	cod.IsShippedToAccount,
	b.OrderQualifierID

FROM QSPCanadaOrderManagement..CustomerOrderDetail COD (NOLOCK)
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH (NOLOCK) ON COH.Instance = COD.CustomerOrderHeaderInstance
	INNER JOIN QSPCanadaOrderManagement..Student S (NOLOCK) ON S.Instance = COH.StudentInstance
	INNER JOIN QSPCanadaOrderManagement..Batch B (NOLOCK) ON COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	INNER JOIN QSPCanadaCommon..Campaign C (NOLOCK) ON C.ID = B.CampaignID
--'NNNN' Issue MS Sept 12
	LEFT JOIN QSPCanadaProduct.dbo.Pricing_Details pd  (NOLOCK) ON pd.MagPrice_Instance = cod.PricingDetailsID 
	LEFT JOIN QSPCanadaProduct.dbo.Product product (NOLOCK) ON  Product.Product_Instance  = pd.Product_Instance 
	--INNER JOIN QSPCanadaProduct.dbo.Pricing_Details pd  (NOLOCK) ON pd.MagPrice_Instance = cod.PricingDetailsID 
	--INNER JOIN QSPCanadaProduct.dbo.Product product (NOLOCK) ON 	 Product.Product_Instance  = pd.Product_Instance 
	INNER JOIN QSPCanadaOrderManagement.dbo.Customer customer (NOLOCK) ON   customer.instance = CASE cod.CustomerShipToInstance WHEN 0 THEN coh.customerbilltoinstance ELSE cod.CustomerShipToInstance END
	INNER JOIN QSPCanadaOrderManagement.dbo.Customer customerCC (NOLOCK) ON   customerCC.instance =  coh.customerbilltoinstance 
	LEFT JOIN QspCanadaproduct.dbo.ProductDescription pdesc (NOLOCK) ON pdesc.Product_Code  = product.OracleCode
								  and pdesc.language_code = 'FR'
	LEFT JOIN QSPCanadaOrderManagement..Teacher T (NOLOCK) ON T.Instance =  S.TeacherInstance

WHERE  B.ORDERID in ( Select onlineOrderID 
			From   QSPCanadaOrderManagement.dbo.OnlineOrderMappingTable (NOLOCK)
			Where  LandedOrderId  = @OrderID) 
	 and COD.StatusInstance <>  506 -- Voided Due To Error 
	and cod.delflag <> 1
	and  ( cod.ProductType in (46001,  46002, 46003, 46005, 46006, 46007,46010, 46011,  46012, 46018, 46019, 46020, 46023, 46024)   or cod.ProductType =0)

/*
UNION ALL-- now add up the prizes info of those students who doesnt have regular ground sale but they sold only online

SELECT student.Instance as StudentInstance,
	isnull(student.LastName,'Unknown') + ', ' + isnull(student.FirstName,'Unknown') as ParticipentName, 
	null as ProductType,
	null as ProductCode, 	
	null as ProductName, 	
	null as ProductName_FR,
	null as CustomerBillToInstance,
	null as RecipientName, 
	null as CCHolderName,
	null as ccholderphone,
	null as RecipientAddress1,
	null as RecipientAddress2,
	null as RecipientCity,
	null as RecipientState,
	null as RecipientZip,
	null as RecipientPhone,
	null as ItemPriceTotal,
	null as QTY,
	ISNULL(Teacher.LastName,'Unknown') as TeacherName,
	Teacher.Classroom,
	null as ProductTypeName,
	cast(null as varchar) as Incentive,
	--QSPCanadaOrderManagement.dbo.UDF_GetInternetOrderItems(@OrderID,student.Instance) as TotalInternetItems,
	0 as TotalInternetItems,
	Null as CustomerOEFlag,
	Null as QspPremiumID,
	Null as Product_Instance,
           'ONLINE' AS Sale_Type
From  QSPCanadaOrderManagement.dbo.student as student (NOLOCK) ,
      QSPCanadaOrderManagement.dbo.teacher as teacher (NOLOCK)
Where   student.teacherinstance = teacher.instance

and student.instance in (select studentinstance  
			from QSPCanadaOrderManagement.dbo.customerorderheader (NOLOCK)
			where instance in (   select distinct customerorderheaderinstance 
			     		      from    QSPCanadaOrderManagement.dbo.OnlineOrderMappingTable as map (NOLOCK)
			     		      where  map.LandedOrderID = @OrderID )  )

and  student.instance NOT IN  -- exclude those students who have sold items in ground sale
( Select distinct coh.StudentInstance
  FROM  QSPCanadaOrderManagement.dbo.customerorderdetail cod (NOLOCK),
        QSPCanadaOrderManagement.dbo.customerorderheader coh (NOLOCK),
        QSPCanadaOrderManagement.dbo.batch as batch (NOLOCK)
 where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.instance = cod.customerorderheaderinstance
 and cod.delflag <> 1
 and batch.OrderId = @OrderID
 and cod.ProductType NOT IN (46008,46013,46014,46015) )    
--ORDER BY Classroom,TeacherName, ParticipentName , RecipientName, ProductTypeName, ProductName   -- we shall do sorting in the end  for better performance
*/


-- Create Index on #data
/*
CREATE INDEX TIDX_Data_Student ON #DATA (StudentInstance) 
CREATE INDEX TIDX_Data_Product ON #DATA (Product_Instance) 
CREATE INDEX TIDX_Data_Customer ON #DATA (CustomerBillToInstance) 
*/

--Fill in product type name 

 Update #Data Set ProductTypeName = UPPER (CASE ProductType
		WHEN 46001 Then (CASE Lang
					WHEN 'EN' Then 'MAGAZINE'   --Mag
					WHEN 'FR' Then 'MAGAZINE'   --French
					ELSE 'MAGAZINE'			
				   END) 	 
		WHEN 46002 Then (CASE Lang
					WHEN 'EN' Then 'GIFT'  	   --Gift
					WHEN 'FR' Then 'CADEAU'   --French
					ELSE 'GIFT'			
				   END) 
		WHEN 46003 Then (CASE Lang
					WHEN 'EN' Then 'WFC'  	   --WFC
					WHEN 'FR' Then 'Chocolat Le Meilleur au Monde'   --French
					ELSE 'WFC'			
				   END) 
		WHEN 46005 Then (CASE Lang
					WHEN 'EN' Then 'FOOD'  	   --Food
					WHEN 'FR' Then 'Produit alimentaire'   --French
					ELSE 'FOOD'			
				   END) 
		WHEN 46006 Then (CASE Lang
					WHEN 'EN' Then 'MAGAZINE'   --Mag
					WHEN 'FR' Then 'MAGAZINE'   --French
					ELSE 'Magazine'			
				   END)  --'Book'
		WHEN 46007 Then (CASE Lang
					WHEN 'EN' Then 'MAGAZINE'   --Mag
					WHEN 'FR' Then 'MAGAZINE'   --French
					ELSE 'Magazine'			
				   END)  --'Music'
		WHEN 46010  Then (CASE Lang
					WHEN 'EN' Then 'MAGAZINE'   --MMB
					WHEN 'FR' Then 'MAGAZINE'   --French
					ELSE 'MAGAZINE'			
				   END) 	 --MMB
		WHEN 46011 Then (CASE Lang
					WHEN 'EN' Then 'NATIONAL'   --National
					WHEN 'FR' Then 'NATIONAL'   --French
					ELSE 'NATIONAL'			
				   END)	--National
		WHEN 46012 Then (CASE Lang
					WHEN 'EN' Then 'VIDEO'   --Video
					WHEN 'FR' Then 'VIDEO'   --French
					ELSE 'VIDEO'			
				   END)	--Video
		WHEN 46018 Then (CASE Lang
					WHEN 'EN' Then 'COOKIE DOUGH'   --Video
					WHEN 'FR' Then 'Pâte à biscuit'   --French
					ELSE 'COOKIE DOUGH'			
				   END)	 
		WHEN 46019 Then (CASE Lang
					WHEN 'EN' Then 'CHOCOLATE'   --Video
					WHEN 'FR' Then 'CHOCOLAT'   --French
					ELSE 'CHOCOLATE'			
				   END)
		WHEN 46020 Then (CASE Lang
					WHEN 'EN' Then 'JEWELLERY / ORGANIC EDIBLES'   --Video
					WHEN 'FR' Then 'BIJOUX / ORGANIC EDIBLES'   --French
					ELSE 'JEWELLERY / ORGANIC EDIBLES'			
				   END)
		WHEN 46022 Then (CASE Lang
					WHEN 'EN' Then 'CANDLES'
					WHEN 'FR' Then 'CHANDELLES'   --French
					ELSE 'CANDLES'			
				   END)
		WHEN 46023 Then (CASE Lang
					WHEN 'EN' Then 'TO REMEMBER THIS'
					WHEN 'FR' Then 'SE SOUVENIR DE'   --French
					ELSE 'TO REMEMBER THIS'			
				   END)
		WHEN 46024 Then (CASE Lang
					WHEN 'EN' Then 'ENTERTAINMENT'
					WHEN 'FR' Then 'DIVERTISSEMENT'   --French
					ELSE 'ENTERTAINMENT'			
				   END)
		ELSE 'UNKNOWN PRODUCT'				
	END)
 


--Fill in incentives
--MS Sept 26, 2007 Issue #3509
 --Update #Data Set Incentive = Substring((Cod.productcode),3,1)
 Update #Data Set Incentive = p.Prize_Level 
 From   qspcanadaordermanagement..customerorderdetail  as cod,
	qspcanadaordermanagement..customerorderheader as coh,
	qspcanadaordermanagement..batch as batch,
	qspcanadaproduct..pricing_details as pd,
	qspcanadaproduct..product p,
	#Data
	/*,(
		Select max(transid)as transid from qspcanadaordermanagement..customerorderdetail  as cod2
				where cod2.customerorderheaderinstance = coh.instance
	)x
*/
 where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.instance = cod.customerorderheaderinstance
 and batch.orderid  = @OrderID
 and coh.StudentInstance  = #Data.StudentInstance
 and cod.producttype  in ( 46008,46013,46014,46015) 
 and pd.magprice_Instance=cod.pricingdetailsid
 and pd.product_Instance=p.product_Instance
 and cod.transid = (
		Select max(transid)as transid from qspcanadaordermanagement..customerorderdetail  as cod2
				where cod2.customerorderheaderinstance = coh.instance
	)
 



--fill in online sales count
Update #Data Set TotalInternetItems= 
( select SUM(CASE WHEN cod.ProductType = 46001 AND cod.ProductCode NOT LIKE 'DG%' THEN 1 ELSE cod.Quantity END)--count(cod.ProductCode)
 From  QspCanadaOrderManagement.dbo.customerorderdetail cod,
          QspCanadaOrderManagement.dbo.customerorderheader coh,
          QspCanadaOrderManagement.dbo.batch as batch,
	OnlineOrderMappingTable map
 where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.instance = cod.customerorderheaderinstance
 and COD.StatusInstance <>  506 -- Voided Due To Error 
 and cod.delflag <> 1
 and coh.StudentInstance  = D.StudentInstance
 and cod.customerorderheaderinstance = map.customerorderheaderinstance
 and cod.transid  = map.transid    
 and batch.orderid =  OnlineOrderId 
 and LandedOrderId  = @OrderID
 and D.StudentInstance  =map.StudentInstance
 and cod.ProductType NOT IN (46017, 46021))
from #Data D


--Fill in OE items - magazine

 Update #Data Set  CustomerOEFlag = isnull(CustomerOEFlag,'') +'  1*'
 From 	QSPCanadaOrderManagement.dbo.CustomerOrderHeader 	as coh,
	QSPCanadaOrderManagement.dbo.CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement.dbo.Customer 		as CustBill,
	QSPCanadaOrderManagement.dbo.Batch 		as batch,
	#Data
 Where coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= batch.Date
   and cod.ProductType in  (46001,46006,46007, 46023)
   and ( cod.Recipient is null 		or cod.Recipient = '' or
	 CustBill.Address1 is null 	or CustBill.Address1 = '' or
	 CustBill.City is null 		or CustBill.City = '' or
	 CustBill.State is null 	or CustBill.State   = '' or
	 CustBill.State NOT IN
		(SELECT	distinct 	Province
		FROM		QspCanadaCommon..TaxRegionProvince) OR 

	 CustBill.Zip is null 		or CustBill.Zip = '' or
	 (LEN(RTRIM(CustBill.Zip)) <> 6  or not  CustBill.Zip LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]') or 
	 CustBill.StatusInstance  = 301
          ) 
        and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
        and cod.delflag <>1
  and batch.OrderID 		= @OrderID
  and coh.StudentInstance 	= #Data.StudentInstance
  and coh.CustomerBillToInstance = #Data.CustomerBillToInstance


--Fill in OE items --bad CC

 Update #Data Set  CustomerOEFlag = isnull(CustomerOEFlag,'') +'  2*'
 From 	QSPCanadaOrderManagement.dbo.CustomerPaymentHeader as ph,
	QSPCanadaOrderManagement..CreditCardPayment 	as cp,
	QSPCanadaOrderManagement.dbo.CustomerOrderHeader 	as coh,
	QSPCanadaOrderManagement.dbo.CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement.dbo.Batch 		as batch,
	QSPCanadaOrderManagement.dbo.Customer 		as Customer,
	#Data
 Where  cp.CustomerPaymentHeaderInstance = ph.Instance
   and ph.CustomerOrderHeaderInstance  = coh.Instance	
   and coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= batch.Date 
   and coh.CustomerBillToInstance = Customer.Instance
   and coh.PaymentMethodInstance in (50003,50004,50005) -- credit card payments
   and cp.StatusInstance 	 in ( 19001,19002,19005) -- CC payment fail
   and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
   and cod.delflag <> 1 
  and batch.OrderID 		= @OrderID
  and coh.StudentInstance 	= #Data.StudentInstance
  and coh.CustomerBillToInstance = #Data.CustomerBillToInstance
  and coh.instance = #data.COHInstance
  and cod.TransId=#Data.TransId

--Fill in OE items--illegible item

 Update #Data Set  CustomerOEFlag = isnull(CustomerOEFlag,'') +'  3*'
 From 	QSPCanadaOrderManagement.dbo.CustomerOrderHeader 	as coh,
	QSPCanadaOrderManagement.dbo.CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement.dbo.Customer 		as CustBill,
	QSPCanadaOrderManagement.dbo.Batch 		as batch,

	#Data
 Where coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= batch.Date
   and ( cod.ProductType in  (46001,46002,46003,46006,46007, 46018, 46019, 46020, 46023, 46024) or cod.ProductType =0)
   and (cod.ProductCode = 'NNNN' OR ISNULL(cod.PricingDetailsID, 0) = 0) --illegible items
   and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
  and cod.delflag <>1
  and batch.OrderID 		= @OrderID
  and coh.StudentInstance 	= #Data.StudentInstance
  and coh.CustomerBillToInstance = #Data.CustomerBillToInstance
  and coh.instance = #data.COHInstance
  and cod.TransId=#Data.TransId


Insert 	#Premiums  
Select  StudentInstance,
	Product.Pub_Nbr as Pub_Nbr, 
--	Pub_Name as Publisher,
	Count(QspPremiumID) as PremiumCount
From 	#Data as Data,
	Qspcanadaproduct.dbo.product as Product (NOLOCK)
--	 QspCanadaProduct.dbo.Publishers as pub
Where    Data.QspPremiumID > 0 
	 AND DATA.Product_Instance  = product.Product_Instance 
--	 AND Product.Pub_Nbr = pub.Pub_Nbr
Group by  StudentInstance, Product.Pub_Nbr --, Pub_Name
Order  by StudentInstance  
	

-- Create Index #Premiums
CREATE INDEX TempINDEXPremium on #Premiums (StudentInstance,Pub_Nbr)     


  Select  
 	StudentInstance, 
	ParticipentName, 
	ProductType,
	ProductCode, 	
	ProductName, 	
	ProductName_FR,
	CustomerBillToInstance,
	RecipientName,
	CCHolderName,
	CCHolderPhone, 
	RecipientAddress1,
	RecipientAddress2,
	RecipientCity,
	RecipientState,
	RecipientZip,
	RecipientPhone,
	ItemPriceTotal,
	QTY,
	TeacherInstance,
	TeacherName,
	Classroom,
	ProductTypeName,
	 Incentive ,
	TotalInternetItems,
 	(select count(*) from #Data data1 where data1.studentinstance = #Data.studentinstance and len(ltrim(rtrim(CustomerOEFlag))) >0) as OEItemsCount,
	 CustomerOEFlag, 
	Case  when PATINDEX('%2*%', CustomerOEFlag)> 0 Then 'Y'
	Else 'N'
	End AS HasCCError,
	
	(Select PremiumCount From #Premiums where #Premiums.StudentInstance  = #Data.StudentInstance and pub_nbr = 39) as RDA_Premiums,
	(Select PremiumCount From #Premiums where #Premiums.StudentInstance  = #Data.StudentInstance and pub_nbr = 43) as Rogers_Premiums,
	 
    Sale_Type,
	ISNULL(People_12Issues,0) AS People_12Issues,
	ISNULL(People_22Issues,0) AS People_22Issues,
	ISNULL(People_53Issues,0) AS People_53Issues,
	IsShippedToAccount,
	OrderQualifierID

From #Data
ORDER BY TeacherName, ParticipentName, ProductTypeName, RecipientName, ProductName--Classroom,TeacherInstance,ParticipentName,ProductTypeName,RecipientName,  ProductName

 IF @@RowCount = 0
         Insert into QspCanadaCommon.dbo.SystemErrorLog 
	   ( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed) 
         values ( getdate(),@OrderID,Null, 'GetParticipantListingReport','Zero RowCount',null,0,0 ) 

Drop Table #Data 
Drop Table #Premiums 


-- update dds support table
IF @ReportRequestID > 0  -- if the value is not zero it means the report is called from a data driven subscription
BEGIN
     
   UPDATE Qspcanadaordermanagement.dbo.ReportRequestBatch_ParticipantListing
   Set  RunDateStart = getdate()
   Where [id]  = @ReportRequestID

END


SET NOCOUNT OFF
GO
