USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[PR_TEMP_FIX_TAXES]    Script Date: 06/07/2017 09:33:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE      PROCEDURE [dbo].[PR_TEMP_FIX_TAXES]

	@ORDERID INT

AS

 -- Wrote to fix taxes which were calculated wrongly

  SET nocount on

  DECLARE  @V_TAX_1 	             numeric(14,6) --gst
  DECLARE  @V_TAX_2 		 numeric(14,6) --pst/hst
  DECLARE  @V_GROSS		 numeric(14,6)
  DECLARE  @V_NET			 numeric(14,6)
  DECLARE  @Province			varchar(2)	
  DECLARE  @TotalTax 	             numeric(14,6)
  DECLARE  @TotalPrice 	             numeric(14,6)

  DECLARE  @TAX1 	              numeric(14,6) --gst
  DECLARE  @TAX2 		 numeric(14,6) --pst/hst
  DECLARE  @GROSS		 numeric(14,6)
  DECLARE  @NET		 numeric(14,6)


  DECLARE  @CustomerOrderHeaderInstance       INT
  DECLARE  @Transid				INT
  DECLARE  @CampaignID			INT  
  DECLARE  @ProductCode              	 	VARCHAR(10)
  DECLARE  @ProgramSectionid 			INT
  DECLARE  @PricingDetailsid	 		INT
  DECLARE  @Price				 numeric(14,6)



  select top 1 @Province =  ad.stateprovince
         from 	QSPcanadaOrderManagement.dbo.Batch as batch,
		qspcanadacommon.dbo.cAccount as ac,
		qspcanadacommon.dbo.Address as ad
 	 where 	batch.accountid  = ac.id
		and ac.addresslistid = ad.addresslistid
		and address_type = 54001
		and batch.orderid = @ORDERID ;


 Declare Cur_COD_INFO Cursor  For

 select  cod.CustomerOrderHeaderInstance,cod.Transid,batch.CampaignID, cod.ProductCode,cod.programSectionid,cod.PricingDetailsid,cod.Price
 from 	QSPcanadaOrderManagement.dbo.customerorderdetail as cod,
	QSPcanadaOrderManagement.dbo.customerorderheader as coh,
	QSPcanadaOrderManagement.dbo.batch 		 as Batch
 where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.instance = cod.customerorderheaderinstance
--and cod.programSectionid <> 0 --dont fetch bad data
 and batch.orderid  = @ORDERID
 order by cod.CustomerOrderHeaderInstance, cod.Transid

	---- Build the temp table
	CREATE TABLE #temp
		(
			tax1  	numeric(14,6),
			tax2  	numeric(14,6),
			gross  	numeric(14,6),
			net  	numeric(14,6)
		)  


set @TotalTax 	= 0
set @TotalPrice = 0



OPEN Cur_COD_INFO

	    FETCH NEXT FROM Cur_COD_INFO
	    INTO @CustomerOrderHeaderInstance, @Transid, @CampaignID, @ProductCode, @ProgramSectionid, @PricingDetailsid,@Price

	    WHILE @@FETCH_Status = 0
                  

                BEGIN


	  -- Select  @CustomerOrderHeaderInstance, @Transid, @CampaignID as ca, @ProductCode as pc, @ProgramSectionid as ps, @PricingDetailsid as pd,@Price,@Province

	   Insert into #temp   EXEC  qspcanadacommon.DBO.PR_CALC_ORDER_ITEM_AMOUNTS '09/30/2004',@Price,@ProgramSectionid,@ProductCode,null,@CampaignID,@PricingDetailsid,@Province
	
	   
	  Select top 1 @TAX1 = Tax1, @TAX2 = Tax2, @Gross = gross, @Net = net  From #temp

	


               --now update cod with new values

	 update  QSPcanadaOrderManagement.dbo.customerorderdetail
               set        tax  = @TAX1,  taxA  = @TAX1,tax2 = @TAX2, tax2A = @TAX2,gross = @Gross, net=@Net
              where CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	and  Transid = @Transid;



	delete from #temp

	  --Select @V_TAX_1 as OldTax, @TAX1 as NewTax  
	 -- Set @TotalTax = @TotalTax + @TAX1
	 -- Set @TotalPrice = @TotalPrice + @Price
                                     
	  FETCH NEXT FROM Cur_COD_INFO
	  INTO @CustomerOrderHeaderInstance, @Transid, @CampaignID, @ProductCode, @ProgramSectionid, @PricingDetailsid,@Price
  
                END

	CLOSE Cur_COD_INFO
	DEALLOCATE Cur_COD_INFO
	

	drop table #temp

 -- select @TotalTax as Totaltax
 -- select @TotalPrice as TotalPrice



-----------------------------------------------------------------------------------------
GO
