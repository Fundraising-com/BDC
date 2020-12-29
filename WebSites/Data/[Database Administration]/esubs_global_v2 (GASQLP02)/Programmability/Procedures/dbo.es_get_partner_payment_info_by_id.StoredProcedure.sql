USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_payment_info_by_id]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_payment_info
CREATE PROCEDURE [dbo].[es_get_partner_payment_info_by_id] @Partner_payment_info_id int AS
begin

select Partner_payment_info_id, Partner_id, Payment_info_id, Active, Create_date from Partner_payment_info where Partner_payment_info_id=@Partner_payment_info_id

end
GO
