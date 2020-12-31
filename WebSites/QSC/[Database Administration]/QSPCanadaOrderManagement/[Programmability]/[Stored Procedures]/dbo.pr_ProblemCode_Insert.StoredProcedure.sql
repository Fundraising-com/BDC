USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProblemCode_Insert]    Script Date: 06/07/2017 09:20:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'ProblemCode'
-- Gets: @sDescription varchar(50)
-- Returns: @iInstance int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_ProblemCode_Insert]
	@sDescription varchar(50),
	@iInstance int OUTPUT
AS
-- INSERT a new row in the table.
INSERT [dbo].[ProblemCode]
(
	[Description]
)
VALUES
(
	@sDescription
)
-- Get the IDENTITY value for the row just inserted.
SELECT @iInstance=SCOPE_IDENTITY()
GO
