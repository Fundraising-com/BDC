USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitBatchAPInterface]    Script Date: 06/07/2017 09:20:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE         PROCEDURE [dbo].[pr_RemitBatchAPInterface] @RemitBatchID int, @Result varchar(150) output   AS

-- Written by Saqib Shah -  June 2004
-- this procedure creates AP invoices for  current Remit Batch and store into Oracle Interface tables
-- it returns 0 as a string value if the procedure completed successfully else return error description

DECLARE @APStatus int, @GL_CODE varchar(30), @Season_Year int, @Season varchar(1), @Batch_Season_Date DateTime, @Month int, @ErrorSave int,  @CurrentError int, @RunID int,
	    @TitleCode varchar(5), @RemitAmount Numeric (10,2), @TaxAmount Numeric(10,2), @GST Numeric(10,2),@HST Numeric(10,2),@PST Numeric(10,2),@Total Numeric(10,2), @Currency Varchar(3),
	    @TermsName varchar(50), @PayGroupLookupCode varchar(25), @VendorNumber varchar(30),  @VendorSiteName varchar(15),
	    @dist_legal_entity varchar(4), @dist_natural_account varchar(4), @dist_sub_account varchar(4), @dist_product_line_dept varchar(4), @dist_language_market varchar(4), @dist_channel varchar(4), @dist_segment7 varchar(4),
  	    @ap_intf_dist_acctseg_1 varchar(4),@ap_intf_dist_acctseg_2 varchar(4), @ap_intf_dist_acctseg_3 varchar(4),@ap_intf_dist_acctseg_4 varchar(4), @ap_intf_dist_acctseg_5 varchar(4),	@ap_intf_dist_acctseg_6 varchar(4),@ap_intf_dist_acctseg_7 varchar(4),
	    @ap_intf_prepay_acctseg_1 varchar(4), @ap_intf_prepay_acctseg_2 varchar(4),  @ap_intf_prepay_acctseg_3 varchar(4),	 @ap_intf_prepay_acctseg_4 varchar(4),  @ap_intf_prepay_acctseg_5 varchar(4),	 @ap_intf_prepay_acctseg_6 varchar(4),  @ap_intf_prepay_acctseg_7 varchar(4)

 SET @Result = '0'
 SET @ErrorSave  = 0
 SET @CurrentError = 0

 set nocount on


 Select @APStatus = APStatus, @RunID = RunID
 From QSPCanadaOrderManagement..RemitBatch
 Where ID = @RemitBatchID

IF    @APStatus <> 1  -- # 1     if invoices are not already calculated then create new invoices else dont regenrate them and return error

 BEGIN


  Select  Top 1 @Season_Year = pd.Pricing_Year, @Season = pd.Pricing_Season
  From QSPCanadaOrderManagement..CustomerOrderDetailRemithistory oh,
      QSPCanadaOrderManagement..CustomerOrderDetail od,
	QSPCanadaProduct..pricing_details pd
 where oh.CustomerOrderHeaderInstance = od.CustomerOrderHeaderInstance
and oh.TransID = od.TransID
and od.pricingdetailsid = pd.magPrice_Instance
and oh.status  in (42000, 42001) --needs to be sent, sent
and oh.RemitBatchId = @RemitBatchID;    

	SELECT		codrh.RemitCode AS TitleCode,
				RemitCode AS ProductCode,
				CASE --Marvel needs EXACT price, but RemitRate is a rounded amount (note that if we do this for all FHs, it's different logic for CA and US FHs, as well as perhaps the tax proc may need changing)
					WHEN codrh.RemitCode = '10RK' OR codrh.RemitCode = '7639' OR codrh.RemitCode = '118M' THEN --Marvel
						SUM((ISNULL(pd.BasePriceSansPostage, 0) * ISNULL(pd.BaseRemitRate, 0)) + (ISNULL(pd.PostageAmount, 0) * ISNULL(pd.PostageRemitRate, 0))) 
					ELSE SUM(ISNULL(BasePrice, 0) * ISNULL(RemitRate, 0)) END AS RemitAmount,
				CASE codrh.CurrencyID WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' END AS Currency,  
				ROUND(SUM(ISNULL(codrh.Tax2, 0)), 2) AS PST
	INTO		#TEMP_AMOUNTS
	FROM		QSPCanadaOrderManagement..CustomerOrderDetailRemithistory codrh
	JOIN		QSPCanadaOrderManagement..CustomerRemitHistory crh
					ON	crh.Instance = codrh.CustomerRemitHistoryInstance
	JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
					ON	cod.customerorderheaderinstance = codrh.customerorderheaderinstance
					AND	cod.TransID = codrh.TransID
	JOIN		QSPCanadaProduct..Pricing_Details pd
					ON	pd.MagPrice_Instance = cod.PricingDetailsID
	WHERE		codrh.RemitBatchID = @RemitBatchID
	AND			codrh.Status IN (42000, 42001) --needs to be sent, sent
	GROUP BY	codrh.RemitCode,
				codrh.CurrencyID


   Select    RemitCode  as TitleCode , 
	  Case codrh.CurrencyID when 801 then 'CAD' when 802 then 'USD' end Currency,
	  Round(sum(Isnull(codrh.Tax,0)),2) as GST  				
   into #GST
   From QSPCanadaOrderManagement..CustomerOrderDetailRemithistory codrh,
    	QSPCanadaOrderManagement..CustomerRemitHistory rh,
	QSPCanadaOrderManagement..CustomerOrderDetail cod
   where codrh.RemitBatchID = @RemitBatchID
   and cod.customerorderheaderinstance = codrh.customerorderheaderinstance
   and cod.transid = codrh.transid
   and  codrh.CustomerRemitHistoryInstance = rh.Instance
   and  rh.state not in ( 'NB','NS','NL') 
   and codrh.status  in (42000, 42001) --needs to be sent, sent
--   group by cod.ProductCode,codrh.RemitCode,codrh.CurrencyID;   
   group by codrh.RemitCode,codrh.CurrencyID;   


   Select  RemitCode  as TitleCode , 
	  Case codrh.CurrencyID when 801 then 'CAD' when 802 then 'USD' end Currency,
	  Round(sum(Isnull(codrh.Tax,0)),2) as HST 				
   Into #HST 
   From QSPCanadaOrderManagement..CustomerOrderDetailRemithistory codrh,
    	QSPCanadaOrderManagement..CustomerRemitHistory rh,
	QSPCanadaOrderManagement..CustomerOrderDetail cod
   where codrh.RemitBatchID = @RemitBatchID
   and cod.customerorderheaderinstance = codrh.customerorderheaderinstance
   and cod.transid = codrh.transid

   and  codrh.CustomerRemitHistoryInstance = rh.Instance
   and  rh.state  in ( 'NB','NS','NL') 
   and codrh.status  in (42000, 42001) --needs to be sent, sent
   group by codrh.RemitCode,codrh.CurrencyID; 

   select  distinct prod.remitcode,
	prod.TermsName, prod.PayGroupLookupCode,prod.VendorNumber,
             prod.VendorSiteName Into #prod
	from QSPCanadaProduct..Product prod
	where  prod.product_year = @Season_Year
	   and prod.product_season = @Season
/*
 SELECT tt.TitleCode, Round(tt.RemitAmount,2) as RemitAmount,
  Round(isnull((Select GST from #GST as gst where gst.titlecode = tt.titlecode and gst.currency  = tt.currency),0)+
	isnull((Select HST from #HST as hst where hst.titlecode = tt.titlecode and hst.currency  = tt.currency),0)+
	isnull(tt.PST,0),2) as TaxAmount,
	isnull(Round(tt.RemitAmount,2),0) +
  	Round(isnull((Select GST from #GST as gst where gst.titlecode = tt.titlecode and gst.currency  = tt.currency),0)+
	      isnull((Select HST from #HST as hst where hst.titlecode = tt.titlecode and hst.currency  = tt.currency),0)+
	      isnull(tt.PST,0),2) as Total,
        isnull((Select GST from #GST as gst where gst.titlecode = tt.titlecode and gst.currency  = tt.currency),0) as GST,
	isnull((Select HST from #HST as hst where hst.titlecode = tt.titlecode and hst.currency  = tt.currency),0) as HST, 
	isnull(tt.PST,0) as PST,
       	tt.Currency, prod.TermsName, prod.PayGroupLookupCode,prod.VendorNumber,
             prod.VendorSiteName
 FROM #TEMP_AMOUNTS tt, #prod prod
 WHERE 
	   tt.ProductCode = prod.RemitCode
	  
 Order by TitleCode;
*/
 Declare Cur_Vendor_Amounts Cursor  For
 SELECT tt.TitleCode, Round(tt.RemitAmount,2) as RemitAmount,
  Round(isnull((Select GST from #GST as gst where gst.titlecode = tt.titlecode and gst.currency  = tt.currency),0)+
	isnull((Select HST from #HST as hst where hst.titlecode = tt.titlecode and hst.currency  = tt.currency),0)+
	isnull(tt.PST,0),2) as TaxAmount,
	isnull(Round(tt.RemitAmount,2),0) +
  	Round(isnull((Select GST from #GST as gst where gst.titlecode = tt.titlecode and gst.currency  = tt.currency),0)+
	      isnull((Select HST from #HST as hst where hst.titlecode = tt.titlecode and hst.currency  = tt.currency),0)+
	      isnull(tt.PST,0),2) as Total,
        isnull((Select GST from #GST as gst where gst.titlecode = tt.titlecode and gst.currency  = tt.currency),0) as GST,
	isnull((Select HST from #HST as hst where hst.titlecode = tt.titlecode and hst.currency  = tt.currency),0) as HST, 
	isnull(tt.PST,0) as PST,
       	tt.Currency, prod.TermsName, prod.PayGroupLookupCode,prod.VendorNumber,
             prod.VendorSiteName
 FROM #TEMP_AMOUNTS tt,  #prod prod
 WHERE tt.ProductCode = prod.RemitCode
	  
 Order by TitleCode;

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



        BEGIN TRAN  -- save point started	

	OPEN Cur_Vendor_Amounts
	    FETCH NEXT FROM Cur_Vendor_Amounts  INTO  	@TitleCode , @RemitAmount , @TaxAmount , @Total , @GST,@HST,@PST,@Currency,
								@TermsName , @PayGroupLookupCode , @VendorNumber,
								@VendorSiteName
	   WHILE @@FETCH_Status = 0

                BEGIN
		declare @invoice_num varchar(50)

		select @invoice_num = @TitleCode+' '+Cast(@RunID as varchar(10))+' '+@Currency 

	 Insert into QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE
	  (country_code, invoice_num, invoice_type, invoice_date, invoice_amount,
	   invoice_currency_code, terms_name, pay_group_lookup_code, description )
	 values
	  ('CA', @TitleCode+' '+Cast(@RunID as varchar(10))+' '+@Currency,'STANDARD',getdate(),@Total,
	   @Currency, @TermsName,@PayGroupLookupCode,
	   'Remit batch #'+Cast(@RunID as varchar(10))+' for UMC #'+@TitleCode);   

	SET @CurrentError = @@ERROR
	IF @CurrentError <> 0 
	 BEGIN
   	   SET @ErrorSave = @CurrentError
	   SET @CurrentError = 0
	 END


              -- Line # 1 for Remit Amount only
	 INSERT INTO QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
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

----Start GST Line processing-------------

     IF @GST > 0

       BEGIN

	    SELECT Top 1	@ap_intf_dist_acctseg_1 	= ap_intf_dist_acctseg_1,
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

               -- Line # 2 for GST only
	 INSERT INTO QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
		(country_code, invoice_num, line_number, description, amount,
		 dist_legal_entity, dist_natural_account, dist_sub_account, dist_product_line_dept, dist_language_market,
		 dist_channel, dist_segment7, prepay_legal_entity, prepay_natural_account, prepay_sub_account,
		 prepay_product_line_dept, prepay_language_market, prepay_channel, prepay_segment7)
	    VALUES('CA',  @TitleCode+' '+Cast(@RunID as Varchar(10))+' '+@Currency, 2, 'GST (DESC. Line 2)',
		@GST, @ap_intf_dist_acctseg_1, @ap_intf_dist_acctseg_2,
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

    END

------ Start HST line processing-------

      IF @HST > 0
         BEGIN

	    SELECT Top 1	@ap_intf_dist_acctseg_1 	= ap_intf_dist_acctseg_1,
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
	    AND tax_id = 2 --  HST

               -- Line # 3 for HST only
	 INSERT INTO QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
		(country_code, invoice_num, line_number, description, amount,
		 dist_legal_entity, dist_natural_account, dist_sub_account, dist_product_line_dept, dist_language_market,
		 dist_channel, dist_segment7, prepay_legal_entity, prepay_natural_account, prepay_sub_account,
		 prepay_product_line_dept, prepay_language_market, prepay_channel, prepay_segment7)
	    VALUES('CA',  @TitleCode+' '+Cast(@RunID as Varchar(10))+' '+@Currency, 3, 'HST (DESC. Line 3)',
		@HST, @ap_intf_dist_acctseg_1, @ap_intf_dist_acctseg_2,
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
     END

---End HST line-------------

----- Start PST line processing-------this is also called QST coz till now only quebec is charging provincial sales tax on magazines
 
    IF @PST > 0  

      BEGIN

	    SELECT Top 1	@ap_intf_dist_acctseg_1 	= ap_intf_dist_acctseg_1,
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
	    AND tax_id = 3 --  PST

               -- Line # 4 for PST only
	 INSERT INTO QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
		(country_code, invoice_num, line_number, description, amount,
		 dist_legal_entity, dist_natural_account, dist_sub_account, dist_product_line_dept, dist_language_market,
		 dist_channel, dist_segment7, prepay_legal_entity, prepay_natural_account, prepay_sub_account,
		 prepay_product_line_dept, prepay_language_market, prepay_channel, prepay_segment7)
	    VALUES('CA',  @TitleCode+' '+Cast(@RunID as Varchar(10))+' '+@Currency, 4, 'PST (DESC. Line 4)',
		@PST, @ap_intf_dist_acctseg_1, @ap_intf_dist_acctseg_2,
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

     END

---End PST line-------------


              -- vendor information entry
	 INSERT INTO QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE
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

         
	SET @CurrentError = @@ERROR
	IF @CurrentError <> 0 
	 BEGIN
   	   SET @ErrorSave = @CurrentError
	   SET @CurrentError = 0
	 END
  

	    FETCH NEXT FROM Cur_Vendor_Amounts  INTO  	@TitleCode , @RemitAmount , @TaxAmount , @Total , @GST,@HST,@PST,@Currency,
								@TermsName , @PayGroupLookupCode , @VendorNumber,
								@VendorSiteName

  
              END -- end while loop

	CLOSE Cur_Vendor_Amounts
	DEALLOCATE Cur_Vendor_Amounts


           IF @ErrorSave <> 0  
               begin 
                   rollback
	      set @Result  = 'An error occured while AP Invoices processing, All AP transactions rollbacked, Error# '+ str(@ErrorSave)
              end
          ELSE
             begin
		Update QSPCanadaOrderManagement..RemitBatch
		SET APStatus = 1
		Where ID = @RemitBatchID;
	   commit
	   set  @Result = '0'
	end

  	Drop Table #TEMP_AMOUNTS
	Drop Table #GST
	Drop Table #HST

   END -- # 1

ELSE
  begin
    SET @Result  = 'AP invoices are already calculated for this batch. Remit Batch#'+str(@RemitBatchID) 
  end
GO
