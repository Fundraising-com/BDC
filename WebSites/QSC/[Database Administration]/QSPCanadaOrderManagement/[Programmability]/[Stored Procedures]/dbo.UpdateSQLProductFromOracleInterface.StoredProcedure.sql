USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[UpdateSQLProductFromOracleInterface]    Script Date: 06/07/2017 09:20:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[UpdateSQLProductFromOracleInterface]
as
	declare @email varchar(2000)
	declare @subject varchar(200)
	declare @recipient varchar(1000) -- read from system options

	/* make a copy of everything */
	exec UpdateInventory
	exec UpdateInventoryNotInInterfaceButInQSP


	/* send an email on what happened */

	/** send out and email notification  **/
	declare @prod_count int
	declare @prod_processed int
	declare @prod_inv_errors int
	declare @zero_onhand_errors int

--select * from om_tbl_product_inv_interface
	select @prod_count = count(*) from om_tbl_product_inv_interface
	select @prod_processed=count(*) from om_tbl_product_inv_interface where IS_RECORD_POSTED_QSP ='Y'
	select @prod_inv_errors=count(*) from om_tbl_product_inv_interface where ERROR_MESSAGE is not null
	select @zero_onhand_errors=count(*) from om_tbl_product_inv_interface where IS_RECORD_POSTED_QSP ='Z'


	if(@prod_inv_errors > 0 or @zero_onhand_errors > 0)
	begin
		
		select @subject= 'Product Inventory Interface:  ERRORS exist!'
	end
	else if(@prod_processed = 0)
	begin
		select @subject= 'Product Inventory Interface not processed!'
	end
	else
	begin
		select @subject= 'Product Inventory Interface SUCCESSFULLY processed!'
	end

	select @email = 'Date and Time Run: ' + Convert( varchar(100),GetDate(), 100) + '\r\n'
	select @email = @email + ' Product Inventory Interface: ' + '\r\n'
	
	select @email = @email + ' Product Inventory Interface: No of Records ' + str(@prod_count) + 
			' Processed: '+ str(@prod_processed) + ' Zero on Hand: ' + str(@zero_onhand_errors) +
			' Errors: ' + str(@prod_inv_errors) + '\r\n'

	select @email = @email + 'Country Code: CA\r\n'
	select @email = @email + 'Product Code          Dis Centre Error Message\r\n'
	select @email = @email + '--------------------- ---------- -------------\r\n'

	/* push a few into the email - check the count of errors first */



/*
   exec QSPCanadaCommon..Send_EMail  'pr_RemitBatchAPInterface@qsp.com','karen_tracy@readersdigest.com',
					'pr_RemitBatchAPInterface', @Result
*/
GO
