USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetUniqueRegularBatchID]    Script Date: 06/07/2017 09:19:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetUniqueRegularBatchID]
	@date datetime
AS

--SELECT  ISNULL(MAX(OrderID),1) as ID From Batch Where OrderID < 2500000

Declare @id int

--SELECT  @id = (isnull(max(OrderID),1)) from Batch where OrderID < 85000
SELECT @id=ISNULL(MAX(OrderID), 1000000) FROM Batch WHERE OrderID >= 1000000 -- Ben, 2005-09-14

If (@id = NULL)
Begin
	Select id = 0
End
ELSE
Begin
	--SELECT isnull(max(OrderID),1) from Batch where OrderID < 85000
	SELECT ISNULL(MAX(OrderID), 1000000) FROM Batch WHERE OrderID >= 1000000 -- Ben, 2005-09-14
End
GO
