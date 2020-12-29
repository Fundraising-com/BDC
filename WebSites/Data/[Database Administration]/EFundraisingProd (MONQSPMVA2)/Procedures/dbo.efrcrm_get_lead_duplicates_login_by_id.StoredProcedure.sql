USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_duplicates_login_by_id]    Script Date: 02/14/2014 13:04:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Duplicates_Login
CREATE PROCEDURE [dbo].[efrcrm_get_lead_duplicates_login_by_id] @LEAD_DUPLICATES_LOGIN_ID int AS
begin

select LEAD_DUPLICATES_LOGIN_ID, DUPLICATES_FOUND, RELATED_TABLE, DETECTED_FIELDS, FIELDS_VALUES, NT_LOGIN, MACHINE, TIME_STAMP from Lead_Duplicates_Login where LEAD_DUPLICATES_LOGIN_ID=@LEAD_DUPLICATES_LOGIN_ID

end
GO
