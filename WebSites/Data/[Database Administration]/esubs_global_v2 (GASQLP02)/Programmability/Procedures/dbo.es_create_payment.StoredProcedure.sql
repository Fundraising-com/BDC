USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_payment]    Script Date: 02/14/2014 13:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[es_create_payment]
    @payment_type_id int
    , @payment_info_id int
    , @payment_period_id int
    , @cheque_number int
    , @cheque_date datetime
    , @paid_amount money
    , @payment_id int OUTPUT
AS
BEGIN
    DECLARE @errorCode int
    
    DECLARE @name varchar(100)

    DECLARE @address_1 varchar(100)
    DECLARE @address_2 varchar(100)
    DECLARE @city varchar(100)
    DECLARE @zip_code varchar(10)
    DECLARE @country_code nvarchar(2)
    DECLARE @subdivision_code nvarchar(7)
    DECLARE @phone_number varchar(50)


    BEGIN TRAN
    
    SELECT
          @name = pinfo.payment_name
        , @address_1 = pa.address_1
        , @address_2 = pa.address_2
        , @city = pa.city
        , @zip_code = pa.zip_code
        , @country_code = pa.country_code
        , @subdivision_code = pa.subdivision_code
        , @phone_number = pn.phone_number
    FROM payment_info as pinfo
            INNER JOIN postal_address as pa
                ON pa.postal_address_id = pinfo.postal_address_id
	        INNER JOIN phone_number as pn
	            ON pn.phone_number_id = pinfo.phone_number_id
    WHERE pinfo.payment_info_id = @payment_info_id
    
    SET @errorCode = @@error

    IF @errorCode <> 0
    BEGIN
        ROLLBACK TRAN
        RETURN -1
    END 

    INSERT INTO payment (
        payment_type_id
        , payment_info_id
        , payment_period_id
        , cheque_number
        , cheque_date
        , paid_amount
        , [name]
        , phone_number	
        , address_1
        , address_2
        , city
        , zip_code
        , country_code
        , subdivision_code
    ) VALUES (
        @payment_type_id
        , @payment_info_id
        , @payment_period_id
        , @cheque_number
        , @cheque_date
        , @paid_amount
        , @name
        , @phone_number
        , @address_1
        , @address_2
        , @city
        , @zip_code
        , @country_code
        , @subdivision_code
    )
    
    SET @errorCode = @@error

    IF @errorCode <> 0
    BEGIN
        ROLLBACK TRAN
        RETURN -1
    END 

    SET @payment_id = SCOPE_IDENTITY()
    
    SET @errorCode = @@error
    
    IF @errorCode <> 0
    BEGIN
        ROLLBACK TRAN
        RETURN -2
    END

    COMMIT TRAN
    RETURN 0
END
GO
