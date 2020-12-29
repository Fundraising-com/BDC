USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_credit_card_by_id]    Script Date: 02/14/2014 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Credit_card
CREATE PROCEDURE [dbo].[efrstore_get_credit_card_by_id] @Credit_card_id int AS
begin

select Credit_card_id, Online_user_id, Credit_card_type_id, Credit_card, Last_digits, Year_expire, Month_expire, Date_created, Removed from Credit_card where Credit_card_id=@Credit_card_id

end
GO
