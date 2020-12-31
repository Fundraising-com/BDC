USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Write_APInterface_For_Adjustment]    Script Date: 06/07/2017 09:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Write_APInterface_For_Adjustment](	@AdjustmentId 	Int,
								@Description	Varchar(100),		--For Group refunds 'Refund to Group+GroupId ' will be received
								@CurrencyId	Int,			-- 801 CAD  802 USD
								@RetVal	Int OutPut,
								@ErrorMessage	 Varchar(150) OutPut)
As
Declare @AdjustmentTypeId 	Int,
	@AccountId		Int,
	@OrderId		Int,
	@CampaignId		Int,
	@EffectiveDate		DateTime,
	@Amount		Numeric(10,2),
	@CountryCode		Varchar(10),
	
	@VendorNumber	Varchar(30),
	@VendorSiteName	Varchar(15),
	@VendorPayGroup	Varchar(25),
	@TermsName      	Varchar(15),
	@APDescription		Varchar(240),
	@IsCheckRequired	Varchar(1),

	@CurrencyCode		Varchar(10),
	@InvoiceNumber	Varchar(50),
	@InvoiceType     	Varchar(15),

	 @PrepayLegalEntity 		Varchar(3),
   	 @PrepayNaturalAccount 	Varchar(4),
   	 @PrepaySubAccount 		Varchar(4),
   	 @PrepayProductlineDept 	Varchar(4),
   	 @PrepayLanguageMarket 	Varchar(2),
   	 @PrepayChannel 		Varchar(2),
   	 @PrepaySegment7 		Varchar(3),

	@ErrorFlag 			Varchar(1)

Declare @APInvoiceLineInterface Table(	
	TIndex			Int 	Identity,
	Country			Varchar(10),	
	InvoiceNumber		Varchar(50),
	APDescription		Varchar(240),
	Amount			Numeric(10,2),
	DistLegalEntity     	Varchar(3),
  	DistNaturalAccount  	Varchar(4),
	DistSubAccount      	Varchar(4),
	DistProductLineDept	Varchar(4),
	DistLanguageMarket 	Varchar(2),
	DistChannel          	Varchar(2),
	DistSegment7         	Varchar(3),
	PrepayLegalEntity 	Varchar(3),
   	PrepayNaturalAccount 	Varchar(4),
   	PrepaySubAccount 	Varchar(4),
   	PrepayProductlineDept 	Varchar(4),
   	PrepayLanguageMarket 	Varchar(2),
   	PrepayChannel 		Varchar(2),
   	PrepaySegment7 	Varchar(3)
					)

--Validate Adjustment Id
If IsNull(@AdjustmentId,0) > 0  
Begin

	Set @ErrorFlag = 'N'

	-- Set Currency Id to default CAD
	Select @CurrencyCode =
	   Case IsNull(@CurrencyId,0)
	   When  	0  	Then  'CAD'
	   When  	801 	Then  'CAD'
	   When		802	Then  'USD'
	   Else
		'###'
	   End

	If @CurrencyCode = '###'
	 Begin
	     Set  @RetVal = 1
	     Set @ErrorMessage = 'Invalid currency code for Adjustment Id '+Cast(@AdjustmentId As Varchar)+ ' in procedure dbo.Write_APInterface_For_Adjustment'
	     Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com,qsp-finance-canada@qsp.com',
							'AP Creation error for Group Refund',@ErrorMessage
	     Return
	  End

	Select   @AdjustmentTypeId 	=  AdjT.Adjustment_Type_Id,
	  	@AccountId		=  Account_Id,
	   	@OrderId		= Order_Id,
		@CampaignId		= Campaign_Id,
	   	@EffectiveDate		= Adjustment_Effective_Date,
	   	@Amount		= Abs(Adjustment_Amount), --Adjustment has negative amount for Group Refunds
	   	@CountryCode		= Country_Code,
	   	@IsCheckRequired	= IsNull(Upper(AdjT.Is_Cheque_required),'@')
	   From QSPCanadaFinance.dbo.Adjustment Adj,  QSPCanadaFinance.dbo.Adjustment_Type  AdjT
	   Where Adj.Adjustment_Type_id = AdjT.Adjustment_Type_id 
	   And adj.Adjustment_Id = @AdjustmentId

	  If @@Error <> 0  Or @@Rowcount <> 1
	  Begin
	     Set  @RetVal = 1
	     Set @ErrorMessage = 'Error While Checking Adjustment type for Adjustment Id '+Cast(@AdjustmentId As Varchar)+ ' in procedure dbo.Write_APInterface_For_Adjustment'
	      Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com,qsp-finance-canada@qsp.com',
							'AP Creation error for Group Refund',@ErrorMessage
	     Return
	  End

	-- Only Insert AP interface record if the adjustment would result issuance of cheque
	If  @IsCheckRequired <> 'Y'
	Begin
	     Set  @RetVal = 1
	     Set @ErrorMessage = 'Invalid Adjustment Type (for Adjustment Id '+Cast(@AdjustmentId As Varchar)+ ') for  AP interface record, error in procedure dbo.Write_APInterface_For_Adjustment'
	     Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com,qsp-finance-canada@qsp.com',
							'AP Creation error for Group Refund',@ErrorMessage
	     Return
	End
	
	--Only non zero possitive amounts
	If  ( IsNull(@Amount ,0) <= 0   )
	Begin
	     Set  @RetVal = 1
	     Set @ErrorMessage = 'Invalid Adjustment Amount for Adjustment Id '+Cast(@AdjustmentId As Varchar)+ ', error in procedure dbo.Write_APInterface_For_Adjustment'
	       Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com,qsp-finance-canada@qsp.com',
							'AP Creation error for Group Refund',@ErrorMessage
	     Return
	End

	-- Get Vendor Information
	If IsNull(@AccountId,0) > 0
	Begin
	     Select   @VendorNumber	=  VendorNumber,
		    @VendorSiteName	= VendorSiteName,
		    @VendorPayGroup	= VendorPayGroup
	     From QSPCanadaCommon.dbo.CAccount
	     Where Id = @AccountId
	End   
	Else
	Begin
	   Set  @RetVal = 1
	   Set @ErrorMessage = 'Invalid Account Id for Adjustment Id '+Cast(@AdjustmentId As Varchar)+ ' error in procedure dbo.Write_APInterface_For_Adjustment'
	   Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com,qsp-finance-canada@qsp.com',
							'AP Creation error for Group Refund',@ErrorMessage
	   Return
	End

	--Validate vendor information
	If  ( ( IsNull(@VendorNumber ,'$') = '$' ) Or ( IsNull(@VendorSiteName ,'$') = '$' ) Or ( IsNull(@VendorPayGroup ,'$') = '$' ) ) 
	Begin
	   Set  @RetVal = 1
	   Set @ErrorMessage = 'Invalid vendor info for Adjustment Id '+Cast(@AdjustmentId As Varchar)+ ', error in procedure dbo.Write_APInterface_For_Adjustment'
	   Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com,qsp-finance-canada@qsp.com',
							'AP Creation error for Group Refund',@ErrorMessage
	   Return
	End

	--Generate description text
	--Set @APDescription = IsNull(@Description, '') + ' CA '+Cast(@CampaignId As Varchar)+ ' ORDER '+Cast(@OrderId As Varchar)	
	--MS March 09, 2007 
	Set @APDescription = IsNull(@Description, '') + ' CA '+Cast(@CampaignId As Varchar)+ ' ORDER '+ Case  Cast(Isnull(@OrderId,0) As Varchar)  When 0 Then 'N/A' Else Cast(@OrderId As Varchar) End	

	Set @InvoiceNumber = Cast(@AccountId As Varchar) + '-' +Cast(@AdjustmentId As Varchar)
	Set @InvoiceType =  'STANDARD'
	Set @TermsName  =  'IMMEDIATE'


	 Set @PrepayLegalEntity 	=  '062'
   	 Set @PrepayNaturalAccount 	=  '1656'
   	 Set @PrepaySubAccount 	=  '0000'
   	 Set @PrepayProductlineDept 	=  '0000'
   	 Set @PrepayLanguageMarket 	=  '00'
   	 Set @PrepayChannel 		=  '00'
   	 Set @PrepaySegment7 		=  '000'

	Begin Transaction 

	--Insert AP Invoice interface record
	Insert Into QSPOracleInterface.dbo.OM_TBL_AP_INVOICES_INTERFACE_REFUND_STAGING(  --Should be inserted in staging table  first MS May 8, 2005
	--Insert Into QSPOracleInterface.dbo.Om_Tbl_AP_Invoices_Interface (
									  Country_Code, 
									  Invoice_Num, 
									  Invoice_Type, 
								 	  Invoice_Date,
									  Invoice_Amount,
									  Invoice_Currency_code,
									  Terms_Name, 
								  	  Pay_Group_Lookup_Code,
									  Description
									 )
								Values	(@CountryCode,
									 @InvoiceNumber, 
								  	 @InvoiceType, 
									 @EffectiveDate,
									 @Amount, 
									 @CurrencyCode,
									 @TermsName, 
									 @VendorPayGroup, 
									 @APDescription
									) 

	If @@Error <> 0 Or @@RowCount <> 1
	Begin
	   Set  @ErrorFlag = 'Y'
	   Set @ErrorMessage = 'Unable to Insert into AP Invoice Interface Table for '+Cast(@AdjustmentId As Varchar)+ ', error in procedure dbo.Write_APInterface_For_Adjustment'
	     Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com', 'AP Creation error for Group Refund',@ErrorMessage
	End	

	If @ErrorFlag <>  'Y'
	Begin

	--Get All credit entries and insert into Invoice Line interface table
	Insert into @APInvoiceLineInterface
	Select   @CountryCode,	
		@InvoiceNumber,
		@APDescription	,
		@Amount,
	   	Substring(Trans.GL_ACCOUNT_NUMBER,1,3), 
		Substring(Trans.GL_ACCOUNT_NUMBER,5,4), 
		Substring(Trans.GL_ACCOUNT_NUMBER,10,4),
	  	Substring(Trans.GL_ACCOUNT_NUMBER,15,4),
	  	Substring(Trans.GL_ACCOUNT_NUMBER,20,2),
		Substring(Trans.GL_ACCOUNT_NUMBER,23,2),
		Substring(Trans.GL_ACCOUNT_NUMBER,26,4),
		@PrepayLegalEntity,
	  	@PrepayNaturalAccount ,
   	 	@PrepaySubAccount,
   	 	@PrepayProductlineDept,
   	 	@PrepayLanguageMarket,
   		@PrepayChannel ,
   	 	@PrepaySegment7 		
	From  QSPCanadaFinance.dbo.GL_ENTRY E , QSPCanadaFinance.dbo.Gl_Transaction Trans 
	Where E.Gl_Entry_Id = Trans.Gl_Entry_Id 
	And   Trans.Debit_Credit = 'C' 
	And   E.Adjustment_Id = @AdjustmentId
	
	--Insert AP detail
		Insert Into QSPOracleInterface.dbo.OM_TBL_AP_INV_LINES_INTERFACE_REFUND_STAGING     --Should be inserted in staging table  first MS May 8, 2005
		--Insert Into QSPOracleInterface.dbo.Om_Tbl_AP_Inv_Lines_Interface
		Select  	@CountryCode,	
			@InvoiceNumber,
			Tindex,
			@APDescription	,
			@Amount,
			DistLegalEntity,
  			DistNaturalAccount,
			DistSubAccount,
			DistProductLineDept,
			DistLanguageMarket,
			DistChannel,
			DistSegment7,
			PrepayLegalEntity,	
			PrepayNaturalAccount,
   			PrepaySubAccount,
   			PrepayProductlineDept,
   			PrepayLanguageMarket,
   			PrepayChannel ,
   			PrepaySegment7 	
		From 	@APInvoiceLineInterface						
	End

	If @@Error <> 0 
	Begin
	   Set  @ErrorFlag = 'Y'
	   Set @ErrorMessage = 'Unable to Insert into AP Invoice Line Interface Table for '+Cast(@AdjustmentId As Varchar)+ ', error in procedure dbo.Write_APInterface_For_Adjustment'
	   Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com', 'AP Creation error for Group Refund',@ErrorMessage
	End	

	If @ErrorFlag <>  'Y'
	Begin
	--Insert Vendor record
	Insert Into QSPOracleInterface.dbo.OM_TBL_PO_VENDORS_INTERFACE_REFUND_STAGING(		--Should Be inserted in staging table  first MS May 8, 2005
	--Insert Into QSPOracleInterface.dbo.Om_Tbl_PO_Vendors_Interface(
			Country_code, 
			Invoice_num, 
			Vendor_number, 
			Vendor_site_name, 
			Vendor_name, 
			Address_line1,
			Address_line2, 
			City, 
			Province, 
			State, 
			County, 
			Postal_code, 
			Country, 
			Vendor_type_lookup_code, 
			Liab_legal_entity,
			Liab_natural_account,
			Liab_sub_account, 
			Liab_product_line_dept, 
			Liab_language_market,
			Liab_channel, Liab_segment7
					)
		Values	(@CountryCode,
			 @InvoiceNumber, 
			 @VendorNumber,
			 @VendorSiteName,
			 Null,
			 Null,
			 Null,
			 Null,
			 Null,
			 Null, 			
			 Null, 			
			 Null,
			 Null,
			 Null,
			 Null,
  			 Null,
			 Null,
			 Null,
			 Null,
			 Null,
			 Null
			)

	
	If @@Error <> 0 Or @@RowCount <> 1
	Begin
	   Set  @ErrorFlag = 'Y'
	   Set @ErrorMessage = 'Unable to Insert into PO Vendor Intterface Table for '+Cast(@AdjustmentId As Varchar)+ ', error in procedure dbo.Write_APInterface_For_Adjustment'
	    Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com', 'AP Creation error for Group Refund',@ErrorMessage
	End	

	End

	If @ErrorFlag  <>  'Y'
	Begin

		Set @RetVal = 0
		Commit
	End
	If @ErrorFlag  =  'Y'
	Begin

		Set @RetVal = 1
		Rollback
	End
End	--Validate AdjustemntId
Else
-- If Invalid Adjustmnent Id value is passed
Begin

    Set  @RetVal = 1
    Set @ErrorMessage = 'Invalid  Adjustment Id , error in procedure dbo.Write_APInterface_For_Adjustment'
     Exec QSPCanadaCommon.dbo.Send_EMAIL 	'APGeneration@QSP.com','qsp-IT-canada@qsp.com', 'AP Creation error for Group Refund',@ErrorMessage
End
GO
