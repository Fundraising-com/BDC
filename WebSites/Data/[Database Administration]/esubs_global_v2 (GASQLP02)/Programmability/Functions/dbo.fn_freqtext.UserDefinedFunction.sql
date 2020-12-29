USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_freqtext]    Script Date: 02/14/2014 13:08:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[fn_freqtext] (
@string varchar(255)
,@substring char(3)
)
returns int
AS
begin
    declare @count int
    declare @last int
    
    set @count = 0
    set @last = 0
    
    set @last = CHARINDEX(@substring, @string, 0)

    while @last > 0
    begin
        set @count = @count + 1
        set @last = CHARINDEX(@substring, @string, @last+1)
    end

    return @count
end
GO
