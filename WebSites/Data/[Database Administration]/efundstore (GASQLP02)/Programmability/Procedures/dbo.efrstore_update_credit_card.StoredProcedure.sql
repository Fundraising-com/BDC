USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_credit_card]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Credit_card
CREATE PROCEDURE [dbo].[efrstore_update_credit_card] @Credit_card_id int, @Online_user_id int, @Credit_card_type_id tinyint, @Credit_card varbinary, @Last_digits char(4), @Year_expire smallint, @Month_expire tinyint, @Date_created datetime, @Removed bit AS
begin

update Credit_card set Online_user_id=@Online_user_id, Credit_card_type_id=@Credit_card_type_id, Credit_card=@Credit_card, Last_digits=@Last_digits, Year_expire=@Year_expire, Month_expire=@Month_expire, Date_created=@Date_created, Removed=@Removed where Credit_card_id=@Credit_card_id

end
GO
