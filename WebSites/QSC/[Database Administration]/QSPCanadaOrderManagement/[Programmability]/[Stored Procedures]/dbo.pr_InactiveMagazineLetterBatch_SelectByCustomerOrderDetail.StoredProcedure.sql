USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_InactiveMagazineLetterBatch_SelectByCustomerOrderDetail]    Script Date: 06/07/2017 09:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jeff Miles
-- Create date: 09/03/2007
-- Description:	Gets Inactive MagazineLetter Batch by CustomerOrderDetail
-- =============================================
CREATE PROCEDURE [dbo].[pr_InactiveMagazineLetterBatch_SelectByCustomerOrderDetail]

	@iCustomerOrderHeaderInstance		int,
	@iTransID							int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT		lb.ID,
				lb.LetterTemplateID,
				lt.Name AS LetterTemplateName,
				lb.LetterBatchType,
				cdlbt.Description AS LetterBatchTypeDescription,
				lb.DateFrom,
				lb.DateTo,
				lb.RunID,
				lb.IsPrinted,
				lb.DatePrinted,
				lb.IsLocked,
				lb.UserIDCreated,
				cup.FirstName + ' ' + cup.LastName AS UserNameCreated,
				lb.DateCreated,
				lb.DeletedTF,
				lt.ReportName,
				imlb.ProductCode,
				imlb.Reason
	FROM		LetterBatch lb
	JOIN		LetterTemplate lt
					ON	lt.ID = lb.LetterTemplateID
	JOIN		QSPCanadaCommon..CodeDetail cdlbt
					ON	cdlbt.Instance = lb.LetterBatchType
	JOIN		QSPCanadaCommon..CUserProfile cup
					ON	cup.Instance = lb.UserIDCreated
	JOIN		LetterBatchCustomerOrderDetail lbcod
					ON	lbcod.LetterBatchID = lb.ID
	JOIN		InactiveMagazineLetterBatch imlb
					ON	imlb.LetterBatchID = lb.ID
	WHERE		lb.DeletedTF = 0
	AND			lbcod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND			lbcod.TransID = @iTransID
	ORDER BY	lb.DateCreated

END
GO
