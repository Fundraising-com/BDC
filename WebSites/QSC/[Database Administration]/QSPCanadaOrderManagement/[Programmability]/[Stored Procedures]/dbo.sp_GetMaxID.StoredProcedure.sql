USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetMaxID]    Script Date: 06/07/2017 09:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_GetMaxID]
	@d datetime
AS

Declare @id int
Declare @date datetime

SELECT @date = date, @id = (isnull(max(id),1)) from Batch where date=@d and id between 1 and 19999 group by date

If (@id is NULL)
Begin
	Select date = @d, id = 0

End
ELSE
Begin
	SELECT date, isnull(max(id),1) from Batch where date=@d and id between 1 and 19999 group by date
End
GO
