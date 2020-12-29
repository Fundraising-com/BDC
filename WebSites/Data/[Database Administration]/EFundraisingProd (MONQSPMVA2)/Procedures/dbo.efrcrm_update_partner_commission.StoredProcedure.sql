USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_partner_commission]    Script Date: 02/14/2014 13:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_commission
CREATE PROCEDURE [dbo].[efrcrm_update_partner_commission] @Partner_id int, @Product_class_id tinyint, @Commission_rate decimal AS
begin

update Partner_commission set Product_class_id=@Product_class_id, Commission_rate=@Commission_rate where Partner_id=@Partner_id

end
GO
