USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_PublisherContactProduct_Insert]    Script Date: 06/07/2017 09:17:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_PublisherContactProduct_Insert]

	@iPublisherContactID		int,
	@zProductCode			varchar(20),
	@iUserID			int

AS

	INSERT INTO	PublisherContact_Product
			(PublisherContactID,
			Product_Code,
			DateCreated,
			UserIDCreated,
			DateChanged,
			UserIDChanged)
	VALUES	(@iPublisherContactID,
			@zProductCode,
			GetDate(),
			@iUserID,
			GetDate(),
			@iUserID)

	SELECT SCOPE_IDENTITY()
GO
