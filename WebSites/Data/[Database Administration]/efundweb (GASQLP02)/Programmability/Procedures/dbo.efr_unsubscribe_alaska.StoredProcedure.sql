USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_unsubscribe_alaska]    Script Date: 02/14/2014 13:04:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/06/20
-- Description:	Unsubscribe an email from 
--				the alaska promo.
-- =============================================
CREATE PROCEDURE [dbo].[efr_unsubscribe_alaska]
	@email varchar(256)
AS
BEGIN
	UPDATE tell_a_friend 
	SET bounced=1 
	WHERE To_email LIKE @email 
	  AND (bounced <> 1 OR bounced IS NULL)
END
GO
