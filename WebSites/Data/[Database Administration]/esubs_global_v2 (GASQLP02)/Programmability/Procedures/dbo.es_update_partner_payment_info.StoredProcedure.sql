USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_partner_payment_info]    Script Date: 02/14/2014 13:07:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_payment_info
CREATE PROCEDURE [dbo].[es_update_partner_payment_info] @Partner_payment_info_id int, @Partner_id int, @Payment_info_id int, @Active bit, @Create_date datetime AS
begin

update Partner_payment_info set Partner_id=@Partner_id, Payment_info_id=@Payment_info_id, Active=@Active, Create_date=@Create_date where Partner_payment_info_id=@Partner_payment_info_id

end
GO
