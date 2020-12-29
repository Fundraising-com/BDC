USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_payment_info_by_group_id]    Script Date: 02/14/2014 13:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cc_update_payment_info_by_group_id]
    @group_id int
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
    
    BEGIN TRAN
        
           SET @errorCode = @@error
    
        IF @errorCode <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -1
        END

        UPDATE payment_info
           SET active = 0
        WHERE group_id= @group_id
        
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
            , postal_address_id
            , payment_name
         --   , on_behalf_of_name
         --   , ship_to_name
        --    , ssn
            , active 
            , create_date
        ) VALUES (
            @group_id
            , @postal_address_id
            , @payment_name
        --    , @on_behalf_of_name
        --    , @ship_to_name
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
