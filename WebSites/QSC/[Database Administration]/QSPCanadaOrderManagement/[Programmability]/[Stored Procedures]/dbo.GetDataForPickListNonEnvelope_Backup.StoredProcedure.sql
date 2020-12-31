USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetDataForPickListNonEnvelope_Backup]    Script Date: 06/07/2017 09:19:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetDataForPickListNonEnvelope_Backup] 
	@OrderID		Int,
	@BatchId		Int,
	@BatchDate		DateTime,
	@ShipDateFrom		DateTime,
	@ShipDateTo		DateTime,
	@ReportType		Int  -- PackingSlip(2) or PickList(1)
As
Declare @SelectString 		Varchar(8000),
	@GroupByString 	Varchar(4000),
	@WhereShipdate	Varchar(500)

If  (IsNull(@ShipDateFrom,  ' ') = ' '  And IsNull(@ShipDateTo,' ') = ' ') Or (@ShipDateFrom is Null And @ShipDateTo Is Null)
Begin
	--Set  @WhereShipdate = ' And ShipmentDate >= '+''''+Convert(Varchar(10),@ShipDateFrom,101)+''''+'  And  ShipmentDate <= '+''''+ Convert(Varchar(10),@ShipDateTo,101) +''''

Select @ReportType  ReportType, 
	 (Case  OrderTypeCode
	  When 41002 Then  -- CAFS
		Case IsNull(ShipAccountId,0) 
		When 0  then ShiptoFMId
		else
			ShipAccountId
		End
	  When 41006 then ShiptoFMId	--FM
	  When 41007 then ShiptoFMId	--FMBULK
	 -- Account Id 1 (QSP) will be used for EMP and POS orders
	  When 41005 then		--EMP
		Case IsNull(ShipAccountId,0) 
		When 0  then  BillToAccountId 
		else
			ShipAccountId
		End
	When 41010 then		--POS
		Case IsNull(ShipAccountId,0) 
		When 0  then BillToAccountId 
		else
			ShipAccountId
		End
          Else
              ShipAccountId   
	  End)ShipAccountId,
	
	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FirstName+' '+LastName
		else
		ShiptoAccount
		End
	   When 41006 then FirstName+' '+LastName  	--FM
	   When 41007 then FirstName+' '+LastName	--FMBULK
	   When 41005 then Recipient			--EMP 
	   When 41010 then Recipient			--POS
	   Else
		ShiptoAccount
	   End)ShipAccountName,

	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMAddress1
		else
		ShipAccountAddress1
		End
	   When 41006 then FMAddress1  	--FM
	   When 41007 then FMAddress1		--FMBULK
	   When 41005 then QSPAccShipToAddress1	
	   When 41010 then QSPAccShipToAddress1
	   Else
	      ShipAccountAddress1
	   End)ShipAccountAddress1,

	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMAddress2
		else
		ShipAccountAddress2
		End
	  When 41006 then FMAddress2  	--FM
	   When 41007 then FMAddress2	--FMBULK
                When 41005 then QSPAccShipToAddress2	
	   When 41010 then QSPAccShipToAddress2
	   Else
		ShipAccountAddress2
	   End)ShipAccountAddress2,
	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMCity
		else
		ShipAccountCity
		End
	  When 41006 then FMCity  	--FM
	   When 41007 then FMCity	--FMBULK
	   When 41005 then QSPAccShiptoCity	
	   When 41010 then QSPAccShiptoCity
	   Else
		ShipAccountCity
	   End)ShipAccountCity,
	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMProvince
		else
		ShipAccountProvince
		End
	   When 41006 then FMProvince  	--FM
	   When 41007 then FMProvince	--FMBULK
	   When 41005 then QSPAccShiptoProvince	
	   When 41010 then QSPAccShiptoProvince
	   Else
		ShipAccountProvince
	   End)ShipAccountState,

	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMPCode
		else
		ShipAccountPCode
		End
	   When 41006 then FMPCode  	--FM
	   When 41007 then FMPCode	--FMBULK
	   When 41005 then QSPAccShiptoPcode
	   When 41010 then QSPAccShiptoPcode
	   Else
		ShipAccountPCode
	   End)ShipPCode,

	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FirstName+' '+LastName
		else
		ShipContactFname+' '+ShipContactLName
		End
	   When 41006 then FirstName+' '+LastName  	--FM
	   When 41007 then FirstName+' '+LastName	--FMBULK
	   When 41005 then Recipient			--EMP 
	   When 41010 then Recipient			--POS
	   Else
		ShipContactFname+' '+ShipContactLName
	   End)ShipAccountContact,
	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FmContact
		else
		ShipContactPhone
		End
	  When 41006 then FmContact  	--FM
	  When 41007 then FmContact	--FMBULK
	   Else
		ShipContactPhone
	   End)ShipAccountContactPhone,

	(Case  OrderTypeCode
	  When 41006  then BilltoFMId	--FM 
	  When 41007 then BilltoFMId	--FMBULK
	  When 41005 then		--EMP
		Case IsNull(ShipAccountId,0) 
		When 0  then BillToAccountId 
		else
			ShipAccountId
		End
	When 41010 then		--POS
		Case IsNull(ShipAccountId,0) 
		When 0  then BillToAccountId 
		else
			ShipAccountId
		End
		
          Else
              BillToAccountId   
	  End)AccountId,

	(Case  OrderTypeCode
	  When 41006 then FirstName+' '+LastName	--FM
	  When 41007 then FirstName+' '+LastName	--FMBULK
	   When 41005 then SupporterName		--EMP 
	   When 41010 then SupporterName		--POS
          Else
              BillToAccount   
	  End)AccountName,
---------------------------------------------------------------------------------------------------------------------
	(Case  OrderTypeCode
	   When 41006 then FMAddress1  	--FM
	   When 41007 then FMAddress1		--FMBULK
                 When 41005 then QSPAccBillToAddress1	
	   When 41010 then QSPAccBillToAddress1
	   Else
	      AccountAddress1
	   End)AccountAddress1,

	(Case  OrderTypeCode
	   When 41006 then FMAddress2  	--FM
	   When 41007 then FMAddress2		--FMBULK
	   When 41005 then QSPAccBillToAddress2
	   When 41010 then QSPAccBillToAddress2
	   Else
	      AccountAddress2
	   End)AccountAddress2,
	(Case  OrderTypeCode
	   When 41006 then FMCity  	--FM
	   When 41007 then FMCity	--FMBULK
	   When 41005 then QSPAccBillToCity	
	   When 41010 then QSPAccBillToCity
	   Else
		AccountCity
	   End)AccountCity,
	(Case  OrderTypeCode
	   When 41006 then FMProvince  	--FM
	   When 41007 then FMProvince	--FMBULK
	   When 41005 then QSPAccBillToProvince	
	   When 41010 then QSPAccBillToProvince
	   Else
		AccountProvince
	   End)AccountState,

	(Case  OrderTypeCode
	   When 41006 then FMPCode  	--FM
	   When 41007 then FMPCode	--FMBULK
	   When 41005 then QSPAccBillToPcode
	   When 41010 then QSPAccBillToPcode
	   Else
		AccountPCode
	   End)AccountZip,
	(Case  OrderTypeCode
	   When 41006 then FirstName  	--FM
	   When 41007 then FirstName	--FMBULK
	   When 41005 then SupporterName			--EMP 
	   When 41010 then SupporterName			--POS
	   Else
		AccountContactFname
	   End)ContactFirstName,
	(Case  OrderTypeCode
	   When 41006 then LastName  	--FM
	   When 41007 then LastName	--FMBULK
	   When 41005 then Null			--EMP 
	   When 41010 then Null			--POS
	   Else
		AccountContactLName
	   End)ContactLastName,
	(Case  OrderTypeCode
	  When 41006 then FmContact  	--FM
	  When 41007 then FmContact	--FMBULK
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
	Quantity,
	(Case @ReportType 
		 When 2 Then
			Case StatusInstance
			When 508 Then QuantityShipped --Item Shipped
			Else
				0
			End
		When 1 Then
			Case StatusInstance
			When 500 Then Quantity	      -- Good /Approved
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
	Null Class
FROM         dbo.PickListForNonEnvelope
WHERE OrderId = @OrderId
And Id  = @BatchId 
And Date = @BatchDate 
Group By OrderID, 
	DateReceived,
	DateCreated,
	ShipmentDate,
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
	Quantity,
	QuantityShipped,
	ReplacedProductCode,
	ReplacedProductQty,
	ShiptoFMId,
	ShipAccountId,
	OrderTypeCode,
	ShiptoAccount,
	FMAddress1,
	ShipAccountAddress1,
	FMAddress2,
	ShipAccountAddress2,
	FmCity,
	ShipAccountCity,
	FMProvince,
	ShipAccountProvince,
	FMPCode,
	ShipAccountPCode,
	ShipContactFname,
	ShipContactLname,
	FMContact,
	ShipContactPhone,
	BilltoFMId,
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
	QSPAccShipToAddress1,
	QSPAccShipToAddress2,
	QSPAccShiptoCity,
	QSPAccShiptoProvince,
	QSPAccShiptoPcode,
	SupporterName,
	QSPAccBillToAddress1,
	QSPAccBillToAddress2,
	QSPAccBillToCity,
	QSPAccBillToProvince,
	QSPAccBillToPcode
End
If  Len(@ShipDateFrom) > 0   And len(@ShipDateTo) > 0 -- Is Not Null
Begin
	--Set  @WhereShipdate = ' And ShipmentDate >= '+''''+Convert(Varchar(10),@ShipDateFrom,101)+''''+'  And  ShipmentDate <= '+''''+ Convert(Varchar(10),@ShipDateTo,101) +''''

Select @ReportType  ReportType, 
	 (Case  OrderTypeCode
	  When 41002 Then  -- CAFS
		Case IsNull(ShipAccountId,0) 
		When 0  then ShiptoFMId
		else
			ShipAccountId
		End
	  When 41006 then ShiptoFMId	--FM
	  When 41007 then ShiptoFMId	--FMBULK
	 -- Account Id 1 (QSP) will be used for EMP and POS orders
	  When 41005 then		--EMP
		Case IsNull(ShipAccountId,0) 
		When 0  then  BillToAccountId 
		else
			ShipAccountId
		End
	When 41010 then		--POS
		Case IsNull(ShipAccountId,0) 
		When 0  then BillToAccountId 
		else
			ShipAccountId
		End
          Else
              ShipAccountId   
	  End)ShipAccountId,
	
	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FirstName+' '+LastName
		else
		ShiptoAccount
		End
	   When 41006 then FirstName+' '+LastName  	--FM
	   When 41007 then FirstName+' '+LastName	--FMBULK
	   When 41005 then Recipient			--EMP 
	   When 41010 then Recipient			--POS
	   Else
		ShiptoAccount
	   End)ShipAccountName,

	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMAddress1
		else
		ShipAccountAddress1
		End
	   When 41006 then FMAddress1  	--FM
	   When 41007 then FMAddress1		--FMBULK
	   When 41005 then QSPAccShipToAddress1	
	   When 41010 then QSPAccShipToAddress1
	   Else
	      ShipAccountAddress1
	   End)ShipAccountAddress1,

	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMAddress2
		else
		ShipAccountAddress2
		End
	  When 41006 then FMAddress2  	--FM
	   When 41007 then FMAddress2	--FMBULK
                When 41005 then QSPAccShipToAddress2	
	   When 41010 then QSPAccShipToAddress2
	   Else
		ShipAccountAddress2
	   End)ShipAccountAddress2,
	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMCity
		else
		ShipAccountCity
		End
	  When 41006 then FMCity  	--FM
	   When 41007 then FMCity	--FMBULK
	   When 41005 then QSPAccShiptoCity	
	   When 41010 then QSPAccShiptoCity
	   Else
		ShipAccountCity
	   End)ShipAccountCity,
	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMProvince
		else
		ShipAccountProvince
		End
	   When 41006 then FMProvince  	--FM
	   When 41007 then FMProvince	--FMBULK
	   When 41005 then QSPAccShiptoProvince	
	   When 41010 then QSPAccShiptoProvince
	   Else
		ShipAccountProvince
	   End)ShipAccountState,

	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FMPCode
		else
		ShipAccountPCode
		End
	   When 41006 then FMPCode  	--FM
	   When 41007 then FMPCode	--FMBULK
	   When 41005 then QSPAccShiptoPcode
	   When 41010 then QSPAccShiptoPcode
	   Else
		ShipAccountPCode
	   End)ShipPCode,

	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FirstName+' '+LastName
		else
		ShipContactFname+' '+ShipContactLName
		End
	   When 41006 then FirstName+' '+LastName  	--FM
	   When 41007 then FirstName+' '+LastName	--FMBULK
	   When 41005 then Recipient			--EMP 
	   When 41010 then Recipient			--POS
	   Else
		ShipContactFname+' '+ShipContactLName
	   End)ShipAccountContact,
	(Case  OrderTypeCode
	  When 41002 Then -- CAFS
		Case IsNull(ShipAccountId,0)
		When 0  then FmContact
		else
		ShipContactPhone
		End
	  When 41006 then FmContact  	--FM
	  When 41007 then FmContact	--FMBULK
	   Else
		ShipContactPhone
	   End)ShipAccountContactPhone,

	(Case  OrderTypeCode
	  When 41006  then BilltoFMId	--FM 
	  When 41007 then BilltoFMId	--FMBULK
	  When 41005 then		--EMP
		Case IsNull(ShipAccountId,0) 
		When 0  then BillToAccountId 
		else
			ShipAccountId
		End
	When 41010 then		--POS
		Case IsNull(ShipAccountId,0) 
		When 0  then BillToAccountId 
		else
			ShipAccountId
		End
		
          Else
              BillToAccountId   
	  End)AccountId,

	(Case  OrderTypeCode
	  When 41006 then FirstName+' '+LastName	--FM
	  When 41007 then FirstName+' '+LastName	--FMBULK
	   When 41005 then SupporterName		--EMP 
	   When 41010 then SupporterName		--POS
          Else
              BillToAccount   
	  End)AccountName,
---------------------------------------------------------------------------------------------------------------------
	(Case  OrderTypeCode
	   When 41006 then FMAddress1  	--FM
	   When 41007 then FMAddress1		--FMBULK
                 When 41005 then QSPAccBillToAddress1	
	   When 41010 then QSPAccBillToAddress1
	   Else
	      AccountAddress1
	   End)AccountAddress1,

	(Case  OrderTypeCode
	   When 41006 then FMAddress2  	--FM
	   When 41007 then FMAddress2		--FMBULK
	   When 41005 then QSPAccBillToAddress2
	   When 41010 then QSPAccBillToAddress2
	   Else
	      AccountAddress2
	   End)AccountAddress2,
	(Case  OrderTypeCode
	   When 41006 then FMCity  	--FM
	   When 41007 then FMCity	--FMBULK
	   When 41005 then QSPAccBillToCity	
	   When 41010 then QSPAccBillToCity
	   Else
		AccountCity
	   End)AccountCity,
	(Case  OrderTypeCode
	   When 41006 then FMProvince  	--FM
	   When 41007 then FMProvince	--FMBULK
	   When 41005 then QSPAccBillToProvince	
	   When 41010 then QSPAccBillToProvince
	   Else
		AccountProvince
	   End)AccountState,

	(Case  OrderTypeCode
	   When 41006 then FMPCode  	--FM
	   When 41007 then FMPCode	--FMBULK
	   When 41005 then QSPAccBillToPcode
	   When 41010 then QSPAccBillToPcode
	   Else
		AccountPCode
	   End)AccountZip,
	(Case  OrderTypeCode
	   When 41006 then FirstName  	--FM
	   When 41007 then FirstName	--FMBULK
	   When 41005 then SupporterName			--EMP 
	   When 41010 then SupporterName			--POS
	   Else
		AccountContactFname
	   End)ContactFirstName,
	(Case  OrderTypeCode
	   When 41006 then LastName  	--FM
	   When 41007 then LastName	--FMBULK
	   When 41005 then Null			--EMP 
	   When 41010 then Null			--POS
	   Else
		AccountContactLName
	   End)ContactLastName,
	(Case  OrderTypeCode
	  When 41006 then FmContact  	--FM
	  When 41007 then FmContact	--FMBULK
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
	Quantity,
	(Case @ReportType 
		 When 2 Then
			Case StatusInstance
			When 508 Then QuantityShipped --Item Shipped
			Else
				0
			End
		When 1 Then
			Case StatusInstance
			When 500 Then Quantity	      -- Good /Approved
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
	Null Class
FROM         dbo.PickListForNonEnvelope
WHERE OrderId = @OrderId
And Id  = @BatchId 
And Date = @BatchDate 
And ( ShipmentDate >=  Convert(Varchar(10),@ShipDateFrom,101) And ShipmentDate <= Convert(Varchar(10),@ShipDateTo,101) )
Group By OrderID, 
	DateReceived,
	DateCreated,
	ShipmentDate,
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
	Quantity,
	QuantityShipped,
	ReplacedProductCode,
	ReplacedProductQty,
	ShiptoFMId,
	ShipAccountId,
	OrderTypeCode,
	ShiptoAccount,
	FMAddress1,
	ShipAccountAddress1,
	FMAddress2,
	ShipAccountAddress2,
	FmCity,
	ShipAccountCity,
	FMProvince,
	ShipAccountProvince,
	FMPCode,
	ShipAccountPCode,
	ShipContactFname,
	ShipContactLname,
	FMContact,
	ShipContactPhone,
	BilltoFMId,
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
	QSPAccShipToAddress1,
	QSPAccShipToAddress2,
	QSPAccShiptoCity,
	QSPAccShiptoProvince,
	QSPAccShiptoPcode,
	SupporterName,
	QSPAccBillToAddress1,
	QSPAccBillToAddress2,
	QSPAccBillToCity,
	QSPAccBillToProvince,
	QSPAccBillToPcode
End
GO
