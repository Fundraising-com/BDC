USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_PublisherContactProduct_Delete]    Script Date: 06/07/2017 09:17:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_PublisherContactProduct_Delete]

	@iID	int

AS

	DELETE FROM	PublisherContact_Product
	WHERE	ID = @iID
GO
