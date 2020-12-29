USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_create_partner]    Script Date: 02/14/2014 13:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: February 7, 2006
-- Description:	Create a partner
-- =============================================
CREATE PROCEDURE [dbo].[efr_create_partner]
	@partner_id int
	, @partner_group_type_id tinyint
	, @partner_subgroup_type_id tinyint
    , @country_id int
    , @partner_name varchar(50)
    , @partner_path varchar(50)
	, @esubs_url varchar(150)
	, @estore_url varchar(150)
	, @free_kit_url varchar(150) = null
	, @logo varchar(50)
	, @phone_number varchar(25) = null
	, @email_ext varchar(50) = null
	, @url varchar(50)
	, @guid uniqueidentifier
	, @prize_eligible bit
	, @has_collection_site bit
	, @partner_folder varchar(1024)
AS
BEGIN
	DECLARE @errorCode int
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [eFundweb].[dbo].[partner] (
			[partner_id]
           ,[partner_group_type_id]
           ,[partner_subgroup_type_id]
           ,[country_id]
           ,[partner_name]
           ,[partner_path]
           ,[esubs_url]
           ,[estore_url]
           ,[free_kit_url]
           ,[logo]
           ,[phone_number]
           ,[email_ext]
           ,[url]
           ,[guid]
           ,[prize_eligible]
           ,[has_collection_site]
           ,[partner_folder]
	 ) VALUES ( 
			@partner_id
           ,@partner_group_type_id
           ,@partner_subgroup_type_id
           ,@country_id
           ,@partner_name
           ,@partner_path
           ,@esubs_url
           ,@estore_url
           ,@free_kit_url
           ,@logo
           ,@phone_number
           ,@email_ext
           ,@url
           ,@guid
           ,@prize_eligible
           ,@has_collection_site
           ,@partner_folder
	)
	SET @errorCode = @@error
	
	IF @errorCode <> 0
		RETURN -1
    
	RETURN 0
END
GO
