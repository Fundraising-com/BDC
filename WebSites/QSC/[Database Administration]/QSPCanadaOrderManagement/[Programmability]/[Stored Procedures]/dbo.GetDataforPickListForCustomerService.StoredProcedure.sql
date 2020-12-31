USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetDataforPickListForCustomerService]    Script Date: 06/07/2017 09:19:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetDataforPickListForCustomerService]
	@OrderID		Int,
	@BatchId		Int,
	@BatchDate		DateTime,
	@ShipDateFrom		DateTime,
	@ShipDateTo		DateTime,
	@ReportType		Int = 1, -- PackingSlip(2) or PickList(1)
	@ShipmentGroupID Int

As
Declare @SelectString 		Varchar(8000),
	@GroupByString 	Varchar(4000),
	@WhereShipdate	Varchar(500)

If  (@ShipDateFrom is  Null  And @ShipDateTo Is Null) or (IsNull(@ShipDateFrom, ' ') = ' ' And  IsNull(@ShipDateTo, ' ')= ' ' )
Begin
	--Set  @WhereShipdate = ' And ShipmentDate >= '+''''+Convert(Varchar(10),@ShipDateFrom,101)+''''+'  And  ShipmentDate <= '+''''+ Convert(Varchar(10),@ShipDateTo,101) +''''

SELECT @ReportType   ReportType,  
	IsNull(ShipAccountId,BillToAccountId ) 		ShipAccountId,
	IsNull(ShiptoAccount, BillToAccount)      		ShipAccountName,
	IsNull(ShipAccountAddress1,AccountAddress1)	ShipAccountAddress1,
	IsNull(ShipAccountAddress2,AccountAddress2)	ShipAccountAddress2,
	IsNull(ShipAccountCity,AccountCity)		ShipAccountCity,
	IsNull(ShipAccountProvince,AccountProvince)	ShipAccountState,
	IsNull(ShipAccountPCode, AccountPCode)	ShipPCode,
	(Case    (IsNull(ShipContactFname,'@')+' '+IsNull(ShipContactLname,'@'))	
		When '@ @' then AccountContactFName+' '+AccountContactLName
		Else
			ShipContactFname+' '+ShipContactLname
		End) ShipAccountContact,
	IsNull(ShipContactPhone,AccountContactPhone)  	ShipAccountContactPhone,
	BillToAccountId     	AccountId,
	BillToAccount      	AccountName,
	AccountAddress1,
	AccountAddress2,
	AccountCity,
	AccountProvince     AccountState,
	AccountPCode        AccountZip,
	AccountContactFName ContactFirstName,
	AccountContactLName ContactLastName,
	AccountContactPhone ContactPhone,
	OrderID, 
	Convert(Varchar(10),DateReceived,101)DateReceived,
	(Case @ReportType
		 When 2 Then
			Case StatusInstance
			When 508 Then Convert(Varchar(10), ShipmentDate,101) --Item Shipped
			Else
				Null
			End
		Else
			Convert(Varchar(10), DateCreated,101)
	End) as DateCreated,
	FMID, 
	FirstName, 
	LastName,
	CampaignID,
	(Case IsNull(CALang,'@@')
	 When '@@' then 
		Case IsNull(AccountLang, '##')
		When '##'  then 'EN'
		Else
			AccountLang
		End
	 Else
		CALang
	 End) Lang,
	PickNumber,
	Product_Code, 
	PRODUCT_DESCRIPTION_ALT,
	LANGUAGE_CODE, 
	CatalogProductCode, 
	OracleCode, 
	StatusInstance, 
	BatchStatus,
	Comment,
	SUM(Quantity) Quantity,
	(Case @ReportType 
		 When 2 Then
			Case StatusInstance
			When 508 Then SUM(QuantityShipped) --Item Shipped
			Else
				0
			End
		When 1 Then
			Case StatusInstance
			When 500 Then SUM(Quantity)	      -- Good /Approved
			Else
				0
			End
	End) QuantityToShip_Pack, 
	Case ReplacedProductCode
	When Null Then 0
	When '' Then 0
	Else ReplacedProductCode
	End ReplacedProductCode,
	--IsNull(ReplacedProductCode,'') ReplacedProductCode,
	ReplacedProductQty,
	Round(Sum ((Quantity * CatalogPrice)/(Case Quantity
						When 0 Then 1
						Else Quantity
					      End)),2)  UnitPrice,
	Sum (Quantity * CatalogPrice) ExtendedPrice,
	ShipmentId,
	ShipmentDate,	
	ParticipantFirstName, 
	ParticipantLastName, 
	ParticipantInstance,
	TeacherLastName, 
	TeacherFirstName, 
	Class,
	Recipient,
	SuppliesDeliveryDate,
	Case BatchStatus
	When 40014 Then  
			Case DATEDIFF(dd, Convert(datetime, Convert(varchar, IsNull(ShipmentDate,getdate()),101)), Convert(datetime, Convert(varchar, GetDate(),101))) 
			  When 0 Then  0  	--'Item not shipped previously
			  When Null Then 0
			  Else 1  	
			  End
	When 40013 Then  
			Case DATEDIFF(dd, Convert(datetime, Convert(varchar, IsNull(ShipmentDate,getdate()),101)), Convert(datetime, Convert(varchar, GetDate(),101))) 
			  When 0 Then  0  	--'Item not shipped previously
			  When Null Then 0
			  Else 1  	
			  End
	Else	0
	End PreviouslyShipped,
	Convert(Varchar(10), RequestedShipDate,101) RequestedShipDate,
	CarrierID,
	SpecialInstructions,
	(SELECT CASE WHEN COUNT(*) > 1 THEN 1 ELSE 0 END FROM ReportRequestBatch WHERE BatchOrderID = @OrderID AND ShipmentGroupID = ISNULL(@ShipmentGroupID, ShipmentGroupID) GROUP BY BatchOrderId) Reprint
FROM  dbo.PickListForEnvelopeCustomerService
WHERE OrderId = @OrderId 
 And Id  = @BatchId 
 And Date = @BatchDate
 AND ShipmentGroupID = ISNULL(@ShipmentGroupID, ShipmentGroupID)
Group By ShipAccountId,
	ShiptoAccount 	    ,
	ShipAccountAddress1,
	ShipAccountAddress2,
	ShipAccountCity,
	ShipAccountProvince ,
	ShipAccountPCode    ,
	ShipContactFname,
	ShipContactLname,
	ShipContactPhone    ,
	BillToAccountId     ,
	BillToAccount       ,
	AccountAddress1,
	AccountAddress2,
	AccountCity,
	AccountProvince     ,
	AccountPCode        ,
	AccountContactFName ,
	AccountContactLName ,
	AccountContactPhone ,
	OrderID,
	DateReceived,
	StatusInstance,
	--QuantityShipped,
	--Quantity,
	DateCreated,
	FMID, 
	FirstName, 
	LastName,
	CampaignID,
	PickNumber,
	Product_Code, 
	PRODUCT_DESCRIPTION_ALT,
	LANGUAGE_CODE, 
	CatalogProductCode, 
	OracleCode, 
	StatusInstance, 
	BatchStatus,
	Comment,
	--Quantity,
	ShipmentId,
	ShipmentDate,	
	ParticipantFirstName, 
	ParticipantLastName, 
	ParticipantInstance,
	TeacherLastName, 
	TeacherFirstName, 
	Class,
	CALang,
	AccountLang,
	ReplacedProductCode,
	ReplacedProductQty,
	Recipient,
	SuppliesDeliveryDate,
	RequestedShipDate,
	CarrierID,
	SpecialInstructions
Order By CampaignId, ParticipantInstance,ParticipantLastName,TeacherLastName,TeacherFirstName
End

If  @ShipDateFrom is Not Null  And @ShipDateTo Is Not Null
Begin

SELECT @ReportType   ReportType,  
	IsNull(ShipAccountId,BillToAccountId ) 		ShipAccountId,
	IsNull(ShiptoAccount, BillToAccount)      		ShipAccountName,
	IsNull(ShipAccountAddress1,AccountAddress1)	ShipAccountAddress1,
	IsNull(ShipAccountAddress2,AccountAddress2)	ShipAccountAddress2,
	IsNull(ShipAccountCity,AccountCity)		ShipAccountCity,
	IsNull(ShipAccountProvince,AccountProvince)	ShipAccountState,
	IsNull(ShipAccountPCode, AccountPCode)	ShipPCode,
	(Case    (IsNull(ShipContactFname,'@')+' '+IsNull(ShipContactLname,'@'))	
		When '@ @' then AccountContactFName+' '+AccountContactLName
		Else
			ShipContactFname+' '+ShipContactLname
		End) ShipAccountContact,
	IsNull(ShipContactPhone,AccountContactPhone)  	ShipAccountContactPhone,
	BillToAccountId     	AccountId,
	BillToAccount      	AccountName,
	AccountAddress1,
	AccountAddress2,
	AccountCity,
	AccountProvince     AccountState,
	AccountPCode        AccountZip,
	AccountContactFName ContactFirstName,
	AccountContactLName ContactLastName,
	AccountContactPhone ContactPhone,
	OrderID, 
	Convert(Varchar(10),DateReceived,101)DateReceived,
	(Case @ReportType
		 When 2 Then
			Case StatusInstance
			When 508 Then Convert(Varchar(10), ShipmentDate,101) --Item Shipped
			Else
				Null
			End
		Else
			Convert(Varchar(10), DateCreated,101)
	End) as DateCreated,
	FMID, 
	FirstName, 
	LastName,
	CampaignID,
	(Case IsNull(CALang,'@@')
	 When '@@' then 
		Case IsNull(AccountLang, '##')
		When '##'  then 'EN'
		Else
			AccountLang
		End
	 Else
		CALang
	 End) Lang,
	PickNumber,
	Product_Code, 
	PRODUCT_DESCRIPTION_ALT,
	LANGUAGE_CODE, 
	CatalogProductCode, 
	OracleCode, 
	StatusInstance, 
	BatchStatus,
	Comment,
	SUM(Quantity) Quantity,
	(Case @ReportType 
		 When 2 Then
			Case StatusInstance
			When 508 Then SUM(QuantityShipped) --Item Shipped
			Else
				0
			End
		When 1 Then
			Case StatusInstance
			When 500 Then SUM(Quantity)	      -- Good /Approved
			Else
				0
			End
	End) QuantityToShip_Pack, 
	ReplacedProductCode,
	ReplacedProductQty,
	Round(Sum ((Quantity * CatalogPrice)/(Case Quantity
						When 0 Then 1
						Else Quantity
					      End)),2)  UnitPrice,
	Sum (Quantity * CatalogPrice) ExtendedPrice,
	ShipmentId,
	ShipmentDate,	
	ParticipantFirstName, 
	ParticipantLastName, 
	ParticipantInstance,
	TeacherLastName, 
	TeacherFirstName, 
	Class,
	Recipient,
	SuppliesDeliveryDate,
	Case BatchStatus
	When 40014 Then  
			Case DATEDIFF(dd, Convert(datetime, Convert(varchar, IsNull(ShipmentDate,getdate()),101)), Convert(datetime, Convert(varchar, GetDate(),101))) 
			  When 0 Then  0  	--'Item not shipped previously
			  When Null Then 0
			  Else 1  	
			  End
	When 40013 Then  
			Case DATEDIFF(dd, Convert(datetime, Convert(varchar, IsNull(ShipmentDate,getdate()),101)), Convert(datetime, Convert(varchar, GetDate(),101))) 
			  When 0 Then  0  	--'Item not shipped previously
			  When Null Then 0
			  Else 1  	
			  End
	Else	0
	End PreviouslyShipped,
	Convert(Varchar(10), RequestedShipDate,101) RequestedShipDate,
	CarrierID,
	SpecialInstructions,
	(SELECT CASE WHEN COUNT(*) > 1 THEN 1 ELSE 0 END FROM ReportRequestBatch WHERE BatchOrderID = @OrderID AND ShipmentGroupID = ISNULL(@ShipmentGroupID, ShipmentGroupID) GROUP BY BatchOrderId) Reprint
FROM  dbo.PickListForEnvelopeCustomerService
WHERE OrderId = @OrderId 
 And Id  = @BatchId 
 And Date = @BatchDate
 AND ShipmentGroupID = ISNULL(@ShipmentGroupID, ShipmentGroupID)
And (ShipmentDate >= Convert(Varchar(10),@ShipDateFrom,101) And  ShipmentDate <= Convert(Varchar(10),@ShipDateTo,101) )
Group By ShipAccountId,
	ShiptoAccount 	    ,
	ShipAccountAddress1,
	ShipAccountAddress2,
	ShipAccountCity,
	ShipAccountProvince ,
	ShipAccountPCode    ,
	ShipContactFname,
	ShipContactLname,
	ShipContactPhone    ,
	BillToAccountId     ,
	BillToAccount       ,
	AccountAddress1,
	AccountAddress2,
	AccountCity,
	AccountProvince     ,
	AccountPCode        ,
	AccountContactFName ,
	AccountContactLName ,
	AccountContactPhone ,
	OrderID,
	DateReceived,
	StatusInstance,
	--QuantityShipped,
	--Quantity,
	DateCreated,
	FMID, 
	FirstName, 
	LastName,
	CampaignID,
	PickNumber,
	Product_Code, 
	PRODUCT_DESCRIPTION_ALT,
	LANGUAGE_CODE, 
	CatalogProductCode, 
	OracleCode, 
	StatusInstance, 
	BatchStatus,
	Comment,
	--Quantity,
	ShipmentId,
	ShipmentDate,	
	ParticipantFirstName, 
	ParticipantLastName, 
	ParticipantInstance,
	TeacherLastName, 
	TeacherFirstName, 
	Class,
	CALang,
	AccountLang,
	ReplacedProductCode,
	ReplacedProductQty,
	Recipient,
	SuppliesDeliveryDate,
	RequestedShipDate,
	CarrierID,
	SpecialInstructions
Order By CampaignId, ParticipantInstance, ParticipantLastName,TeacherLastName,TeacherFirstName
End
GO
