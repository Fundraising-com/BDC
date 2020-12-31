USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[PaymentAllocation]    Script Date: 06/07/2017 09:17:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[PaymentAllocation] ( @TransTypeId	  	Int ,
					           @Entity		   	Int ,
					           @GlobalAmount		Numeric(10,2),
					           @GlEntryModelId    	Int,
		 			           @GlEntryTypeId     	Int ,
					           @TaxId                  	Int,
					           @GlAccountTaxPayable 	Varchar(50),
					           @EffectiveDate	         	DateTime,
					           @ErrorCode	   	 Int OutPut)
  	         	           
As
	Set NoCount On
	Declare	@RowCount			Int,
		@RowCount1			Int,
		@GlEntryProductLineId		Int,
		@AppProductLineId		Int,
		@SourceId			Varchar(50),
		@MaxId			Int,
		@Cnt				Int,
		@Cnt1				Int,
		@AccountNumber		Varchar(50),
		@DebitCredit			Varchar(20),
		@ProductLineAmount		Numeric(10,2), 
		@ErrorCode1			Int,
		@ProductTaxId			Varchar(20),
		@GlAccountTaxPayableForTax 	Varchar(50),
		@TaxAmount			Numeric(10,2),
		@TotalApplicablePLAmount	Numeric(10,2),
		@TotalARAmount		Numeric(10,2),
		@ApplicablePLAmount		Numeric(10,2),
		@GlEntryModelIdTax		Numeric(10,2),
		@AllocatedAmount		Numeric(10,2),		
		@MiscChargesId		Int,
		@MiscAmount			Numeric(10,2)
		
	Declare @GL_Transaction_PL Table(
			TIndex		Int Identity,
			ProductLineId	Int,
			GLAccount	Varchar(50),
			DebitCredit	Varchar(20)
					)

	Declare @Applicable_PL Table(
			TIndex		Int Identity,
			ProductLineId	Int,
			Amount		Numeric(10,2)
					)

	Declare @AR_Table Table(
			TIndex		Int Identity,
			ProductLineId	Int,
			Amount		Numeric(10,2)
					)

	Declare @Tax_Table  Table	(
			TIndex		Int Identity,
			TaxId		Int,
			Amount		Numeric(10,2)
					)

	Declare @MiscCharges_Table  Table	(
			TIndex		Int Identity,
			MiscChargeId	Int,
			Amount		Numeric(10,2)
					)
Begin

	Set @Entity = 62
	--Copy Product Line entries into AR temp table variable
	Insert Into @AR_Table
		Select  Source_Id, Amount 
		From QSPCanadaFinance.dbo.GL_Data_Cache  
		Where Source_Type = 'PRODUCT_LINE' 


	If @@Rowcount =0 or @@Error <> 0
	Begin
		Print 'No product Line entries found in GL data cache (PaymentAllocation)'
		Set @ErrorCode = 1
		Return
	End

	-- Add Tax entries to Product Line amount in AR table ------------------------------------------------------------------------------------------------------------------------------------------------------
	Insert Into @Tax_Table
		Select  Source_Id, Amount 
		From QSPCanadaFinance.dbo.GL_Data_Cache  
		 Where Source_Type = 'TAX'

	Select  @RowCount= Count(*) From @Tax_Table
	
	While @RowCount > 0
	Begin

		Select @ProductTaxId = TaxId, @TaxAmount=Amount From @Tax_Table

	 	Select  	@GlAccountTaxPayableForTax = Gl_Account_Tax_Payable, @GlEntryModelIdTax = Gl_Entry_Model_Id
		From QSPCanadaFinance.dbo.Gl_Entry_Model
		Where Entity_Id = @Entity
		And Tax_Id =@ProductTaxId	-- Tax Id 
		And Effective_Date = 	(Select Max(Effective_Date)
					 From QSPCanadaFinance.dbo.Gl_Entry_Model	
					Where Entity_Id = @Entity
					And Tax_Id =@ProductTaxId
					And Effective_Date <= GetDate()	         
					)
		
		If @@RowCount <> 1 Or @@Error <> 0
                           Begin
		      Print 'GL entry model Id for Tax not found (Payment Allocation)'
		      Set @ErrorCode = 1
		      Return 
		End	

		--Determine  applicable  product lines For Tax for Receivable

		Delete From @Applicable_PL
		
		Set @TotalApplicablePLAmount = 0

		Select  @RowCount1= Count(*) From @AR_Table
	
		While @RowCount1 > 0
		Begin

			Select @SourceId=ProductLineId, @ProductLineAmount=Amount  From @AR_Table where Tindex=@RowCount1

			Select @GlEntryProductLineId = Gl_Entry_Product_Line_Id 
			From QSPCanadaFinance.dbo.Gl_Entry_Product_Line
			Where Gl_Entry_Model_Id = @GlEntryModelIdTax  And QSP_Product_Line_id = @SourceId


			--If there is a entry model exists for product line for tax 
			If @@RowCount > 0
			Begin
				Insert Into @Applicable_PL(ProductLineId	, Amount)
						Values      (@SourceId, @ProductLineAmount)
			End

		Set @RowCount1 = @RowCount1 -1
		End

		Select @TotalApplicablePLAmount = Sum(Amount) From @Applicable_PL

		-- Loop through all applicable product lines to allocate Tax amount by product line
		 
		Select @RowCount1 = Count(*) From @Applicable_PL
		While @RowCount1 > 0
		Begin
			Select @AppProductLineId = ProductLineId, @ApplicablePLAmount= Amount From @Applicable_PL Where Tindex = @RowCount1

			If @@RowCount <> 0
			Begin
				Set @AllocatedAmount = @TaxAmount*@ApplicablePLAmount/@TotalApplicablePLAmount
			End

			Update @AR_Table
			Set Amount = Amount + @AllocatedAmount
			Where ProductLineId = @AppProductLineId

		Set @RowCount1 = @RowCount1 -1
		End
		

	Set @RowCount = @RowCount -1
	End	---------------------------------------------------------------------------------------------  Tax  End---------------------------------------------------------------------------------------------------------------

	--Process Misc Charges Total

	Set @TotalApplicablePLAmount = 0 
	Set @AllocatedAmount = 0

	Insert Into @MiscCharges_Table
		Select  Source_Id, Amount 
		From QSPCanadaFinance.dbo.GL_Data_Cache  
		 Where Source_Type = 'MISC_CHARGES'

	Select  @RowCount= Count(*) From @MiscCharges_Table

	While @RowCount > 0
	Begin

		Select @MiscChargesId = MiscChargeId, @MiscAmount=Amount From @MiscCharges_Table Where TIndex=@RowCount

		Select   	@GlEntryModelId  = Gl_Entry_Model_Id
		From QSPCanadaFinance.dbo.Gl_Entry_Model
		Where Entity_Id = @Entity
		And Transaction_Type_Id =@MiscChargesId
		And Effective_Date = 	(Select Max(Effective_Date)
					 From QSPCanadaFinance.dbo.Gl_Entry_Model	
					Where Entity_Id = @Entity
					And Transaction_Type_Id =@MiscChargesId
					And Effective_Date <= GetDate()	         
					)
		
		If @RowCount <> 1 Or @@Error <> 0
		Begin
			-- GL model for Misc charges not found
			 Print 'GL model for Misc charges not found in Payment Allocation '
			Set @ErrorCode =1
			Return
		End	

		Delete From @Applicable_PL
		Set @TotalApplicablePLAmount = 0


		Select  @RowCount1= Count(*) From @AR_Table
	
		While @RowCount1 > 0
		Begin

			Select @SourceId=ProductLineId, @ProductLineAmount=Amount  From @AR_Table Where Tindex = @RowCount1

			Select @GlEntryProductLineId = Gl_Entry_Product_Line_Id 
			From QSPCanadaFinance.dbo.Gl_Entry_Product_Line
			Where Gl_Entry_Model_Id = @GlEntryModelId  And QSP_Product_Line_id = @SourceId


			--If there is a entry model exists for product line for Misc Charges
			If @@RowCount > 0
			Begin
				Insert Into @Applicable_PL(ProductLineId	, Amount)
						Values      (@SourceId, @ProductLineAmount)
			End

		Set @RowCount1 = @RowCount1 -1
		End



		Select @TotalApplicablePLAmount = Sum(Amount) From @Applicable_PL

		Select @RowCount1 = Count(*),  @MaxId=Max(Tindex) From @Applicable_PL
		Set @Cnt = 0
		While @RowCount1 > 0
		Begin

			Select @AppProductLineId = ProductLineId, @ApplicablePLAmount= Amount From @Applicable_PL Where Tindex = @MaxId- @Cnt

			If @@RowCount <> 0
			Begin

				Set @AllocatedAmount = @MiscAmount*@ApplicablePLAmount/@TotalApplicablePLAmount
			End

			Update @AR_Table
			Set Amount = Amount + IsNull(@AllocatedAmount,0)
			Where ProductLineId = @AppProductLineId
						
		Set @Cnt = @Cnt+1
		Set @RowCount1 = @RowCount1 -1
		End


	Set @RowCount = @RowCount -1

	End	---------------------------------------------------------------------------------------------  Misc Charges  End---------------------------------------------------------------------------------------------------------------
	
	-- Tax and Misc Charges have been added to Product Line total get the GL account for each product line and update

	Set @TotalARAmount = 0
	Set @AllocatedAmount = 0
	Select @RowCount1 = Count(*), @MaxId=Max(Tindex) , @TotalARAmount = Sum(amount) From @AR_Table

	Set @Cnt =0
	While @RowCount1 > 0
	Begin
		Select @SourceId=ProductLineId, @ProductLineAmount=Amount  From @AR_Table Where Tindex = @MaxId - @Cnt

		Select @GlEntryProductLineId = Gl_Entry_Product_Line_Id 
		From QSPCanadaFinance.dbo.Gl_Entry_Product_Line
		Where Gl_Entry_Model_Id = @GlEntryModelId  And QSP_Product_Line_id = @SourceId

		If @@RowCount > 0
		Begin

			Set @AllocatedAmount = @ProductLineAmount * @GlobalAmount / @TotalARAmount   /*@GlobalAmount 500 for testing*/
		
			Insert Into @GL_Transaction_PL (ProductLineId, GLAccount, DebitCredit )
				Select Gl_Entry_Product_Line_Id , GL_Account_Number, Debit_Credit
				From QSPCanadaFinance.dbo.GL_Transaction_PL Where GL_Entry_Product_Line_Id = @GlEntryProductLineId
		
			Select @RowCount = Count(*) , @MaxId = Max(Tindex) From @GL_Transaction_PL  Where ProductLineId = @GlEntryProductLineId
			Set @Cnt1 = 0
			While @RowCount > 0
			Begin

				Select @AccountNumber = GLAccount, @DebitCredit = DebitCredit  
				From @GL_Transaction_PL   Where ProductLineId = @GlEntryProductLineId And Tindex = @MaxId - @Cnt1

				Exec dbo.Update_GL_Transaction     @AccountNumber,
								       @DebitCredit,
								       @ProductLineAmount,
								       @ErrorCode1 			
		
				If @@Error <> 0 or @ErrorCode1 = 1
                          			Begin
					 Print 'Error executing Update_GL_Transaction in Payment Allocation'
		     			 Set @ErrorCode = 1
		     			 Return 
				End
			Set @Cnt1 = @Cnt1+1
			Set @RowCount = @RowCount -1
			End
		End

	Set @Cnt = @Cnt+1
	Set @RowCount1 = @RowCount1 -1
	End
	 Set @ErrorCode = 0
End
GO
