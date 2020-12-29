USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[sc_create_newsletter_subscription]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sc_create_newsletter_subscription]
    @partner_id int = 500
    , @language_code nvarchar(2) = 'en'
    , @email varchar(100)
    , @fullname varchar(100)
AS
BEGIN
    DECLARE @errorCode int
    
    BEGIN TRAN
        
        -- Si inscription existe deja
        IF EXISTS(SELECT subscription_id FROM newsletter_subscription WHERE email = @email)
        BEGIN
            ROLLBACK TRAN
            RETURN -1
        END

        INSERT INTO newsletter_subscription
        (
            partner_id
            , language_code
            , referrer
            , email
            , fullname
            , unsubscribed
            , subscribe_date
            , unsubscribe_date
        ) VALUES (
            @partner_id
            , @language_code
            , NULL
            , @email
            , @fullname
            , 0
            , GETDATE()
            , NULL
        )

        SET @errorCode = @@error	

    	IF (@errorCode <> 0)
    	BEGIN
      		ROLLBACK TRAN
    		RETURN -2
    	END        

    COMMIT TRAN
END
GO
