USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ForceCloseOrder]    Script Date: 06/07/2017 09:19:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_ForceCloseOrder]

	@iOrderID int

AS


--exec pr_CloseOrder @iOrderID
/*
declare @msg varchar(20)
select @msg = convert(varchar, @iOrderID)
exec QSPCanadaCommon..Send_EMail  'ForceClose@qsp.com','karen_tracy@readersdigest.com',
	'Force Close',  @msg
*/
SELECT	convert(bit, 1)
GO
