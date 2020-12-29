USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_interest]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
CREATE PROCEDURE [dbo].[efrstore_get_package_interest] @Package_id int AS
begin

select Package_id, Package_interest_id  
FROM package_interest
WHERE Package_id=@Package_id

end
GO
