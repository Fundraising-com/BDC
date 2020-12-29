USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_update_partner]    Script Date: 02/14/2014 13:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[efr_update_partner] @partner_id int, @phone_number varchar(25), @url varchar(50), @partner_path varchar(50)
as
update [partner]
set phone_number = @phone_number,
    url = @url,
    partner_path = @partner_path
where partner_id = @partner_id
GO
