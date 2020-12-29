USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_status]    Script Date: 02/14/2014 13:07:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment_status
CREATE PROCEDURE [dbo].[es_update_payment_status] @Payment_status_id int, @Description varchar(50) AS
begin

update Payment_status set Description=@Description where Payment_status_id=@Payment_status_id

end
GO
