USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_partner]    Script Date: 02/14/2014 13:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner
CREATE PROCEDURE [dbo].[efrcrm_update_partner] @Partner_id int, @Partner_group_type_id tinyint, @Partner_subgroup_type_id tinyint, @Partner_name varchar(50), @Partner_path varchar(50), @Esubs_url varchar(150), @Estore_url varchar(150), @Free_kit_url varchar(150), @Logo varchar(50), @Phone_number varchar(25), @Email_ext varchar(50), @Url varchar(50), @Guid uniqueidentifier, @Prize_eligible bit, @Has_collection_site bit AS
begin

update Partner set Partner_group_type_id=@Partner_group_type_id, Partner_subgroup_type_id=@Partner_subgroup_type_id, Partner_name=@Partner_name, Partner_path=@Partner_path, Esubs_url=@Esubs_url, Estore_url=@Estore_url, Free_kit_url=@Free_kit_url, Logo=@Logo, Phone_number=@Phone_number, Email_ext=@Email_ext, Url=@Url, Guid=@Guid, Prize_eligible=@Prize_eligible, Has_collection_site=@Has_collection_site where Partner_id=@Partner_id

end
GO
