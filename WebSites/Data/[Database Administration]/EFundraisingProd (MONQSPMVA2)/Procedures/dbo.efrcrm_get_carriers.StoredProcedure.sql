USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_carriers]    Script Date: 02/14/2014 13:03:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Carrier
CREATE PROCEDURE [dbo].[efrcrm_get_carriers] AS
begin

select Carrier_id, Description, active from Carrier where active = 1

end
GO
