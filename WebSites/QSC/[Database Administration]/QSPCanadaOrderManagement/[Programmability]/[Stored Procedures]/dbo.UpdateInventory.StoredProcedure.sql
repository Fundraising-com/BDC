USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[UpdateInventory]    Script Date: 06/07/2017 09:20:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateInventory]
as

	update om_tbl_product_inv_interface Set Is_Record_Posted_qsp='N'


	-- First find out where i don't have 'em yet
	select *  
		 from om_tbl_product_inv_interface 
		Left Join qspcanadaproduct..product on oraclecode=om_tbl_product_inv_interface.product_code
		where oraclecode is null
	
	if @@rowcount <> 0
	begin
		update om_tbl_product_inv_interface Set error_code=0, error_message= 'Product Code '+om_tbl_product_inv_interface.product_code+
			' does NOT exist in table Product'
		 from om_tbl_product_inv_interface 
			Left Join qspcanadaproduct..product on oraclecode=om_tbl_product_inv_interface.product_code
			where oraclecode is null
	end

	/* check that the distribution center codes are compatible with product codes 

		if 4st 4 characters numeric should be CA-10 aka QSP SC FGA

  	  	 1st two alpha and not PK or SA then  CA-11 aka QSP SC FGE

		1st two PK or SA then CA-12

		Need to see CA-13 in action		
	*/

	--Check CA-10

	select *  
	 from om_tbl_product_inv_interface where distribution_center_code='CA-10'
	and isnumeric(substring(product_code, 1,4)) = 0

	if @@rowcount <> 0
	begin
	
		update om_tbl_product_inv_interface set error_code=0,
			error_message = 'Distribution Center Code incorrect for Product code '+product_code		
			from om_tbl_product_inv_interface where distribution_center_code='CA-10'
			and isnumeric(substring(product_code, 1,4)) = 0
	end

	-- Check for CA-11 Alpha but not PK or SA
	select *  
		 from om_tbl_product_inv_interface where distribution_center_code='CA-11'
		and substring(product_code, 1,2) not in ('PK', 'SA') 
		and isnumeric(substring(product_code, 1,2)) = 1
		

	if @@rowcount <> 0
	begin
	
		update om_tbl_product_inv_interface set error_code=0,
			error_message = 'Distribution Center Code incorrect for Product code '+product_code		
			from om_tbl_product_inv_interface where distribution_center_code='CA-11'
			and substring(product_code, 1,2) not in ('PK', 'SA') 
			and isnumeric(substring(product_code, 1,2)) = 1

	end

	-- Check CA-12
	select *  
		 from om_tbl_product_inv_interface where distribution_center_code='CA-12'
			and substring(product_code, 1,2) not in ('PK', 'SA') 

	if @@rowcount <> 0
	begin
	
		update om_tbl_product_inv_interface set error_code=0,
			error_message = 'Distribution Center Code should be CA-12 for Product code '+product_code		
			from om_tbl_product_inv_interface where distribution_center_code='CA-12'
			and substring(product_code, 1,2) not in ('PK', 'SA') 

	end

	/** Update whatever is there **/
	Update QSPCanadaProduct..ProductInventory
		set QtyOnHand= On_Hand - QtyReserved, InventoryLoadDate=Extracted_date
		from QSPCanadaProduct..ProductInventory, om_tbl_product_inv_interface
		where Product_code  = OracleCode and DistributionCenterName=DISTRIBUTION_CENTER_CODE

	select 1 from 	QSPCanadaProduct..ProductInventory where QtyReserved > QtyOnHand
	
	if @@rowcount <> 0
	begin
	
		update om_tbl_product_inv_interface set error_code=0,
			error_message = 'Insufficient qty on hand ('+str(QtyOnHand)+'), Current reserved qty '		
			+ Str(QtyReserved)
			from 	QSPCanadaProduct..ProductInventory where QtyReserved > QtyOnHand
	end

	/** Insert into inventory any new records **/
	Insert Into QSPCanadaProduct..ProductInventory
	( 
		DistributionCenterName,
		OracleCode,
		QtyOnHand,
		QtyReserved,
		InventoryLoadDate
	)
	select DISTRIBUTION_CENTER_CODE,
		PRODUCT_CODE,
		ON_HAND,
		0,
		Extracted_date
		from om_tbl_product_inv_interface where Product_code 
			not in (select OracleCode from QSPCanadaProduct..ProductInventory)

	
	if(@@Error<> 0)
	begin
		-- bad thing send an email
	   exec QSPCanadaCommon..Send_EMail  'pr_RemitBatchAPInterface@qsp.com','qsp-qspfulfillment-dev@qsp.com',
					'pr_RemitBatchAPInterface', 'Error in inserting ProductInventory'

	end
	

	update om_tbl_product_inv_interface Set Is_Record_Posted_qsp='Y'
GO
