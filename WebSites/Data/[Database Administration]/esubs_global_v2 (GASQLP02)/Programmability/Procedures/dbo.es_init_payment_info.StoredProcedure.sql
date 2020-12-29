USE [esubs_global_v2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: March 8 2015
-- Description:	Initialize payment_info
-- =============================================
ALTER PROCEDURE [dbo].[es_init_payment_info]
	@event_id int
AS
BEGIN
	DECLARE @group_id INT, @postal_address_id INT
	
	SELECT @group_id = group_id FROM [event_group] WHERE event_id = @event_id;
	
	IF NOT EXISTS (SELECT * FROM [esubs_global_v2].[dbo].[payment_info] where event_id = @event_id and active=1)
	BEGIN
		BEGIN TRANSACTION
		
		INSERT INTO [esubs_global_v2].[dbo].[postal_address]
			   ([address_1]
			   ,[address_2]
			   ,[city]
			   ,[zip_code]
			   ,[country_code]
			   ,[subdivision_code]
			   ,[create_date]
			   ,[matching_code]
			   ,[is_validated])
		 VALUES
			   (NULL
			   ,NULL
			   ,NULL
			   ,NULL
			   ,'US'
			   ,NULL
			   ,GETDATE()
			   ,NULL
			   ,NULL);
	     
		 IF @@error <> 0
		 BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		 END

		 SET @postal_address_id = SCOPE_IDENTITY()

		 IF @@error <> 0
		 BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		 END

		 INSERT INTO [esubs_global_v2].[dbo].[payment_info]
			   ([group_id]
			   ,[postal_address_id]
			   ,[phone_number_id]
			   ,[payment_name]
			   ,[on_behalf_of_name]
			   ,[ship_to_name]
			   ,[ssn]
			   ,[active]
			   ,[create_date]
			   ,[event_id])
		 VALUES
			   (@group_id
			   ,@postal_address_id
			   ,NULL
			   ,''
			   ,NULL
			   ,NULL
			   ,NULL
			   ,1
			   ,GETDATE()
			   ,@event_id);
	     
		 IF @@error <> 0
		 BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		 END

		 COMMIT TRANSACTION
		 RETURN 1
	 END
END
GO
