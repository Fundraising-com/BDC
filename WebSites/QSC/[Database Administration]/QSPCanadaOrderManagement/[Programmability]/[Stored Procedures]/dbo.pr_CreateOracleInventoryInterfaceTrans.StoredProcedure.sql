USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CreateOracleInventoryInterfaceTrans]    Script Date: 06/07/2017 09:19:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      procedure [dbo].[pr_CreateOracleInventoryInterfaceTrans]
as
	
	insert into QSPOracleInterface..OM_TBL_INV_TRANS_INTERFACE_backup
		select * from QSPOracleInterface..OM_TBL_INV_TRANS_INTERFACE
	
	truncate table QSPOracleInterface..OM_TBL_INV_TRANS_INTERFACE

	insert QSPOracleInterface..OM_TBL_INV_TRANS_INTERFACE
	(
		COUNTRY_CODE,
		SEQUENCE_NUMBER,
		TRANSACTION_DATE,
		INPUT_SOURCE,
		DISTRIBUTION_CENTER_CODE,
		TRANSACTION_TYPE,
		PROCESS_FLAG,
		ERROR_MESSAGE,
		SEGMENT1,
		SEGMENT2,
		SEGMENT3,
		PRODUCT_CONDITION,
		CHANNEL,
		LOT_NUMBER,
		QUANTITY,
		REASON_CODE,
		LANGUAGE_CODE,
		PRODUCT_LINE
	)
	select 'CA', TransactionNumber, GetDate(), 'RDA QSP CA FULFILLMENT',
		DistributionCenterCode,
		TransactionType,
		'1',
		NULL,
		substring(OracleCode,1, 10),
		substring(OracleCode,11, 2),
		substring(OracleCode,13, 2),
		ProductCondition,
		Channel,
		Null,
		TransactionQty,
		Null,
		OraLanguageCode,
		QSPProductLine from
		InventoryOracleTrans,ShipmentBatch--, QSPCanadaCommon..QSPProductLine QPL
		where InventoryOracleTrans.ShipmentBatchID = ShipmentBatch.ID
			and CountryCode='CA'
			and  SentToOracle=0
--			and QPL.ID = QSPProductline
--			and QSPProductLine > 46000

/*
	insert QSPOracleInterface..OM_TBL_INV_TRANS_INTERFACE
	(
		COUNTRY_CODE,
		SEQUENCE_NUMBER,
		TRANSACTION_DATE,
		INPUT_SOURCE,
		DISTRIBUTION_CENTER_CODE,
		TRANSACTION_TYPE,
		PROCESS_FLAG,
		ERROR_MESSAGE,
		SEGMENT1,
		SEGMENT2,
		SEGMENT3,
		PRODUCT_CONDITION,
		CHANNEL,
		LOT_NUMBER,
		QUANTITY,
		REASON_CODE,
		LANGUAGE_CODE,
		PRODUCT_LINE
	)
	select 'CA', TransactionNumber, GetDate(), 'RDA QSP CA FULFILLMENT',
		DistributionCenterCode,
		TransactionType,
		'1',
		NULL,
		substring(OracleCode,1, 10),
		substring(OracleCode,11, 2),
		substring(OracleCode,13, 2),
		ProductCondition,
		Channel,
		Null,
		TransactionQty,
		Null,
		OraLanguageCode,
		QSPProductLine from
		InventoryOracleTrans,ShipmentBatch--, QSPCanadaCommon..QSPProductLine QPL
		where InventoryOracleTrans.ShipmentBatchID = ShipmentBatch.ID
			and CountryCode='CA'
			and  SentToOracle=0
--			and QPL.ID = QSPProductline+46000
--			and QSPProductLine < 46000

*/

	if(@@error <> 0)
	begin
		return 1

	end

	update ShipmentBatch Set DateSentToOracle=GetDate(), SentToOracle=1 where
		CountryCode='CA' and SentToOracle=0
GO
