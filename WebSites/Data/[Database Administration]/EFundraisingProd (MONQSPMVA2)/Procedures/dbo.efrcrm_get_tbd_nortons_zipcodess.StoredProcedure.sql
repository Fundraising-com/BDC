USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tbd_nortons_zipcodess]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Tbd_nortons_zipcodes
CREATE PROCEDURE [dbo].[efrcrm_get_tbd_nortons_zipcodess] AS
begin

select Zip_codes from Tbd_nortons_zipcodes

end
GO
