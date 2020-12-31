USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[testproc]    Script Date: 06/07/2017 09:17:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[testproc] (@pInvoiceId Int, 
				       @pAllocated Varchar(1) ='N'  ,
				       @pAllocatedAmount Numeric (10,2) = Null ,
				       @pAllocationEffectiveDate DateTime= Null, 
				       @RetVal Int =0 Output
				      )
As
Begin 

	Set NoCount On

    	Declare @InvoiceCount		Int,
		@MaxRowCnt		Int,
		@ProductLineCnt 	Int,
		@SectionTypeCnt 	Int,
		@PriceOverrideid 	Int,
		@OrderItemCount 	Int,
		@OrderItemPrice 	Numeric(10,2),
		@OrderItemTotalProductLine Numeric(10,2),
		@ErrorFlag 		Varchar(1),
		@Entity 		Int,
		@SectionType 		Int,
		@QSPProductLine 	Int,
                           @OrderTypeCode 	Int,
                           @IsStraightOrderEntry 	Varchar(1),
		@InvoiceId		Int,
		@Allocated 		Varchar(1),
	              @ProductLineIdComp 	Int, 
		@SectionTypeComp 	Int,
		@NetBeforeTax  	Numeric(10,2),
		@Tax1Amount 		Numeric(10,2),
		@Tax2Amount		Numeric(10,2),
		@TotalAllProdLine 	Numeric(10,2),
		@AllocatedAmount 	Numeric(10,2),
		@ErrorCode 		Int,	
		@TransType		Int
	
	Declare @TabEntity Table(
			 Tindex		Int Identity,
			 Entityid		Int
				 )
	Declare @TabSectionInvoice Table (
 			TIndex		Int Identity,
			SectionTypeId 	Int,
			NetBeforeTax 	Numeric(10,2),
			IsPriceWithTax  	Varchar(1)
				                  )
	Declare @TabDistinctProductLine Table (
			TIndex		Int Identity,
			ProductLineId	Int,
			SectionTypeId	Int
 					            )
	Declare @TabInvoiceByProduct Table(
			TIndex 		Int Identity,
			ItemPriceTotal 	Numeric(10,2),
			Tax  		Numeric(10,2),
			Tax2 		 Numeric(10,2),
			ProductLineId 	Int,
			SectionTypeId 	Int
					        )

	Declare @TabProductLineTotal Table(
			Tindex 		Int Identity,
			ProductLineId 	Int,
			TotalAmount 	Numeric (10,2),
			SectionTypeId 	Int
					      )
	Declare @TabTaxTotal Table (
			Tindex 		Int Identity ,
			TaxId		Int,
			TaxAmount 	Numeric (10,2)
				        )

	Declare @TabMiscCharges Table(
			Tindex 		Int Identity,
			MisTransTypeId Int,
			MiscAmount	Numeric(10,2)
					)

	Begin Transaction 	
	Insert Into @TabEntity (Entityid)	
		Select Distinct  EntityId
		From QSPCanadaCommon.dbo.QSPProductLine 
		Where EntityId=62  --QSP

	--Set @ErrorFlag= 'N' 
	Set @InvoiceId = @pInvoiceId  
	Set @Allocated=  Isnull(@pAllocated,'N')
	

            Select @MaxRowCnt= Count(*)  From @TabEntity
	
	While @MaxRowCnt  > 0	
            
	--For each entity loop          
	 Begin
                         Set @ErrorFlag= 'N' 
		Select  @InvoiceCount = Count(*) 
		From QSPCanadaFinance.dbo.Invoice_By_QSP_Product
		Where Invoice_Id = @InvoiceId

		Select @Entity = EntityId From @TabEntity Where TIndex = @MaxRowCnt 	

		-- If no Invoice data found in Invoice_By_QSP_Product check allocation parameter 
		--If (IsNUll(@InvoiceCount,0) = 0  And    Upper(@Allocated) <> 'Y') 
If (IsNUll(@InvoiceCount,0) > 0  And    Upper(@Allocated) = 'Y')                      ---------For Testing only
		Begin
print '#1'
			-- Get  Order Type and check if it is an staright order
				
			Select  Top 1  @OrderTypeCode = b.OrderTypeCode --, @IsStraightOrderEntry = 
			From QSPCanadaOrderManagement.dbo.batch b,
			         QSPCanadaFinance.dbo.Invoice inv,QSPCanadaCommon.dbo.codedetail cd
			Where inv.order_id = b.orderId
			And inv.invoice_id = @InvoiceId
			
			If   @OrderTypeCode =  41004              --  'DEBITCM' 
                                        Begin     
                                             Set @TransType = 16
			End
			If   @OrderTypeCode =  41003              -- 'CREDITCM' 
			Begin
			       Set @TransType = 12
                                        End
                                        If  @OrderTypeCode  <>  41004   And @OrderTypeCode <>   41003   
			Begin
			       Set @TransType = 1	        -- Invoice Revenue
                                        End

			--Find and populate the section and total and populate
			 Insert Into @TabSectionInvoice
			        	Select InvSec.Section_Type_Id, InvSec.Net_Before_Tax , SType.IsPriceWithTax
			        	From QSPCanadaFinance..Invoice_Section InvSec , 
				         QSPCanadaProduct..ProgramSectionType Stype
				Where InvSec.Section_Type_Id = Stype.Id
				And InvSec.Invoice_Id=@InvoiceId
				And (Select QSPCanadaFinance.dbo.UDF_Section_Vs_Entity(@InvoiceId, Stype.Id ,@Entity))= 'Y'
				And InvSec.Section_Type_Id  In 
								(Select Type From QSPCanadaProduct..ProgramSection) 
			         	Order By 1 Desc  /* Last record will be picked up first */
                  
				Select @SectionTypeCnt = Count(*) from @TabSectionInvoice
					
				While  @SectionTypeCnt > 0   
				--For Each Section found, find distinct product-line for invoice and populate table
	              		Begin
                    
					Select @SectionType = SectionTypeId, @NetBeforeTax= NetBeforeTax  
					From @TabSectionInvoice Where Tindex = @SectionTypeCnt 

					Insert Into @TabDistinctProductLine
			                           -- Disabled need to ask Karen
				Select distinct qsp.id,ps.Type
						From QSPCanadaOrderManagement.dbo.CustomerOrderDetail od ,
						         QSPCanadaProduct.dbo.ProgramSection Ps ,
						         QSPCanadaProduct.dbo.ProgramSectionType  stype,
						         QSPCanadaCommon.dbo.QSPProductLine      qsp,
						         QSPCanadaFinance.dbo.Invoice_Section InvSec,
						         QSPCanadaFinance.dbo.Invoice inv
						Where Inv.Invoice_id = od.InvoiceNumber
						And od.InvoiceNumber =@InvoiceId
	   					And od.ProgramSectionId= ps.id
    						And ps.Type = stype.id
    						And od.producttype = qsp.id
						And  ps.Type =  	@SectionType
						And  qsp.id not in (46004) -- Field Supplies 
						Order by 2 

						/*Select Distinct od.producttype ,ps.Type
						From QSPCanadaOrderManagement.dbo.CustomerOrderDetail od ,
						         QSPCanadaProduct.dbo.Product pr,
						        --QSPCanadaFinance.dbo.Invoice_Section InvSec,
						         QSPCanadaFinance.dbo.Invoice inv,
		     				         QSPCanadaProduct.dbo.ProgramSection Ps ,
				                  	         QSPCanadaCommon.dbo.QSPProductLine      qsp,
						         QSPCanadaProduct.dbo.ProgramSectionType  stype
						Where   od.ProductCode= pr.product_Code
				       		 --And Inv.Invoice_id = InvSec.Invoice_id
						And od.ProgramSectionId= ps.id
						And ps.Type = stype.id
				                          and  ps.Type =  	@SectionType
						And od.producttype = qsp.id
				       		--AND invsec.section_type_id= stype.id
						And Inv.Invoice_id = od.InvoiceNumber
						And od.InvoiceNumber =@InvoiceId
						and qsp.id not in (46004,46008) -- Field Supplies 
						Order By 2 */

                         		
						Select @ProductLineCnt = Count(*) From @TabDistinctProductLine
						While  @ProductLineCnt  >0
				                           Begin
							Set @OrderItemTotalProductLine =0
							Select  @QSPProductLine = ProductLineId From @TabDistinctProductLine Where TIndex = @ProductLineCnt
                              
	                 					-- Insert all Order Item price and quantity for given invoice for section and product line		
							Insert Into @TabInvoiceByProduct
								 Select Sum(Price) ItemPriceTotal, Sum(Tax) Tax,Sum(Tax2) Tax2 ,qsp.Id,ps.Type
			 					 From QSPCanadaOrderManagement.dbo.CustomerOrderDetail od ,
								          QSPCanadaProduct.dbo.ProgramSection Ps ,
								          QSPCanadaProduct.dbo.ProgramSectionType  stype,
								          QSPCanadaCommon.dbo.QSPProductLine      qsp
							 	 Where od.InvoiceNumber = @InvoiceId
					    			And od.ProgramSectionId= ps.id
					    			And ps.Type = stype.id
					    			And stype.Id = @SectionType
					    			And od.producttype = qsp.id
								And qsp.Id = @QSPProductLine								Group by qsp.Id,ps.Type
					                                        Order By QSP.Id

					                      	Select @OrderItemCount = Count(*) From @TabInvoiceByProduct
                        
						               While    @OrderItemCount > 0
						               Begin    
                   	
                          							-- Check if the Item's product line is same as Productline Id being processes if so add to productline total
								Select @ProductLineIdComp = ProductLineId , 
								          @SectionTypeComp =SectionTypeId ,
								          @OrderItemPrice=ItemPriceTotal, @Tax1Amount=Tax , @Tax2Amount= Tax2 
								From @TabInvoiceByProduct Where Tindex = @OrderItemCount
                                                                                                           
			                          			  If  (@ProductLineIdComp = @QSPProductLine  And  @SectionTypeComp = @SectionType) 
						                          Begin
								       /*Need to exclude the Tax amount if included in the price*/
						                                 If  (Select Upper(IsPriceWithTax) from @TabSectionInvoice Where SectionTypeId= @SectionType) = 'Y' 
								       Begin 
									Set @OrderItemTotalProductLine = @OrderItemTotalProductLine + @OrderItemPrice - (IsNull(@Tax1Amount,0 )+ IsNull(@Tax2Amount,0 ))
                                      						       End
                                  						       Else
                                      						       Begin
						                                       Set @OrderItemTotalProductLine = @OrderItemTotalProductLine + @OrderItemPrice
								        End
                            						End
                          
               							-- Process next Order Item 
                            					 	Set @OrderItemCount =@OrderItemCount -1    
                         						  End

							-- All Item have been processed for a product line Insert total to temp table
                         						If @OrderItemTotalProductLine > 0
                         						Begin
								Insert Into @TabProductLineTotal (ProductLineId,TotalAmount,SectionTypeId)  
								Values(@QSPProductLine,Round(Abs(@OrderItemTotalProductLine),2),@SectionType)
                          						End
							
		                                       		Set @ProductLineCnt = @ProductLineCnt-1  
                          					End  -- End Populate Order Item by Product line and Section 

	            
                          					Select @TotalAllProdLine = Sum(TotalAmount) from @TabProductLineTotal where SectionTypeId =@SectionType
			                           
						Select @ProductLineCnt = Count(*) From @TabProductLineTotal
	
                                                                           

						While  @ProductLineCnt  > 0
                          					 Begin 
							Declare @OldSection int
                               					Select @QSPProductLine = ProductLineId, @OrderItemTotalProductLine = TotalAmount 
							From  @TabProductLineTotal 
							Where Tindex=@ProductLineCnt  and SectionTypeId =@SectionType

														
							If @QSPProductLine <>@OldSection
                                        				-- Change Table Name after Testing
							-- If the invoice already exist in  Invoice_By_QSP_Product then Update else Insert Total
							-- of each ProductLine    

         							 If IsNUll((Select Count(*) From QSPCanadaFinance.msiddiq.Invoice_By_QSP_Product 
								  Where Invoice_Id = @InvoiceId And QSP_Product_Line_Id= @QSPProductLine),0) > 0
                           					Begin

                                 						Update QSPCanadaFinance.msiddiq.Invoice_By_QSP_Product
								Set Product_Amount = Product_Amount + Round((@OrderItemTotalProductLine*@NetBeforeTax/@TotalAllProdLine),2)
								Where QSP_Product_Line_Id= @QSPProductLine
		     						And Invoice_Id = @InvoiceId
                           					End
                          						Else
							
                        						Begin
--print @SectionType
--print @QSPProductLine 
--print @OrderItemTotalProductLine 
--print @TotalAllProdLine
--print @NetBeforeTax
--delete  From  @TabProductLineTotal Where ProductLineId =@QSPProductLine 
--print (@OrderItemTotalProductLine* @NetBeforeTax/ @TotalAllProdLine)
--print @QSPProductLine
                                     						Insert Into QSPCanadaFinance.msiddiq.Invoice_By_QSP_Product 
								values (@InvoiceId,@QSPProductLine, Round((@OrderItemTotalProductLine*@NetBeforeTax/@TotalAllProdLine),2))
                          					End
							Set @Oldsection = @QSPProductLine
							Set @ProductLineCnt = @ProductLineCnt -1
                         					 End   
                                                                                 
                          			Set @SectionTypeCnt = @SectionTypeCnt-1		
	     			End  -- Section loop end
	      
               	End -- Enf If (IsNUll(@InvoiceCount,0) = 0    And    Upper(@Allocated) <> 'Y') 

		/* In all possible cases we need to populate temp tables */			

	             	If (IsNUll(@InvoiceCount,0) > 0    And    Upper(@Allocated) = 'Y')  Or  (IsNUll(@InvoiceCount,0) = 0    And    Upper(@Allocated) = 'N')  

               	-- If Invoice already exists in QSP_Invoice_By_Product Just populate the Temp table
 		Begin
print '#2'
			Delete  From @TabProductLineTotal 

			Insert Into @TabProductLineTotal (ProductLineId,TotalAmount,SectionTypeId)  
				Select  QSP_Product_Line_Id,Product_Amount,Null
	    			From QSPCanadaFinance.dbo.Invoice_By_QSP_Product
	    			Where Invoice_Id = @InvoiceId

			Select @TotalAllProdLine = Sum(TotalAmount) from @TabProductLineTotal
			
			--Findout the  if the Product line total and total for all section are equal

			Set @TotalAllProdLine = 0
			Select @TotalAllProdLine =  IsNull(Sum(Product_Amount),0) from QSPCanadaFinance.msiddiq.Invoice_By_QSP_Product Where Invoice_Id = @InvoiceId
                                 
			 If Abs(@TotalAllProdLine - (Select IsNull(Sum(Net_Before_Tax),0) from QSPCanadaFinance.dbo.Invoice_Section 
						     Where Invoice_Id = @InvoiceId 
						     And Section_Type_Id <> 31005 ))  >  1     --Misc Section Type 
			Begin
	                          	          print   'Section total does not match'

				Set @ErrorFlag='Y' 
				Break
			End

			-- Populate the Tax and MiscCharges Table for the Entity
			-- These tables will be passed to calling procedures alongwith ProductlineTotal table
Insert Into dbo.Gl_Data_Cache (Source_Type, Source_Id,Amount)   
			--Insert Into @TabTaxTotal
				Select 'TAX',Tax_Id, Sum(Tax_Amount) 
				From QSPCanadaFinance.dbo.Invoice_Section_Tax InvSecTax
				Where InvSecTax.Invoice_Section_Id in 
								( Select Invoice_Section_Id 
								  From  QSPCanadaFinance.dbo.Invoice_Section InvSec
								  Where InvSec.Invoice_Id = @InvoiceId
								   And ( (Select QSPCanadaFinance.dbo.UDF_Section_Vs_Entity(@InvoiceId, InvSec.Section_Type_Id ,@Entity)) = 'Y'
									Or InvSec.Section_Type_Id = 31005       /*Misc Section Type */
    								          )
								)
				Group By Tax_Id	
Insert Into dbo.Gl_Data_Cache (Source_Type, Source_Id,Amount)   
			--Insert Into @TabMiscCharges
				Select 'MISC_CHARGES ',MCType.Transaction_Type_Id, Sum(MInvItem.Quantity*MInvItem.Unit_Price) MiscAmount
				From QSPCanadaFinance.dbo.Misc_Charge_Type MCType,
			         	         QSPCanadaFinance.dbo.Misc_Invoice_Item MInvItem
				Where MInvItem.Invoice_Section_Id In(
							Select InvSec.Invoice_Section_Id 
							From  QSPCanadaFinance.dbo.Invoice_Section InvSec
							Where InvSec.Invoice_Id = @InvoiceId
							And  (Select QSPCanadaFinance.dbo.UDF_Section_Vs_Entity(@InvoiceId, InvSec.Section_Type_Id ,@Entity)) = 'Y' )
				And MCType.Misc_Charge_type_id = MInvItem.Misc_Charge_type_id
				Group By MCType.Transaction_Type_Id

                                                            
			If Abs(@TotalAllProdLine - (Select IsNull(Sum(Net_Before_Tax),0) from QSPCanadaFinance.dbo.Invoice_Section 
						    Where Invoice_Id = @InvoiceId 
						    And Section_Type_Id <> 31005 )) > 0     /*Misc Section Type */
                         
		 	-- Amount differs by cents Add the difference amount to product line table
                           	Begin
                                                    
				Update @TabProductLineTotal 
				Set TotalAmount = TotalAmount+  Abs(@TotalAllProdLine - (Select IsNull(Sum(Net_Before_Tax),0) from QSPCanadaFinance.dbo.Invoice_Section 
											  Where Invoice_Id = @InvoiceId 
											  And Section_Type_Id <> 31005 ))
				Where Tindex = 1
			End

		--print 'Table #TTabProductLineTotal populated'
		/*Create Table #TTabProductLineTotal ( Tindex 		Int,
						          ProductLineId 	Int,
						          TotalAmount 	Numeric (10,2)
					    	          )
	
		

 		Create Table #TTabTaxTotal ( Tindex		Int,
					          TaxId		Int,
					          TaxAmount 	Numeric (10,2)
					       )

		 Create Table #TTabMiscCharges (Tindex			 Int,
						   MisTransTypeId	 Int,
						   MiscAmount 		Numeric(10,2)
						 )

Declare @TabProductLineTotal Table(
			Tindex 		Int Identity,
			ProductLineId 	Int,
			TotalAmount 	Numeric (10,2),
			SectionTypeId 	Int
					      )
	Declare @TabTaxTotal Table (
			Tindex 		Int Identity ,
			TaxId		Int,
			TaxAmount 	Numeric (10,2)
				        )

	Declare @TabMiscCharges Table(
			Tindex 		Int Identity,
			MisTransTypeId Int,
			MiscAmount	Numeric(10,2)
					)

*/
                Insert Into dbo.Gl_Data_Cache (Source_Type, Source_Id,Amount)   Select  'PRODUCT_LINE', ProductLineId,TotalAmount  From @TabProductLineTotal            
		--Insert Into  #TTabProductLineTotal   Select  *  From @TabProductLineTotal 

                          -- Insert Into Gl_Data_Cache (Source_Type, Source_Id,Amount)   

		--Insert Into #TTabTaxTotal  Select  *  From   @TabTaxTotal

                       --    Insert Into   #TTabMiscCharges Select  *  From @TabMiscCharges


		If Upper(IsNull(@Allocated,'N'))= 'N'  Or    @Allocated = '' 

		Begin

			Select @AllocatedAmount = Sum(Allocated_Amount) From QSPCanadaFinance.dbo.Invoice_Allocation
			Where Invoice_Id =@InvoiceId
			And Upper(Source_Type) <> ' ADJUSTMENT' 
		End
		Else
		Begin
			Set @AllocatedAmount = IsNull(@pAllocatedAmount ,0)
		End
		If @AllocatedAmount >  0
		Begin
			Set @InvoiceId =@InvoiceId  -- null statement
                        		
                          		/* Exec  dbo.GL_Function 	@InvoiceId ,
							Null ,   			--@PaymentId 
							Null,			--@AdjustmentId  
							3,			--@TransTypeId  'Invoice Payment Allocation'
							@Entity, 		--@EntityId 
							@AllocatedAmount,	--Global Amount for Payment Recording
							@pAllocationEffectiveDate, --Allocation Effective Date
							@ErrorCode Output

			If @ErrorCode <> 0  -- Not successfull 
                           	Begin
		    	   Set @RetVal = @ErrorCode
		    	   Set @ErrorFlag='Y' 
			End	*/			
		End

		--If Upper(IsNull(@Allocated,'N'))= 'N'  Or    @Allocated = '' 
If Upper(IsNull(@Allocated,'Y'))= 'Y'  Or    @Allocated = ''   -- For testing
		Begin
                    print 'saaaaaaa'
			Exec  dbo.GL_Function 	@InvoiceId ,
							Null ,   			--@PaymentId 
							Null,			--@AdjustmentId  
							@TransType,		--@TransTypeId 
							@Entity, 		--@EntityId 
							Null,			--Global Amount for Payment Recording
							Null,			--Allocation Effective Date
							@ErrorCode Output
			If @ErrorCode = 999  -- Not successfull 
                           	Begin
                                
		    	   Set @RetVal = @ErrorCode
		     	   Set @ErrorFlag='Y' 
			End			
			--Set @RetVal = @ErrorCode -- null statement
                           
                       
		End        

		End -- Populating Temp Table without creating Invoice Record in QSp_By_Product------------------------------------>


	Set @MaxRowCnt = @MaxRowCnt -1
	End  	-- Entity Loop End

       
             If @ErrorFlag = 'Y'
             Begin
             print 'Error Occured'
                 	Set @RetVal =1  -- Error
                 	Rollback 
             End
  	Else
	Begin
	 -- print ' no Error Occured'
		Set @RetVal =0 --Success
                	Commit       
              	/* If GL_Function returns success it has already commit the transaction */
          	End

End -- Main Proc
GO
