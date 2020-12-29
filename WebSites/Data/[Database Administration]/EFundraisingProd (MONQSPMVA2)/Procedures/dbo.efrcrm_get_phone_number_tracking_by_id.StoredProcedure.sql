USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_phone_number_tracking_by_id]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Phone_number_tracking
CREATE PROCEDURE [dbo].[efrcrm_get_phone_number_tracking_by_id] @Phone_number_tracking_id int AS
begin

select Phone_number_tracking_id, Phone_number_tracking_desc from Phone_number_tracking where Phone_number_tracking_id=@Phone_number_tracking_id

end
GO
