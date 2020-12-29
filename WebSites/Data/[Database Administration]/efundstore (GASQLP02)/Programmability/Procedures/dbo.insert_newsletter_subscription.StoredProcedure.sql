USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[insert_newsletter_subscription]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 11, 2004
Description:	This stored procedure inserts a subscription for a newsletter
*/
CREATE PROCEDURE [dbo].[insert_newsletter_subscription]
	@strName VARCHAR(100)
	, @strEmail VARCHAR(100)
	, @strReferrer VARCHAR(120)
	, @intPartnerID INT = 0
	, @strCultureName VARCHAR(10) = 'en-US'
AS
INSERT INTO newsletter_subscriptions(
	partner_id
	, language_id
	, referrer
	, email
	, fullname
)
SELECT 
	@intPartnerID
	, language_id
	, LTRIM( RTRIM( LOWER( @strReferrer ) ) )
	, LTRIM( RTRIM( LOWER( @strEmail ) ) )
	, LTRIM( RTRIM( @strName ) )
FROM	cultures
WHERE
	LOWER( culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )

IF @@ERROR = 0
	RETURN( 0 )
ELSE
	RETURN( -1 )
GO
