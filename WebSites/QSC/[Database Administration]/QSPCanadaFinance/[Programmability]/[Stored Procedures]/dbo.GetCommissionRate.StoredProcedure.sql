USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetCommissionRate]    Script Date: 06/07/2017 09:17:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE   [dbo].[GetCommissionRate]	(@FmId	 	Int,
							@AccountId	Int,
							@ComType	Varchar(30),
							@SectionType	Int,	--2 for Magazine 1 for Gift
             						    	@StartDate        DateTime,
						    	@EndDate         DateTime,
						    	@profit	 	Numeric(10,2),
							@NetSales	Numeric(10,2),	
							@Level		Int,
							@Commission_Bonus Numeric(10,2) Output
							 )
As

		Declare 	
		@TransId		Int,
		@FM			Int,
		@Account		Int,
  		@PriorYearDateFrom	DateTime , 
		@PriorYearDateTo	DateTime ,
		@TotalSubs		Numeric(10,2), 
		@Target		Numeric(10,2), 
		@PriorYearSale		Numeric(10,2), 
		@PercentComm		Int,
		@Bonus		Numeric(10,2),
		@SQL 			varchar(2000)


Begin
	Set @Commission_Bonus = 0
	If @SectionType = 2	--Magazine
	Begin

		If @ComType = 'PERCENT'
		Begin

			--Get commision percentage defined in commission table
			Select    @PercentComm =  Percent_Comm ,  @Bonus= Bonus 
			From      dbo.Commission
			Where	Section_Type_Id      = @SectionType
			And 	Commission_Type_Code = @ComType			--'PERCENT'
			And 	FM_Id = IsNull(@FmId,FM_Id)
			And 	Convert(Varchar(10),Comm_Effective_Date,101) <= Convert(varchar(10),@EndDate,101) 
			And 	IsNull(Convert(Varchar(10),Comm_End_Date,101),Convert(varchar(10),@EndDate,101)) = Convert(Varchar(10),@EndDate,101) 
			Order By (Case IsNull(FM_Id, ' ')
				  When ' ' Then 2
				  Else 1
			  	  End) , Comm_Effective_Date 

			-- If commission rates are not found get the last year sales to find rate
			If  IsNull(@PercentComm,0) = 0
			Begin

				Select @PriorYearDateFrom = DateAdd(month,-12,@StartDate)
				Select @PriorYearDateTo    = DateAdd(month,-12,@EndDate)

				--Number of sub sold last year
				Select @PriorYearSale =  QSPCanadaOrderManagement.dbo.UDF_GetMagSubQuantitySold  ( Null,  @FmId,  @PriorYearDateFrom  ,  @PriorYearDateTo   ,  'N' )
				
				If IsNull(@PriorYearSale,0) > 0 
				Begin

				--Get commission rate based on last year magazine sale
				Select    @PercentComm =  PERCENT_COMM ,  @Bonus= BONUS --,Convert(Varchar(10),COMM_EFFECTIVE_DATE,101)
				From       dbo.COMMISSION
				Where    Section_Type_Id       = @SectionType
				And Commission_Type_Code = @ComType			
				And FM_Id = IsNull(@FmId,FM_Id)
				And IsNull(Min_Target_Number,0) <= @PriorYearSale
				And IsNull(Max_Target_Number,0) >= @PriorYearSale
				And Convert(Varchar(10),Comm_Effective_Date,101) <= @EndDate 
				And IsNull(Convert(Varchar(10),Comm_End_Date,101),Convert(Varchar(10),@EndDate,101))=Convert(Varchar(10),@EndDate,101) 
				Order By (Case IsNull(FM_ID, ' ')
					 When   ' ' 	Then 2
					 Else 		1
			  	              End) , Comm_Effective_Date 
				End

			End

		End	-- ComType 'PERCENT'

		-- For bonus sales fig will be passed  
		/*  -- Sales Retention Bonus is not applicable in FY05

		If @ComType = 'BONUS_ON_SALES'
		Begin

			SELECT    @Bonus= BONUS
			FROM       dbo.COMMISSION
			WHERE    SECTION_TYPE_ID = @SectionType
			AND COMMISSION_TYPE_CODE = @ComType				-- 'BONUS_ON_SALES'	
			AND IsNull(Min_Target_Number,0) <= Cast(round(@NetSales,0)as int)
			AND IsNull(Max_Target_Number,0) >=Cast(round(@NetSales,0)as int) 
			AND Convert(Varchar(10),COMM_EFFECTIVE_DATE,101) <= @EndDate 
			AND IsNUll(Convert(Varchar(10),COMM_END_DATE,101),Convert(varchar(10),@EndDate,101))=Convert(varchar(10),@EndDate,101) 
			ORDER BY (case ISNULL(FM_ID, ' ')
				       When ' ' then 2
				       Else 1
			                    End) , COMM_EFFECTIVE_DATE 
		End
		 */
		
	End			--   Section Type Magazine


	-- Return commission percent or bonus according to commission Type parameter
	If @ComType = 'PERCENT'

		Set 	@Commission_Bonus = @PercentComm

	Else
		Set 	@Commission_Bonus = @Bonus


End	--Main Block
GO
