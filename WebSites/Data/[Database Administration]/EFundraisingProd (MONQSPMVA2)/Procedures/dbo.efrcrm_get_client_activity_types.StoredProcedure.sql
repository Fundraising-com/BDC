USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_client_activity_types]    Script Date: 02/14/2014 13:03:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Client_activity_type
CREATE PROCEDURE [dbo].[efrcrm_get_client_activity_types] AS
begin

select Client_activity_type_id, Carrier_shipping_status_id, Description from Client_activity_type

end
GO
