USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_create_partner]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
example
declare @partner_id int 
declare @guid uniqueidentifier
declare @error int
 
--EXEC @error = [efrc_create_partner] @partner_id OUTPUT, @guid OUTPUT, 'partner_name', 'partner_path', 'partner_folder', 'myurl.fundraising.com', 'template'
EXEC @error = [efrc_create_partner] @partner_id OUTPUT, @guid OUTPUT, 'NEW TEST PARTNER 2005', 'partner_path5', 'partner_folder5', 'www.fundraising.com/partnername5', 'myurl.fundraising.com', 'template5'

print 'partner info'
print @partner_id 
print @guid 
print @error

*/
CREATE   procedure [dbo].[efrc_create_partner]
	 @partner_id int OUTPUT	
	, @guid uniqueidentifier OUTPUT
	
	, @partner_name varchar(100)
	, @partner_path varchar(20) -- ONLINE
	, @partner_folder VARCHAR(20) -- TRAD
	, @program_url varchar(1000) = ''
	, @esubs_url varchar(1000)
	, @template_description  varchar(100)
	
	, @pap_id varchar(50) = NULL -- For post affiliate pro integration
	, @culture_code varchar(10) = 'en-US'
	, @linked_partner_id int = NULL 
	, @payment_to int = 0 -- 0 = group, 1 = partner
	-- traditional specific
	-- online specific
	, @opportunity_id int = NULL
	, @partner_type_id int = 1
	, @has_collection_site int = 1
	, @program_id int = 1
	, @store_id int = 0
	, @aggregator_id int = 13
	, @has_movie_ticket int = 0
	, @profit_group_id int = 2
	, @product_offer_id int = 1
	, @is_promotion_needed int = 1 -- create promotion by default
	
	
as
BEGIN

begin transaction 

	--declare @partner_id int
	--declare @guid as uniqueidentifier
	declare @errorCode int
	declare @linked_partner_culture_code varchar(10)
	DECLARE @profit_percentage int
	declare @promotion int 
	declare @promotion_name varchar(255)
	declare @script_name varchar(255)
	
	
	-- if partner exist match via partner_folder
	select @partner_id = partner_id from partner_attribute_value where partner_attribute_id = 11 and value  = @pap_id
	IF @partner_id is not null and @pap_id is not null 
	begin

			insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
			select @partner_id ,12,@culture_code,@pap_id,getdate() 

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -8
			END
		
			select  @promotion_name = partner_name + ' PAP' from partner where partner_id = @partner_id
			exec [efrc_insert_promotion] @promotion OUTPUT, @partner_id, 'PAP', @promotion_name, 0, @pap_id, @pap_id
			
			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -8
			END

	END
	ELSE 
	BEGIN
		--select @partner_id = max(partner_id)+ 1 from partner

		select @guid =newid()

		/* create the partner */ 
		insert into partner
		(
			-- partner_id, no more need to provide it's now auto-increment
			partner_type_id
			, partner_name
			, has_collection_site
			, guid
			, create_date
			, is_active
		)
		select 
			--@partner_id,
			@partner_type_id
			, @partner_name
			, @has_collection_site
			, @guid
			, getdate()
			, 1

		select @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -1
		END

		set @partner_id = @@IDENTITY
		
		-- partner name 
		insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
		select @partner_id ,3,@culture_code,@partner_name,getdate() 

		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -2
		END

		-- partner path
		if @partner_path is not null
		begin
			insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
			select @partner_id ,1,@culture_code,@partner_path,getdate() 

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -3
			END
		END
		-- duplicate fundraising.com partner path
		if @partner_path is not null
		begin
			insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
			select @partner_id ,14,@culture_code,@partner_path,getdate() 

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -3
			END
		END

		-- esubs_url 
		if @esubs_url is not null
		begin
			insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
			select @partner_id ,2,@culture_code,@esubs_url,getdate() 

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -4
			END
		END

		-- @country_id = 15
		insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
		select @partner_id ,7,@culture_code,15,getdate() 

		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -5
		END
		-- trad url @url = @program_url
		if @program_url is not null
		begin
			insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
			select @partner_id ,10,@culture_code,@program_url,getdate() 

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -6
			END
		END
		-- @partner_folder 
		insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
			select @partner_id ,11,@culture_code,@partner_folder,getdate() 

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -7
			END
		-- @estore_url = NULL
		-- @pap_id
		if @pap_id is not null
		begin
		insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
			select @partner_id ,12,@culture_code,@pap_id,getdate() 

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -8
			END
		END
		-- esubs specific information
		insert into esubs_global_v2.dbo.partner_culture_link
		(
			partner_id 
			,culture_code
			,linked_partner_id
		)
		VALUES(@partner_id, @culture_code, @partner_id) -- LINK TO ITSELF

		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -100
		END

		IF @linked_partner_id IS NOT null
		BEGIN
			select @linked_partner_culture_code = culture_code from esubs_global_v2..partner_culture_link
			where partner_id = @linked_partner_id AND linked_partner_id = @linked_partner_id

			insert into esubs_global_v2.dbo.partner_culture_link
			(
				partner_id 
				,culture_code
				,linked_partner_id
			)
			VALUES(@partner_id, @linked_partner_culture_code, @linked_partner_id) -- PARTNER_ID LINK TO LINKED_PARTNER_ID

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -101
			END

			insert into esubs_global_v2.dbo.partner_culture_link
			(
				partner_id 
				,culture_code
				,linked_partner_id
			)
			VALUES(@linked_partner_id, @culture_code, @partner_id) -- LINKED_PARTNER_ID LINK TO PARTNER_ID

			SET @errorCode = @@error

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -102
			END
		END

		insert into esubs_global_v2..program_partner(program_id,partner_id,program_url,create_date)
		SELECT  @program_id,@partner_id,@program_url,getdate()

		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -103
		END


		-- Create profit info in EFRCommon:
		INSERT INTO EFRCommon.dbo.partner_profit(partner_id,start_date,profit_group_id)
		VALUES (@partner_id, GETDATE(), @profit_group_id)


		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -104
		END


		-- Create product offer in esubs_global_v2:
		INSERT INTO esubs_global_v2.dbo.partner_product_offer(partner_id,product_offer_id)
		VALUES (@partner_id, @product_offer_id)

		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -105
		END

		-- Create partner_payment_config:
		select @profit_percentage = profit_percentage 
		from EFRCommon.dbo.profit 
		where profit_group_id = @profit_group_id and qsp_catalog_type_id is null

		INSERT INTO esubs_global_v2.dbo.partner_payment_config(partner_id,profit_percentage,payment_to,email_template_id,first_email_template_id,is_default)
		VALUES (@partner_id, @profit_percentage,@payment_to,336,336,0)

		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -106
		END


		-- store related insertion
		declare @store_template_id int
		declare @account_number int

		if @has_collection_site = 1 
		begin
			select top 1 @account_number = account_number
			--select account_number 
			from esubs_global_v2..unused_account_number
			where account_number not in(select account_number from esubs_global_v2..store_template where account_number is not null)
		end
		else
		begin
			set @account_number = null
		end

		select @store_template_id = max(store_template_id)+1 from esubs_global_v2..store_template

		if @culture_code = 'en-US'
		begin
			insert into esubs_global_v2..store_template(
				store_template_id
				,culture_code
				,store_id
				,aggregator_id
				,account_number
				,[description]
				,create_date
			)values(
				@store_template_id
				,@culture_code
				,@store_id
				,@aggregator_id
				,@account_number
				,@template_description
				,getdate()
			)
		end

		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -200
		END

		if @culture_code = 'en-US'
		begin
			insert into esubs_global_v2..partner_store_template (partner_id,store_template_id,create_date)
			values(@partner_id,@store_template_id,getdate())	
		end

		SET @errorCode = @@error

		IF @errorCode <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -201
		END

		
		--select * from promotion_type
		-- Create default promotions
		if @is_promotion_needed = 1 
		begin
			--create all default promotion 
			

			SET @promotion_name = @partner_name + ' Direct'
			set @script_name = 'URL-' + convert(varchar(200), @partner_id)
			exec [efrc_insert_promotion] @promotion OUTPUT, @partner_id, 'PS', @promotion_name, 0, @script_name
			SET @promotion_name = @partner_name + ' Website'
			set @script_name = 'HTTP_REFERER-' + convert(varchar(200), @partner_id)
			exec [efrc_insert_promotion] @promotion OUTPUT, @partner_id, 'PS', @promotion_name, 0, @script_name
			SET @promotion_name = @partner_name + ' Search Engine'
			set @script_name = 'SEARCH_ENGINE-' + convert(varchar(200), @partner_id)
			exec [efrc_insert_promotion] @promotion OUTPUT, @partner_id, 'PS', @promotion_name, 0, @script_name
			
			SET @promotion_name = @partner_name + ' Self-Registration'
			exec [efrc_insert_promotion] @promotion OUTPUT, @partner_id, 'ON', @promotion_name
		end 
		if @pap_id is not null 
		begin
			SET @promotion_name = @partner_name + ' PAP'
			exec [efrc_insert_promotion] @promotion OUTPUT, @partner_id, 'PAP', @promotion_name, 0, @pap_id, @pap_id
		end
		
		--TBD insertion to webtracking db as per Jiro we should keep for now 
		if @has_collection_site = 1 
		begin
			exec @errorCode = web_tracking_2.dbo.wt_create_website 
					  @partner_id = @partner_id
					, @webproject_id = 1
					, @website_dns = @partner_name

			IF @errorCode <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -202
			END
		end

	END
	COMMIT TRAN

END
GO
