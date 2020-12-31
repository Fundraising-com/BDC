USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_Report]    Script Date: 06/07/2017 09:20:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 10/05/2006
-- Description:	Prints a generated report
-- =============================================
CREATE PROCEDURE [dbo].[pr_LetterBatch_Report]

	@iLetterBatchID					int

AS
BEGIN
    
	DECLARE	@zViewName		varchar(100),
			@sqlStatement	nvarchar(4000)

	SELECT		@zViewName = lt.ViewName
	FROM		LetterTemplate lt
	JOIN		LetterBatch lb
					ON	lb.LetterTemplateID = lt.ID
	WHERE		lb.ID = @iLetterBatchID

	SET @sqlStatement = 
	'SELECT		vw.*
	FROM		' + @zViewName + ' vw
	JOIN		LetterBatchCustomerOrderDetail lbcod
					ON	lbcod.CustomerOrderHeaderInstance = vw.CustomerOrderHeaderInstance
					AND	lbcod.TransID = vw.TransID
	JOIN		LetterBatch lb
					ON	lb.ID = lbcod.LetterBatchID
					AND	lb.DeletedTF = 0
					AND	lb.ID = ' + CONVERT(nvarchar, @iLetterBatchID) + '
	ORDER BY	vw.RecipientLastName,
				vw.RecipientFirstName,
				vw.CustomerOrderHeaderInstance,
				vw.TransID '

	EXEC(@sqlStatement)

END
GO
