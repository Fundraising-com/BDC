USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_organization_status_by_id]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Organization_Status
CREATE PROCEDURE [dbo].[efrcrm_get_organization_status_by_id] @Organization_Status_ID int AS
begin

select Organization_Status_ID, Description from Organization_Status where Organization_Status_ID=@Organization_Status_ID

end
GO
