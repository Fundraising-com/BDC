USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeletePhone]    Script Date: 06/07/2017 09:17:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DeletePhone]

	@iPhoneID	int

AS

	DELETE FROM	Phone
	WHERE	ID = @iPhoneID
GO
