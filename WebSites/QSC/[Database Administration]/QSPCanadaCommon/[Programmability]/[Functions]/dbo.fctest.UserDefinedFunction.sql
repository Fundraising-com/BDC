USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[fctest]    Script Date: 06/07/2017 09:33:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[fctest] (@TEST INT)
RETURNS varchar
AS  
BEGIN 
declare @test1 varchar(100)
 set @test1 = 'test by saqib from function'
 RETURN @test1
END
GO
