USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[UpdateInventoryNotInInterfaceButInQSP]    Script Date: 06/07/2017 09:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[UpdateInventoryNotInInterfaceButInQSP]
as
	/* Process records in our inventory but not in Oracle */

	/* If they don't exist set on hand to qty reserved */
	update QSPCanadaProduct..ProductInventory set QtyOnHand =0 
		from QSPCanadaProduct..ProductInventory
		left join  om_tbl_product_inv_interface  on Product_code = OracleCode and
			DistributionCenterName=Distribution_Center_code
			where product_code is null 
			and QtyReserved < 0

	update QSPCanadaProduct..ProductInventory set QtyOnHand =QtyReserved 
		from QSPCanadaProduct..ProductInventory
		left join  om_tbl_product_inv_interface  on Product_code = OracleCode and
			DistributionCenterName=Distribution_Center_code
		where product_code is null 
			and QtyReserved >= 0

	insert into om_tbl_product_inv_interface
	(
		COUNTRY_CODE,
		DISTRIBUTION_CENTER_CODE,
		PRODUCT_CODE,
		ON_HAND,
		LOT_CONTROL,
		EXPIRY_DATE,
		EXTRACTED_DATE,
		IS_RECORD_POSTED_QSP,
		ERROR_CODE,
		ERROR_MESSAGE
	)
	select 'CA',
		DistributionCenterName,
		OracleCode,
		0,
		NULL,
		NULL,
		GetDate(),
		'Z', -- code for 0 on hand
		0,
		''
		from QSPCanadaProduct..ProductInventory
		left join  om_tbl_product_inv_interface  on Product_code = OracleCode and
			DistributionCenterName=Distribution_Center_code
		where product_code is null
GO
