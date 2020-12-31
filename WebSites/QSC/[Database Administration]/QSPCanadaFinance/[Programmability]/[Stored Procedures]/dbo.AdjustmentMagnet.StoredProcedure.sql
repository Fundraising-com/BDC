USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AdjustmentMagnet]    Script Date: 06/07/2017 09:17:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[AdjustmentMagnet] (	@TransTypeId	  	Int ,
						@Entity	   		Int ,
						@GlEntryModelId    	Int,
						@Amount		Numeric(10,2),
						@ErrorCode	      	Int OutPut)
  	         	           
As
	Set NoCount On
	Declare	@RowCount			Int,
		@GlEntryProductLineId		Int,
		@DataCacheMaxId1		Int,
		@Cnt1				Int,
		@AccountNumber		Varchar(50),
		@DebitCredit			Varchar(20),
		@GLTransCount		Int,
		@ErrorCode1			Int,
		@ErrMessage			Varchar(200)


	Declare @GL_Transaction_PL Table(
			TIndex		Int Identity,
			ProductLineId	Int,
			GLAccount	Varchar(50),
			DebitCredit	Varchar(20)
					)

Begin
	
	Set @Entity =62

	Select @GlEntryProductLineId = Gl_Entry_Product_Line_Id    
	From QSPCanadaFinance.dbo.Gl_Entry_Product_Line
	Where Gl_Entry_Model_Id = @GlEntryModelId 
	And QSP_Product_Line_id = '46001'	--  Magnet program is Magazine only program	

	If @@RowCount = 0  Or @@Error <> 0 
	Begin
	 Select @ErrMessage =  'GL Entry ProductLine Id not found for GL entry model  ' + Cast(@GlEntryModelId As Varchar)+'  productline  Magazine in procedure AdjustmentMagnet'
	 Exec dbo.LogGLError 'Adjustment Magnet', 0  , @ErrMessage
	 Set @ErrorCode = 1
	 Return 
	End

	-- Each productline has unique GlEntryProductLineId, get the account number using GlEntryProductLineId
	Insert Into @GL_Transaction_PL
		Select GL_Entry_Product_Line_Id, GL_Account_Number ,  Debit_Credit
		From QSPCanadaFinance.dbo.GL_Transaction_Pl
		Where GL_Entry_Product_Line_Id = @GlEntryProductLineId    

	--Loop through each account and update the amount
	Select @GLTransCount = Count(*) ,  @DataCacheMaxId1= Max(TIndex) 
	From @GL_Transaction_PL Where ProductlineId= @GlEntryProductLineId 
		
	Set @Cnt1 =0
	While  @GLTransCount > 0
	Begin
				
	Select @AccountNumber = GLAccount, @DebitCredit = DebitCredit 
	From @GL_Transaction_PL
	Where   Tindex = @DataCacheMaxId1 - @Cnt1

	Set @ErrorCode1 = 0

	-- Update_GL_Transaction will insert the updated record in GL_DATA_CACHE table
	Exec dbo.Update_GL_Transaction @AccountNumber,
					  @DebitCredit,
				       	  @Amount,
				       	  @ErrorCode1 			
		
	If @@Error <> 0 or @ErrorCode1 = 1
        	Begin
		 Exec dbo.LogGLError 'Invoice', 0  , 'Error executing Update_GL_Transaction in procedure AdjustmentMagnet'
		 Set @ErrorCode = 1
		 Return 
	End
			
	Set @Cnt1 = @Cnt1+1
	Set @GLTransCount = @GLTransCount -1
	End
			        Set @ErrorCode = 0	--Success

End
GO
