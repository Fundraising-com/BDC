USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_PublisherLogin]    Script Date: 06/07/2017 09:17:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_PublisherLogin]

@zUsername varchar(50),
@zPassword varchar(50)

/*
	Name:		pr_PublisherLogin
	Usage:		Used to login on the Magazine Contract Authorization Form website
	Creation Date : 	March 8, 2005
	By: 		Nick Hamel
*/

 AS

DECLARE @pub_id int		-- publisher id

SET @pub_id = -1		-- Default is bad login :   -1 = bad, 0 = superuser, else publiserid

-- find the publisher id for this username/password
SELECT @pub_id = pub_nbr FROM Publishers WHERE Pub_UserName = @zUsername AND Pub_Password = @zPassword

-- if not found
IF @pub_id = -1
BEGIN
	--find if it is a superuser
	IF @zUsername = 'kphillips' and @zPassword = '1234567'
	BEGIN
		SET @pub_id=0
	END
END

SELECT @pub_id
GO
