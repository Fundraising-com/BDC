USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[wfq_TestLouis]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[wfq_TestLouis] @pFirstName AS VARCHAR(100), @pLastName AS VARCHAR(100), 
	@pEmail AS VARCHAR(100), @pCountry AS VARCHAR(100), @pState AS VARCHAR(100) 
AS

	INSERT INTO wfq_TestTableLouis(FirstName, LastName, Email, Country, State)
		VALUES(@pFirstName, @pLastName, @pEmail, @pCountry, @pState)
GO
