USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_admin]    Script Date: 02/14/2014 13:07:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Admin
CREATE PROCEDURE [dbo].[efrcrm_update_efo_admin] @Admin_ID int, @UID varchar(20), @Password varchar(32) AS
begin

update EFO_Admin set UID=@UID, Password=@Password where Admin_ID=@Admin_ID

end
GO
