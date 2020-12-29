USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_credit_card]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Credit_card
CREATE PROCEDURE [dbo].[efrstore_insert_credit_card] @Credit_card_id int OUTPUT, @Online_user_id int, @Credit_card_type_id tinyint, @Credit_card varbinary, @Last_digits char(4), @Year_expire smallint, @Month_expire tinyint, @Date_created datetime, @Removed bit AS
begin

insert into Credit_card(Online_user_id, Credit_card_type_id, Credit_card, Last_digits, Year_expire, Month_expire, Date_created, Removed) values(@Online_user_id, @Credit_card_type_id, @Credit_card, @Last_digits, @Year_expire, @Month_expire, @Date_created, @Removed)

select @Credit_card_id = SCOPE_IDENTITY()

end
GO
