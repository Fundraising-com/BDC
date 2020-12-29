USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_by_payment_batch_id]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_payment_by_payment_batch_id] @Batch_id int
	
AS
BEGIN
 SELECT [payment_id]
      ,[payment_type_id]
      ,[payment_info_id]
      ,[payment_period_id]
      ,[payment_batch_id]
      ,[cheque_number]
      ,[cheque_date]
      ,[paid_amount]
      ,[name]
      ,[phone_number]
      ,[address_1]
      ,[address_2]
      ,[city]
      ,[zip_code]
      ,[country_code]
      ,[subdivision_code]
      ,[create_date]
      ,[is_validated]
      ,[is_processed]
  FROM [dbo].[payment]
WHERE [payment_batch_id] = @Batch_id 
END
GO
