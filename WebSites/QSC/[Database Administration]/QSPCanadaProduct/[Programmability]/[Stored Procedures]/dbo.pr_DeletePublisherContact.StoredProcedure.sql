USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeletePublisherContact]    Script Date: 06/07/2017 09:17:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DeletePublisherContact]

	@iPublisherContactID	int

AS

DELETE FROM	Publisher_Contacts
WHERE	PContact_Instance = @iPublisherContactID
GO
