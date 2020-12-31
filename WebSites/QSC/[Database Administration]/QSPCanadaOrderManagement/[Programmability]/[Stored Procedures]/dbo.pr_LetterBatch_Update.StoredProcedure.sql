USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_Update]    Script Date: 06/07/2017 09:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 10/10/2006
-- Description:	Update a Letter Batch
-- =============================================
CREATE PROCEDURE [dbo].[pr_LetterBatch_Update]

	@iID			int,
	@bIsPrinted		bit,
	@dDatePrinted	datetime,
	@bIsLocked		bit	

AS
BEGIN

    UPDATE		lb
	SET			lb.IsPrinted = @bIsPrinted,
				lb.DatePrinted = @dDatePrinted,
				lb.IsLocked = @bIsLocked
	FROM		LetterBatch lb
	WHERE		lb.ID = @iID
	AND			lb.DeletedTF = 0

END
GO
