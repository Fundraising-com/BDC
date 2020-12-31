USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_InactiveMagazineLetterBatch_GetUnprocessedCount]    Script Date: 06/07/2017 09:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jeff Miles
-- Create date: 02/23/2007
-- Description:	Gets the number of unprinted inactive magazine letters
--				for a set of parameters
-- =============================================
CREATE PROCEDURE [dbo].[pr_InactiveMagazineLetterBatch_GetUnprocessedCount]

	@iLetterTemplateID				int,
	@iRunID							int,
	@dDateFrom						datetime,
	@dDateTo						datetime,
	@iCustomerOrderHeaderInstance	int,
	@iTransID						int,
	@sProductCode					varchar(50),
	@iReason						int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE	@sqlParameterDefinition		nvarchar(4000),
			@sqlStatement				nvarchar(4000)

	EXEC dbo.pr_InactiveMagazineLetterBatch_GetUnprocessedQuery @iLetterTemplateID, @iRunID, @dDateFrom, @dDateTo, @iCustomerOrderHeaderInstance, @iTransID, @sProductCode, @iReason, @sqlStatement OUTPUT, @sqlParameterDefinition OUTPUT

	SET @sqlStatement = 'SELECT COUNT(*) ' + @sqlStatement

	SET NOCOUNT OFF;

	EXEC sp_executesql @sqlStatement, @sqlParameterDefinition,
		  @iLetterTemplateID = @iLetterTemplateID,
		  @iRunID = @iRunID,
		  @dDateFrom = @dDateFrom,
		  @dDateTo = @dDateTo,
		  @iCustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance,
		  @iTransID = @iTransID,
		  @sProductCode = @sProductCode,
		  @iReason = @iReason
END
GO
