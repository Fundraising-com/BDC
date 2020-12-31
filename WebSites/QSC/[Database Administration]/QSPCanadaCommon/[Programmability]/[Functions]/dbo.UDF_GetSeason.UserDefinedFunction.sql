USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetSeason]    Script Date: 06/07/2017 09:33:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE Function [dbo].[UDF_GetSeason](@Date datetime)
Returns Varchar(1)
As
Begin


declare @season varchar(1)

if (datepart(month, @Date)  >=7 and datepart(month, @Date) <=12)
begin
	select @season='F'
end
else
begin
	select @season='S'
end

Return @season

End
GO
