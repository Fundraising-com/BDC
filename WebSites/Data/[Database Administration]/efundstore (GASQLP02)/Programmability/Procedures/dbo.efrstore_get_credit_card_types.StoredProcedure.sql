USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_credit_card_types]    Script Date: 02/14/2014 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Credit_card_type
CREATE PROCEDURE [dbo].[efrstore_get_credit_card_types] AS
begin

select Credit_card_type_id, Payment_method_id, Credit_card_type_name, Credit_card_image, Display_order, Displayable from Credit_card_type

end
GO
