USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[efr_insert_partner_all_DB]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_insert_partner_all_DB]
	 @partner_type_id int
	, @partner_name varchar(100)
	, @has_collection_site int
	, @program_id int
	, @program_url varchar(1000)
	, @culture_code varchar(10)
	, @partner_path varchar(10)
	, @esubs_url varchar(1000)
	, @store_id int
	, @aggregator_id int
	, @template_description  varchar(100)
	, @has_movie_ticket int
	, @linked_partner_id int = NULL		-- added by JIRO HIDAKA
	, @opportunity_id int = NULL		-- added by MELISSA COTE 
as

begin transaction 

declare @partner_id int
declare @guid as uniqueidentifier
declare @errorCode int
declare @linked_partner_culture_code varchar(10)

select @partner_id = max(partner_id)+ 1 from partner

set @guid =newid()

insert into partner
(
	partner_id
	,partner_type_id
	, partner_name
	, has_collection_site
	, guid
	, create_date
)
select 
	@partner_id
	, @partner_type_id
	, @partner_name
	, @has_collection_site
	, @guid
	, getdate()

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -1
END


-- added by JIRO HIDAKA --
insert into dbo.partner_culture_link
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
    RETURN -2
END



IF @linked_partner_id IS NOT null
BEGIN
select @linked_partner_culture_code = culture_code from partner_culture_link
where partner_id = @linked_partner_id AND linked_partner_id = @linked_partner_id

insert into dbo.partner_culture_link
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
    RETURN -3
END

insert into dbo.partner_culture_link
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
    RETURN -4
END
--- END ADD ---
END

insert into program_partner(program_id,partner_id,program_url,create_date)
SELECT  @program_id,@partner_id,@program_url,getdate()

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -5
END

insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
select @partner_id ,1,@culture_code,@partner_path,getdate() 

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -6
END

insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
select @partner_id ,2,@culture_code,@esubs_url,getdate() 

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -7
END

insert into partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
select @partner_id ,3,@culture_code,@partner_name,getdate() 

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -8
END

declare @store_template_id int
declare @account_number int

if @has_collection_site = 1 
begin
	select top 1 @account_number = account_number from unused_account_number
	where account_number not in(select account_number from store_template where account_number is not null)
end
else
begin
	set @account_number = null
end

select @store_template_id = max(store_template_id)+1 from store_template

-- added by MELISSA COTE select * from store_template
IF @opportunity_id IS NOT null
BEGIN
	if @culture_code = 'en-US'
	begin
		insert into store_template(
			store_template_id
			,culture_code
			,store_id
			,aggregator_id
			,account_number
			,[description]
			,create_date
			,opportunity_id
		)values(
			@store_template_id
			,@culture_code
			,@store_id
			,@aggregator_id
			,@account_number
			,@template_description
			,getdate()
			,@opportunity_id
		)
	end
END 
ELSE 
BEGIN
	if @culture_code = 'en-US'
	begin
		insert into store_template(
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
END

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -9
END

if @culture_code = 'en-US'
begin
	insert into partner_store_template (partner_id,store_template_id,create_date)
	values(@partner_id,@store_template_id,getdate())	
end

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -10
END

--on insère dans webtracking
if @has_collection_site = 1 
begin
	exec @errorCode = web_tracking_2.dbo.wt_create_website 
			  @partner_id = @partner_id
			, @webproject_id = 1
			, @website_dns = @partner_name

	IF @errorCode <> 0
	BEGIN
	    ROLLBACK TRAN
	    RETURN -11
	END
end

-- =======================
-- on insère dans efundweb
--

EXEC @errorCode = efundweb.dbo.efr_create_partner
					 @partner_id = @partner_id
					,@partner_group_type_id = @partner_type_id
					,@partner_subgroup_type_id = 0
					,@country_id = 15
					,@partner_name = @partner_name
					,@partner_path = @partner_path
					,@esubs_url = @esubs_url
					,@estore_url = null
					,@url = @program_url
					,@logo = null
					,@guid = @guid
					,@prize_eligible = @has_movie_ticket 
					,@has_collection_site = @has_collection_site
					,@partner_folder = null

IF @errorCode <> 0
BEGIN
	ROLLBACK TRAN
    RETURN -12
END

-- =======================
-- on insère dans EFRCommon
--
insert into efrcommon.dbo.partner
(
	  partner_id
	, partner_type_id
	, partner_name
	, has_collection_site
	, guid
	, create_date
)
select 
	  @partner_id
	, @partner_type_id
	, @partner_name
	, @has_collection_site
	, @guid
	, getdate()

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -13
END

insert into efrcommon.dbo.partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
select @partner_id ,1,@culture_code,@partner_path,getdate() 

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -14
END

insert into efrcommon.dbo.partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
select @partner_id ,2,@culture_code,@esubs_url,getdate() 

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -15
END

insert into efrcommon.dbo.partner_attribute_value(partner_id,partner_attribute_id,culture_code,value,create_date)
select @partner_id ,3,@culture_code,@partner_name,getdate() 

SET @errorCode = @@error

IF @errorCode <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -16
END


--on insère dans efundraisingprod sur sqlerose
--Cette SP tranfère les partner et promotion
--exec sqlerose_efundraisingprod.efundraisingprod.dbo.efr_insert_partner 

/*
pas encore implanté dans la BD
if @has_movie_ticket = 1
begin
	insert into program_partner_prize(program_id,parnter_id,prize_id,create_date)
	values(@program_id,@partner_id,2,getdate()
	insert into program_partner_prize(program_id,parnter_id,prize_id,create_date)
	values(@program_id,@partner_id,5,getdate()
end
*/

COMMIT TRAN
GO
