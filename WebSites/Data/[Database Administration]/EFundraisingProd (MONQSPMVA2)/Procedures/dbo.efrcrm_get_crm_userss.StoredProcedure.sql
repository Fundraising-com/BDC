USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_crm_userss]    Script Date: 02/14/2014 13:04:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Crm_users
CREATE PROCEDURE [dbo].[efrcrm_get_crm_userss] AS
begin

select Consultant_ID, User_name, Password, Access_level from Crm_users

end
GO
