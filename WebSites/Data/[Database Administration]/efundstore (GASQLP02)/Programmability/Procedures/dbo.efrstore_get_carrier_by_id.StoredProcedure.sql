USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_carrier_by_id]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Carrier
CREATE PROCEDURE [dbo].[efrstore_get_carrier_by_id] @Carrier_id int AS
begin

select Carrier_id, Description from Carrier where Carrier_id=@Carrier_id

end
GO
