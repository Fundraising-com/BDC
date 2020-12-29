USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_update_partner]    Script Date: 02/14/2014 13:04:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[efr_update_partner] @partner_id int, @phone_number varchar(25), @url varchar(50), @partner_folder varchar(1024), @partner_password varchar(50)
as
update [partner]
set phone_number = @phone_number,
    url = @url,
    partner_folder = @partner_folder,
    partner_password = @partner_password
where partner_id = @partner_id
GO
