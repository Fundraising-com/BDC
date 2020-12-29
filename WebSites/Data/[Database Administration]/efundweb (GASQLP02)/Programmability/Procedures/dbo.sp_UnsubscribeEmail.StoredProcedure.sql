USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[sp_UnsubscribeEmail]    Script Date: 02/14/2014 13:04:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UnsubscribeEmail](
	@strEmail AS VARCHAR(50),
	@intPartnerID AS INTEGER)
AS
	IF EXISTS(select * from newsletter where email = @strEmail AND partner_Id = @intPartnerId) 
		UPDATE newsletter SET unsubscribed = 1
			WHERE email = @strEmail
			AND partner_Id = @intPartnerId
	ELSE
		INSERT INTO NewsLetter(Email, Unsubscribed, Subscribe_Date, Partner_ID, Referrer, Fullname)
		VALUES(@strEmail, 1, GETDATE(), @intPartnerID, '', @strEmail)
GO
