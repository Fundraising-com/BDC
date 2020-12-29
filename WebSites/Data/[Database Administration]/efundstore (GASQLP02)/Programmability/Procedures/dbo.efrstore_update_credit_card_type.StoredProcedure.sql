USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_credit_card_type]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Credit_card_type
CREATE PROCEDURE [dbo].[efrstore_update_credit_card_type] @Credit_card_type_id tinyint, @Payment_method_id tinyint, @Credit_card_type_name varchar(25), @Credit_card_image varchar(25), @Display_order tinyint, @Displayable bit AS
begin

update Credit_card_type set Payment_method_id=@Payment_method_id, Credit_card_type_name=@Credit_card_type_name, Credit_card_image=@Credit_card_image, Display_order=@Display_order, Displayable=@Displayable where Credit_card_type_id=@Credit_card_type_id

end
GO
