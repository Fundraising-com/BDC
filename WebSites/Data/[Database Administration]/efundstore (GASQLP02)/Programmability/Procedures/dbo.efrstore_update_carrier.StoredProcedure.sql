USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_carrier]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Carrier
CREATE PROCEDURE [dbo].[efrstore_update_carrier] @Carrier_id tinyint, @Description varchar(50) AS
begin

update Carrier set Description=@Description where Carrier_id=@Carrier_id

end
GO
