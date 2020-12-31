USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetDataForPickListNonEnvelope]    Script Date: 06/07/2017 09:19:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDataForPickListNonEnvelope]
	@OrderID		Int,
	@BatchId		Int,
	@BatchDate		DateTime,
	@ShipDateFrom		DateTime,
	@ShipDateTo		DateTime,
	@ReportType		Int,  -- PackingSlip(2) or PickList(1)
	@ShipmentGroupID Int
/*****************************************************************************************************
Added IsStaffOrder Jan 18, 2007
******************************************************************************************************/
As
Declare @SelectString 		Varchar(8000),
	@GroupByString 	Varchar(4000),
	@WhereShipdate	Varchar(500)

If  (IsNull(@ShipDateFrom,  ' ') = ' '  And IsNull(@ShipDateTo,' ') = ' ') Or (@ShipDateFrom is Null And @ShipDateTo Is Null)
Begin
	--Set  @WhereShipdate = ' And ShipmentDate >= '+''''+Convert(Varchar(10),@ShipDateFrom,101)+''''+'  And  ShipmentDate <= '+''''+ Convert(Varchar(10),@ShipDateTo,101) +''''


Select @ReportType  ReportType, 
	/*(Case IsNull(CustomerType,0) 
	 	When 50602  then FMId
	 Else
	     	IsNull(ShipAccountId,BillToAccountId)
	 End )ShipAccountId,*/
	IsNull(ShipAccountId,BillToAccountId ) ShipAccountId,

	--Shiping information from customer table
	(Case IsNull(CustomerType,0) 
	 	When 50602  then ShiptoAccount +' C/o ('+CustomerFirstName+' '+CustomerLastName+')'
	 Else
	     	ShipToAccount--CustomerFirstName+' '+CustomerLastName
	 End )	ShipAccountName,
	
	 CustomerAddress1 ShipAccountAddress1,
	 CustomerAddress2 ShipAccountAddress2,
	 CustomerCity ShipAccountCity,
	 CustomerState ShipAccountState,
	 CustomerZip ShipPCode,

	--For FM and Employee contact will customer
	(Case  IsNull(CustomerType,0)
		When 50602  then CustomerFirstName+' '+CustomerLastName
		When 50604  then CustomerFirstName+' '+CustomerLastName
	Else
		ShipContactFname+' '+ShipContactLName
	End) ShipAccountContact,
	
	(Case  IsNull(CustomerType,0)
		When 50602  then IsNull(CustomerPhone,FMContact)
	 Else
		IsNull(CustomerPhone,ShipContactPhone)
	 End) ShipAccountContactPhone,

	/*(Case  IsNull(CustomerType,0)
		When 50602  then FMID
	Else
	  	BillToAccountId 
        End) AccountId,*/
	BillToAccountId AccountId,
	--For FM and employee customer is Account name
	(Case  IsNull(CustomerType,0)
	  	When 50602  then CustomerFirstName+' '+CustomerLastName	
	  	When 50604  then CustomerFirstName+' '+CustomerLastName		
	 Else
              	BillToAccount   
	 End)AccountName,

	--If it is account(not FM/EMP) Ship address may be different 
	-- so use address from account address
	(Case  IsNull(CustomerType,0)
	   	When 50602  then  CustomerAddress1  	--FM
	   	When 50604  then  CustomerAddress1  	--EMP
	 Else
	      	AccountAddress1
	 End)AccountAddress1,
	
	(Case   IsNull(CustomerType,0)
	 	When 50602  then  CustomerAddress2
	 	When 50604  then  CustomerAddress2  	--EMP
	 Else
	      	AccountAddress2
	 End)AccountAddress2,

	(Case   IsNull(CustomerType,0)
	 	When 50602 then CustomerCity  	--FM
	   	When 50604  then  CustomerCity  	--EMP
	 Else
		AccountCity
	 End)AccountCity,

	(Case  IsNull(CustomerType,0)
	 	When 50602  then  CustomerState	--FM
	    	When 50604  then  CustomerState  	--EMP
	 Else
		AccountProvince
	 End)AccountState,

	(Case  IsNull(CustomerType,0)
	 	When 50602  then  CustomerZip	  	--FM
	    	When 50604  then  CustomerZip  	--EMP
	 Else
		AccountPCode
	 End)AccountZip,

	(Case  IsNull(CustomerType,0)
		When 50602  then CustomerFirstName
		When 50604  then  CustomerFirstName  	--EMP
	Else
		AccountContactFname
	End )ContactFirstName,

	(Case  IsNull(CustomerType,0)
		When 50602  then CustomerLastName
		When 50604  then  CustomerFirstName  	--EMP
	Else
		AccountContactLName
	End)ContactLastName,

	(Case  IsNull(CustomerType,0)
	  	When 50602  then IsNull(CustomerPhone,FMContact)
	 Else
		AccountContactPhone
	 End)ContactPhone,

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
	Case  IsStaffOrder
	When 1 Then 'Y'
	Else 'N'
	End IsStaffOrder,
	(Case IsNull(CALang,'@@')
	 When '@@' then 
		Case IsNull(AccountLang, '##')
		When '##' then 'EN'
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

	Null ParticipantFirstName, 
	Null ParticipantLastName, 
	Null TeacherLastName, 
	Null TeacherFirstName, 
	Null Class,
	Null Recipient,
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
FROM         dbo.PickListNonEnvelope1
WHERE	OrderId = @OrderId
AND		ShipmentGroupID = ISNULL(@ShipmentGroupID, ShipmentGroupID)
Group By OrderID, 
	DateReceived,
	DateCreated,
	ShipmentDate,
	FMID, 
	FirstName, 
	LastName,
	CampaignID,
	 IsStaffOrder,
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
	--QuantityShipped,
	ReplacedProductCode,
	ReplacedProductQty,
	ShipAccountId,
	OrderTypeCode,
	ShiptoAccount,
	ShipContactFname,
	ShipContactLname,
	FMContact,
	ShipContactPhone,
	BillToAccountId,
	BillToAccount,
	AccountAddress2,
	AccountAddress1,
	AccountCity,
	AccountProvince,
	AccountPCode,
	AccountContactFname,
	AccountContactLName,
	AccountContactPhone,
	ShipmentId,
	ShipmentDate,
	AccountLang,
	CALang,
	Recipient,
	SupporterName,
	CustomerType,
	CustomerFirstName,CustomerLastName,CustomerAddress1,CustomerAddress2,
	CustomerCity,CustomerState,CustomerZip,customerphone,
	SuppliesDeliveryDate,
	RequestedShipDate,
	CarrierID,
	SpecialInstructions
End

If  Len(@ShipDateFrom) > 0   And len(@ShipDateTo) > 0 -- Is Not Null
Begin
	--Set  @WhereShipdate = ' And ShipmentDate >= '+''''+Convert(Varchar(10),@ShipDateFrom,101)+''''+'  And  ShipmentDate <= '+''''+ Convert(Varchar(10),@ShipDateTo,101) +''''

Select @ReportType  ReportType, 
	(Case IsNull(CustomerType,0) 
	 	When 50602  then FMId
	 Else
	     	IsNull(ShipAccountId,BillToAccountId)
	 End )ShipAccountId,

	--Shiping information from customer table
	 CustomerFirstName+' '+CustomerLastName  ShipAccountName,
	 CustomerAddress1 ShipAccountAddress1,
	 CustomerAddress2 ShipAccountAddress2,
	 CustomerCity ShipAccountCity,
	 CustomerState ShipAccountState,
	 CustomerZip ShipPCode,
	--For FM and Employee contact will customer
	(Case  IsNull(CustomerType,0)
		When 50602  then CustomerFirstName+' '+CustomerLastName
		When 50604  then CustomerFirstName+' '+CustomerLastName
	Else
		ShipContactFname+' '+ShipContactLName
	End) ShipAccountContact,
	
	(Case  IsNull(CustomerType,0)
		When 50602  then IsNull(CustomerPhone,FMContact)
	 Else
		IsNull(CustomerPhone,ShipContactPhone)
	 End) ShipAccountContactPhone,

	(Case  IsNull(CustomerType,0)
		When 50602  then FMID
	Else
	  	BillToAccountId 
        End) AccountId,

	--For FM and employee customer is Account name
	(Case  IsNull(CustomerType,0)
	  	When 50602  then CustomerFirstName+' '+CustomerLastName	
	  	When 50604  then CustomerFirstName+' '+CustomerLastName		
	 Else
              	BillToAccount   
	 End)AccountName,

	--If it is account(not FM/EMP) Ship address may be different 
	-- so use address from account address
	(Case  IsNull(CustomerType,0)
	   	When 50602  then  CustomerAddress1  	--FM
	   	When 50604  then  CustomerAddress1  	--EMP
	 Else
	      	AccountAddress1
	 End)AccountAddress1,
	
	(Case   IsNull(CustomerType,0)
	 	When 50602  then  CustomerAddress2
	 	When 50604  then  CustomerAddress2  	--EMP
	 Else
	      	AccountAddress2
	 End)AccountAddress2,

	(Case   IsNull(CustomerType,0)
	 	When 50602 then CustomerCity  	--FM
	   	When 50604  then  CustomerCity  	--EMP
	 Else
		AccountCity
	 End)AccountCity,

	(Case  IsNull(CustomerType,0)
	 	When 50602  then  CustomerState	--FM
	    	When 50604  then  CustomerState  	--EMP
	 Else
		AccountProvince
	 End)AccountState,

	(Case  IsNull(CustomerType,0)
	 	When 50602  then  CustomerZip	  	--FM
	    	When 50604  then  CustomerZip  	--EMP
	 Else
		AccountPCode
	 End)AccountZip,

	(Case  IsNull(CustomerType,0)
		When 50602  then CustomerFirstName
		When 50604  then  CustomerFirstName  	--EMP
	Else
		AccountContactFname
	End )ContactFirstName,

	(Case  IsNull(CustomerType,0)
		When 50602  then CustomerLastName
		When 50604  then  CustomerFirstName  	--EMP
	Else
		AccountContactLName
	End)ContactLastName,

	(Case  IsNull(CustomerType,0)
	  	When 50602  then IsNull(CustomerPhone,FMContact)
	 Else
		AccountContactPhone
	 End)ContactPhone,

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
	Case  IsStaffOrder
	When 1 Then 'Y'
	Else 'N'
	End IsStaffOrder,
	(Case IsNull(CALang,'@@')
	 When '@@' then 
		Case IsNull(AccountLang, '##')
		When '##' then 'EN'
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

	Null ParticipantFirstName, 
	Null ParticipantLastName, 
	Null TeacherLastName, 
	Null TeacherFirstName, 
	Null Class,
	Null Recipient,
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
	SpecialInstructions
	
FROM         dbo.PickListNonEnvelope1
WHERE	OrderId = @OrderId
AND		ShipmentGroupID = ISNULL(@ShipmentGroupID, ShipmentGroupID)
And		( ShipmentDate >=  Convert(Varchar(10),@ShipDateFrom,101) And ShipmentDate <= Convert(Varchar(10),@ShipDateTo,101) )
Group By OrderID, 
	DateReceived,
	DateCreated,
	ShipmentDate,
	FMID, 
	FirstName, 
	LastName,
	CampaignID,
	 IsStaffOrder,
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
	--QuantityShipped,
	ReplacedProductCode,
	ReplacedProductQty,
	ShipAccountId,
	OrderTypeCode,
	ShiptoAccount,
	ShipContactFname,
	ShipContactLname,
	FMContact,
	ShipContactPhone,
	BillToAccountId,
	BillToAccount,
	AccountAddress2,
	AccountAddress1,
	AccountCity,
	AccountProvince,
	AccountPCode,
	AccountContactFname,
	AccountContactLName,
	AccountContactPhone,
	ShipmentId,
	ShipmentDate,
	AccountLang,
	CALang,
	Recipient,
	SupporterName,
	CustomerType,
	CustomerFirstName,CustomerLastName,CustomerAddress1,CustomerAddress2,
	CustomerCity,CustomerState,CustomerZip,customerphone,
	SuppliesDeliveryDate,
	RequestedShipDate,
	CarrierID,
	SpecialInstructions
Order By CampaignId, TeacherLastName, ParticipantLastName,ParticipantFirstName
End
GO
