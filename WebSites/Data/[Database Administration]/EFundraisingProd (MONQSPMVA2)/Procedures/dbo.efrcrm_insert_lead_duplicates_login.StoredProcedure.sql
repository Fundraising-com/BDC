USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_duplicates_login]    Script Date: 02/14/2014 13:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Duplicates_Login
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_duplicates_login] @LEAD_DUPLICATES_LOGIN_ID int OUTPUT, @DUPLICATES_FOUND varchar(255), @RELATED_TABLE varchar(255), @DETECTED_FIELDS varchar(255), @FIELDS_VALUES varchar(255), @NT_LOGIN varchar(255), @MACHINE varchar(255), @TIME_STAMP smalldatetime AS
begin

insert into Lead_Duplicates_Login(DUPLICATES_FOUND, RELATED_TABLE, DETECTED_FIELDS, FIELDS_VALUES, NT_LOGIN, MACHINE, TIME_STAMP) values(@DUPLICATES_FOUND, @RELATED_TABLE, @DETECTED_FIELDS, @FIELDS_VALUES, @NT_LOGIN, @MACHINE, @TIME_STAMP)

select @LEAD_DUPLICATES_LOGIN_ID = SCOPE_IDENTITY()

end
GO
