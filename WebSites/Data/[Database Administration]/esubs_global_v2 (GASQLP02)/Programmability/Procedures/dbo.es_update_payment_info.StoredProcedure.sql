USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_info]    Script Date: 02/14/2014 13:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_update_payment_info]
    @old_payment_info_id int
    , @payment_name varchar(100)
    , @on_behalf_of_name varchar(100)
    , @ship_to_name varchar(100)
    , @phone_number varchar(20)
    , @ssn varchar(50)
    , @address_1 varchar(100)
    , @address_2 varchar(100)
    , @city varchar(100)
    , @zip_code varchar(10)
    , @country_code nvarchar(2)
    , @subdivision_code nvarchar(7)
    , @address_is_validated tinyint = null
    , @postal_address_id int OUTPUT
    , @phone_number_id int OUTPUT
    , @new_payment_info_id int OUTPUT
AS
BEGIN
    DECLARE @errorCode int
    DECLARE @group_id int
    DECLARE @event_id int
    
    BEGIN TRAN
        
        SELECT @group_id = group_id
               , @event_id = event_id
            FROM payment_info
            WHERE payment_info_id = @old_payment_info_id

        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -1
        END

        UPDATE payment_info
           SET active = 0
        WHERE payment_info_id = @old_payment_info_id
        
        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -2
        END
        
        INSERT INTO postal_address (
            address_1
            , address_2
            , city
            , zip_code
            , country_code
            , subdivision_code
            , create_date
	, matching_code
	, is_validated
        ) VALUES (
            @address_1
            , @address_2
            , @city
            , @zip_code
            , @country_code
            , @subdivision_code
            , GETDATE()
	, dbo.es_generate_matching_code(@address_1, @zip_code)
	, @address_is_validated
        )
        
        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -3
        END

        SELECT @postal_address_id = SCOPE_IDENTITY()

        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -4
        END
        
        INSERT INTO phone_number (
	        phone_number
	        , create_date
        ) VALUES (
	        @phone_number
	        , GETDATE()
        )

        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -5
        END

        SELECT @phone_number_id = SCOPE_IDENTITY()

        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -6
        END

        INSERT INTO payment_info (
            group_id
            , event_id
            , postal_address_id
            , phone_number_id
            , payment_name
            , on_behalf_of_name
            , ship_to_name
            , ssn
            , active 
            , create_date
        ) VALUES (
            @group_id
            , @event_id
            , @postal_address_id
            , @phone_number_id
            , @payment_name
            , @on_behalf_of_name
            , @ship_to_name
            , @ssn
            , 1
            , GETDATE()
        )
        
        SET @errorCode = @@error
        
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -7
        END
        
        SET @new_payment_info_id = SCOPE_IDENTITY()
        
        SET @errorCode = @@error
        
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -8
        END
        
    COMMIT TRAN
    RETURN 0
END
GO
