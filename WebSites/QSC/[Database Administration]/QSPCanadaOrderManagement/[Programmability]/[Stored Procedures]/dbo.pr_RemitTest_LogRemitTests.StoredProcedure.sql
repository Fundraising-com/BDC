USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_LogRemitTests]    Script Date: 06/07/2017 09:20:27 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_RemitTest_LogRemitTests]

@iRunID int, @ID int, @iResultCodeInstance int

AS

INSERT INTO [dbo].[RemitTestHistory]
(
	[RunID]
	, [RemitTestID]
	, [ResultCodeInstance]
	, [TestDate]
)

VALUES
( 
	@iRunID
	, @ID
	, @iResultCodeInstance
	, getdate()
)
GO
