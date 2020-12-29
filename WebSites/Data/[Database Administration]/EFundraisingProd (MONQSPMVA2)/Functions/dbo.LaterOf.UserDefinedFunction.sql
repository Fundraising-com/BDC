USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[LaterOf]    Script Date: 02/14/2014 13:09:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SELECT LaterOf ('2012-01-02','2012-01-01')


CREATE FUNCTION [dbo].[LaterOf] (
      @Date1                              DATETIME,
      @Date2                              DATETIME
)
 
RETURNS DATETIME
 
AS
BEGIN
      DECLARE @ReturnDate     DATETIME
 
      IF (@Date1 IS NULL AND @Date2 IS NULL)
      BEGIN
            SET @ReturnDate = NULL
            GOTO EndOfFunction
      END
 
      ELSE IF (@Date1 IS NULL AND @Date2 IS NOT NULL)
      BEGIN
            SET @ReturnDate = @Date2
            GOTO EndOfFunction
      END
 
      ELSE IF (@Date1 IS NOT NULL AND @Date2 IS NULL)
      BEGIN
            SET @ReturnDate = @Date1
            GOTO EndOfFunction
      END
 
      ELSE
      BEGIN
            SET @ReturnDate = @Date1
            IF @Date2 > @Date1
                  SET @ReturnDate = @Date2
            GOTO EndOfFunction
      END
 
      EndOfFunction:
      RETURN @ReturnDate
 
END -- End Function
GO
