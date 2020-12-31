USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_GetPreview]    Script Date: 06/07/2017 09:20:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 09/19/2006
-- Description:	Gets a letter batch preview
-- =============================================
CREATE PROCEDURE [dbo].[pr_LetterBatch_GetPreview]

	@iLetterTemplateID				int,
	@iRunID							int,
	@dDateFrom						datetime,
	@dDateTo						datetime,
	@iCustomerOrderHeaderInstance	int,
	@iTransID						int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE	@sqlStatement	nvarchar(4000)

	EXEC dbo.pr_LetterBatch_GetUnprocessedQuery @iLetterTemplateID, @iRunID, @dDateFrom, @dDateTo, @iCustomerOrderHeaderInstance, @iTransID, @sqlStatement OUTPUT

	SET @sqlStatement = 'SELECT vw.* ' + @sqlStatement + '
	ORDER BY	vw.RecipientLastName,
				vw.RecipientFirstName,
				vw.CustomerOrderHeaderInstance,
				vw.TransID '

	SET NOCOUNT OFF;

	EXEC(@sqlStatement)
END
GO
