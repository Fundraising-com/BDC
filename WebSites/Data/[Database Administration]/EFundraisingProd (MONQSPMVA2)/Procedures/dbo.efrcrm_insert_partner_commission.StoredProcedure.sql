USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_partner_commission]    Script Date: 02/14/2014 13:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_commission
CREATE PROCEDURE [dbo].[efrcrm_insert_partner_commission] @Partner_id int OUTPUT, @Product_class_id tinyint, @Commission_rate decimal AS
begin

insert into Partner_commission(Product_class_id, Commission_rate) values(@Product_class_id, @Commission_rate)

select @Partner_id = SCOPE_IDENTITY()

end
GO
