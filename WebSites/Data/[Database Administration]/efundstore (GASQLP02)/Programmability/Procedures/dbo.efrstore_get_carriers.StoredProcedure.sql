USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_carriers]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Carrier
CREATE PROCEDURE [dbo].[efrstore_get_carriers] AS
begin

select Carrier_id, Description from Carrier

end
GO
