USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeleteProduct]    Script Date: 06/07/2017 09:17:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DeleteProduct]

	@iProductInstance		int

AS
	DELETE FROM Product WHERE Product_Instance = @iProductInstance
GO
