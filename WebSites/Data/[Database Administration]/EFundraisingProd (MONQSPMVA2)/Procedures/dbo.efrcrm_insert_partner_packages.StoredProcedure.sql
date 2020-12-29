USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_partner_packages]    Script Date: 02/14/2014 13:07:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_packages
CREATE PROCEDURE [dbo].[efrcrm_insert_partner_packages] @Partner_id int OUTPUT, @Package_id tinyint AS
begin

insert into Partner_packages(Package_id) values(@Package_id)

select @Partner_id = SCOPE_IDENTITY()

end
GO
