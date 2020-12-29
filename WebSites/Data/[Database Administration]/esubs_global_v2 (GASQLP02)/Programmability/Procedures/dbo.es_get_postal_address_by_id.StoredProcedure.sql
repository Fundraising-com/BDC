USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_postal_address_by_id]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[es_get_postal_address_by_id]
    @postal_address_id int
AS
BEGIN
    SELECT postal_address_id
          , address_1
          , address_2
          , city
          , zip_code
          , country_code
          , subdivision_code
          , create_date
          , matching_code
          
     FROM postal_address
     WHERE postal_address_id = @postal_address_id
END
GO
