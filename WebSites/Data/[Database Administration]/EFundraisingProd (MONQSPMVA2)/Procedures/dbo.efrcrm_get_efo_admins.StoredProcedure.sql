USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_admins]    Script Date: 02/14/2014 13:04:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Admin
CREATE PROCEDURE [dbo].[efrcrm_get_efo_admins] AS
begin

select Admin_ID, UID, Password from EFO_Admin

end
GO
