USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_insert_promotion]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efrc_insert_promotion]
	@promotion_id int OUTPUT
	, @partner_id int 
	, @promotion_type_code char(3)
	, @promotion_name varchar(255) 
	, @is_displayable bit = 0
	, @script_name varchar(255) = NULL
	, @cookie_content varchar(255) = NULL
	, @promotion_destination_id int = NULL
	, @keyword varchar(255) = NULL
	, @active bit = 1
AS
BEGIN

begin transaction 
	declare @errorCode int

	insert into efrcommon..promotion(promotion_type_code, promotion_destination_id, promotion_name, script_name, active, create_date , cookie_content, keyword, is_displayable)
	select @promotion_type_code, @promotion_destination_id, @promotion_name, @script_name, @active, getdate() , @cookie_content, @keyword, @is_displayable

	SET @errorCode = @@error

	IF @errorCode <> 0
	BEGIN
		ROLLBACK TRAN
		RETURN -1
	END
	
	select @promotion_id = @@Identity
	
	insert into efrcommon..partner_promotion (partner_id, promotion_id, create_date)
	select @partner_id, @promotion_id, getdate()

	SET @errorCode = @@error

	IF @errorCode <> 0
	BEGIN
		ROLLBACK TRAN
		RETURN -2
	END
	
	
COMMIT TRANSACTION
END
GO
