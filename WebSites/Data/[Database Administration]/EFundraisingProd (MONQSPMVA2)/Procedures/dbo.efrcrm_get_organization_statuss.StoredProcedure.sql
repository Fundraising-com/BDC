USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_organization_statuss]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Organization_Status
CREATE PROCEDURE [dbo].[efrcrm_get_organization_statuss] AS
begin

select Organization_Status_ID, Description from Organization_Status

end
GO
