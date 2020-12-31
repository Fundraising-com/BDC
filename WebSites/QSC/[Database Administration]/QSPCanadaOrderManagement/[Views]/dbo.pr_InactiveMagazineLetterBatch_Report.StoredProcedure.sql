USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_InactiveMagazineLetterBatch_Report]    Script Date: 06/07/2017 09:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jeff Miles
-- Create date: 02/24/2007
-- Description:	Prints an inactive magazine generated report
-- =============================================
CREATE PROCEDURE [dbo].[pr_InactiveMagazineLetterBatch_Report]

	@iLetterBatchID					int

AS
BEGIN
    
	DECLARE	@zViewName		varchar(100),
			@iReason		int,
			@sqlStatement	nvarchar(4000)

	SELECT		@zViewName = lt.ViewName,
				@iReason = imlb.Reason
	FROM		LetterTemplate lt
	JOIN		LetterBatch lb
					ON	lb.LetterTemplateID = lt.ID
	JOIN		InactiveMagazineLetterBatch imlb
					ON	imlb.LetterBatchID = lb.ID
	WHERE		lb.ID = @iLetterBatchID

	SET @sqlStatement = 
	'SELECT		vw.*, ' + CONVERT(nvarchar, @iReason) + ' AS Reason
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
