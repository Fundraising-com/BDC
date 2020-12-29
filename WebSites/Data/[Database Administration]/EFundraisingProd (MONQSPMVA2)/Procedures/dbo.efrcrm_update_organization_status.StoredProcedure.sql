USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_organization_status]    Script Date: 02/14/2014 13:08:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Organization_Status
CREATE PROCEDURE [dbo].[efrcrm_update_organization_status] @Organization_Status_ID int, @Description varchar(200) AS
begin

update Organization_Status set Description=@Description where Organization_Status_ID=@Organization_Status_ID

end
GO
