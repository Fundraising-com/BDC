USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_promotion_destinations]    Script Date: 02/14/2014 13:05:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion_destination
CREATE PROCEDURE [dbo].[efrstore_get_promotion_destinations] AS
begin

select Promotion_destination_id, Url, Create_date from Promotion_destination

end
GO
