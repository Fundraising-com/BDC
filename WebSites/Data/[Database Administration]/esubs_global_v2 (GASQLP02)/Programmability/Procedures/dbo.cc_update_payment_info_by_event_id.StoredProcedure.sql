USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_payment_info_by_event_id]    Script Date: 02/14/2014 13:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[cc_update_payment_info_by_event_id]
    @event_id int
    , @payment_name varchar(100)
    , @on_behalf_of_name varchar(100)
    , @ship_to_name varchar(100)
    , @ssn varchar(50)
    , @address_1 varchar(100)
    , @city varchar(100)
    , @zip_code varchar(10)
    , @country_code nvarchar(2)
    , @subdivision_code nvarchar(7)

AS
BEGIN
    DECLARE @errorCode int
    declare @postal_address_id int
    declare @phone_number_id int
    declare @group_id int
    declare @behalf varchar(40)
    declare @ship_to varchar(40)

    select @group_id = group_id from event_group where event_id = @event_id
    
    BEGIN TRAN
        
           SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -1
        END

        UPDATE payment_info
           SET active = 0
        WHERE event_id= @event_id

       --va cherche les infos inchangees
        select @phone_number_id = phone_number_id, 
               @behalf = on_behalf_of_name,
               @ship_to =  ship_to_name
        from payment_info 
        WHERE event_id = @event_id
        
        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -2
        END
        
        INSERT INTO postal_address (
            address_1
            , city
            , zip_code
            , country_code
            , subdivision_code
            , create_date
        ) VALUES (
            @address_1
            , @city
            , @zip_code
            , @country_code
            , @subdivision_code
            , GETDATE()
        )
        
        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -3
        END

        SET @postal_address_id = SCOPE_IDENTITY()
        
        SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -4
        END

        INSERT INTO payment_info (
            group_id
            ,event_id
            , postal_address_id
            , payment_name
            ,phone_number_id
            , on_behalf_of_name
            , ship_to_name
        --    , ssn
            , active 
            , create_date
        ) VALUES (
            @group_id
            ,@event_id
            , @postal_address_id
            , @payment_name
            , @phone_number_id
            , @behalf
            , @ship_to
       --      , @ssn
            , 1
            , GETDATE()
        )
        
        SET @errorCode = @@error
        
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -5
        END
        
    --    SET @new_payment_info_id = SCOPE_IDENTITY()
        
        SET @errorCode = @@error
        
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -6
        END
        
    COMMIT TRAN
    RETURN 0
END
GO
