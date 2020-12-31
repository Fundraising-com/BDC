USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterBatch_Delete]    Script Date: 06/07/2017 09:20:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 10/10/2006
-- Description:	Marks a LetterBatch deleted
-- =============================================
CREATE PROCEDURE [dbo].[pr_LetterBatch_Delete]

	@iID		int

AS
BEGIN

	UPDATE		lb
	SET			lb.DeletedTF = 1
	FROM		LetterBatch lb
	WHERE		lb.ID = @iID
	AND			lb.DeletedTF = 0

END
GO
