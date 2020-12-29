USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_insert_partner]    Script Date: 02/14/2014 13:03:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
CREATE BY : Melissa Cote
CREATION DATE : Febuary 18, 2005
Insert a partner in the db... And update the promotions
*/	
CREATE PROCEDURE [dbo].[efr_insert_partner] 
AS
BEGIN 
	INSERT INTO partner (
		partner_id
		, partner_group_type_id	-- default value 1
		, partner_subgroup_type_id -- default value 0
		--, template_desc_id 
		, partner_name
		, partner_path		-- where the template are located
		, esubs_url
		, estore_url
		, free_kit_url
		, phone_number		-- for traditionnal web site
		, email_ext
		, url			-- for traditionnal web site
		, prize_eligible
		, has_collection_site	-- indicate that they are doing esubs	
		--, partner_type_id	-- esubs / mvp / alumnie reporting purpices
		, GUID
	)
	SELECT 
		fw.partner_id
		, fw.partner_group_type_id
		, fw.partner_subgroup_type_id
		, fw.partner_name
		, fw.partner_path
		, fw.esubs_url
		, fw.estore_url
		, fw.free_kit_url
		, fw.phone_number 
		, fw.email_ext
		, fw.url 
		, fw.prize_eligible
		, fw.has_collection_site
		, fw.guid
	--SELECT * 
	FROM efundraisingprod.dbo.partner fp
		right outer join eFundweb.dbo.partner fw
			on fw.partner_id = fp.partner_id 
	where 
		fp.partner_id is null
	

	-- Transfer of all the promotion
	insert into efundraisingprod.dbo.promotion (
		Promotion_ID
		, Promotion_Type_Code
		, [Description]
		, Visibility
		, Contact_Name
		, Tracking_Serial
		, Nb_Impression_Bought
		, Is_Active
		, Targeted_Market_ID
		, Advertising_Support_ID
		, Advertisement_Id
		, Partner_ID
		, Cookie_Content
		, Grabber_Id
		, Is_Predictive
		, Advertiser_ID
		, Keyword
		, Script_Name
		, Advertisment_Type_ID
		, Destination_ID
		, Advertiser_Partner_ID
	)
	select 
		fw.Promotion_ID
		, fw.Promotion_Type_Code
		, fw.[Description]
		, fw.Visibility
		, fw.Contact_Name
		, fw.Tracking_Serial
		, fw.Nb_Impression_Bought
		, fw.Is_Active
		, fw.Targeted_Market_ID
		, fw.Advertising_Support_ID
		, fw.Advertisement_Id
		, fw.Partner_ID
		, fw.Cookie_Content
		, fw.Grabber_Id
		, fw.Is_Predictive
		, fw.Advertiser_ID
		, fw.Keyword
		, fw.Script_Name
		, fw.Advertisment_Type_ID
		, fw.Destination_ID
		, fw.Advertiser_Partner_ID
	from 	efundraisingprod.dbo.promotion fp
		right outer join eFundweb.dbo.promotion fw 
			on fw.promotion_id = fp.promotion_id 
	where 
		fp.promotion_id is null
END
GO
