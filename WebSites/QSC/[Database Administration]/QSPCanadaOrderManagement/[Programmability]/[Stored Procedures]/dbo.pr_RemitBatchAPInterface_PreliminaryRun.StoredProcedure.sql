USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitBatchAPInterface_PreliminaryRun]    Script Date: 06/07/2017 09:20:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE          PROCEDURE [dbo].[pr_RemitBatchAPInterface_PreliminaryRun] @RemitBatchID int, @RunID int,@Result varchar(150) output   AS

-- Written by Saqib Shah -  June 2004
-- this procedure creates AP invoices for  current Remit Batch and store into Oracle Interface tables
-- it returns 0 as a string value if the procedure completed successfully else return error description

DECLARE @APStatus int, @GL_CODE varchar(30), @Season_Year int, @Season varchar(1), @Batch_Season_Date DateTime, @Month int, @ErrorSave int,  @CurrentError int, 
	    @TitleCode varchar(5), @RemitAmount Numeric (10,2), @TaxAmount Numeric(10,2), @Total Numeric(10,2), @Currency Varchar(3),
	    @TermsName varchar(50), @PayGroupLookupCode varchar(25), @VendorNumber varchar(30),  @VendorSiteName varchar(15),
	    @dist_legal_entity varchar(4), @dist_natural_account varchar(4), @dist_sub_account varchar(4), @dist_product_line_dept varchar(4), @dist_language_market varchar(4), @dist_channel varchar(4), @dist_segment7 varchar(4),
  	    @ap_intf_dist_acctseg_1 varchar(4),@ap_intf_dist_acctseg_2 varchar(4), @ap_intf_dist_acctseg_3 varchar(4),@ap_intf_dist_acctseg_4 varchar(4), @ap_intf_dist_acctseg_5 varchar(4),	@ap_intf_dist_acctseg_6 varchar(4),@ap_intf_dist_acctseg_7 varchar(4),
	    @ap_intf_prepay_acctseg_1 varchar(4), @ap_intf_prepay_acctseg_2 varchar(4),  @ap_intf_prepay_acctseg_3 varchar(4),	 @ap_intf_prepay_acctseg_4 varchar(4),  @ap_intf_prepay_acctseg_5 varchar(4),	 @ap_intf_prepay_acctseg_6 varchar(4),  @ap_intf_prepay_acctseg_7 varchar(4)

 SET @Result = '0'
 SET @ErrorSave  = 0
 SET @CurrentError = 0

 set nocount on




Select  Top 1 @Season_Year = pd.Pricing_Year, @Season = pd.Pricing_Season
  From QSPCanadaOrderManagement..CustomerOrderDetailRemithistory oh,
      QSPCanadaOrderManagement..CustomerOrderDetail od,
	QSPCanadaProduct..pricing_details pd
 where oh.CustomerOrderHeaderInstance = od.CustomerOrderHeaderInstance
and oh.TransID = od.TransID
and od.pricingdetailsid = pd.magPrice_Instance
and oh.RemitBatchId = @RemitBatchID;    


 
   Select TitleCode, 
 	 SUM(isnull(BasePrice,0)*isnull(RemitRate,0)) 							AS RemitAmount,
	 QspCanadaCommon.dbo.FNC_GET_CURRENCY_DESC(CurrencyID) 				AS Currency,
          	 QSPCanadaCommon.dbo.FNC_CALC_GST(SUM(isnull(BasePrice,0)*isnull(RemitRate,0)), TitleCode) 	AS TaxAmount
  INTO #TEMP_AMOUNTS
   From QSPCanadaOrderManagement..CustomerOrderDetailRemithistory oh,
    	QSPCanadaOrderManagement..CustomerRemitHistory rh
   where oh.RemitBatchID = @RemitBatchID
   and  oh.CustomerRemitHistoryInstance = rh.Instance
   and oh.status  in (42000, 42001) --needs to be sent, sent

   group by TitleCode,CurrencyID; 


  Declare Cur_Vendor_Amounts Cursor  For
 SELECT tt.TitleCode, Round(tt.RemitAmount,2) as RemitAmount, Round(tt.TaxAmount,2) as TaxAmount, (Round(tt.RemitAmount,2) + Round(tt.TaxAmount,2)) as Total,
       tt.Currency, prod.TermsName, prod.PayGroupLookupCode,prod.VendorNumber,
        prod.VendorSiteName
 FROM #TEMP_AMOUNTS tt, QSPCanadaProduct..Product prod
 WHERE tt.TitleCode = prod.Product_Code
	   and prod.product_year = @Season_Year
	   and prod.product_season = @Season
 order by TitleCode;

-- to fetch the AP GL account number segments info for Remit Amount
Select @GL_CODE= tp.GL_Account_number
From  Qspcanadafinance..gl_entry_model em,
      Qspcanadafinance..gl_entry_product_line pl,
      Qspcanadafinance..gl_transaction_pl tp
Where  em.gl_entry_model_id = pl.gl_entry_model_id
    AND pl.gl_entry_product_line_id = tp.gl_entry_product_line_id
    AND tp.debit_credit = 'D'
    AND pl.qsp_product_line_id = 46001
    AND em.transaction_type_id = 9
    AND em.gl_entry_type_id = 1;

 Set @dist_legal_entity 		= substring(@GL_CODE,1,3)    
 Set @dist_natural_account 	= substring(@GL_CODE,5,4)     
 Set @dist_sub_account 	= substring(@GL_CODE,10,4)  
 Set @dist_product_line_dept 	= substring(@GL_CODE,15,4)  
 Set @dist_language_market 	= substring(@GL_CODE,20,2)  
 Set @dist_channel 		= substring(@GL_CODE,23,2)  
 Set @dist_segment7 		= substring(@GL_CODE,26,3)  

-- to fetch the AP GL account number segments info for GST

    SELECT top 1	@ap_intf_dist_acctseg_1 	= ap_intf_dist_acctseg_1,
		 	@ap_intf_dist_acctseg_2 	= ap_intf_dist_acctseg_2,
	           		@ap_intf_dist_acctseg_3 	= ap_intf_dist_acctseg_3,
			@ap_intf_dist_acctseg_4 	= ap_intf_dist_acctseg_4,
			@ap_intf_dist_acctseg_5 	= ap_intf_dist_acctseg_5,
			@ap_intf_dist_acctseg_6 	= ap_intf_dist_acctseg_6,
			@ap_intf_dist_acctseg_7 	= ap_intf_dist_acctseg_7,
			@ap_intf_prepay_acctseg_1 	= ap_intf_prepay_acctseg_1,
			@ap_intf_prepay_acctseg_2 	= ap_intf_prepay_acctseg_2,
			@ap_intf_prepay_acctseg_3 	= ap_intf_prepay_acctseg_3,
			@ap_intf_prepay_acctseg_4 	= ap_intf_prepay_acctseg_4,
			@ap_intf_prepay_acctseg_5 	= ap_intf_prepay_acctseg_5,
			@ap_intf_prepay_acctseg_6 	= ap_intf_prepay_acctseg_6,
			@ap_intf_prepay_acctseg_7 	= ap_intf_prepay_acctseg_7
    FROM  QSPCanadaCommon..TaxProvince
    WHERE country_code = 'CA'
    AND tax_id = 1 -- tax id = 1code  is for GST



	OPEN Cur_Vendor_Amounts
	    FETCH NEXT FROM Cur_Vendor_Amounts  INTO  	@TitleCode , @RemitAmount , @TaxAmount , @Total , @Currency,
								@TermsName , @PayGroupLookupCode , @VendorNumber,
								@VendorSiteName

	   WHILE @@FETCH_Status = 0

                BEGIN
		declare @invoice_num varchar(50)
--print @TitleCode
--print Cast(@RemitBatchID as varchar(10))
--print @currency
		select @invoice_num = @TitleCode+' '+Cast(@RunID as varchar(10))+' '+@Currency 
--print '??'
--print @invoice_num

	 -- invoice amount entry, including taxes	
	 Insert into QSPCanadaOrderManagement..OM_TBL_AP_INVOICES_INTERFACE
	  (country_code, invoice_num, invoice_type, invoice_date, invoice_amount,
	   invoice_currency_code, terms_name, pay_group_lookup_code, description )
	 values
	  ('CA', @invoice_num,'STANDARD',getdate(),@Total,
	   @Currency, @TermsName,@PayGroupLookupCode,
	   'Remit batch #'+Cast(@RunID as varchar(10))+' for UMC #'+@TitleCode);   

	SET @CurrentError = @@ERROR
	IF @CurrentError <> 0 
	 BEGIN
   	   SET @ErrorSave = @CurrentError
	   SET @CurrentError = 0
	 END


              -- Line # 1 for Remit Amount only
	 INSERT INTO QSPCanadaOrderManagement..OM_TBL_AP_INV_LINES_INTERFACE
		(country_code, invoice_num, line_number, description, amount,
		 dist_legal_entity, dist_natural_account, dist_sub_account, dist_product_line_dept, dist_language_market,
		 dist_channel, dist_segment7, prepay_legal_entity, prepay_natural_account, prepay_sub_account,
		 prepay_product_line_dept, prepay_language_market, prepay_channel, prepay_segment7)
	    VALUES('CA',  @TitleCode+' '+Cast(@RunID as Varchar(10))+' '+@Currency, 1, 'Remit amount (DESC. Line 1)',
		@RemitAmount, @dist_legal_entity, @dist_natural_account,
		@dist_sub_account, @dist_product_line_dept, @dist_language_market,
		@dist_channel, @dist_segment7, '062', '1650', '0000', '0000', '00', '00', '000');

	
	SET @CurrentError = @@ERROR
	IF @CurrentError <> 0 
	 BEGIN
   	   SET @ErrorSave = @CurrentError
	   SET @CurrentError = 0
	 END

               -- Line # 2 for GST only
	 INSERT INTO QSPCanadaOrderManagement..OM_TBL_AP_INV_LINES_INTERFACE
		(country_code, invoice_num, line_number, description, amount,
		 dist_legal_entity, dist_natural_account, dist_sub_account, dist_product_line_dept, dist_language_market,
		 dist_channel, dist_segment7, prepay_legal_entity, prepay_natural_account, prepay_sub_account,
		 prepay_product_line_dept, prepay_language_market, prepay_channel, prepay_segment7)
	    VALUES('CA',  @TitleCode+' '+Cast(@RunID as Varchar(10))+' '+@Currency, 2, 'GST (DESC. Line 2)',
		@TaxAmount, @ap_intf_dist_acctseg_1, @ap_intf_dist_acctseg_2,
		@ap_intf_dist_acctseg_3, @ap_intf_dist_acctseg_4, @ap_intf_dist_acctseg_5,
		@ap_intf_dist_acctseg_6, @ap_intf_dist_acctseg_7, 
		@ap_intf_prepay_acctseg_1, @ap_intf_prepay_acctseg_2,
		@ap_intf_prepay_acctseg_3, @ap_intf_prepay_acctseg_4, @ap_intf_prepay_acctseg_5,
		@ap_intf_prepay_acctseg_6, @ap_intf_prepay_acctseg_7);

	SET @CurrentError = @@ERROR
	IF @CurrentError <> 0 
	 BEGIN
   	   SET @ErrorSave = @CurrentError
	   SET @CurrentError = 0
	 END

              -- vendor information entry
	 INSERT INTO QSPCanadaOrderManagement..OM_TBL_PO_VENDORS_INTERFACE
	       (country_code,  invoice_num,
	        vendor_number, vendor_site_name, Liab_legal_entity, liab_natural_account,
		liab_sub_account,liab_product_line_dept,liab_language_market,liab_channel,liab_segment7)
	 VALUES('CA', 
		@TitleCode+' '+Cast(@RunID as Varchar(10))+' '+@Currency,@VendorNumber, 
		@VendorSiteName, '062','2001','0000',
			'0000',
			'00',
			'00',
			'000') ;   

/*	
update om_tbl_po_vendors_interface
set
liab_sub_account ='0000',
liab_product_line_dept = '0000',
liab_language_market ='00',
liab_channel = '00',
liab_segment7 = '000'
*/          
	SET @CurrentError = @@ERROR
	IF @CurrentError <> 0 
	 BEGIN
   	   SET @ErrorSave = @CurrentError
	   SET @CurrentError = 0
	 END
  

                FETCH NEXT FROM Cur_Vendor_Amounts  INTO	@TitleCode , @RemitAmount , @TaxAmount , @Total , @Currency,								@TermsName , @PayGroupLookupCode , @VendorNumber,@VendorSiteName ;

  
              END -- end while loop

	CLOSE Cur_Vendor_Amounts
	DEALLOCATE Cur_Vendor_Amounts


	

  	Drop table #TEMP_AMOUNTS
GO
