USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_card_typess]    Script Date: 02/14/2014 13:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Credit_card_types
CREATE PROCEDURE [dbo].[efrcrm_get_credit_card_typess] AS
begin

select Credit_card_type_id, Payment_method_id, Credit_card_type_name, Credit_card_image, Display_order, Displayable from Credit_card_types

end
GO
