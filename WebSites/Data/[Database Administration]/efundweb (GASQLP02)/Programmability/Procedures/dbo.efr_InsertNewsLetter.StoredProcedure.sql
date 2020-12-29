USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_InsertNewsLetter]    Script Date: 02/14/2014 13:04:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[efr_InsertNewsLetter](
	@newsletter_id as int OUTPUT,
	@strReferrer AS VARCHAR(50),
	@strFullName AS VARCHAR(50), 
	@strEmail AS VARCHAR(50),
	@intPartnerID AS INTEGER)
AS
	INSERT INTO NewsLetter(Referrer, Email, FullName, Subscribe_Date, Partner_ID)
	VALUES(@strReferrer, @strEmail, @strFullName, GETDATE(), @intPartnerID)
	select @newsletter_id = SCOPE_IDENTITY()
GO
