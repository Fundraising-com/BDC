USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_payment_status]    Script Date: 02/14/2014 13:07:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment_payment_status
CREATE PROCEDURE [dbo].[es_update_payment_payment_status] @Payment_id int, @Payment_status_id int, @Create_date datetime AS
begin

update Payment_payment_status set Payment_status_id=@Payment_status_id, Create_date=@Create_date where Payment_id=@Payment_id

end
GO
