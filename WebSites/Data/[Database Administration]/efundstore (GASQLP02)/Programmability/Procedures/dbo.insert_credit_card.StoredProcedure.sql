USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[insert_credit_card]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	jf lavigne
Created On:	May 28, 2004
Description:	Insert an entry into table credit cards after a client as placed an order
*/
CREATE  PROCEDURE [dbo].[insert_credit_card]
	@intOnlineUserID INT
	, @intCardTypeID TINYINT
	, @strCreditCard VARBINARY(30)
	, @intLastDigits CHAR(4)
	, @intYearExpire SMALLINT
	, @intMonthExpire TINYINT 
AS
INSERT INTO dbo.credit_cards (
	online_user_id
	, credit_card_type_id
	, credit_card
	, last_digits
	, year_expire
	, month_expire
)
VALUES (
	@intOnlineUserID
	, @intCardTypeID
	, @strCreditCard
	, @intLastDigits
	, @intYearExpire 
	, @intMonthExpire
)
	
IF @@ERROR = 0
	RETURN( @@IDENTITY )
ELSE
	RETURN( -1 )
GO
