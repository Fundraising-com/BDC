USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_info_by_id]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[es_get_payment_info_by_id]
    @payment_info_id int
AS
BEGIN

    SELECT payment_info_id
          , pinfo.group_id
          , pinfo.event_id
          , payment_name
          , on_behalf_of_name
          , ship_to_name
          , pn.phone_number_id
          , pn.phone_number
          , ssn
          , active
          , pinfo.create_date
          , pa.postal_address_id
          , pa.address_1
          , pa.address_2
          , pa.city
          , pa.zip_code
          , pa.country_code
          , pa.subdivision_code
      FROM payment_info as pinfo
            LEFT JOIN postal_address as pa
                ON pa.postal_address_id = pinfo.postal_address_id
            LEFT JOIN phone_number as pn
                ON pn.phone_number_id = pinfo.phone_number_id
           
     WHERE payment_info_id = @payment_info_id
END
GO
