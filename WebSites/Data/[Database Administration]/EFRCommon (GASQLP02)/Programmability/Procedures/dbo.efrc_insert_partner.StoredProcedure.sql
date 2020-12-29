USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_insert_partner]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efrc_insert_partner]
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
	, @linked_partner_id int = NULL   -- added by JIRO HIDAKA
as

declare @partner_id int 
declare @guid uniqueidentifier
declare @error int
 
EXEC @error = [efrc_create_partner] @partner_id OUTPUT, @guid OUTPUT
, @partner_name
, @partner_path
, @partner_path
, @program_url
, @esubs_url
, @template_description
, NULL -- pap id
, @culture_code
, @linked_partner_id
, NULL -- opportunity id
, @partner_type_id
, @has_collection_site
, @program_id
, @store_id
, @aggregator_id
, @has_movie_ticket
GO
