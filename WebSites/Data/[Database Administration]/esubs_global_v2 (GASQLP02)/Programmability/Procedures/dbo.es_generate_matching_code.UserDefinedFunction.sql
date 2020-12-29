USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_generate_matching_code]    Script Date: 02/14/2014 13:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_generate_matching_code] (@address varchar(127), @zip varchar(10))
RETURNS varchar(10) AS  
BEGIN 

DECLARE @flagpole varchar(10) 

--set @zip = '90218'
--set @address = '123 bellair'

IF qspcommon.dbo.fct_get_zzzzz(@zip) = '00000' OR qspcommon.dbo.fct_get_aa99(@address) = '****'
BEGIN
        SET @flagpole = 'invalid'
END
ELSE
BEGIN
 SET @flagpole = isnull(qspcommon.dbo.fct_get_zzzzz(@zip),'')+isnull(qspcommon.dbo.fct_get_aa99(@address),'')
END

return @flagpole   


END
GO
