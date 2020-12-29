USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_formatfullname]    Script Date: 02/14/2014 13:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_formatfullname]
(
    @ls_FirstName varchar(100),
    @ls_MiddleName varchar(100),
    @ls_LastName varchar(100)
)  
RETURNS varchar(500) 
BEGIN
    DECLARE @ls_CompleteName varchar(300)
    SET @ls_CompleteName = Isnull(@ls_FirstName, '') + ' ' + Isnull(@ls_MiddleName, '') + ' ' + Isnull(@ls_LastName, '')
RETURN LTrim(@ls_CompleteName)
END
GO
