USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[MagazineAuthFormReport]    Script Date: 06/07/2017 09:19:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE Procedure [dbo].[MagazineAuthFormReport] 	@ProductYear  Int,
							@ProductSeason Varchar(1),
							@ProductCode Varchar(10),
							@TitleLanguage Varchar(05),
							@PublisherName Varchar(80),
							@PublisherCountryCode Varchar(10),
							@DueDate DateTime,
							@OnlyStaffTiles int
As
Begin


Create Table #ProductInfo (
	Pub_Nbr		Int,
	Pub_Name		Varchar(80),
	Pub_Addr_1		Varchar(50),
	Pub_Addr_2		Varchar(50),
	Pub_City		Varchar(50),
	Pub_State		Varchar(5),
	Pub_Zip		Varchar(10),
	Pub_CountryCode	Varchar(10),
	PubContactFirstName 	Varchar(50),
	PubContactLastName  	Varchar(50),
	PubContactPositionTitle 	Varchar(50),
	PubContactEmail   	Varchar(50),
	PhoneContactPhone 	Varchar(30),
	PubContactFax     	Varchar(30),
	PubPhone	  	Varchar(30),
	PubFax		  	Varchar(30),
	Product_Code	  	Varchar(20),
	RemitCode	  	Varchar(20),
	Product_Sort_Name 	Varchar(60),
	Lang 		  	Varchar(5),
	Category_Code	  	Varchar(50),
	DaysLeadTime	  	Int,
	Nbr_Of_Issues_Per_Year Int,
	QSPEXCL		Varchar(5),
	Currency 		Varchar(5),
	GST_NO		Varchar(20),
	QST_No		Varchar(20),
	Ful_Nbr			Varchar(5),
	Ful_Name		Varchar(80),
	Ful_Addr_1		Varchar(50),
	Ful_Addr_2		Varchar(50),
	Ful_City			Varchar(25),
	Ful_State		Varchar(25),
	Ful_Zip			Varchar(10),
	Ful_CountryCode	Varchar(5),
	FulfContactFirstName	Varchar(50),
	FulfContactLastName	Varchar(50),
	FulfContactPositionTitle 	Varchar(50),
	FulfContactEmail		Varchar(50),
	FulfContactPhone	Varchar(30),
	FulfContactFax		Varchar(30),
	InterfaceMediaID	Varchar(50),
	InterfaceLayoutID	Varchar(50),
	TransmissionMethodID	Varchar(50),
	QSPAgencyCode 	Varchar(20),
	Remit_Rate 		Varchar(10),
	NewsStand_Price_Yr	Numeric(10,2),
	NewsStandPriceOriginalCurrency Numeric(10,2),
	ListingLevelID		Varchar(70),
	ListingCopyText		Varchar(500),
	AdPageSizeID		Int,
	AdCostCurrency		Varchar(5),
	AdCost			Numeric(10,2),
	Effort_Key		Varchar(40),
	Nbr_of_Issues		Int,
	BasePriceOriginalCurrency Numeric(10,2),
	Basic_Price_Yr		Numeric(10,2),
	PostageAmount		Numeric(10,2),
	RemitPercentage	Varchar(20),
	Actual_Base_Price	Numeric(10,2),
	ActualRemitAmount	Numeric(10,2),
	InternetApproval		Varchar(1),
	ABCCode		Varchar(20),
	QSPPremiumID		Int,
	prdPremiumCode	Varchar(50),
	prdPremiumCopy		Varchar(500)
	)


--Temp Solution
Create Table #postageByProduct (ID 			Int Identity  Not Null,
				Product_year		Int,
				Product_Season 	Varchar(1),
				Product_Code 		Varchar(10),
				PostageAmount  	Numeric(10,2),
				RemitPercentage	Numeric(10,2)
				)

Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			 Values(2007,'S','0240'	, 10.00 ,25)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','0250'	, 10.00,25 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','0255'	, 10.00 ,25)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','0700'	, 10.00,25) 
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','0780'	, 10.00 ,25)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','3217'	, 10.00,25 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','4846'	, 10.00,25 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','3931'	, 10.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','2499'	, 10.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(	2007,'S','3111'	, 13.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','0155'	,	 11.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','3955'	,	 13.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','2355'	,	 10.00,10)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','3113'	,	 8.00 ,20)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','3890'	,	 10.00,10)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','8143'	,	 15.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','6385'	,	 15.00,6 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','0690'	,	 16.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1350'	,	 16.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1440'	,	 12.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1640'	,	 10.00,5 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','0096'	,	 15.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','3471'	,	 10.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','2953'	,	 22.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','4320'	,	 5.00 ,10)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1309'	,	 5.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','5816'	,	 5.00,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','5639'	,	 7.50,10 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','3639'	,	 10.00,20 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','4158'	,	 10.00,20 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','4911'	,	 10.00,20 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','9464'	,	 10.00,20 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','10TM'	,	 6.00,20 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','8217'	,	 6.00 ,20)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1399'	,	 8.00,20 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','7292'	,	 6.00,20 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','105W'	,	 6.00 ,20)
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1346'	,	 6.00,20 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','6683'	,	 0 ,0 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','2063'	,	 12.00,0 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','0068'	,	 0 ,0 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','5362'	,	 0 ,0 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','0082'	,	 0 ,0 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','4852'	,	 0 ,0 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1220'	,	 0 ,0 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1390'	,	 0 ,0 )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','1505'	,	  0,0  )
Insert into  #postageByProduct (Product_year,Product_Season,Product_Code,PostageAmount,RemitPercentage)
			Values(2007,'S','104G'	,	 0,0  )


--Select Count(*) from #postageByProduct

Insert #ProductInfo
Select  Distinct 
--########################Publisher
	pub.Pub_Nbr,
	pub.Pub_Name,
	Pub_Addr_1,
	Pub_Addr_2,
	Pub_City,
	Pub_State,
	CASE Pub_CountryCode WHEN 'CA' then Substring(Pub_Zip,1,3)+' '+Substring(Pub_Zip,4,3) ELSE Pub_Zip END Pub_Zip,
	Pub_CountryCode,
--###################### pub contact
	CONVERT(Varchar(50),null)  PubContactFirstName,
	CONVERT(Varchar(50),null)  PubContactLastName,
	CONVERT(Varchar(50),null)  PubContactPositionTitle,
	CONVERT(Varchar(50),null)  PubContactEmail,
	CONVERT(Varchar(50),null)  PhoneContactPhone,
	CONVERT(Varchar(50),null)  PubContactFax,
	CONVERT(Varchar(50),null)  PubPhone,
	CONVERT(Varchar(50),null)  PubFax,
--######################product
	p.Product_Code,
	RemitCode,
	Product_Sort_Name,
	Lang ,
	CategoryCode.Description Category_Code,
	DaysLeadTime,
	Nbr_Of_Issues_Per_Year,
	CASE ISNULL(IsQSPExclusive,0) WHEN 1 THEN 'YES' else 'NO' END QSPEXCL,
	CASE Currency WHEN 802 THEN 'USD'ELSE 'CAD' END Currency ,
	gst.TAX_REGISTRATION_NUMBER GST_NO,
	qst.TAX_REGISTRATION_NUMBER QST_No,
--###########################fulf house
	fh.Ful_Nbr,
	fh.Ful_Name,
	Ful_Addr_1,
	Ful_Addr_2,
	Ful_City,
	Ful_State,
	--Ful_Zip,
	CASE fh.CountryCode WHEN 'CA' then Substring(ful_Zip,1,3)+' '+Substring(ful_Zip,4,3) ELSE ful_Zip END ful_Zip,
	fh.CountryCode,
	CONVERT(Varchar(50),null)  FulfContactFirstName,
	CONVERT(Varchar(50),null)  FulfContactLastName,
	CONVERT(Varchar(50),null)  FulfContactPositionTitle,
	CONVERT(Varchar(50),null)  FulfContactEmail,
	CONVERT(Varchar(50),null)  FulfContactPhone,
	CONVERT(Varchar(50),null)  FulfContactFax,
	mediaCode.Description InterfaceMediaID,
	layoutCode.Description InterfaceLayoutID,
	methodCode.Description TransmissionMethodID,
	fh.QSPAgencyCode,
--###############Pricing###################	
	Convert(Varchar(10),Convert(Numeric(10,1),pd.Remit_Rate*100))+'%' Remit_Rate,
	pd.NewsStand_Price_Yr,pd.NewsStandPriceOriginalCurrency,
	ll.description ListingLevelID, 
	pd.ListingCopyText,pd.AdPageSizeID,
	CASE pd.AdCostCurrency WHEN 802 THEN 'USD'ELSE 'CAD' END AdCostCurrency,
	pd.AdCost,pd.Effort_Key,
	pd.Nbr_of_Issues,pd.BasePriceOriginalCurrency,pd.Basic_Price_Yr,
	IsNull(#postageByProduct.PostageAmount,0) PostageAmount,
	RemitPercentage,
	pd.Basic_Price_Yr-IsNull(#postageByProduct.PostageAmount,0) Actual_Base_Price,
	CASE SIGN(IsNull(#postageByProduct.PostageAmount,0)) When 0 THEN Round(pd.Remit_Rate*pd.Basic_Price_Yr,2)
	ELSE  Round((pd.Basic_Price_Yr-IsNull(#postageByProduct.PostageAmount,0))*Round((#postageByProduct.RemitPercentage/100),2),2)+IsNull(#postageByProduct.PostageAmount,0) 
	END ActualRemitAmount,
	CASE pd.InternetApproval WHEN 1 THEN 'Y' ELSE 'N' END InternetApproval,
	pd.ABCCode,
	Max(QSPPremiumID)QSPPremiumID,pd.prdPremiumCode,pd.prdPremiumCopy
From [QSPCanadaProduct].[dbo].[Product]P
	Left Join qspcanadacommon.dbo.TaxMagRegistration GST on p.Product_Code=gst.TITLE_CODE and gst.tax_id=1
	Left join qspcanadacommon.dbo.TaxMagRegistration QST on p.Product_Code=Qst.TITLE_CODE and Qst.tax_id=3
	LEFT JOIN [QSPCanadaProduct].dbo.PRICING_DETAILS pd on  p.product_instance=pd.Product_Instance 
	LEFT JOIN [QSPCanadaProduct].[dbo].ListingLevel ll on pd.ListingLevelID=ll.ID
	LEFT JOIN #postageByProduct ON  p.Product_Code= #postageByProduct.Product_Code,
	[QSPCanadaProduct].[dbo].PUBLISHERS pub ,
	[QSPCanadaProduct].dbo.FULFILLMENT_HOUSE fh ,
	qspcanadacommon..codedetail mediaCode,
	qspcanadacommon..codedetail layoutCode,
	qspcanadacommon..codedetail methodCode,
	qspcanadacommon..codedetail CategoryCode
Where p.Product_year= @ProductYear
and p.Product_Season= @ProductSeason
and p.Product_Code = IsNull(@ProductCode,p.Product_Code)
and pub.Pub_CountryCode= IsNull(@PublisherCountryCode ,pub.Pub_CountryCode)
and p.Product_Code Like  CASE IsNull(@OnlyStaffTiles,0) WHEN 1 THEN 'S%' ELSE '%' END 
and pub.pub_name Like  CASE IsNull(@PublisherName,'') WHEN '' THEN '%' ELSE @PublisherName+'%' END  		
and p.Lang = IsNull(@TitleLanguage,p.Lang)
and p.type in (46001)
and p.status=30600
and p.Pub_Nbr=pub.Pub_Nbr
and P.Fulfill_House_Nbr=fh.Ful_Nbr
and mediaCode.Instance=fh.InterfaceMediaID
and layoutCode.Instance=fh.InterfaceLayoutID
and methodCode.Instance=fh.TransmissionMethodID
and p.Category_Code=CategoryCode.Instance
Group BY
pd.Remit_Rate,pd.NewsStand_Price_Yr,pd.NewsStandPriceOriginalCurrency,
pd.ListingLevelID,pd.ListingCopyText,pd.AdPageSizeID,pd.AdCostCurrency,pd.AdCost,pd.Effort_Key,
pd.Nbr_of_Issues,pd.BasePriceOriginalCurrency,pd.Basic_Price_Yr,pd.InternetApproval,pd.ABCCode,
pd.prdPremiumCode,pd.prdPremiumCopy,
---------------------------------------
pub.Pub_Nbr,
pub.Pub_Name,
Pub_Addr_1,
Pub_Addr_2,
Pub_City,
Pub_State,
Pub_Zip,
Pub_CountryCode,
--------------------
p.Product_Code,
RemitCode,
Product_Sort_Name,
Lang ,
Category_Code,
DaysLeadTime,
Nbr_Of_Issues_Per_Year,
IsQSPExclusive,Currency ,
gst.TAX_REGISTRATION_NUMBER ,
qst.TAX_REGISTRATION_NUMBER ,
fh.Ful_Nbr,
fh.Ful_Name,
Ful_Addr_1,
Ful_Addr_2,
Ful_City,
Ful_State,
Ful_Zip,
fh.CountryCode,
mediaCode.Description ,
layoutCode.Description ,
methodCode.Description ,
CategoryCode.Description,
fh.QSPAgencyCode,ll.DESCRIPTION,
#postageByProduct.PostageAmount,RemitPercentage
Order by p.product_code

--Select * from #ProductInfo

--update to latest contact Regardless of product code 
Update #ProductInfo
Set 
	 PubContactFirstName = pubCont.PContact_FName ,
	 PubContactLastName =pubCont.PContact_LName,
	 PubContactPositionTitle =pubCont.PContact_Title,
	 PubContactEmail=pubCont.PContact_Email,
	 PhoneContactPhone =pubCont.PContact_Tel+'EXT.'+pubCont.PContact_Tel_Extn,
	 PubContactFax =pubCont.Pcontact_Fax,
	  PubPhone =ph.PhoneNumber,
	 PubFax =fax.PhoneNumber 
FROM
	QSPCanadaProduct.dbo.PUBLISHER_CONTACTS pubCont 
	LEFT JOIN QSPCanadaProduct.dbo.Phone ph ON pubCont.PhoneListID=ph.PhoneListID and IsNull(ph.Type,1)=1
	LEFT JOIN QSPCanadaProduct.dbo.Phone fax ON pubCont.PhoneListID=fax.PhoneListID and IsNull(fax.Type,3)=3
Where pubCont.Pub_Nbr in (Select Distinct Pub_Nbr from #ProductInfo)
and #ProductInfo.Pub_Nbr=pubCont.Pub_Nbr
and pubCont.PContact_DateChanged in  (Select Max(PContact_DateChanged) From QSPCanadaProduct.dbo.PUBLISHER_CONTACTS pubContMax
	    			     	 Where pubContMax.Pub_Nbr=pubCont.Pub_Nbr
				      	)


Update #ProductInfo
Set 
	 PubContactFirstName = pubCont.PContact_FName ,
	 PubContactLastName =pubCont.PContact_LName,
	 PubContactPositionTitle =pubCont.PContact_Title,
	 PubContactEmail=pubCont.PContact_Email,
	 PhoneContactPhone =pubCont.PContact_Tel+'EXT.'+pubCont.PContact_Tel_Extn,
	 PubContactFax =pubCont.Pcontact_Fax,
	 PubPhone =ph.PhoneNumber,
	 PubFax =fax.PhoneNumber 
FROM
	QSPCanadaProduct.dbo.PUBLISHER_CONTACTS pubCont
	LEFT JOIN QSPCanadaProduct.dbo.Phone ph ON pubCont.PhoneListID=ph.PhoneListID and IsNull(ph.Type,1)=1
	LEFT JOIN QSPCanadaProduct.dbo.Phone fax ON pubCont.PhoneListID=fax.PhoneListID and IsNull(fax.Type,3)=3
where pubCont.Pub_Nbr = #ProductInfo.Pub_Nbr 
and pubcont.Product_Code=#ProductInfo.Product_code


--update all fulkf house regardless 
Update #ProductInfo
Set
  FulfContactFirstName =fulCont.FirstName,
  FulfContactLastName =fulCont.LastName,
  FulfContactPositionTitle = fulCont.Title,
  FulfContactEmail = fulCont.Email ,
  FulfContactPhone =  fulCont.WorkPhone,
  FulfContactFax= fulCont.Fax 
from QSPCanadaProduct.dbo.FULFILLMENT_HOUSE_CONTACTS fulCont,#ProductInfo
where fulCont.ful_Nbr in (Select Distinct Ful_Nbr from #ProductInfo)
AND  #ProductInfo.FUL_NBR=fulCont.Ful_Nbr
and fulCont.DateChanged in  (Select Max(DateChanged) From QSPCanadaProduct.dbo.FULFILLMENT_HOUSE_CONTACTS fulContMax
       			        Where fulContMax.Ful_Nbr=fulCont.ful_Nbr
   			        AND fulContMax.FUL_NBR=fulCont.Ful_Nbr)



Update #ProductInfo
Set
  FulfContactFirstName =fulCont.FirstName,
  FulfContactLastName =fulCont.LastName,
  FulfContactPositionTitle = fulCont.Title,
  FulfContactEmail = fulCont.Email ,
  FulfContactPhone =  fulCont.WorkPhone,
  FulfContactFax= fulCont.Fax 
from QSPCanadaProduct.dbo.FULFILLMENT_HOUSE_CONTACTS fulCont
where fulCont.ful_Nbr =#ProductInfo.Ful_Nbr 
and fulcont.product_code=#ProductInfo.product_code
/*
Update #a
Set PubContactFirstName ='Valerie'   ,
PubContactLastName='Tyrer',
PubContactEmail='circ@formulapublications.com',
PhoneContactPhone='905-842-6591x228 ',
PubContactFax='905-842-4810 ' 
*/
--Output
Select  Convert(Varchar,DATENAME(month, @DueDate))+' '+ Convert(Varchar,DATENAME(day, @DueDate))+', '+Convert(Varchar,DATENAME(year, @DueDate)) DueDate,* from #ProductInfo


Drop Table #postageByProduct
Drop table #ProductInfo
End
GO
