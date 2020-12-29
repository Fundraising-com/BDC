USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_credit_cards]    Script Date: 02/14/2014 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Credit_card
CREATE PROCEDURE [dbo].[efrstore_get_credit_cards] AS
begin

select Credit_card_id, Online_user_id, Credit_card_type_id, Credit_card, Last_digits, Year_expire, Month_expire, Date_created, Removed from Credit_card

end
GO
