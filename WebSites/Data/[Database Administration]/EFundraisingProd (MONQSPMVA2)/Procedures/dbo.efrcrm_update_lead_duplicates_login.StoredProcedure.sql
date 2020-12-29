USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_duplicates_login]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Duplicates_Login
CREATE PROCEDURE [dbo].[efrcrm_update_lead_duplicates_login] @LEAD_DUPLICATES_LOGIN_ID int, @DUPLICATES_FOUND varchar(255), @RELATED_TABLE varchar(255), @DETECTED_FIELDS varchar(255), @FIELDS_VALUES varchar(255), @NT_LOGIN varchar(255), @MACHINE varchar(255), @TIME_STAMP smalldatetime AS
begin

update Lead_Duplicates_Login set DUPLICATES_FOUND=@DUPLICATES_FOUND, RELATED_TABLE=@RELATED_TABLE, DETECTED_FIELDS=@DETECTED_FIELDS, FIELDS_VALUES=@FIELDS_VALUES, NT_LOGIN=@NT_LOGIN, MACHINE=@MACHINE, TIME_STAMP=@TIME_STAMP where LEAD_DUPLICATES_LOGIN_ID=@LEAD_DUPLICATES_LOGIN_ID

end
GO
