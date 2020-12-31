USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdatePublisherContact]    Script Date: 06/07/2017 09:18:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdatePublisherContact]

	@iPublisherContactID	int,
	@zContactFirstName	varchar(30),
	@zContactLastName	varchar(30),
	@zPosition		varchar(50),
	@zEmail		varchar(50)

AS

	UPDATE	PUBLISHER_CONTACTS
	SET		PContact_FName = @zContactFirstName,
			PContact_LName = @zContactLastName,
			PContact_Title = @zPosition,
			PContact_Email = @zEmail
	WHERE	PContact_Instance = @iPublisherContactID
GO
