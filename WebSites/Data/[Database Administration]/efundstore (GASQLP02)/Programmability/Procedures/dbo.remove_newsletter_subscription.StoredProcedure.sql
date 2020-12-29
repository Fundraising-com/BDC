USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[remove_newsletter_subscription]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 11, 2004
Description:	This stored procedure unsubscribes an email from our newsletters.
*/
CREATE PROCEDURE [dbo].[remove_newsletter_subscription]
	@strEmail VARCHAR(100)
AS
UPDATE 
	newsletter_subscriptions
SET	unsubscribed = 1
	, unsubscribed_date = GETDATE()
WHERE
	email = LTRIM( RTRIM( LOWER( @strEmail ) ) )
IF @@ERROR = 0
	RETURN( @@ROWCOUNT )
ELSE
	RETURN( -1 )
GO
