USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_insert_space_before_CapCase]    Script Date: 02/14/2014 13:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_insert_space_before_CapCase] (@str varchar(max))
returns varchar(max)
as
begin

declare
    @i int, @j int
,   @cp nchar, @c0 nchar, @c1 nchar
,   @result nvarchar(max)

select
    @i = 1
,   @j = len(@str)
,   @result = ''

while @i <= @j
begin
    select
        @cp = substring(@str,@i-1,1)
    ,   @c0 = substring(@str,@i+0,1)
    ,   @c1 = substring(@str,@i+1,1)

    if @c0 = UPPER(@c0) collate Latin1_General_CS_AS
    begin
        -- Add space if Current is UPPER 
        -- and either Previous or Next is lower
        -- and Previous or Current is not already a space
        if @c0 = UPPER(@c0) collate Latin1_General_CS_AS
        and (
                @cp <> UPPER(@cp) collate Latin1_General_CS_AS
            or  @c1 <> UPPER(@c1) collate Latin1_General_CS_AS
        )
        and @cp <> ' '
        and @c0 <> ' '
            set @result = @result + ' '
    end -- if @co

    set @result = @result + @c0
    set @i = @i + 1
end -- while

return @result
end
GO
