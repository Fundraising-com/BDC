USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_admin]    Script Date: 02/14/2014 13:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Admin
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_admin] @Admin_ID int OUTPUT, @UID varchar(20), @Password varchar(32) AS
begin

insert into EFO_Admin(UID, Password) values(@UID, @Password)

select @Admin_ID = SCOPE_IDENTITY()

end
GO
