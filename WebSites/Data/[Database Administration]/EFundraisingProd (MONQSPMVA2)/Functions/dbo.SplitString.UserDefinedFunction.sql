USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 02/14/2014 13:09:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[SplitString] 
    (
        @str nvarchar(4000), 
        @separator char(1)
    )
    returns table
    AS
    return (
        with tokens(p, a, b) AS (
            select 
                1, 
                1, 
                charindex(@separator, @str)
            union all
            select
                p + 1, 
                b + 1, 
                charindex(@separator, @str, b + 1)
            from tokens
            where b > 0
        )
        select
            p-1 zeroBasedOccurance,
            substring(
                @str, 
                a, 
                case when b > 0 then b-a ELSE 4000 end) 
            AS s
        from tokens
      )
GO
