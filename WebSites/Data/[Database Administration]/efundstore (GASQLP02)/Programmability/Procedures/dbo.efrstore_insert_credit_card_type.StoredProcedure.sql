USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_credit_card_type]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Credit_card_type
CREATE PROCEDURE [dbo].[efrstore_insert_credit_card_type] @Credit_card_type_id int OUTPUT, @Payment_method_id tinyint, @Credit_card_type_name varchar(25), @Credit_card_image varchar(25), @Display_order tinyint, @Displayable bit AS
begin

insert into Credit_card_type(Payment_method_id, Credit_card_type_name, Credit_card_image, Display_order, Displayable) values(@Payment_method_id, @Credit_card_type_name, @Credit_card_image, @Display_order, @Displayable)

select @Credit_card_type_id = SCOPE_IDENTITY()

end
GO
