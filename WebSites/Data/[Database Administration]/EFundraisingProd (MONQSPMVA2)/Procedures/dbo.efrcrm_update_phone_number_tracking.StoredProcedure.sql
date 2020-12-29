USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_phone_number_tracking]    Script Date: 02/14/2014 13:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Phone_number_tracking
CREATE PROCEDURE [dbo].[efrcrm_update_phone_number_tracking] @Phone_number_tracking_id int, @Phone_number_tracking_desc varchar(50) AS
begin

update Phone_number_tracking set Phone_number_tracking_desc=@Phone_number_tracking_desc where Phone_number_tracking_id=@Phone_number_tracking_id

end
GO
